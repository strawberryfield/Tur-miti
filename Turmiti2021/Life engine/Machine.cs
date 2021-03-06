﻿// copyright (c) 2021 Roberto Ceccarelli - Casasoft
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

namespace Casasoft.Life.Engine
{
    public class Machine
    {
        public int[,] World { get; set; }
        public int MaxX { get; init; }
        public int MaxY { get; init; }

        private int[,] NextWorld;

        public enum Direction { North, East, South, West, NorthEast, SouthEast, SouthWest, NorthWest }

        public Machine(int x, int y)
        {
            MaxX = x;
            MaxY = y;
            World = new int[MaxX, MaxY];
            NextWorld = new int[MaxX, MaxY];
        }

        public void NextGeneration()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    int n = (x == 0 || y == 0) ? 0 : World[x - 1, y - 1];
                    n += y == 0 ? 0 : World[x, y - 1];
                    n += (x == MaxX - 1 || y == 0) ? 0 : World[x + 1, y - 1];
                    n += x == MaxX - 1 ? 0 : World[x + 1, y];
                    n += (x == MaxX - 1 || y == MaxY - 1) ? 0 : World[x + 1, y + 1];
                    n += y == MaxY - 1 ? 0 : World[x, y + 1];
                    n += (x == 0 || y == MaxY - 1) ? 0 : World[x - 1, y + 1];
                    n += x == 0 ? 0 : World[x - 1, y];

                    if (World[x, y] == 0)
                    {
                        NextWorld[x, y] = n == 3 ? 1 : 0;
                    }
                    else
                    {
                        NextWorld[x, y] = (n == 2 || n == 3) ? 1 : 0;
                    }
                }
            }

            World = (int[,])NextWorld.Clone();
        }

        public void Clear()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    World[x, y] = 0;
                }
            }
        }

        #region patterns
        // see https://www.conwaylife.com/wiki/Most_common_objects_on_Catagolue

        public void InsertRow(int x, int y, int len, bool vertical = false)
        {
            for (int j = 0; j < len; j++)
            {
                if (vertical)
                    World[x, y + j] = 1;
                else
                    World[x + j, y] = 1;
            }
        }

        public void InsertBlock(int x, int y, int len)
        {
            for (int j = 0; j < len; j++)
            {
                InsertRow(x, y + j, len);
            }
        }

        public void InsertPattern(int x, int y, Pattern p)
        {
            for (int j = 0; j < p.MaxX; j++)
            {
                for (int k = 0; k < p.MaxY; k++)
                {
                    World[x + j - p.HotPointX, y + k - p.HotPointY] = p.Data[j, k];
                }
            }
        }
        #endregion
    }
}
