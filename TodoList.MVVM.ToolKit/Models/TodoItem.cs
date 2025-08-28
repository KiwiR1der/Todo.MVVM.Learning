using CommunityToolkit.Mvvm.ComponentModel;

namespace TodoList.MVVM.ToolKit.Models
{
    public partial class TodoItem : ObservableObject
    {
        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private bool _isDone = false;
    }
}
