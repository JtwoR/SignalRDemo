using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;

namespace ChatDemo.DAL
{
    public class DapperHelper: IDbHelper
    {

        //public static string ConnectionString = "server=127.0.0.1;port=3306;database=chatdemo;uid=root;pwd=123;";//Mysql
        public static string ConnectionString = $@"Data Source={System.IO.Directory.GetCurrentDirectory()}\chat.db;";
        public async Task<IEnumerable<T>> Query<T>(string sql)
            where T : class
        {
            using (IDbConnection conn = new SqliteConnection(ConnectionString))
            {
                return await conn.QueryAsync<T>(sql);
            }
        }

        public async Task<IEnumerable<T>> Query<T>(string sql,T t)
            where T : class
        {
            using (IDbConnection conn = new SqliteConnection(ConnectionString))
            {
                return await conn.QueryAsync<T>(sql,t);
            }
        }

        public async Task<int> Add<T>(string sql, T t)
            where T : class
        {
            using (IDbConnection conn = new SqliteConnection(ConnectionString))
            {
                return await conn.ExecuteAsync(sql, t);
            }
        }
    }
}
