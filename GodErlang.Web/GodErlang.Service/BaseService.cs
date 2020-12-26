using GodErlang.Entity;
using GodErlang.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodErlang.Service
{
    public class BaseService
    {
        protected GodErlangEntities db;

        public BaseService(GodErlangEntities db)
        {
            this.db = db;
        }

    }
}
