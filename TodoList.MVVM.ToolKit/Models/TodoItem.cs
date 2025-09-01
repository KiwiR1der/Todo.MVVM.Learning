using CommunityToolkit.Mvvm.ComponentModel;
using SqlSugar;

namespace TodoList.MVVM.ToolKit.Models
{
    [SugarTable("TodoItems")]
    public partial class TodoItem : ObservableObject
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]   // 主键自增
        public int Id { get; set; }

        [property: SugarColumn(Length = 200)]
        [ObservableProperty]
        private string _title = string.Empty;

        [property: SugarColumn]
        [ObservableProperty]
        private bool _isDone = false;

        [property: SugarColumn]
        [ObservableProperty]
        private DateTime _dueDate = DateTime.Now.AddDays(1);
    }
}
