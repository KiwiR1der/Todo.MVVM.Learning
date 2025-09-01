using System.Windows;
using TodoList.MVVM.ToolKit.ViewModels;

namespace TodoList.MVVM.ToolKit.Views
{
    /// <summary>
    /// EditTodoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditTodoWindow : Window
    {
        public EditTodoWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is EditTodoItemViewModel vm)
            {
                if (vm.HasErrors) return;
                DialogResult = true;
            }
        }
    }
}
