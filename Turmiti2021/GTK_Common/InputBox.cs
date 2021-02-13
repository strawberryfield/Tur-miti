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
using Gtk;
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
    public class InputBox : BaseDialogBox
    {
        private Label label;
        private Entry entry;

        public string Value => entry.Text;
        public int IntValue
        {
            get
            {
                int ret;
                if (!int.TryParse(entry.Text, out ret))
                {
                    ret = 0;
                }
                return ret;
            }
        }

        public InputBox()
        {
        }

        public InputBox(IntPtr raw) : base(raw)
        {
        }


        public InputBox(string title, Window parent, DialogFlags flags, params object[] button_data) :
            base(title, parent, flags, button_data)
        { }

        public InputBox(string title, Window parent) : base(title, parent)
        { }

        protected override void AddComponents()
        {
            label = new("Value:");
            hbox.PackStart(label, false, false, 0);
            entry = new();
            hbox.PackStart(entry, false, false, 0);
            label.MnemonicWidget = entry;
        }
    }
}
