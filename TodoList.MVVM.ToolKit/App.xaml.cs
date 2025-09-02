using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TodoList.MVVM.ToolKit.Services;
using TodoList.MVVM.ToolKit.ViewModels;

namespace TodoList.MVVM.ToolKit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // 注册服务
            services.AddSingleton<TodoDbContext>();
            services.AddSingleton<TodoItemViewModel>();
            services.AddSingleton<IDialogService, EditTodoDialogService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IReminderService, ReminderServie>();

            // 注册主窗口
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();
            
            // 启动提醒服务
            var reminderService = ServiceProvider.GetRequiredService<IReminderService>();
            reminderService.Start();

            // 显示主窗口
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
