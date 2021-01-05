﻿// copyright (c) 1989,2021 Roberto Ceccarelli - Casasoft
// http://strawberryfield.altervista.org 
// 
// This file is part of Casasoft Turmiti
// https://github.com/strawberryfield/Tur-miti
// 
// Casasoft Turmiti is free software: 
// you can redistribute it and/or modify it
// under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Casasoft Turmiti is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU AGPL v.3
// along with Casasoft Turmiti.  
// If not, see <http://www.gnu.org/licenses/>.

using Casasoft.Turmiti.Engine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Casasoft.Turmiti
{
    public partial class TurmitiForm : Form
    {
        private Machine machine;
        private Graphics g;
        
        public TurmitiForm()
        {
            InitializeComponent();
        }

        public TurmitiForm(string filename) : this()
        {
            machine = new(filename);
            ClientSize = new(machine.MaxX, machine.MaxY);
            g = CreateGraphics();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            g.FillRectangle(colorTable[0], 0, 0, machine.MaxX, machine.MaxY);
        }

        private void TurmitiForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Space:
                    if(backgroundWorker.IsBusy)
                    {
                        backgroundWorker.CancelAsync();
                    }
                    else
                    {
                        backgroundWorker.RunWorkerAsync();
                    }
                    break;

                default:
                    break;
            }     
        }

        protected  void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                machine.Next();
                g.FillRectangle(colorTable[machine.CurrentColor], machine.currentX, machine.currentY, 1, 1);
            }
        }

        private Brush[] colorTable = { Brushes.Black,  Brushes.Red, Brushes.Blue, Brushes.White };                  
    }
}
