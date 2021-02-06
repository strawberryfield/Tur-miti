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
using Casasoft.Life.Engine;
using Casasoft.GTK;
using Gtk;
using System;
using System.Configuration;

namespace Casasoft.Life
{
    public class LifeForm : BaseForm
    {
        private Machine machine;

        public LifeForm(IntPtr raw) : base(raw)
        {
            Init();
        }

        public LifeForm(WindowType type) : base(type)
        {
            Init();
        }

        public LifeForm(string title) : base(title)
        {
            Init();
        }

        private void Init()
        {
            machine = new();
            saveName = System.IO.Path.Combine(ConfigurationManager.AppSettings["SavePath"], "Life_");
            base.Init(machine.MaxX * 4, machine.MaxY * 4, new Color(1, 1, 1));

            // sample
            machine.World[100, 100] = 1;
            machine.World[101, 100] = 1;
            machine.World[102, 100] = 1;
            machine.World[102, 99] = 1;
            machine.World[101, 98] = 1;

            ShowGeneration();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);

        }

        protected void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                machine.NextGeneration();
                ShowGeneration();
            }
        }

        protected void ShowGeneration()
        {
            cr.SetSourceColor(new Color(1, 1, 1));
            cr.Rectangle(0, 0, machine.MaxX * 4, machine.MaxY * 4);
            cr.Fill();

            cr.SetSourceColor(new Color(0, 1, 0));
            for (int x = 0; x < machine.MaxX; x++)
            {
                for (int y = 0; y < machine.MaxY; y++)
                {
                    if (machine.World[x, y] == 1)
                    {
                        cr.Rectangle(x * 4, y * 4, 3, 3);
                        cr.Fill();
                    }
                }
            }
            img.QueueDraw();
        }
    }
}
