using System;
using System.Collections.Generic;

namespace GodErlang.Entity.Models
{
    public partial class ProductStatus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReferUrl { get; set; }
        public ProductExecState State { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? StartExecTime { get; set; }
        public DateTime? EndExecTime { get; set; }
        public string ExecMessage { get; set; }
    }
}
