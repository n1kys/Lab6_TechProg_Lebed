using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6_TechProg_Lebed
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        Bitmap clockBitmap;
        Graphics clockGraphics;
        Settings settingsForm;
        public SettingsSets defaultSettings;
        public SettingsSets settings;

        public Form1()
        {
            InitializeComponent();
            defaultSettings = new SettingsSets();
            settings = defaultSettings; // Создание экземпляра класса настроек
            InitializeClock();
        }

        private void InitializeClock()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10; // Обновление 
            timer.Tick += Timer_Tick;
            timer.Start();

            // Создание битмапа для циферблата
            clockBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            clockGraphics = Graphics.FromImage(clockBitmap);

            pictureBox1.BackColor = settings.ClockBackColor;
        }

        private void ShowSettingsForm_Click()
        {
            settingsForm = new Settings(this); // Передайте defaultSettings и this при создании экземпляра Settings
            settingsForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DrawClockNumbers()
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            int radius = (int)(pictureBox1.Width * 0.335);

            Font numberFont = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular);
            Brush numberBrush = new SolidBrush(settings.NumbersColor); // Использование цвета цифр из настроек

            for (int i = 1; i <= settings.NumberCount; i++)
            {
                double angle = (i - 3) * 30 * Math.PI / 180; // Смещение угла для правильного расположения цифр
                int numberX = centerX + (int)(radius * Math.Cos(angle)) - (int)(numberFont.Size * 0.7);
                int numberY = centerY + (int)(radius * Math.Sin(angle)) - (int)(numberFont.Size * 0.9);

                clockGraphics.DrawString(i.ToString(), numberFont, numberBrush, numberX, numberY);
            }
        }

        private void DrawClockTicks()
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            int radius = (int)(pictureBox1.Width * 0.38);

            for (int i = 1; i <= 60; i++)
            {
                double angle = (i - 15) * 6 * Math.PI / 180; // Смещение угла для правильного расположения делений
                int startX = centerX + (int)((radius - 10) * Math.Cos(angle));
                int startY = centerY + (int)((radius - 10) * Math.Sin(angle));
                int endX = centerX + (int)(radius * Math.Cos(angle));
                int endY = centerY + (int)(radius * Math.Sin(angle));

                if (i % 5 == 0) // Часовые деления
                {
                    clockGraphics.DrawLine(new Pen(settings.HourDivColor, 4), startX, startY, endX, endY);
                }
                else // Второстепенные деления
                {
                    clockGraphics.DrawLine(new Pen(settings.DivColor), startX, startY, endX, endY);
                }
            }
        }

        private void DrawClockFrame()
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            int radius = (int)(pictureBox1.Width * 0.38);

            Pen framePen = new Pen(settings.ClockBorderColor, settings.ClockBorderWidth); // Использование цвета и толщины рамки из настроек

            clockGraphics.DrawEllipse(framePen, centerX - radius, centerY - radius, radius * 2, radius * 2);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            // Очистка циферблата
            clockGraphics.Clear(settings.ClockBackColor); // Использование цвета фона из настроек

            // Расчет угла для стрелок
            int secondsAngle = (currentTime.Second * 6) - 90;
            int minutesAngle = (currentTime.Minute * 6) - 90;
            int hoursAngle = ((currentTime.Hour % 12) * 30) + (currentTime.Minute / 2) - 90;

            // Рисование стрелок на циферблате
            DrawClockHands(secondsAngle, minutesAngle, hoursAngle);

            // Рисование цифр на циферблате
            DrawClockNumbers();

            // Рисование рамки циферблата
            DrawClockFrame();

            DrawClockTicks();

            // Отображение циферблата на PictureBox
            pictureBox1.Image = clockBitmap;

            // Вывод времени в элемент Label
            label1.Text = currentTime.ToString("HH:mm:ss");
        }

        private void DrawClockHands(int secondsAngle, int minutesAngle, int hoursAngle)
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            int handLength;

            // Рисование секундной стрелки
            handLength = (int)(pictureBox1.Width * settings.SecondHandLength);
            DrawClockHand(secondsAngle, handLength, settings.SecondHandThickness, settings.SecondHandColor);

            // Рисование минутной стрелки
            handLength = (int)(pictureBox1.Width * settings.MinuteHandLength);
            DrawClockHand(minutesAngle, handLength, settings.MinuteHandThickness, settings.MinuteHandColor);

            // Рисование часовой стрелки
            handLength = (int)(pictureBox1.Width * settings.HourHandLength);
            DrawClockHand(hoursAngle, handLength, settings.HourHandThickness, settings.HourHandColor);
        }

        private void DrawClockHand(int angle, int length, int width, Color color)
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            double radians = angle * Math.PI / 180;
            int handX = centerX + (int)(length * Math.Cos(radians));
            int handY = centerY + (int)(length * Math.Sin(radians));

            Pen pen = new Pen(color, width);
            clockGraphics.DrawLine(pen, centerX, centerY, handX, handY);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ShowSettingsForm_Click();
        }

    }
}
