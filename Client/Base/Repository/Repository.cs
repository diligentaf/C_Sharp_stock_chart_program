using Common.Api;
using Common.DataCls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Client.Base.Repository {

    public abstract class Repository<T> : IRepository<T> { 

        public Repository(GrClient client) {
            this.Client = client;
        }

        public GrClient Client { get; private set; }
    }
}
