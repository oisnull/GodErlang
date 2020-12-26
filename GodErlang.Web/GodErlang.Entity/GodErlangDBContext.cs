using GodErlang.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace GodErlang.Entity
{
    public class GodErlangDBContext
    {
        //Scaffold-DbContext "Data Source=.;Initial Catalog=getest;Persist Security Info=True;User ID=sa;Password=WCNM.weiruan.2" Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -Context "GodErlangEntities" -UseDatabaseNames -StartupProject "GodErlang.Entity" -Project "GodErlang.Entity" -Force
        public static GodErlangEntities GetDBContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("DB ConnectionString");

            var builder = new DbContextOptionsBuilder<GodErlangEntities>();
            builder.UseSqlServer(connectionString);

            return new GodErlangEntities(builder.Options);
        }

        public static void ExecuteTransaction(GodErlangEntities db, Action<IDbContextTransaction> execFunc)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    execFunc(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static T ExecuteTransaction<T>(GodErlangEntities db, Func<IDbContextTransaction, T> execFunc)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    T t = execFunc(transaction);
                    transaction.Commit();
                    return t;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
