using System;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using TaskManagerLibrary;

namespace TaskManagerWPF
{
    public partial class TaskDetailsWindow : Window
    {
        private TaskItem task;
        private bool editMode = false; // Режим редактирования и просмотра

        // Конструктор окна деталей задачи. Принимает объект задачи и отображает его данные.
        public TaskDetailsWindow(TaskItem task)
        {
            InitializeComponent();
            this.task = task;
            DisplayTaskDetails(); // Показать данные задачи
        }

        // Отображает текущую задачу в режиме только для чтения (без редактирования)
        private void DisplayTaskDetails()
        {
            editMode = false;
            ToggleEditFields(false); // Скрываем поля редактирования

            // Заполняем элементы интерфейса текущими данными задачи
            txtTitleBlock.Text = task.Title;
            txtDescriptionBlock.Text = task.Description;
            txtDeadlineBlock.Text = task.Deadline.HasValue
                ? task.Deadline.Value.ToString("dd.MM.yyyy")
                : "(нет дедлайна)";
            txtImportanceBlock.Text = task.Importance.ToString();
            txtStatusBlock.Text = task.IsCompleted ? "Выполнено" : "Не выполнено";

            // Загружаем изображение, если оно указано
            if (!string.IsNullOrEmpty(task.ImagePath))
            {
                try
                {
                    var bitmap = new BitmapImage(new Uri(task.ImagePath));
                    imgPreview.Source = bitmap;
                }
                catch
                {
                    imgPreview.Source = null; // Ошибка загрузки — очищаем изображение
                }
            }
            else
            {
                imgPreview.Source = null;
            }
        }

        // Показывает или скрывает поля редактирования в зависимости от режима
        private void ToggleEditFields(bool editing)
        {
            // Переключение видимости текстов и текстбоксов
            txtTitleBlock.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
            txtTitleEdit.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;

            txtDescriptionBlock.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
            txtDescriptionEdit.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;

            txtDeadlineBlock.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
            dpDeadlineEdit.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;

            txtImportanceBlock.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
            cmbImportanceEdit.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;

            imgPreview.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
            spImageEditPanel.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;

            // Управление видимостью кнопок
            btnEdit.Visibility = editing ? Visibility.Collapsed : Visibility.Visible;
            btnSave.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;
            btnCancel.Visibility = editing ? Visibility.Visible : Visibility.Collapsed;
        }

        // Переключает интерфейс в режим редактирования и заполняет поля значениями задачи
        private void SwitchToEditMode()
        {
            editMode = true;
            ToggleEditFields(true);

            // Копируем текущие данные задачи в поля редактирования
            txtTitleEdit.Text = task.Title;
            txtDescriptionEdit.Text = task.Description;
            dpDeadlineEdit.SelectedDate = task.Deadline;

            var tag = task.Importance.ToString(); // "High", "Medium", "Low"
            foreach (var item in cmbImportanceEdit.Items)
            {
                if (item is System.Windows.Controls.ComboBoxItem cbi && (string)cbi.Tag == tag)
                {
                    cmbImportanceEdit.SelectedItem = cbi;
                    break;
                }
            }

            txtImageEdit.Text = task.ImagePath ?? "";
        }

        // Обработчик кнопки "Редактировать" — включает режим редактирования
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SwitchToEditMode();
        }

        // Обработчик кнопки "Сохранить" — сохраняет изменения в задачу и возвращает интерфейс в режим просмотра
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            task.Title = txtTitleEdit.Text.Trim();
            task.Description = txtDescriptionEdit.Text.Trim();

            // Устанавливаем или очищаем дедлайн
            if (dpDeadlineEdit.SelectedDate.HasValue)
                task.SetDeadline(dpDeadlineEdit.SelectedDate.Value);
            else
                task.ClearDeadline();

            // Обновляем важность в зависимости от выбора в ComboBox
            if (cmbImportanceEdit.SelectedItem is System.Windows.Controls.ComboBoxItem cbi)
            {
                var tg = (string)cbi.Tag;
                if (tg == "High") task.Importance = ImportanceLevel.High;
                else if (tg == "Medium") task.Importance = ImportanceLevel.Medium;
                else if (tg == "Low") task.Importance = ImportanceLevel.Low;
            }

            // Устанавливаем путь к изображению
            task.ImagePath = string.IsNullOrEmpty(txtImageEdit.Text) ? null : txtImageEdit.Text;

            // Отображаем обновлённые данные
            DisplayTaskDetails();

            // Обновляем список задач в главном окне
            if (this.Owner is MainWindow mainWin)
            {
                mainWin.RefreshList();
            }
        }

        // Обработчик кнопки "Отмена" — отменяет изменения и возвращает интерфейс в режим просмотра
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DisplayTaskDetails();
        }

        // Обработчик кнопки выбора изображения — открывает диалог выбора файла и сохраняет путь
        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.jpeg;*.bmp|All files|*.*"
            };
            if (ofd.ShowDialog() == true)
            {
                txtImageEdit.Text = ofd.FileName;
            }
        }

        // Обработчик кнопки "Закрыть" — закрывает окно и обновляет список задач в главном окне, если оно открыто
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner is MainWindow mainWin)
            {
                mainWin.RefreshList();
            }

            this.Close();
        }

    }
}
