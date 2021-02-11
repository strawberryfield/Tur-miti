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
using Casasoft.GTK;
using Casasoft.Turmiti.Engine;
using Gtk;
using System;
using System.Configuration;

namespace Casasoft.Turmiti.GTK
{
    public class TurmitiForm : BaseForm
    {
        private Machine machine;

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

            base.Init(machine.MaxX, machine.MaxY, colorTable[0]);
        }

        private void Init() => Init(string.Empty, false);

        protected override void DoWork()
        {
            machine.Next();
            cr.SetSourceColor(colorTable[machine.CurrentColor]);
            cr.Rectangle(machine.currentX, machine.currentY, 1, 1);
            cr.Fill();
            img.QueueDraw();
        }

        protected override void Clear()
        {
            machine.Clear();
            cr.SetSourceColor(colorTable[0]);
            cr.Rectangle(0, 0, machine.MaxX, machine.MaxY);
            cr.Fill();
        }

    }
}
