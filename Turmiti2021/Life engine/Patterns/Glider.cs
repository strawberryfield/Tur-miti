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
    /// <see cref="https://www.conwaylife.com/wiki/Glider"/>
    /// </remarks>
    public class Glider : Pattern
    {
        public Glider(int[,] data) : base(data)
        {
        }

        public Glider(int x, int y) : base(x, y)
        {
        }

        public Glider() : this(new int[,] {
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 1, 1, 1 }
        })
        {
            Rotate();
            FlipHorizontally();
        }

        public Glider(Machine.Direction dir) : this()
        {
            switch (dir)
            {
                case Machine.Direction.NorthEast:
                    FlipVerically();
                    break;
                case Machine.Direction.SouthEast:
                    break;
                case Machine.Direction.SouthWest:
                    FlipHorizontally();
                    break;
                case Machine.Direction.NorthWest:
                    FlipHorizontally();
                    FlipVerically();
                    break;
                default:
                    break;
            }
        }
    }
}
