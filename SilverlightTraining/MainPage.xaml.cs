using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightTraining
{
    public partial class MainPage : UserControl
    {
        private DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        private List<TextBlock> calendarTexts = new List<TextBlock>();
        private List<Rectangle> calendarRectangles = new List<Rectangle>();

        public MainPage()
        {
            InitializeComponent();

            MakeShapes();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTextBlockTop();
            UpdateCalendarText();
        }

        private void MakeShapes()
        {
            for (int i = 0; i < 42; i++)
            {
                int col = i % 7;
                int row = i / 7 + 1;

                TextBlock text = new TextBlock();
                Grid.SetColumn(text, col);
                Grid.SetRow(text, row);

                Rectangle rect = new Rectangle();
                Grid.SetColumn(rect, col);
                Grid.SetRow(rect, row);
                calendarRectangles.Add(rect);

                gridCalendar.Children.Add(rect);
                gridCalendar.Children.Add(text);
                calendarTexts.Add(text);
            }
        }

        private void UpdateTextBlockTop()
        {
            textBlockTop.Text = string.Format("{0}年 {1:D2}月", currentDate.Year, currentDate.Month);
        }

        private void UpdateCalendarText()
        {
            DateTime firstDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime dateTime = firstDate.AddDays(-(int)firstDate.DayOfWeek);

            for (int i = 0; i < calendarTexts.Count; i++)
            {
                string textStyle, rectStyle;

                if (dateTime.Month == currentDate.Month)
                {
                    if (IsSunday(dateTime))
                    {
                        textStyle = "DateSunTextStyle";
                        rectStyle = "DateSunRectStyle";
                    }
                    else if (IsSaturday(dateTime))
                    {
                        textStyle = "DateSatTextStyle";
                        rectStyle = "DateSatRectStyle";
                    }
                    else
                    {
                        textStyle = "DateDayTextStyle";
                        rectStyle = "DateDayRectStyle";
                    }
                }
                else
                {
                    if (IsSunday(dateTime))
                    {
                        textStyle = "WhiteSunTextStyle";
                        rectStyle = "DateSunRectStyle";
                    }
                    else if (IsSaturday(dateTime))
                    {
                        textStyle = "WhiteSatTextStyle";
                        rectStyle = "DateSatRectStyle";
                    }
                    else
                    {
                        textStyle = "WhiteDayTextStyle";
                        rectStyle = "DateDayRectStyle";
                    }
                }

                calendarTexts[i].Text = dateTime.Day.ToString();
                calendarTexts[i].Style = Resources[textStyle] as Style;
                calendarRectangles[i].Style = Resources[rectStyle] as Style;

                dateTime = dateTime.AddDays(1);
            }
        }

        private bool IsSunday(DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Sunday);
        }

        private bool IsSaturday(DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Saturday);
        }

        private void buttonLeft_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            UpdateTextBlockTop();
            UpdateCalendarText();
        }

        private void buttonRight_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            UpdateTextBlockTop();
            UpdateCalendarText();
        }
    }
}
