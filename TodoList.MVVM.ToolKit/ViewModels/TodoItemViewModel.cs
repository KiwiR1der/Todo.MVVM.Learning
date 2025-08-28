using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using TodoList.MVVM.ToolKit.Models;

namespace TodoList.MVVM.ToolKit.ViewModels
{
    public partial class TodoItemViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> TodoItems { get; } = new();

        // 选中项变化会影响“删除”是否可点
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        private TodoItem? selectedTodoItem;

        // 文本变化会影响“添加”是否可点
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private string newTitle = string.Empty;

        //[ObservableProperty]
        //private TodoItem _selectedTodoItem = null;

        //[ObservableProperty]
        //private string _newTitle = string.Empty;

        private readonly string _dataFile = "todoItems.json";

        public TodoItemViewModel()
        {
            Load();
        }

        // 添加命令
        [RelayCommand(CanExecute = nameof(CanAddItem))]
        private void Add()
        {
            TodoItems.Add(new TodoItem { Title = NewTitle });
            NewTitle = string.Empty;
        }

        private bool CanAddItem()
        {
            return !string.IsNullOrWhiteSpace(NewTitle);
        }

        //private bool CanAddItem => !string.IsNullOrEmpty(NewTitle);

        [RelayCommand(CanExecute = nameof(CanRemoveItem))]
        private void Remove()
        {
            TodoItems.Remove(SelectedTodoItem);
            SelectedTodoItem = null;
        }

        private bool CanRemoveItem => SelectedTodoItem != null;

        [RelayCommand]
        private void Save()
        {
            var dump = TodoItems.Select(i => new { i.Title, i.IsDone }).ToList();
            File.WriteAllText(_dataFile, JsonSerializer.Serialize(dump, new JsonSerializerOptions { WriteIndented = true }));
        }

        [RelayCommand]
        private void Load()
        {
            if (!File.Exists(_dataFile))
            {
                return;
            }

            try
            {
                var json = JsonSerializer.Deserialize<List<TodoItem>>(File.ReadAllText(_dataFile));
                if (json == null)
                    return;
                TodoItems.Clear();
                foreach (var item in json)
                {
                    TodoItems.Add(new TodoItem { Title = item.Title, IsDone = item.IsDone });
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}
