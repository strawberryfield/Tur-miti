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

using System.Collections.Generic;
using System.IO;

namespace Casasoft.Turmiti.Engine
{
    public class Rules
    {
        private List<List<Rule>> matrix;

        public Rules(string filename)
        {
            matrix = new();
            string[] lines = File.ReadAllLines(filename);
            int nCols = int.Parse(lines[0]);
            List<Rule> row = new();
            for(int i=1; i < lines.Length; i++)
            {
                if((i-1) % nCols == 0)
                {
                    row = new();
                }
                row.Add(new(lines[i]));
                if ((i - 1) % nCols == (i-1))
                {
                    matrix.Add(row);
                }
            }
        }

        public Rule Get(int status, int color) => matrix[status-1][color];
    }
}
