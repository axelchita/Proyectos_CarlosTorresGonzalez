﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCTS.Presentation.Components
{
    public partial class ucMultiLinePassport : UserControl
    {
       // private int lastCtrlLocation = 60 + 29;

        public ucMultiLinePassport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int lastCtrlLocation = 15 + 10;

            LineTextPassport lineSmart = new LineTextPassport();
            Panel pnl1 = (Panel)this.Controls.Find("panel1", false)[0];
            lineSmart.Location = new System.Drawing.Point(1, 38);
            lineSmart.Focus();
            panel1.Controls.Add(lineSmart);
        }
    }
}
