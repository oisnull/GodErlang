using GodErlang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GodErlang.ConsoleTest
{
    public enum ExecuteTaskStatus
    {
        NotStart = 0,
        Running = 1,
        Finished = 2
    }

    public class ExecuteTask
    {
        const string URLS_QUEUE_NAME = "product:urls:queue";
        const string URLS_TRIGGER_NAME = "product:urls:trigger";
        const int MAX_TASK_COUNT = 5;
        private static ExecuteTask _instance = null;
        public static ExecuteTask DefaultInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExecuteTask();
                }
                return _instance;
            }
        }

        public ExecuteTaskStatus Status { get; private set; } = ExecuteTaskStatus.NotStart;
        public bool IsStop { get; private set; } = false;
        public Func<string, Task> ExtractProcess { get; set; }
        public int IntervalSeconds { get; set; } = 10;

        private Dictionary<string, Task> taskManager;
        public ExecuteTask()
        {
            taskManager = new Dictionary<string, Task>();
        }

        public void Stop()
        {
            this.IsStop = true;
        }

        public void StartBlock()
        {
            if (this.Status == ExecuteTaskStatus.Running)
                return;

            while (true)
            {
                if (IsStop) break;
                Start();
                Thread.Sleep(this.IntervalSeconds * 1000);
                if (IsStop) break;
            }
        }

        public void StartMonitoring()
        {
            Start();

            RedisClient.ProdcutUrlsInstance.SubscribeAsync(URLS_TRIGGER_NAME, message =>
            {
                Console.WriteLine($"Trigger message: {message}");
                Start();
            });
        }

        public void Start()
        {
            if (this.Status == ExecuteTaskStatus.Running)
            {
                //Console.WriteLine($"Execute status: {this.Status.ToString()}");
                return;
            }
            this.Status = ExecuteTaskStatus.Running;

            string firstValue = RedisClient.ProdcutUrlsInstance.Exec(db => db.ListLeftPop(URLS_QUEUE_NAME));
            Console.WriteLine($"{DateTime.Now} Execute task start...");
            int processCount = 0;
            while (!string.IsNullOrEmpty(firstValue))
            {
                Task task = ExtractAsync(firstValue);
                this.AddWaitTask(firstValue, task);

                firstValue = RedisClient.ProdcutUrlsInstance.Exec(db => db.ListLeftPop(URLS_QUEUE_NAME));
                processCount++;
            }

            Task.WaitAll(taskManager.Values.ToArray());
            taskManager.Clear();
            this.Status = ExecuteTaskStatus.Finished;
            if (processCount > 0)
                Console.WriteLine($"{DateTime.Now} Execute {processCount} task completed.");
        }

        private void AddWaitTask(string url, Task task)
        {
            if (task.IsCanceled || task.IsCompleted)
                return;

            bool succ = taskManager.TryAdd(url, task);
            if (succ && taskManager.Count >= MAX_TASK_COUNT)
            {
                Task.WaitAny(taskManager.Values.ToArray());

                foreach (var item in taskManager.Where(kv => kv.Value.IsCanceled || kv.Value.IsCompleted).ToList())
                {
                    taskManager.Remove(item.Key, out task);
                }
            }
        }

        private Task ExtractAsync(string queueValue)
        {
            if (ExtractProcess != null)
            {
                return ExtractProcess(queueValue);
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
