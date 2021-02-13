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
    /// Implements a Toad
    /// </summary>
    /// <remarks>
    /// <see cref="https://www.conwaylife.com/wiki/Toad"/>
    /// </remarks>
    public class Toad : Pattern
    {
        public Toad(int[,] data) : base(data)
        {
        }

        public Toad(int x, int y) : base(x, y)
        {
        }

        public Toad() : this(new int[,] {
            { 0, 1, 1, 1 },
            { 1, 1, 1, 0 }
        })
        {
            Rotate();
            FlipHorizontally();
        }

        public Toad(Machine.Direction dir) : this()
        {
            switch (dir)
            {
                case Machine.Direction.North:
                    Rotate();
                    FlipHorizontally();
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
