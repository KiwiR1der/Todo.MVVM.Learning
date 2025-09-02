using TodoList.MVVM.ToolKit.Models;

namespace TodoList.MVVM.ToolKit.Services
{
    public interface INotificationService
    {
        // 基础提醒 标题 + 文本 + 回调
        void Notify(string titile, string message, Action? onClick = null);

        // 任务到期提醒
        void NotifyTodoDue(TodoItem item, Action? onOpen = null, Action? onComplete = null);
    }
}
