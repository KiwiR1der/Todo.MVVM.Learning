using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using TodoList.MVVM.Native.Models;

namespace TodoList.MVVM.Native.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        public ObservableCollection<TodoItem> Items { get; } = new();

        private TodoItem _selectedItem;
        public TodoItem SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _newItemTitle = string.Empty;
        public string NewItemTitle
        {
            get => _newItemTitle;
            set => SetProperty(ref _newItemTitle, value);
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ToggleCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

        private readonly string _dataFile = "todos.json";

        public TodoViewModel()
        {
            AddCommand = new RelayCommand(_ =>
            {
                if (!string.IsNullOrWhiteSpace(NewItemTitle))
                {
                    Items.Add(new TodoItem { Title = NewItemTitle, IsDone = false });
                    NewItemTitle = string.Empty;
                }
            });

            RemoveCommand = new RelayCommand(_ =>
            {
                if (SelectedItem != null)
                {
                    Items.Remove(SelectedItem);
                    SelectedItem = null;
                }
            }, _ => SelectedItem != null);

            ToggleCommand = new RelayCommand(_ =>
            {
                if (SelectedItem != null)
                {
                    SelectedItem.IsDone = !SelectedItem.IsDone;
                }
            }, _ => SelectedItem != null);

            SaveCommand = new RelayCommand(_ => Save());
            LoadCommand = new RelayCommand(_ => Load());
            Load();
        }

        private void Save()
        {
            var dump = Items.Select(i => new { i.Title, i.IsDone }).ToList();
            File.WriteAllText(_dataFile, JsonSerializer.Serialize(dump, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void Load()
        {
            if (!File.Exists(_dataFile)) return;

            try
            {
                var list = JsonSerializer.Deserialize<List<TodoItem>>(File.ReadAllText(_dataFile));
                if (list == null) return;
                Items.Clear();
                foreach (var item in list)
                {
                    Items.Add(new TodoItem { Title = item.Title, IsDone = item.IsDone });
                }
            }
            catch (Exception)
            {
                // ignore
            }
        }

    }
}
