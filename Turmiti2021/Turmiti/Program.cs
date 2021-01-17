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

using Casasoft.Turmiti;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

string ExeName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
bool ShouldShowHelp = false;
bool OnSphere = false;
List<string> FilesList;

OptionSet Options = new OptionSet
    {
        { "sphere", "map the world over a sphere (default torus)", o => OnSphere = o != null },
        { "help", "prints this message and exit", o =>  ShouldShowHelp = o != null },
    };

try
{
    FilesList = Options.Parse(args);
}
catch (OptionException e)
{
    MessageBox.Show($"{ExeName}: {e.Message}\nTry '{ExeName} --help' for more informations.");
    return 1;
}

if(FilesList == null || FilesList.Count == 0)
{
    MessageBox.Show("No input file!");
    return 1;
}

if (ShouldShowHelp)
{
    Console.WriteLine("Casasoft Tur-miti edition 2021");
    Console.WriteLine("Copyright 1989,2021 Roberto Ceccarelli - Casasoft\n");
    Console.WriteLine($"Usage: {ExeName} [options] tablename");
    Console.WriteLine("\nOptions:");
    Options.WriteOptionDescriptions(Console.Out);
    return 0;
}

Application.SetHighDpiMode(HighDpiMode.SystemAware);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new TurmitiForm(FilesList[0], OnSphere));
return 0;