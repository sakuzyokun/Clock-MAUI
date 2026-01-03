using System;

namespace Clock
{
    public partial class MainPage : ContentPage
    {
        bool is12Hour = false;
        public MainPage()
        {
            InitializeComponent();

            RunClock();
        }

        private async void RunClock()
        {
            while (true)
            {
                GetTime();
                //UpdateClock();
                await Task.Delay(16);
            }
        }

        private void GetTime()
        {
            // Local Time
            DateTime localTime = DateTime.Now;

            AllLabel.Text = $"Local Time: {localTime}";

            DateLabel.Text = localTime.ToString("yyyy/MM/dd (ddd)");
            UtcLabel.Text = $"UTC Time: {localTime.ToUniversalTime()}";

            long unix = DateTimeOffset.Now.ToUnixTimeSeconds();
            long unixMs = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            UnixLabel.Text = $"Unix Time: {unix} sec \n {unixMs} ms";

            TimezoneLabel.Text = $"Timezone: {TimeZoneInfo.Local.DisplayName} + {TimeZoneInfo.Local.StandardName}";

            double percent =
                DateTime.Now.TimeOfDay.TotalSeconds /
                TimeSpan.FromDays(1).TotalSeconds * 100;
            TodayprogressLabel.Text = $"Today's Progress: {percent} %";

            if (is12Hour)
            {
                ampm();
            }
            else
            {
                twentyfour();
            }
        }

        void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            is12Hour = e.Value;
        }

        void ampm()
        {
            DateTime localTime = DateTime.Now;
            TimeLabel.Text = localTime.ToString("hh:mm:ss.fff tt");
        }

        void twentyfour()
        {
            DateTime localTime = DateTime.Now;
            TimeLabel.Text = localTime.ToString("HH:mm:ss.fff");
        }
        /*void UpdateClock()
        {
            DateTime now = DateTime.Now;

            double second = now.Second + now.Millisecond / 1000.0;
            double minute = now.Minute + second / 60.0;
            double hour = (now.Hour % 12) + minute / 60.0;

            SecondHand.Rotation = second * 6;
            MinuteHand.Rotation = minute * 6;
            HourHand.Rotation = hour * 30;
        }*/
    }
}
