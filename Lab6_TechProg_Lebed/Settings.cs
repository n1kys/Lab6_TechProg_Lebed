using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6_TechProg_Lebed
{
    public partial class Settings : Form
    {
        SettingsSets defaultSett = new SettingsSets();
        private Form1 mainForm; // Создайте приватное поле для хранения экземпляра Form1

        public Settings(Form1 mainForm) // Передайте defaultSettings и mainForm в конструктор класса Settings
        {
            InitializeComponent();
            this.mainForm = mainForm; // Сохраните mainForm в поле класса
        }

        private void HourHandColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.HourHandColor = selectedColor;
            }
        }

        private void SecondHandColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.SecondHandColor = selectedColor;
            }
        }

        private void MinuteHandColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.MinuteHandColor = selectedColor;
            }
        }

        private void BackGroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.ClockBackColor = selectedColor;
            }
        }

        private void BorderColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.ClockBorderColor = selectedColor;
            }
        }

        private void NumbersColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.NumbersColor = selectedColor;
            }
        }

        private void HourDivColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.HourDivColor = selectedColor;
            }
        }

        private void DivColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Проверка, если пользователь выбрал цвет и нажал "ОК"
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Получение выбранного цвета
                Color selectedColor = colorDialog.Color;

                // Присвоение выбранного цвета свойству экземпляра класса settings
                mainForm.settings.DivColor = selectedColor;
            }
        }

        private void defaultSettings_Click(object sender, EventArgs e)
        {
            mainForm.settings = new SettingsSets(); // Создайте новый экземпляр SettingsSets

        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Text files (*.txt)|*.txt";
                dialog.Title = "Save Settings";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        Type settingsType = typeof(SettingsSets);
                        PropertyInfo[] properties = settingsType.GetProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            string propertyName = property.Name;
                            object propertyValue = property.GetValue(mainForm.settings);

                            if (property.PropertyType == typeof(Color))
                            {
                                // Преобразуем цвет в его 32-битное представление и сохраняем строку
                                int argb = ((Color)propertyValue).ToArgb();
                                writer.WriteLine($"{propertyName}={argb}");
                            }
                            else
                            {
                                // Сохраняем другие свойства без преобразования
                                writer.WriteLine($"{propertyName}={propertyValue}");
                            }
                        }
                    }
                }
            }
        }

        // Загрузка данных из файла
        private void UploadSettings_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Text files (*.txt)|*.txt";
                dialog.Title = "Upload Settings";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;

                    if (File.Exists(filePath))
                    {
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            Type settingsType = typeof(SettingsSets);
                            PropertyInfo[] properties = settingsType.GetProperties();

                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                string[] parts = line.Split('=');

                                if (parts.Length == 2)
                                {
                                    string propertyName = parts[0];
                                    string propertyValue = parts[1];

                                    PropertyInfo property = properties.FirstOrDefault(p => p.Name == propertyName);
                                    if (property != null)
                                    {
                                        if (property.PropertyType == typeof(Color))
                                        {
                                            // Преобразуем строковое значение обратно в цвет из 32-битного представления
                                            int argb = int.Parse(propertyValue);
                                            Color colorValue = Color.FromArgb(argb);
                                            property.SetValue(mainForm.settings, colorValue);
                                        }
                                        else
                                        {
                                            // Преобразуем строковое значение в соответствующий тип свойства
                                            object value = Convert.ChangeType(propertyValue, property.PropertyType);
                                            property.SetValue(mainForm.settings, value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
