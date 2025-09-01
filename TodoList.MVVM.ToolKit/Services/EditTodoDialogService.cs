using TodoList.MVVM.ToolKit.ViewModels;
using TodoList.MVVM.ToolKit.Views;

namespace TodoList.MVVM.ToolKit.Services
{
    public class EditTodoDialogService : IDialogService
    {
        public bool ShowEditTodoDialog(EditTodoItemViewModel vm)
        {
            var dialog = new EditTodoWindow { DataContext = vm, Owner = App.Current.MainWindow };
            return (bool)dialog.ShowDialog();
        }
    }
}
