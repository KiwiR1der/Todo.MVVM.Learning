using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoList.MVVM.Native.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// INotifyPropertyChanged 包含一个 PropertyChanged 事件。
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Sets the field and raises the PropertyChanged event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">属性对应的私有字段引用</param>
        /// <param name="value">属性尝试设置的新值</param>
        /// <param name="propertyName">[CallerMemberName]	编译器自动注入调用者属性名</param>
        /// <returns>返回是否实际修改了值</returns>
        protected bool SetProperty<T>(ref T field,T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
