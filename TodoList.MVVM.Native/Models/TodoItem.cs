namespace TodoList.MVVM.Native.Models
{
    public class TodoItem : ViewModels.ViewModelBase
    {
        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isDone = false;

        public bool IsDone
        {
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }
    }
}
