using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using TodoList.MVVM.ToolKit.Models;
using TodoList.MVVM.ToolKit.Services;

namespace TodoList.MVVM.ToolKit.ViewModels
{
    public partial class TodoItemViewModel : ViewModelBase
    {
        private readonly TodoDbContext _dbContext;

        private ObservableCollection<TodoItem> TodoItems { get; } = new();
        public ICollectionView TodoItemsView { get; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateItemStatusCommand))]
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

        public TodoItemViewModel(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
            TodoItemsView = CollectionViewSource.GetDefaultView(TodoItems);
            Load();
        }

        //public TodoItemViewModel()
        //{
        //    Load();
        //    TodoItemsView = CollectionViewSource.GetDefaultView(TodoItems);
        //}

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
            var newItem = new TodoItem { Title = NewTitle };

            _dbContext.Db.Insertable<TodoItem>(newItem).ExecuteReturnIdentity();

            TodoItems.Add(newItem);
            NewTitle = string.Empty;
        }

        private bool CanAddItem()
        {
            return !string.IsNullOrWhiteSpace(NewTitle);
        }

        [RelayCommand(CanExecute = nameof(CanRemoveItem))]
        private void Remove()
        {
            _dbContext.Db.Deleteable<TodoItem>().Where(x => x.Id == SelectedTodoItem.Id).ExecuteCommand();

            TodoItems.Remove(SelectedTodoItem);
            SelectedTodoItem = null;
        }

        [RelayCommand(CanExecute = nameof(CanRemoveItem))]
        private void UpdateItemStatus()
        {
            selectedTodoItem.IsDone = !selectedTodoItem.IsDone;
            _dbContext.Db.Updateable(selectedTodoItem).ExecuteCommand();
        }

        private bool CanRemoveItem() => SelectedTodoItem != null;

        [RelayCommand]
        private void Save()
        {
            // SQLite 数据库会持久化，此处不需要额外保存操作
            // 但可以添加批量更新或其他业务逻辑

            return;

        }

        [RelayCommand]
        private void Load()
        {
            // 从数据库中加载所有 TodoItem
            var items = _dbContext.Db.Queryable<TodoItem>().ToList();

            TodoItems.Clear();
            foreach (var item in items)
            {
                TodoItems.Add(item);
            }
        }
    }
}
