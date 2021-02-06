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
using Gtk;
using System;

namespace Casasoft.GTK
{
    public class BaseForm : Window
    {
        protected ImageSurface s;
        protected Image img;
        protected Context cr;
        protected string saveName;
        protected System.ComponentModel.BackgroundWorker backgroundWorker;

        public BaseForm(IntPtr raw) : base(raw)
        {
        }

        public BaseForm(WindowType type) : base(type)
        {
        }

        public BaseForm(string title) : base(title)
        {
        }

        protected void Init(int MaxX, int MaxY, Color background)
        {
            Resize(MaxX,MaxY);
            s = new(Format.RGB24, MaxX, MaxY);
            img = new(s);
            cr = new(s);
            cr.SetSourceColor(background);
            cr.Rectangle(0, 0, MaxX, MaxY);
            cr.Fill();
            Add(img);

            KeyPressEvent += BaseForm_KeyPressEvent;
            Destroyed += delegate
            {
                Application.Quit();
            };

            backgroundWorker = new();
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        [GLib.ConnectBefore]
        private void BaseForm_KeyPressEvent(object o, KeyPressEventArgs args)
        {
            OnKeyPress(args.Event.Key);
        }

        protected virtual void OnKeyPress(Gdk.Key k)
        {
            switch (k)
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

        protected virtual void Save()
        {
            s.WriteToPng(saveName + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".png");
        }

    }
}
