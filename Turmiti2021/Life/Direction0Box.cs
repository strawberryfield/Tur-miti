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

using Casasoft.GTK;
using Casasoft.Life.Engine;
using Gtk;
using System;

namespace Casasoft.Life
{
    public class Direction0Box : BaseDialogBox
    {
        public Direction0Box()
        {
        }

        public Direction0Box(IntPtr raw) : base(raw)
        {
        }

        public Direction0Box(string title, Window parent) : base(title, parent)
        {
        }

        public Direction0Box(string title, Window parent, DialogFlags flags, params object[] button_data) : base(title, parent, flags, button_data)
        {
        }

        public Direction0Box(Window parent) : base("Direction", parent) { }

        protected BaseRadio radio;
        protected override void AddComponents()
        {
            radio = new();
            hbox.PackStart(radio, false, false, 0); 
            radio.AddButton(Machine.Direction.North.ToString());
            radio.AddButton(Machine.Direction.East.ToString());
            radio.AddButton(Machine.Direction.South.ToString());
            radio.AddButton(Machine.Direction.West.ToString());
        }

        public Machine.Direction Orientation => 
            (Machine.Direction)Enum.Parse(typeof(Machine.Direction), radio.Selected);
    }
}
