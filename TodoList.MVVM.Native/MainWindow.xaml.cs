using System.Windows;
using TodoList.MVVM.Native.ViewModels;

namespace TodoList.MVVM.Native
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TodoViewModel();
        }
    }
}