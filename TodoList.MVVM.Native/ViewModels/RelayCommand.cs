using System.Windows.Input;

namespace TodoList.MVVM.Native.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;  // 执行的委托
        private readonly Predicate<object?> _canExecute;    // 判断是否可以执行的委托

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        event EventHandler? ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        void ICommand.Execute(object? parameter) => _execute(parameter);
    }
}
