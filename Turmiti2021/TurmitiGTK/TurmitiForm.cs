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

using Cairo;
using Casasoft.Turmiti.Engine;
using Gtk;
using System;
using System.Configuration;

namespace Casasoft.Turmiti.GTK
{
    public class TurmitiForm : Window
    {
        private Machine machine;
        private ImageSurface s;
        private Image img;
        Context cr;
        private string saveName;
        private System.ComponentModel.BackgroundWorker backgroundWorker;

        private Color[] colorTable = { new Color(1, 1, 1), new Color(1, 0, 0), new Color(0, 0, 1), new Color(0, 0, 0) };

        public TurmitiForm(IntPtr raw) : base(raw)
        {
            Init();
        }

        public TurmitiForm(WindowType type) : base(type)
        {
            Init();
        }

        public TurmitiForm(string title) : base(title)
        {
            Init();
        }

        public TurmitiForm(string title, string filename, bool isSphere) : base(title)
        {
            Init(filename, isSphere);
        }

        private void Init(string filename, bool isSphere)
        {
            machine = new(filename, isSphere);
            saveName = System.IO.Path.Combine(ConfigurationManager.AppSettings["SavePath"], "Turmiti_");

            Resize(machine.MaxX, machine.MaxY);
            s = new(Format.RGB24, machine.MaxX, machine.MaxY);
            img = new(s);
            cr = new(s);
            cr.SetSourceColor(colorTable[0]);
            cr.Rectangle(0, 0, machine.MaxX, machine.MaxY);
            cr.Fill();
            Add(img);

            KeyPressEvent += TurmitiForm_KeyPressEvent;

            backgroundWorker = new();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);

        }

        [GLib.ConnectBefore]
        private void TurmitiForm_KeyPressEvent(object o, KeyPressEventArgs args)
        {
            switch (args.Event.Key)
            {
                case Gdk.Key.space:
                    if (backgroundWorker.IsBusy)
                    {
                        backgroundWorker.CancelAsync();
                    }
                    else
                    {
                        backgroundWorker.RunWorkerAsync();
                    }
                    break;

                case Gdk.Key.s:
                case Gdk.Key.S:
                    Save();
                    break;

                default:
                    break;
            }
        }

        private void Init() => Init(string.Empty, false);

        protected void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                machine.Next();
                cr.SetSourceColor(colorTable[machine.CurrentColor]);
                cr.Rectangle(machine.currentX, machine.currentY, 1, 1);
                cr.Fill();
                img.QueueDraw();
            }
        }

        private void Save()
        {
            s.WriteToPng(saveName + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".png");
        }
    }
}
