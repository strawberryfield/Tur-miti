// copyright (c) 1989,2021 Roberto Ceccarelli - Casasoft
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
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Casasoft.Turmiti
{
    public partial class TurmitiForm : Form
    {
        private Machine machine;
        private Graphics g;
        private string saveName;
        
        public TurmitiForm()
        {
            InitializeComponent();
        }

        public TurmitiForm(string filename, bool isSphere) : this()
        {
            machine = new(filename, isSphere);
            saveName = Path.Combine(ConfigurationManager.AppSettings["SavePath"], "Turmiti_");
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

                case Keys.S:
                    Save();
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

        private Brush[] colorTable = { Brushes.White, Brushes.Red, Brushes.Blue, Brushes.Black };         
        
        private void Save()
        {
            Bitmap b = PrintClientRectangleToImage();
            b.Save(saveName+ DateTime.Now.ToString("yyyy-MM-dd_HHmmss")+".png", ImageFormat.Png);
        }

        const int SRCCOPY = 0xCC0020;
        [DllImport("gdi32.dll")]
        static extern int BitBlt(IntPtr hdc, int x, int y, int cx, int cy,
            IntPtr hdcSrc, int x1, int y1, int rop);

        private Bitmap PrintClientRectangleToImage()
        {
            Bitmap bmp = new (ClientSize.Width, ClientSize.Height);
            using (var bmpGraphics = Graphics.FromImage(bmp))
            {
                var bmpDC = bmpGraphics.GetHdc();
                using (Graphics formGraphics = Graphics.FromHwnd(this.Handle))
                {
                    var formDC = formGraphics.GetHdc();
                    BitBlt(bmpDC, 0, 0, ClientSize.Width, ClientSize.Height, formDC, 0, 0, SRCCOPY);
                    formGraphics.ReleaseHdc(formDC);
                }
                bmpGraphics.ReleaseHdc(bmpDC);
            }
            return bmp;
        }
    }
}
