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
using System.Threading.Tasks;

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

        private Color BackgroundColor;
        private Color GridColor;
        private Color CellColor;
        private int CellSize;
        private int Delay;

        private void Init()
        {
            BackgroundColor = new Color(1, 1, 1);   // White
            GridColor = new Color(.8, .8, .8);      // Light gray
            CellColor = new Color(0, .5, 0);        // Dark green
            CellSize = GetConfigInt("CellSize", 4);
            Delay = GetConfigInt("Delay", 100);

            machine = new(GetConfigInt("Width", 160), GetConfigInt("Height", 90));
            saveName = System.IO.Path.Combine(ConfigurationManager.AppSettings["SavePath"], "Life_");
            base.Init(machine.MaxX * CellSize, machine.MaxY * CellSize, BackgroundColor);

            // sample
            //machine.World[10, 10] = 1;
            //machine.World[11, 10] = 1;
            //machine.World[12, 10] = 1;
            //machine.World[12, 9] = 1;
            //machine.World[11, 8] = 1;

            ShowGeneration();

        }

        private int CellX => (int)(mouseX / CellSize);
        private int CellY => (int)(mouseY / CellSize);

        protected override void OnLeftMouseClick(object sender, ButtonPressEventArgs args)
        {
            int x = CellX;
            int y = CellY;
            machine.World[x, y] = 1 - machine.World[x, y];
            cr.SetSourceColor(machine.World[x, y] == 1 ? CellColor : BackgroundColor);
            cr.Rectangle(x * CellSize, y * CellSize, CellSize - 1, CellSize - 1);
            cr.Fill();
            img.QueueDraw();
            args.RetVal = true;
        }

        protected override void DoWork()
        {
            machine.NextGeneration();
            ShowGeneration();
        }

        protected void ShowGeneration()
        {
            BackgroundGrid();

            int rectSize = CellSize - 1;

            cr.SetSourceColor(CellColor);
            for (int x = 0; x < machine.MaxX; x++)
            {
                int col = x * CellSize;
                for (int y = 0; y < machine.MaxY; y++)
                {
                    if (machine.World[x, y] == 1)
                    {
                        cr.Rectangle(col, y * CellSize, rectSize, rectSize);
                        cr.Fill();
                    }
                }
            }
            img.QueueDraw();
            Task.Delay(Delay).Wait();
        }

        protected virtual void BackgroundGrid()
        {
            int MaxX = machine.MaxX * CellSize;
            int MaxY = machine.MaxY * CellSize;
            Background(MaxX, MaxY, BackgroundColor);

            cr.SetSourceColor(GridColor);
            for (int x = CellSize - 1; x < MaxX; x += CellSize)
            {
                cr.Rectangle(x, 0, 1, MaxY);
                cr.Fill();
            }
            for (int y = CellSize - 1; y < MaxY; y += CellSize)
            {
                cr.Rectangle(0, y, MaxX, 1);
                cr.Fill();
            }
        }

        protected override void Clear()
        {
            machine.Clear();
            ShowGeneration();
        }

        protected override void PopulateMenu()
        {
            MenuItem mi = new("Line");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                LineInputBox dialog = new("Line width", this);
                ResponseType response = (ResponseType)dialog.Run();
                if (response == ResponseType.Ok)
                {
                    machine.InsertRow(CellX, CellY, dialog.IntValue, dialog.IsVertical);
                    ShowGeneration();
                }
                dialog.Dispose();
            };
            menu.Append(mi);

            mi = new("Square");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                InputBox dialog = new("Square size", this);
                ResponseType response = (ResponseType)dialog.Run();
                if (response == ResponseType.Ok)
                {
                    machine.InsertBlock(CellX, CellY, dialog.IntValue);
                    ShowGeneration();
                }
                dialog.Dispose();
            };
            menu.Append(mi);

            menu.Append(new SeparatorMenuItem());
            mi = new("Blinker (3 Cells line)");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                machine.InsertRow(CellX, CellY, 3);
                ShowGeneration();
            };
            menu.Append(mi);

            mi = new("Block (2x2 block)");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                machine.InsertBlock(CellX, CellY, 2);
                ShowGeneration();
            };
            menu.Append(mi);

            mi = new("Glider");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                machine.InsertGlider(CellX, CellY);
                ShowGeneration();
            };
            menu.Append(mi);

            mi = new("Toad");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                machine.InsertToad(CellX, CellY);
                ShowGeneration();
            };
            menu.Append(mi);

            mi = new("Lightweight Spaceship (LWSS)");
            mi.Activated += delegate (object sender, EventArgs e)
            {
                machine.InsertLWSS(CellX, CellY);
                ShowGeneration();
            };
            menu.Append(mi);

            menu.Append(new SeparatorMenuItem());
            base.PopulateMenu();
        }
    }
}
