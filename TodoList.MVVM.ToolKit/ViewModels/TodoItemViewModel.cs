using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Data;
using TodoList.MVVM.ToolKit.Models;

namespace TodoList.MVVM.ToolKit.ViewModels
{
    public partial class TodoItemViewModel : ViewModelBase
    {
        private ObservableCollection<TodoItem> TodoItems { get; } = new();
        public ICollectionView TodoItemsView { get; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        private TodoItem? selectedTodoItem;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        private string newTitle = string.Empty;

        private readonly string _dataFile = "todoItems.json";

        // 筛选

        [ObservableProperty]
        private bool _showAllItems = true;

        [ObservableProperty]
        private bool _showCompletedItems;

        [ObservableProperty]
        private bool _showPendingItems;

        public TodoItemViewModel()
        {
            Load();
            TodoItemsView = CollectionViewSource.GetDefaultView(TodoItems);
        }

        [RelayCommand]
        private void ApplyFilter()
        {
            TodoItemsView.Filter = item =>
            {
                if (item is not TodoItem todo) return false;

                if (ShowAllItems) return true;
                if (ShowCompletedItems && todo.IsDone) return true;
                if (ShowPendingItems && !todo.IsDone) return true;

                return false;
            };
        }

        // 排序

        [RelayCommand]
        private void SortByTitle()
        {
            TodoItemsView.SortDescriptions.Clear();
            TodoItemsView.SortDescriptions.Add(new SortDescription(nameof(TodoItem.Title), ListSortDirection.Ascending));
        }

        [RelayCommand]
        private void SortByDueDate()
        {
            TodoItemsView.SortDescriptions.Clear();
            TodoItemsView.SortDescriptions.Add(new SortDescription(nameof(TodoItem.DueDate), ListSortDirection.Ascending));
        }

        [RelayCommand]
        private void SortByStatus()
        {
            TodoItemsView.SortDescriptions.Clear();
            TodoItemsView.SortDescriptions.Add(new SortDescription(nameof(TodoItem.IsDone), ListSortDirection.Ascending));
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
