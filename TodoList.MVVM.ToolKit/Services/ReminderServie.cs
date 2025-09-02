using TodoList.MVVM.ToolKit.Models;

namespace TodoList.MVVM.ToolKit.Services
{
    public class ReminderServie : IReminderService, IDisposable
    {
        private readonly TodoDbContext _dbContext;
        private readonly INotificationService _notify;
        private Timer? _timer;

        public ReminderServie(TodoDbContext db, INotificationService notify)
        {
            _dbContext = db;
            _notify = notify;
        }

        public void Start()
        {
            _timer = new Timer(_ => Tick(), null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private void Tick()
        {
            try
            {
                var now = DateTime.Now;

                // 查询到期或即将到期的任务、开启提醒服务
                var dueCandidates = _dbContext.Db.Queryable<TodoItem>()
                    .Where(t => t.ReminderEnabled && !t.IsDone && t.DueDate != null).ToList();

                foreach (var item in dueCandidates)
                {
                    var due = item.DueDate;
                    var triggerTime = due.AddMinutes(-item.ReminderBeforeMinutes);

                    var shouldRemind = now >= triggerTime &&
                        (item.LastReminderDate == null || item.LastReminderDate < triggerTime);

                    if (!shouldRemind) continue;

                    // 发送提醒
                    _notify.NotifyTodoDue(item, onOpen: () =>
                    {
                        // Todo：聚焦主窗口，选中该任务
                    },
                    onComplete: () =>
                    {
                        item.IsDone = true;
                        _dbContext.Db.Updateable(item).UpdateColumns(x => new { x.IsDone }).ExecuteCommand();
                    });

                    item.LastReminderDate = now;
                    _dbContext.Db.Updateable(item).UpdateColumns(x => new { x.LastReminderDate }).ExecuteCommand();
                }
            }
            catch (Exception)
            {
                // 可选：记录日志
            }
        }

        public void Stop() => _timer?.Change(Timeout.Infinite, Timeout.Infinite);

        public void Dispose() => _timer?.Dispose();
    }
}
