namespace Lab6_TechProg_Lebed
{
    public class SettingsSets
    {
        public Color ClockBackColor { get; set; } = Color.Green;
        public Color DivColor { get; set; } = Color.DarkGray;
        public Color HourDivColor { get; set; } = Color.Black;
        public Color HourHandColor { get; set; } = Color.Black;
        public Color MinuteHandColor { get; set; } = Color.Blue;
        public Color SecondHandColor { get; set; } = Color.Red;
        public int NumberCount { get; set; } = 12;
        public Color ClockBorderColor { get; set; } = Color.Black;
        public Color NumbersColor { get; set; } = Color.Black;
        public int ClockBorderWidth { get; set; } = 3;

        public double HourHandLength { get; set; } = 0.15;
        public double MinuteHandLength { get; set; } = 0.28;
        public double SecondHandLength { get; set; } = 0.320;


        public int HourHandThickness { get; set; } = 7;
        public int MinuteHandThickness { get; set; } = 5;
        public int SecondHandThickness { get; set; } = 3;

    }
}
