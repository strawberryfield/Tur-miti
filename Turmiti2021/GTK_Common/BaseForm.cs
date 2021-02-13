// copyright (c) 2021 Roberto Ceccarelli - Casasoft
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
using System.Configuration;
using System.Threading;

namespace Casasoft.GTK
{
    public class BaseForm : Window
    {
        protected ImageSurface s;
        protected Image img;
        protected Context cr;
        protected string saveName;
        protected System.ComponentModel.BackgroundWorker backgroundWorker;
        protected Menu menu;

        protected double mouseX;
        protected double mouseY;

        private AutoResetEvent bwStoppedEvent = new AutoResetEvent(false);

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
            Resize(MaxX, MaxY);
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
            menu = new();
            PopulateMenu();
            PopupMenu += HandlePopupMenu;
            ButtonPressEvent += OnMouseClick;

            backgroundWorker = new();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
        }

        #region menu
        protected virtual void PopulateMenu()
        {
            MenuItem miClear = new("Clear");
            miClear.Activated += delegate (object sender, EventArgs e)
            {
                DoClear();
            };
            menu.Append(miClear);

            MenuItem miStartStop = new("Start / Stop");
            miStartStop.Activated += delegate (object sender, EventArgs e)
            {
                StartStop();
            };
            menu.Append(miStartStop);

            MenuItem miNext = new("Next");
            miNext.Activated += delegate (object sender, EventArgs e)
            {
                DoWork();
            };
            menu.Append(miNext);

            MenuItem miSave = new("Save");
            miSave.Activated += delegate (object sender, EventArgs e)
            {
                Save();
            };
            menu.Append(miSave);

            menu.Append(new SeparatorMenuItem());
            MenuItem miQuit = new("Quit");
            miQuit.Activated += delegate (object sender, EventArgs e)
            {
                Application.Quit();
            };
            menu.Append(miQuit);

            menu.ShowAll();
        }

        [GLib.ConnectBefore]
        public void HandlePopupMenu(object o, PopupMenuArgs args)
        {           
            menu.Popup();
        }
        #endregion

        #region backgroundworker
        protected void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                DoWork();
            }
            bwStoppedEvent.Set();
        }

        protected virtual void DoWork() { }
        #endregion

        #region mouse
        private void OnMouseClick(object sender, ButtonPressEventArgs args)
        {
            mouseX = args.Event.X;
            mouseY = args.Event.Y;

            switch (args.Event.Button)
            {
                case 1:
                    OnLeftMouseClick(sender, args);
                    break;

                case 3:
                    menu.Popup();
                    break;

                default:
                    break;
            }
        }

        protected virtual void OnLeftMouseClick(object sender, ButtonPressEventArgs args) { }
        #endregion

        #region keyboard
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
                    StartStop();
                    break;

                case Gdk.Key.n:
                case Gdk.Key.N:
                    DoWork();
                    break;

                case Gdk.Key.c:
                case Gdk.Key.C:
                    DoClear();
                    break;

                case Gdk.Key.s:
                case Gdk.Key.S:
                    Save();
                    break;

                default:
                    break;
            }

        }
        #endregion

        #region commands
        protected virtual void Save()
        {
            s.WriteToPng(saveName + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".png");
        }

        private void DoClear()
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                bwStoppedEvent.WaitOne();
            }
            Clear();
        }

        private void StartStop()
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
            else
            {
                bwStoppedEvent.Reset();
                backgroundWorker.RunWorkerAsync();
            }
        }
        #endregion

        #region screen
        protected virtual void Clear() { }

        protected virtual void Background(int x, int y, Color BackColor)
        {
            cr.SetSourceColor(BackColor);
            cr.Rectangle(0, 0, x, y);
            cr.Fill();
        }
        #endregion

        #region config management
        protected string GetConfigString(string parname, string pardefault)
        {
            string ret;
            try
            {
                ret = ConfigurationManager.AppSettings[parname];
            }
            catch
            {
                ret = pardefault;
            }

            if (string.IsNullOrWhiteSpace(ret))
            {
                ret = pardefault;
            }

            return ret;
        }

        protected int GetConfigInt(string parname, int pardefault)
        {
            int ret;

            string sRet = GetConfigString(parname, pardefault.ToString());
            if (!int.TryParse(sRet, out ret))
            {
                ret = pardefault;
            }

            return ret;
        }
        #endregion

    }
}
