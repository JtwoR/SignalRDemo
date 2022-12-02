using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatDemo.DAL
{
    public interface IDbHelper
    {
        public Task<IEnumerable<T>> Query<T>(string sql)where T : class;

        public Task<IEnumerable<T>> Query<T>(string sql, T t) where T : class;

        public Task<int> Add<T>(string sql, T t)where T : class;
    }
}
