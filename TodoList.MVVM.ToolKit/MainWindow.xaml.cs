using System.Windows;
using TodoList.MVVM.ToolKit.ViewModels;

namespace TodoList.MVVM.ToolKit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(TodoItemViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }
    }
}