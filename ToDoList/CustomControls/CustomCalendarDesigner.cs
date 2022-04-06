﻿using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class CustomCalendar
    {
        //fields
        private Panel pnl_Top;
        private Panel pnl_Content;
        private LinkLabel linkLbl_Left;
        private LinkLabel linkLbl_Right;

        private Label lbl_Date;

        void Render()
        {
            //control customization
            this.BackColor = Color.CornflowerBlue;


            //panel customization
            pnl_Top = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.BlueViolet,
            };
            pnl_Content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.MediumVioletRed,
            };
            //linklabel customization
            linkLbl_Left = new LinkLabel
            {
                Text = "<",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                LinkColor = Color.Black,
                VisitedLinkColor = Color.Black,
                ActiveLinkColor = Color.DarkGray,
                LinkBehavior = LinkBehavior.NeverUnderline,
            };

            linkLbl_Right = new LinkLabel
            {
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point),
                Text = ">",
                LinkColor = Color.Black,
                VisitedLinkColor = Color.Black,
                ActiveLinkColor = Color.DarkGray,
                AutoSize = true,
                LinkBehavior = LinkBehavior.NeverUnderline,
            };

            //label customization
            lbl_Date = new Label
            {
                Location = new Point(140, 0),
                Text = "test",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
            };

            linkLbl_Left.Click+=LinkLbl_Left_Click;
            linkLbl_Right.Click+=LinkLbl_Right_Click;

            this.Load += Handle_Load;
            this.Controls.Add(pnl_Content);
            this.Controls.Add(pnl_Top);

            pnl_Top.Controls.Add(lbl_Date);
            pnl_Top.Controls.Add(linkLbl_Right);
            pnl_Top.Controls.Add(linkLbl_Left);
        }

       

    }
}
