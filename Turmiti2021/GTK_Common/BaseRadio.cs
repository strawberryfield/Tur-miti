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

using Gtk;
using System;

namespace Casasoft.GTK
{
    public class BaseRadio : VBox
    {
        public RadioButton radio { get; protected set; }

        public BaseRadio() : this(false, 8)
        {
        }

        public BaseRadio(IntPtr raw) : base(raw)
        {
            AddComponents();
        }

        public BaseRadio(bool homogeneous, int spacing) : base(homogeneous, spacing)
        {
            BorderWidth = (uint)spacing;
            AddComponents();
        }

        public virtual void AddComponents() { }

        public RadioButton AddButton(string caption)
        {
            RadioButton ret;

            if (radio == null)
            {
                radio = new(caption);
                radio.Active = true;
                ret = radio;
            }
            else
            {
                ret = new(radio, caption);
            }
            PackStart(ret, true, true, 0);

            return ret;
        }
    }
}
