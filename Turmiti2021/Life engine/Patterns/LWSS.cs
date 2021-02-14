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
    /// Implements a Lightweight Space Ship
    /// </summary>
    /// <remarks>
    /// <see cref="https://www.conwaylife.com/wiki/Lightweight_spaceship"/>
    /// </remarks>
    public class LWSS : Pattern
    {
        public LWSS(int[,] data) : base(data)
        {
        }

        public LWSS(int x, int y) : base(x, y)
        {
        }

        public LWSS() : this(new int[,] {
            { 0, 1, 0, 0, 1 },
            { 1, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 0 },
        })
        {
            Rotate();
            HotPointX = 4;
            HotPointY = 3;
        }

        public LWSS(Machine.Direction dir) : this()
        {
            switch (dir)
            {
                case Machine.Direction.North:
                    Rotate();
                    FlipVertically();
                    break;
                case Machine.Direction.East:
                    break;
                case Machine.Direction.South:
                    Rotate();
                    break;
                case Machine.Direction.West:
                    FlipHorizontally();
                    break;
                default:
                    break;
            }
        }

    }
}
