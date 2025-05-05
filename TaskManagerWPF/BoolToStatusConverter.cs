using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Globalization;
using System.Windows.Data;


using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskManagerWPF
{
    // Конвертер для преобразования значения bool (IsCompleted)
    // в строковое представление ("Выполнено" / "Не выполнено") и обратно
    public class BoolToStatusConverter : IValueConverter
    {
        // Метод Convert вызывается при отображении данных в интерфейсе
        // value — исходное значение типа bool (например, task.IsCompleted)
        // Возвращает строку "Выполнено" или "Не выполнено"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCompleted = (bool)value;
            return isCompleted ? "Выполнено" : "Не выполнено";
        }

        // Метод ConvertBack вызывается при передаче значения из интерфейса обратно в модель
        // value — строка ("Выполнено" или "Не выполнено")
        // Возвращает bool: true, если "Выполнено", иначе false
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            return status == "Выполнено";
        }
    }
}


