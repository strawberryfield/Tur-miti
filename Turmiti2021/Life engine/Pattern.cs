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

namespace Casasoft.Life.Engine
{
    /// <summary>
    /// Basic class for patterns management
    /// </summary>
    public class Pattern
    {
        public int[,] Data { get; set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public int HotPointX { get; set; }
        public int HotPointY { get; set; }

        public Pattern(int x, int y)
        {
            MaxX = x;
            MaxY = y;
            Data = new int[MaxX, MaxY];
            HotPointX = 0;
            HotPointY = 0;
        }

        public Pattern(int[,] data)
        {
            Data = data;
            MaxX = Data.GetLength(0);
            MaxY = Data.GetLength(1);
            HotPointX = 0;
            HotPointY = 0;
        }

        public void FlipHorizontally()
        {
            int[,] tmp = new int[MaxX, MaxY];

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    tmp[x, y] = Data[MaxX - 1 - x, y];
                }
            }
            Data = tmp;
            HotPointX = MaxX - 1 - HotPointX;
        }

        public void FlipVertically()
        {
            int[,] tmp = new int[MaxX, MaxY];

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    tmp[x, y] = Data[x, MaxY - 1 - y];
                }
            }
            Data = tmp;
            HotPointY = MaxY - 1 - HotPointY;
        }

        /// <summary>
        /// Rotate the pattern 90 deg clockwise
        /// </summary>
        public void Rotate()
        {
            int[,] tmp = new int[MaxY, MaxX];

            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    tmp[MaxY - 1 - y, x] = Data[x, y];
                }
            }
            Data = tmp;

            int t = HotPointX;
            HotPointX = MaxY - 1 - HotPointY;
            HotPointY = t;
            t = MaxX;
            MaxX = MaxY;
            MaxY = t;
        }

    }
}
