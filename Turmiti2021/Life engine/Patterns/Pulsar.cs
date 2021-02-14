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

namespace Casasoft.Life.Engine.Patterns
{
    /// <summary>
    /// Implements a Glider
    /// </summary>
    /// <remarks>
    /// <see cref="https://www.conwaylife.com/wiki/Pulsar"/>
    /// </remarks>
    public class Pulsar : Pattern
    {
        public Pulsar(int[,] data) : base(data)
        {
        }

        public Pulsar(int x, int y) : base(x, y)
        {
        }

        public Pulsar() : this(13,13)
        {
            Row1(0);
            Row2(2);
            Row2(3);
            Row2(4);
            Row1(5);

            Row1(7);
            Row2(8);
            Row2(9);
            Row2(10);
            Row1(12);

            HotPointX = 6;
            HotPointY = 6;
        }

        private void Row1(int y)
        {
            Data[y, 2] = 1;
            Data[y, 3] = 1;
            Data[y, 4] = 1;
            Data[y, 8] = 1;
            Data[y, 9] = 1;
            Data[y, 10] = 1;
        }

        private void Row2(int y)
        {
            Data[y, 0] = 1;
            Data[y, 5] = 1;
            Data[y, 7] = 1;
            Data[y, 12] = 1;
        }

    }
}
