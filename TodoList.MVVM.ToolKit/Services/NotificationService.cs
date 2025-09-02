using HandyControl.Controls;
using HandyControl.Data;
using TodoList.MVVM.ToolKit.Models;

namespace TodoList.MVVM.ToolKit.Services
{
    public class NotificationService : INotificationService
    {
        public void Notify(string title, string message, Action? onClick = null)
        {
            Growl.InfoGlobal(new GrowlInfo
            {
                Message = $"{title}\n{message}",
                ShowDateTime = false,
                WaitTime = 5,
                ActionBeforeClose = b =>
                {
                    if (onClick != null && b == true) onClick();
                    return true;
                }
            });
        }

        public void NotifyTodoDue(TodoItem item, Action? onOpen = null, Action? onComplete = null)
        {
            Growl.AskGlobal(new GrowlInfo
            {
                Message = $"【任务提醒】{item.Title}\n截止时间:{item.DueDate:yyyy-MM-dd HH:mm:ss}",
                ActionBeforeClose = result =>
                {
                    if (result == true) onOpen?.Invoke();
                    return true;
                },
                CancelStr = "忽略",
                ConfirmStr = "查看"
            });

            // 可选：再弹一个“完成？”交互
            if (onComplete != null)
            {
                Growl.AskGlobal(new GrowlInfo
                {
                    Message = $"是否标记【{item.Title}】为完成？",
                    ConfirmStr = "完成",
                    CancelStr = "暂不",
                    ActionBeforeClose = result =>
                    {
                        if (result == true) onComplete();
                        return true;
                    }
                });
            }
        }
    }
}
