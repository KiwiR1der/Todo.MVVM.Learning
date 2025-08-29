using SqlSugar;
using TodoList.MVVM.ToolKit.Models;

namespace TodoList.MVVM.ToolKit.Services
{
    public class TodoDbContext
    {
        private readonly SqlSugarScope _db;

        public TodoDbContext()
        {
            var connectionString = "Data Source = todolist.db";

            _db = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            }, db =>
            {
                // 配置实体与表的映射关系
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);
                };
            });

            // 创建表
            _db.CodeFirst.InitTables<TodoItem>();
        }

        public SqlSugarScope Db => _db;
    }
}
