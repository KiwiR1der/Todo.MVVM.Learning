using TodoList.MVVM.ToolKit.ViewModels;

namespace TodoList.MVVM.ToolKit.Services
{
    public interface IDialogService
    {
        bool ShowEditTodoDialog(EditTodoItemViewModel vm);
    }
}
