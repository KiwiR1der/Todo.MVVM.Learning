using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace TodoList.MVVM.ToolKit.ViewModels
{
    public partial class EditTodoItemViewModel : ObservableValidator
    {
        // 用于判断是“新增”还是“编辑”，可在标题上区分
        public bool IsEdit { get; }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "标题不能为空")]
        [MinLength(2, ErrorMessage = "标题至少 2 个字符")]
        private string _title;

        [ObservableProperty] private bool isDone;
        [ObservableProperty] private DateTime dueDate = DateTime.Today;

        public EditTodoItemViewModel(bool isEdit)
        {
            IsEdit = isEdit;
        }

        [RelayCommand]
        private void OK()
        {
            ValidateAllProperties();

            // 真正的“关闭窗体并返回结果”在 View 中设置
            // 这是不做事
        }

    }
}
