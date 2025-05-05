using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using TaskManagerLibrary;
using System.Windows.Controls;
using System.Windows.Media;

namespace TaskManagerWPF
{
    public partial class MainWindow : Window
    {
        private TaskManager manager;
        // private bool isDarkTheme = false; 

        public MainWindow()
        {
            InitializeComponent();
            manager = new TaskManager(); // Создание менеджера задач
            RefreshList(); // Загрузка и отображение списка задач
        }

        // Темы 
        /*
        private void btnToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            if (isDarkTheme)
            {
                Resources["WindowBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(40, 40, 40));
                Resources["WindowForegroundBrush"] = new SolidColorBrush(Colors.White);
                Resources["PanelBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(60, 60, 60));
                Resources["PanelForegroundBrush"] = new SolidColorBrush(Colors.White);
                btnToggleTheme.Content = "Светлая тема";
            }
            else
            {
                Resources["WindowBackgroundBrush"] = new SolidColorBrush(Colors.White);
                Resources["WindowForegroundBrush"] = new SolidColorBrush(Colors.Black);
                Resources["PanelBackgroundBrush"] = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                Resources["PanelForegroundBrush"] = new SolidColorBrush(Colors.Black);
                btnToggleTheme.Content = "Тёмная тема";
            }
        }
        */

        // Обработчик кнопки "Открыть файл" — загружает задачи из выбранного JSON-файла
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "JSON files|*.json|All files|*.*"
            };
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    manager.LoadFromFile(ofd.FileName); // Загрузка задач
                    RefreshList(); // Обновление списка на экране
                    ClearForm(); // Очистка формы ввода
                    MessageBox.Show("Файл загружен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Обработчик кнопки "Сохранить файл" — сохраняет текущие задачи в JSON-файл
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "JSON files|*.json|All files|*.*",
                FileName = "tasks.json"
            };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    manager.SaveToFile(sfd.FileName); // Сохранение в файл
                    MessageBox.Show("Файл сохранён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Выбор картинки 
        /*
        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.jpeg;*.bmp|All files|*.*"
            };
            if (ofd.ShowDialog() == true)
            {
                txtImagePath.Text = ofd.FileName;
            }
        }
        */

        // Обработчик кнопки "Добавить" — создаёт новую задачу и добавляет её в список
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string desc = txtDescription.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Введите заголовок задачи", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ImportanceLevel importance = ParseImportanceCombo();
            var newTask = manager.AddTask(title, desc, importance); // Добавление новой задачи

            if (dpDeadline.SelectedDate.HasValue)
            {
                newTask.SetDeadline(dpDeadline.SelectedDate.Value); // Установка дедлайна
            }

            // Добавление картинки отключено
            // if (!string.IsNullOrEmpty(txtImagePath.Text))
            // {
            //     newTask.ImagePath = txtImagePath.Text;
            // }

            RefreshList();
            ClearForm(); // Очистка полей формы
        }

        // Обработчик кнопки "Обновить" — обновляет данные выбранной задачи
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var sel = listTasks.SelectedItem as TaskItem;
            if (sel == null)
            {
                MessageBox.Show("Выберите задачу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            sel.Title = txtTitle.Text.Trim();
            sel.Description = txtDescription.Text.Trim();

            if (dpDeadline.SelectedDate.HasValue)
            {
                sel.SetDeadline(dpDeadline.SelectedDate.Value);
            }
            else
            {
                sel.ClearDeadline();
            }

            sel.Importance = ParseImportanceCombo();

            // Обновление картинки отключено
            // sel.ImagePath = string.IsNullOrEmpty(txtImagePath.Text) ? null : txtImagePath.Text;

            RefreshList(); // Обновление списка
        }

        // Обработчик кнопки "Завершить" — помечает выбранную задачу как выполненную
        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            var sel = listTasks.SelectedItem as TaskItem;
            if (sel == null)
            {
                MessageBox.Show("Выберите задачу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            sel.Complete();
            RefreshList();
        }

        // Обработчик кнопки "Открыть заново" — снимает отметку о выполнении задачи
        private void btnReopen_Click(object sender, RoutedEventArgs e)
        {
            var sel = listTasks.SelectedItem as TaskItem;
            if (sel == null)
            {
                MessageBox.Show("Выберите задачу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            sel.Reopen();
            RefreshList();
        }

        // Обработчик кнопки "Удалить" — удаляет выбранную задачу после подтверждения
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = listTasks.SelectedItem as TaskItem;
            if (sel == null)
            {
                MessageBox.Show("Выберите задачу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show($"Удалить задачу '{sel.Title}'?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                manager.RemoveTask(sel.Id);
                RefreshList();
                ClearForm();
            }
        }

        // Фильтр (ОТКЛЮЧЕНО)
        /*
        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }
        */

        // Обработчик двойного клика по элементу списка — открывает окно подробностей задачи
        private void listTasks_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var sel = listTasks.SelectedItem as TaskItem;
            if (sel != null)
            {
                TaskDetailsWindow detailsWindow = new TaskDetailsWindow(sel);
                detailsWindow.Owner = this; // Устанавливаем владельца окна
                detailsWindow.ShowDialog();
            }
        }

        // Обработчик смены выбранного элемента в списке — загружает данные задачи в поля формы
        private void listTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel = listTasks.SelectedItem as TaskItem;
            if (sel != null)
            {
                txtTitle.Text = sel.Title;
                txtDescription.Text = sel.Description;
                dpDeadline.SelectedDate = sel.Deadline;

                var tag = sel.Importance.ToString();
                foreach (ComboBoxItem item in cmbImportance.Items)
                {
                    if ((string)item.Tag == tag)
                    {
                        cmbImportance.SelectedItem = item;
                        break;
                    }
                }

                // Отключено отображение картинки
                // txtImagePath.Text = sel.ImagePath ?? "";
            }
            else
            {
                ClearForm();
            }
        }

        // Обновляет список задач в интерфейсе
        public void RefreshList()
        {
            listTasks.ItemsSource = null;
            var tasks = manager.GetAllTasks();

            // Фильтрация отключена
            /*
            if (cmbFilterImportance.SelectedItem is ComboBoxItem filterItem)
            {
                string tag = filterItem.Tag as string;
                if (tag != "All")
                {
                    if (Enum.TryParse<ImportanceLevel>(tag, out var importance))
                    {
                        tasks = tasks.Where(t => t.Importance == importance).ToList();
                    }
                }
            }

            if (chkShowCompleted.IsChecked == false)
            {
                tasks = tasks.Where(t => !t.IsCompleted).ToList();
            }
            */

            listTasks.ItemsSource = tasks; // Привязка списка задач
        }

        // Очищает все поля формы и снимает выбор задачи
        private void ClearForm()
        {
            txtTitle.Text = "";
            txtDescription.Text = "";
            dpDeadline.SelectedDate = null;
            cmbImportance.SelectedIndex = 1; // Средний приоритет по умолчанию

            // Очищаем путь к картинке (отключено)
            // txtImagePath.Text = "";

            listTasks.SelectedItem = null;
        }

        // Получает выбранный уровень важности из ComboBox
        private ImportanceLevel ParseImportanceCombo()
        {
            if (cmbImportance.SelectedItem is ComboBoxItem selItem)
            {
                string tag = selItem.Tag as string;
                return tag switch
                {
                    "High" => ImportanceLevel.High,
                    "Medium" => ImportanceLevel.Medium,
                    "Low" => ImportanceLevel.Low,
                    _ => ImportanceLevel.Medium
                };
            }
            return ImportanceLevel.Medium;
        }
    }
}
