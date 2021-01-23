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

using Casasoft.Turmiti.GTK;
using Gtk;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;

string ExeName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
bool ShouldShowHelp = false;
bool OnSphere = false;
List<string> FilesList;
Application.Init();

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
    ShowMessage($"{ExeName}: {e.Message}\nTry '{ExeName} --help' for more informations.");
    return 1;
}

if (FilesList == null || FilesList.Count == 0)
{

    ShowMessage("No input file!");
    return 1;
}

if (ShouldShowHelp)
{
    Console.WriteLine("Casasoft Tur-miti/GTK#");
    Console.WriteLine("Copyright 1989,2021 Roberto Ceccarelli - Casasoft\n");
    Console.WriteLine($"Usage: {ExeName} [options] tablename");
    Console.WriteLine("\nOptions:");
    Options.WriteOptionDescriptions(Console.Out);
    return 0;
}

Window myWin = new TurmitiForm("Turmiti/GTK#", FilesList[0], OnSphere);
myWin.Destroyed += delegate
{
    Application.Quit();
};
myWin.ShowAll();

Application.Run();
return 0;

void ShowMessage(string message)
{
    Dialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
    dialog.Title = ExeName;
    dialog.Run();
    dialog.Dispose();
}