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
    public class InputBox : Dialog
    {
        private Label label;
        private Entry entry;

        public string Value => entry.Text;
        public int IntValue
        {
            get
            {
                int ret;
                if(!int.TryParse(entry.Text, out ret))
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
        {
            HBox hbox = new HBox(false, 8);
            hbox.BorderWidth = 8;
            ContentArea.PackStart(hbox, false, false, 0);

            Image stock = new Image(Stock.DialogQuestion, IconSize.Dialog);
            hbox.PackStart(stock, false, false, 0);

            label = new("Value:");
            hbox.PackStart(label, false, false, 0);
            entry = new();
            hbox.PackStart(entry, false, false, 0);
            label.MnemonicWidget = entry;

            hbox.ShowAll();
        }

        public InputBox(string title, Window parent) :
            this(title, parent, DialogFlags.DestroyWithParent | DialogFlags.Modal,
                    Gtk.Stock.Ok, ResponseType.Ok, Gtk.Stock.Cancel, ResponseType.Cancel)
        { }

    }
}
