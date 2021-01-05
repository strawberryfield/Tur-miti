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

using System.Configuration;

namespace Casasoft.Turmiti.Engine
{
    public class Machine
    {
        private Rules rules;
        public int[,] World { get; set; }
        public int MaxX { get; init; }
        public int MaxY { get; init; }

        private int stato = 1;
        public int currentX { get; private set; }
        public int currentY { get; private set; }
        private int dirX = 0;
        private int dirY = 1;
        private int nextX;
        private int nextY;

        public Machine(string filename)
        {
            rules = new(filename);

            MaxX = int.Parse(ConfigurationManager.AppSettings["Width"]);
            MaxY = int.Parse(ConfigurationManager.AppSettings["Height"]);
            World = new int[MaxX, MaxY];

            nextX = MaxX / 2;
            nextY = MaxY / 2;
            nextToCurrent();
        }

        private void nextToCurrent()
        {
            currentX = nextX;
            currentY = nextY;
        }

        /// <summary>
        /// Switch to next status
        /// </summary>
        public void Next()
        {
            // fetch data
            nextToCurrent();
            Rule rule = rules.Get(stato, CurrentColor);

            // modify status
            stato = rule.NextStatus;
            World[currentX, currentY] = rule.NextColor;
            switch (rule.Direction)
            {
                case Directions.forward:
                    break;
                case Directions.backward:
                    chs(ref dirX);
                    chs(ref dirY);
                    break;
                case Directions.left:
                    swapDir();
                    if (dirY != 0) chs(ref dirY);
                    break;
                case Directions.right:
                    swapDir();
                    if (dirX != 0) chs(ref dirX);
                    break;
                default:
                    break;
            }

            // move
            nextX = currentX + dirX;
            nextY = currentY + dirY;

            // bounds check
            if (nextX < 0) nextX += MaxX;
            if (nextX >= MaxX) nextX -= MaxX;
            if (nextY < 0) nextY += MaxY;
            if (nextY >= MaxY) nextY -= MaxY;
        }

        private void swapDir()
        {
            int d = dirX;
            dirX = dirY;
            dirY = d;
        }

        private static void chs(ref int n) => n = -n;

        public int CurrentColor => World[currentX, currentY];
    }
}
