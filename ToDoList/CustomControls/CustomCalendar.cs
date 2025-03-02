﻿using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;

namespace ToDoList
{
    public partial class CustomCalendar : UserControl
    {
        //fields
        DateTime date = DateTime.Now;
        DateTime pre;

        private int _rowCount;
        int LocationX = 0;
        int LocationY = 30;

        int PreMonthDays;
        string[] Days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

        public CustomCalendar()
        {
            Render();
        }
        //Load Event
        void Handle_Load(object sender, EventArgs e)
        {
            linkLbl_Left.Location = new Point(((pnl_Top.Width - linkLbl_Left.Width) / 2) - 100, 5);
            linkLbl_Right.Location = new Point(((pnl_Top.Width - linkLbl_Right.Width) / 2) + 100, 5);
            Generate_Panels();
            reValue_Panels();
        }

        void Generate_Panels()
        {
            LocationY = 30;
            _rowCount = 7;
            int padding = ((this.Width - (40 * _rowCount) - (1 * (_rowCount - 1)))) / 2;

            //Dynamic Day Names
            for (int i = 0; i < 7; i++)
            {
                Label lbl_DayName = new Label();
                Panel pnl_DayName = new Panel();
                //dynamic label
                lbl_DayName.Name = "DayName" + i;
                lbl_DayName.Dock = DockStyle.Fill;
                lbl_DayName.Text = Days[i];
                lbl_DayName.TextAlign = ContentAlignment.MiddleCenter;
                //dynamic panel
                if (i == 0)
                    LocationX = padding;

                pnl_DayName.Location = new Point(LocationX, 0);
                pnl_DayName.Size = new Size(41, 20);
                pnl_DayName.Name = "PnlDayName" + i;
                pnl_DayName.Controls.Add(lbl_DayName);
                pnl_Content.Controls.Add(pnl_DayName);
                LocationX += 41;
            }
            LocationX = 0;

            //Dynamic Day Panels
            for (int i = 1; i <= 42; i++)
            {
                Panel pnl_Day = new Panel();
                Label lbl_DayNum = new Label();

                //dynamic panel
                pnl_Day.Name = "PnlDays" + i;
                pnl_Day.Click += Pnl_Day_Click;
                pnl_Day.BorderStyle = BorderStyle.FixedSingle;

                if (i == 1)
                    LocationX = padding;

                pnl_Day.Location = new Point(LocationX, LocationY);
                pnl_Day.Size = new Size(40, 40);

                if (i % _rowCount == 0)
                {
                    LocationX = padding - 41;
                    LocationY += 41;
                }
                //dynamic label
                lbl_DayNum.Name = "LblDayNumber" + i;
                lbl_DayNum.Click += Lbl_DayNum_Click;
                pnl_Content.Controls.Add(pnl_Day);//panel
                pnl_Day.Controls.Add(lbl_DayNum);//label
                LocationX += 41;
            }
        }
        void reValue_Panels()
        {
            //Month Names
            lbl_Date.Text = date.ToString("MMMM");
            lbl_Date.Location = new Point((pnl_Top.Width - lbl_Date.Width - 70) / 2, 5);
            lbl_Year.Text = date.Year.ToString();
            lbl_Year.Location = new Point((pnl_Top.Width - lbl_Year.Width + 90) / 2, 5);

            Panel pnl_Day = new Panel();
            Label lbl_DayNum = new Label();

            DateTime firstdayofMonth = new DateTime(date.Year, date.Month, 1);
            int DayCount = DateTime.DaysInMonth(date.Year, date.Month);
            int firstdayofWeek = Convert.ToInt32(firstdayofMonth.DayOfWeek);
            int CurrentDay = DateTime.Now.Day;
            int CurrentMonth = DateTime.Now.Month;
            int CurrentYear = DateTime.Now.Year;

            int NextMonthDays = 1;

            if (date.Month > 1)
                PreMonthDays = DateTime.DaysInMonth(date.Year, date.Month - 1);
            PreMonthDays = PreMonthDays - firstdayofWeek + 2;

            for (int i = 8; i <= pnl_Content.Controls.Count - 1; i++)
            {
                Panel p = pnl_Content.Controls[i] as Panel;
                Label l = p.Controls[0] as Label;

                if (i - 7 <= firstdayofWeek - 1)
                {
                    p.BackColor = Color.HotPink;
                    l.Text = PreMonthDays.ToString();
                    p.Enabled = false;
                    PreMonthDays++;
                }
                else if (DayCount == 0)
                {
                    p.BackColor = Color.HotPink;
                    l.Text = NextMonthDays.ToString();
                    p.Enabled = false;
                    NextMonthDays++;
                }
                else if (firstdayofWeek == 0)
                {
                    p.BackColor = Color.Transparent;
                    p.Visible = true;
                    p.Enabled = true;
                    l.Text = (i - firstdayofWeek - 7).ToString();
                    DayCount--;
                }
                else
                {
                    p.BackColor = Color.Transparent;
                    p.Visible = true;
                    p.Enabled = true;
                    l.Text = (i - firstdayofWeek - 6).ToString();
                    DayCount--;
                }
                //Current Day
                if (p.Name == ("PnlDays" + (CurrentDay + firstdayofWeek - 1)) && CurrentMonth == date.Month && CurrentYear == date.Year)
                    p.BackColor = Color.Red;
            }
        }
        //Linklabel Click
        void LinkLbl_Right_Click(object sender, EventArgs e)
        {
            date = date.AddMonths(1);
            reValue_Panels();
        }
        //Linklabel Click
        void LinkLbl_Left_Click(object sender, EventArgs e)
        {
            if (date.Month == 2)
            {
                pre = date.AddYears(-1);
                PreMonthDays = DateTime.DaysInMonth(pre.Year, pre.Month + 10);
            }
            date = date.AddMonths(-1);
            reValue_Panels();
        }
        //Change BackColor of DayNum parent
        void Lbl_DayNum_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (lbl.Parent.BackColor == Color.Red)
                lbl.Parent.BackColor = Color.Transparent;
            else
                lbl.Parent.BackColor = Color.Red;
        }
        //Change BackColor of DayNum parent

        void Pnl_Day_Click(object sender, EventArgs e)
        {
            Panel pnl = (Panel)sender;
            if (pnl.BackColor == Color.Red)
                pnl.BackColor = Color.Transparent;
            else
                pnl.BackColor = Color.Red;
        }
    }
}