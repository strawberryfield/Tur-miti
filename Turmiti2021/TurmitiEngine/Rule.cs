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

namespace Casasoft.Turmiti.Engine
{
    public enum Directions { forward, backward, left, right }

    public class Rule
    {
        public int NextColor { get; set; }
        public int NextStatus { get; set; }
        public Directions Direction { get; set; }

        public Rule(string data)
        {
            string[] el = data.Split(',');
            NextStatus = int.Parse(el[0]);
            NextColor = int.Parse(el[1]);
            switch (el[2].ToUpper()[0])
            {
                case 'F':
                case 'A':
                    Direction = Directions.forward;
                    break;
                case 'B':
                case 'I':
                    Direction = Directions.backward;
                    break;
                case 'L':
                case 'S':
                    Direction = Directions.left;
                    break;
                case 'R':
                case 'D':
                    Direction = Directions.right;
                    break;
                default:
                    break;
            }
        }
    }
}
