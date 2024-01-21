using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using System.Runtime.Serialization;
using Cosmos.Core.IOGroup;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Cosmos.System.FileSystem.VFS;
using Cosmos.Core;
using System.Formats.Asn1;
using System.ComponentModel.Design;
using Cosmos.Core.Memory;
using System.Data;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Linq;
using Cosmos.HAL;
using Cosmos.System.Network.Config;
using System.Runtime.InteropServices;

namespace SamOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            try
            {
                System.Threading.Thread.Sleep(1);
                Console.Clear();
                Console.WriteLine("Starting Up SamOS... ");
                System.Threading.Thread.Sleep(1);
                Console.Clear();
                System.Threading.Thread.Sleep(1);
                Console.Beep();
                Console.WriteLine("Terms and conditions: ");
                Console.WriteLine("You may use this OS only for non-commercial purposes only, and use of this OS for commercial purposes, or to make a profit, is prohibited.");
                Console.WriteLine("This OS is completely free for evreyone, it is prohibited to add code to this OS that collects user data and sells it to a specific IP address or company.");
                Console.WriteLine("Any attempt to make this OS subscription based, make this OS closed source, add malicious code, add code that spies on users or making this OS paid (except for optional donations to the community) is prohibited.");
                Console.WriteLine("\nAgree?");
                Console.WriteLine("y/Y or no args - Yes.");
                Console.WriteLine("n/N - No.");
                Console.WriteLine("Choice: ");
                var UserChoice = Console.ReadLine();
                if (UserChoice == "y" || UserChoice == "Y" || string.IsNullOrWhiteSpace(UserChoice))
                {
                    Run();
                }
                else if (UserChoice == "n" || UserChoice == "N")
                {
                    Console.Clear();
                    Console.WriteLine("Shutting Down...");
                    System.Threading.Thread.Sleep(1);
                    Console.Beep();
                    Cosmos.System.Power.Shutdown();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Argument, Rebooting...");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(1);
                    Console.Beep();
                    Cosmos.System.Power.Reboot();
                }
            }
            catch (Exception e) { Console.Clear();  Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Exception Occured: " + e.ToString()); Console.WriteLine("Press any key to shut down... ");
                Console.ReadKey(); Console.ForegroundColor = ConsoleColor.White; Console.Clear(); Console.WriteLine("Shutting Down... "); System.Threading.Thread.Sleep(2); Console.Beep(); Cosmos.System.Power.Shutdown(); }
        }

        protected override void Run()
        {
            Console.Clear();
            Console.Beep();
            Console.WriteLine("SAM OS menu: ");
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("0 or no args - Start SamOS.");
            Console.WriteLine("1 - Shut Down SamOS.");
            Console.WriteLine("2 - Reboot SamOS.");
            Console.Write("Your Input: ");
            var MenuInput = Console.ReadLine();
            if (MenuInput == "0" || string.IsNullOrWhiteSpace(MenuInput))
            {
                try
                {
                    DateTime time = DateTime.Now;
                    var invalidcommand_msg = "Invalid Command, to get help about valid commands, type 'help'.";
                    var old_prompt = "> ";
                    List<string> commandhistory = new List<string>();
                    Console.Clear();
                    Console.Beep();
                    System.Threading.Thread.Sleep(1);
                    Console.WriteLine("Welcome to SamOS! Please set up your account by entering a username and password.");
                    Console.Write("Set Username: ");
                    var username = Console.ReadLine();
                    Console.Write("Set Password: ");
                    var password = Console.ReadLine();
                    Console.WriteLine("");
                    Console.WriteLine("Your account is set up, press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Enter a PC name, a PC name is a unique identification of real hardware or VM running SamOS, You can change the PC name later using the pcname command.");
                    Console.Write("Enter a PC name: ");
                    var PCName = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Enter a session name, a session name is a name of the current session of SamOS.");
                    Console.Write("Session Name: ");
                    var SessionName = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Loading Login Screen...");
                    Console.Beep();
                    System.Threading.Thread.Sleep(1);
                    Console.Clear();
                    Console.Write("Username: ");
                    var enteredusername = Console.ReadLine();
                    if (enteredusername == username)
                    {
                        Console.Write("Password: ");
                        var enteredpassword = Console.ReadLine();
                        if (enteredpassword == password)
                        {
                            Console.Beep();
                            Console.Clear();
                            if (time.Month == 12 && time.Day == 25)
                            {
                                Console.WriteLine("Merry Christmas! Type 'help' to get help with commands HO HO HO!");
                            }
                            else
                            {
                                Console.WriteLine("Welcome to SamOS, to get infomation about commands, type 'help'.");
                            }
                            while (true)
                            {
                                Console.Write(old_prompt);
                                var input = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(input))
                                {

                                }
                                else if (input.StartsWith("help"))
                                {
                                    var argTwo = input.Remove(0, 5);
                                    if (string.IsNullOrWhiteSpace(argTwo) || argTwo == "all")
                                    {
                                        Console.WriteLine("print <text> - Prints text on the screen.");
                                        Console.WriteLine("shutdown - Shuts down or reboots the computer, use 's' for shutdown, and 'r' for reboot, in this format: 'shutdown <s/r>'.");
                                        Console.WriteLine("help - Shows infomation about commands.");
                                        Console.WriteLine("clear/cls - Clears the screen.");
                                        Console.WriteLine("account <usr/psw> - Displays info about your SamOS account, 'usr' for username, 'psw' for password, or blank for both.");
                                        Console.WriteLine("about - Know about SamOS");
                                        Console.WriteLine("pause <true/false> - Pauses the console until any key is pressed, if the value after semicolon is 'true', it will show the 'Press any key to continue...', if the value is 'false', it will not show the output.");
                                        Console.WriteLine("prompt <prompt_here> - Sets the custom prompt.");
                                        Console.WriteLine("systeminfo - Displays system infomation.");
                                        Console.WriteLine("resetprompt: Resets prompt to default");
                                        Console.WriteLine("pcname <pc-name>: Changes the PC name.");
                                        Console.WriteLine("datetime: Displays date and time.");
                                        Console.WriteLine("sleep <number-of-secs>: Pauses the SamOS console for a specified amount of seconds.");
                                        Console.WriteLine("invmsg <message>: Allows a user to change the message that pops up when the user enters a invalid command.");
                                        Console.WriteLine("resetinvmsg: Resets the message that pops up when a user enters a invalid command, to the default one.");
                                        Console.WriteLine("tcolor <color>: Changes the text color.");
                                        Console.WriteLine("bcolor <color>: Changes the background color.");
                                        Console.WriteLine("mulprint <statements>: Prints multiple lines of text in a single command, each sentence is seperated by commas.");
                                        Console.WriteLine("math: Adds, subtracts, multiplies, or divides numbers.");
                                        Console.WriteLine("rprint <text>: Prints text in reverse.");
                                        Console.WriteLine("To get more infomation on a specific command, type 'help <command-name>'.");
                                        Console.WriteLine("Each Command is case-sensitive, and commands can only be entered in lowercase letters, each command with specific valid args has a '?' argument that displays more infomation about the command.");
                                    }
                                    else if (argTwo == "shutdown")
                                    {
                                        Console.WriteLine("Shuts down or restarts the computer.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("'s' or no arguments - Shuts down the computer.");
                                        Console.WriteLine("r - Restarts the computer.");
                                    }
                                    else if (argTwo == "pause")
                                    {
                                        Console.WriteLine("Pauses the console for a specified amount of seconds.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("'true' or no arguments - Shows 'Press Any Key to Continue...'.");
                                        Console.WriteLine("false - Hides 'Presss Any Key to Continue...'.");
                                    }
                                    else if (argTwo == "account")
                                    {
                                        Console.WriteLine("Displays Username and/or Password of a SamOS account.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("usr - Displays Username.");
                                        Console.WriteLine("psw - Displays Password.");
                                        Console.WriteLine("No Arguments - Displays both username and password.");
                                    }
                                    else if (argTwo == "tcolor")
                                    {
                                        Console.WriteLine("Changes text color in the SamOS console.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("blue - Changes color to blue.");
                                        Console.WriteLine("red - Changes color to red.");
                                        Console.WriteLine("green - Changes color to green.");
                                        Console.WriteLine("gray - Changes color to gray.");
                                        Console.WriteLine("cyan - Changes color to cyan.");
                                        Console.WriteLine("magenta - Changes color to magenta.");
                                        Console.WriteLine("black - Changes color to black.");
                                        Console.WriteLine("white or 'DEFAULT' arg - Changes color to white.");
                                    }
                                    else if (argTwo == "bcolor")
                                    {
                                        Console.WriteLine("Changes background color in the SamOS console.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("blue - Changes color to blue.");
                                        Console.WriteLine("red - Changes color to red.");
                                        Console.WriteLine("green - Changes color to green.");
                                        Console.WriteLine("gray - Changes color to gray.");
                                        Console.WriteLine("cyan - Changes color to cyan.");
                                        Console.WriteLine("magenta - Changes color to magenta.");
                                        Console.WriteLine("black or 'DEFAULT' arg - Changes color to black.");
                                        Console.WriteLine("white - Changes color to white.");
                                    }
                                    else if (argTwo == "math")
                                    {
                                        Console.WriteLine("Adds, subtracts, multipies, or divides numbers.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("add - Adds two numbers.");
                                        Console.WriteLine("sub - Subtracts two numbers.");
                                        Console.WriteLine("mul - Multiplies two numbers.");
                                        Console.WriteLine("div - Divides two numbers.");
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid argument, please type 'help' for more infomation.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("print"))
                                {
                                    var text = input.Remove(0, 6);
                                    Console.WriteLine(text);
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("shutdown"))
                                {
                                    var arg = input.Remove(0, 9);
                                    if (arg == "s" || string.IsNullOrWhiteSpace(arg))
                                    {
                                        Console.Clear();
                                        Console.Beep();
                                        Console.WriteLine("Shutting Down... ");
                                        System.Threading.Thread.Sleep(1);
                                        Cosmos.System.Power.Shutdown();
                                    }
                                    else if (arg == "r")
                                    {
                                        Console.Clear();
                                        Console.Beep();
                                        Console.WriteLine("Restarting... ");
                                        System.Threading.Thread.Sleep(1);
                                        Cosmos.System.Power.Reboot();
                                    }
                                    else if (arg == "?")
                                    {
                                        Console.WriteLine("Shuts down or restarts the computer.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("'s' or no arguments - Shuts down the computer.");
                                        Console.WriteLine("r - Restarts the computer.");
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Argument, please type 'help shutdown' to get help with args.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                else if (input == "clear" || input == "cls")
                                {
                                    Console.Clear();
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("pause"))
                                {
                                    var PauseArg = input.Remove(0, 6);
                                    if (PauseArg == "true" || string.IsNullOrWhiteSpace(PauseArg) || PauseArg == "True")
                                    {
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        commandhistory.Add(input);
                                    }
                                    else if (PauseArg == "false" || PauseArg == "False")
                                    {
                                        Console.ReadKey();
                                        commandhistory.Add(input);
                                    }
                                    else if (PauseArg == "?")
                                    {
                                        Console.WriteLine("Pauses the console for a specified amount of seconds.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("'true', 'True' or no arguments - Shows 'Press Any Key to Continue...'.");
                                        Console.WriteLine("'false' or 'False' - Hides 'Presss Any Key to Continue...'.");
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Argument, please type 'help pause' to get help with the args.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                else if (input.StartsWith("account"))
                                {
                                    var AccountArgs = input.Remove(0, 8);
                                    if (AccountArgs == "usr")
                                    {
                                        Console.WriteLine(username);
                                    }
                                    else if (AccountArgs == "psw")
                                    {
                                        Console.WriteLine(password);
                                    }
                                    else if (AccountArgs == "?")
                                    {
                                        Console.WriteLine("Displays Username and/or Password of a SamOS account.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("usr - Displays Username.");
                                        Console.WriteLine("psw - Displays Password.");
                                        Console.WriteLine("No Arguments - Displays both username and password.");
                                    }
                                    else if (string.IsNullOrWhiteSpace(AccountArgs))
                                    {
                                        Console.WriteLine("Your Username: " + username);
                                        Console.WriteLine("Your Password: " + password);
                                        commandhistory.Add(input);
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Argument, please type 'help account' to get help with the args.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                else if (input.StartsWith("sleep "))
                                {
                                    int number_of_secs = int.Parse(input.Remove(0, 5));
                                    System.Threading.Thread.Sleep(number_of_secs);
                                    commandhistory.Add(input);
                                }
                                else if (input == "history")
                                {
                                    if (!(commandhistory.Count == 0))
                                    {
                                        foreach (string command in commandhistory) { Console.WriteLine(command); }
                                    }
                                    else
                                    {
                                        Console.WriteLine("<EMPTY>");
                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input == "clearhistory")
                                {
                                    commandhistory.Clear();
                                    commandhistory.Add(input);
                                }
                                else if (input == "systeminfo")
                                {
                                    var amountOfRAM = GCImplementation.GetAvailableRAM();
                                    var UsedRAM = GCImplementation.GetUsedRAM();
                                    var NewUsedRAM = UsedRAM / 1024;
                                    var NetAllocated = HeapSmall.GetAllocatedObjectCount();
                                    Console.WriteLine("Amount of RAM: " + amountOfRAM + " MB");
                                    Console.WriteLine("Used RAM (in KB): " + NewUsedRAM + " KB");
                                    Console.WriteLine("Used RAM (in bytes): " + UsedRAM + " bytes");
                                    Console.WriteLine("Username: " + username);
                                    Console.WriteLine("Password: " + password);
                                    Console.WriteLine("OS Name: SamOS");
                                    Console.WriteLine("OS made by: Samdasti\\Windows123123MM");
                                    Console.WriteLine("PC Name: " + PCName);
                                    Console.WriteLine("Number of Allocated Objects: " + NetAllocated);
                                    Console.WriteLine("Session Name: " + SessionName);
                                    Console.WriteLine("OS made with: Cosmos OS C# (userkit), in visual studio 2022.");
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("prompt"))
                                {
                                    var new_prompt = input.Remove(0, 7);
                                    if (!(input == "> "))
                                    {
                                        old_prompt = new_prompt;
                                        commandhistory.Add(input);
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Entered command must be custom command, to use this prompt '> ', please type resetprompt instead.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        commandhistory.Add(input);
                                    }
                                }
                                else if (input == "resetprompt")
                                {
                                    old_prompt = "> ";
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("pcname"))
                                {
                                    PCName = input.Remove(0, 7);
                                    commandhistory.Add(input);
                                }
                                else if (input == "datetime")
                                {
                                    Console.WriteLine(DateTime.Now.ToString("yyyy/MM/ddd HH:m:ss"));
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("session"))
                                {
                                    var new_SessionName = input.Remove(0, 8);
                                    SessionName = new_SessionName;
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    commandhistory.Clear();
                                    Console.WriteLine("Starting New Session... ");
                                    Console.Beep();
                                    System.Threading.Thread.Sleep(1);
                                    Console.Clear();
                                    if (time.Month == 12 && time.Day == 25)
                                    {
                                        Console.WriteLine("Merry Christmas! Type 'help' to get help with commands HO!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Welcome to SamOS, to get infomation about commands, type 'help'.");
                                    }
                                    if (!(old_prompt == "> "))
                                    {
                                        old_prompt = "> ";
                                    }
                                    else
                                    {

                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("bcolor"))
                                {
                                    var BackgroundColorArg = input.Remove(0, 7);
                                    if (BackgroundColorArg == "blue")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                    }
                                    else if (BackgroundColorArg == "red")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Red;
                                    }
                                    else if (BackgroundColorArg == "green")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Green;
                                    }
                                    else if (BackgroundColorArg == "black" || BackgroundColorArg == "DEFAULT")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Black;
                                    }
                                    else if (BackgroundColorArg == "gray")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Gray;
                                    }
                                    else if (BackgroundColorArg == "white")
                                    {
                                        Console.BackgroundColor = ConsoleColor.White;
                                    }
                                    else if (BackgroundColorArg == "magenta")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Magenta;
                                    }
                                    else if (BackgroundColorArg == "cyan")
                                    {
                                        Console.BackgroundColor = ConsoleColor.Cyan;
                                    }
                                    else if (BackgroundColorArg == "?")
                                    {
                                        Console.WriteLine("Changes background color in the SamOS console.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("blue - Changes color to blue.");
                                        Console.WriteLine("red - Changes color to red.");
                                        Console.WriteLine("green - Changes color to green.");
                                        Console.WriteLine("gray - Changes color to gray.");
                                        Console.WriteLine("cyan - Changes color to cyan.");
                                        Console.WriteLine("magenta - Changes color to magenta.");
                                        Console.WriteLine("black or 'DEFAULT' arg - Changes color to black.");
                                        Console.WriteLine("white - Changes color to white.");
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Argument, please type 'help bcolor' to get help with args.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("tcolor"))
                                {
                                    var TextColorArg = input.Remove(0, 7);
                                    if (TextColorArg == "blue")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                    }
                                    else if (TextColorArg == "red")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                    else if (TextColorArg == "green")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                    }
                                    else if (TextColorArg == "black")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black;
                                    }
                                    else if (TextColorArg == "gray")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    else if (TextColorArg == "white" || TextColorArg == "DEFAULT")
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else if (TextColorArg == "magenta")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                    }
                                    else if (TextColorArg == "cyan")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                    }
                                    else if (TextColorArg == "?")
                                    {
                                        Console.WriteLine("Changes text color in the SamOS console.");
                                        Console.WriteLine("Parameters: ");
                                        Console.WriteLine("blue - Changes color to blue.");
                                        Console.WriteLine("red - Changes color to red.");
                                        Console.WriteLine("green - Changes color to green.");
                                        Console.WriteLine("gray - Changes color to gray.");
                                        Console.WriteLine("cyan - Changes color to cyan.");
                                        Console.WriteLine("magenta - Changes color to magenta.");
                                        Console.WriteLine("black - Changes color to black.");
                                        Console.WriteLine("white or 'DEFAULT' arg - Changes color to white.");
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Argument, please type 'help tcolor' to get help with args.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("invmsg"))
                                {
                                    var new_invmsg = input.Remove(0, 7);
                                    invalidcommand_msg = new_invmsg;
                                    commandhistory.Add(input);
                                }
                                else if (input == "resetinvmsg")
                                {
                                    invalidcommand_msg = "Invalid Command, to get help about valid commands, type 'help'.";
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("mulprint"))
                                {
                                    var statements = input.Remove(0, 9);
                                    var statementstoprint = statements.Split(",");
                                    foreach (string statement in statementstoprint)
                                    {
                                        Console.WriteLine(statement);
                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("math"))
                                {
                                    var value1 = input.Remove(0, 5);
                                    if (value1 == "add")
                                    {
                                        Console.Write("First Number: ");
                                        var num11 = Console.ReadLine();
                                        int num1_1 = int.Parse(num11);
                                        Console.Write("Second Number: ");
                                        var num21 = Console.ReadLine();
                                        int num2_1 = int.Parse(num21);
                                        var result = num1_1 + num2_1;
                                        Console.WriteLine(result);
                                    }
                                    else if (value1 == "sub")
                                    {
                                        Console.Write("First Number: ");
                                        var num12 = Console.ReadLine();
                                        int num1_2 = int.Parse(num12);
                                        Console.Write("Second Number: ");
                                        var num22 = Console.ReadLine();
                                        int num2_2 = int.Parse(num22);
                                        var result2 = num1_2 - num2_2;
                                        Console.WriteLine(result2);
                                    }
                                    else if (value1 == "mul")
                                    {
                                        Console.Write("First Number: ");
                                        var num13 = Console.ReadLine();
                                        int num1_3 = int.Parse(num13);
                                        Console.Write("Second Number: ");
                                        var num23 = Console.ReadLine();
                                        int num2_3 = int.Parse(num23);
                                        var result3 = num1_3 * num2_3;
                                        Console.WriteLine(result3);
                                    }
                                    else if (value1 == "div")
                                    {
                                        try
                                        {
                                            Console.Write("First Number: ");
                                            var num14 = Console.ReadLine();
                                            int num1_4 = int.Parse(num14);
                                            Console.Write("Second Number: ");
                                            var num24 = Console.ReadLine();
                                            int num2_4 = int.Parse(num24);
                                            var result4 = num1_4 / num2_4;
                                            Console.WriteLine(result4);
                                        }
                                        catch (DivideByZeroException)
                                        {
                                            Console.WriteLine("Cannot divide by Zero.");
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Invalid Argument, to get help with args, type 'help math'.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    commandhistory.Add(input);
                                }
                                else if (input.StartsWith("rprint"))
                                {
                                    string rev = input.Remove(0, 6);
                                    char[] revcharacters = rev.ToCharArray();
                                    Array.Reverse(revcharacters);
                                    string revstr = new string(revcharacters);
                                    Console.WriteLine(revcharacters);
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(invalidcommand_msg);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    commandhistory.Add(input);
                                }
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.Beep();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wrong Password, Rebooting... ");
                            Console.ForegroundColor = ConsoleColor.White;
                            System.Threading.Thread.Sleep(1);
                            Console.Clear();
                            Console.Beep();
                            Cosmos.System.Power.Reboot();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.Beep();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Username, Rebooting... ");
                        Console.ForegroundColor = ConsoleColor.White;
                        System.Threading.Thread.Sleep(1);
                        Console.Clear();
                        Console.Beep();
                        Cosmos.System.Power.Reboot();
                    }

                }
                catch (Exception e)
                {
                    Console.Beep();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exception Occured: " + e.ToString());
                    Console.WriteLine("Rebooting...");
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Threading.Thread.Sleep(1);
                    Console.Beep();
                    Cosmos.System.Power.Reboot();
                }
            }
            else if (MenuInput == "1")
            {
                Console.Clear();
                Console.Beep();
                Console.WriteLine("Shutting Down... ");
                System.Threading.Thread.Sleep(1);
                Cosmos.System.Power.Shutdown();
            }
            else if (MenuInput == "2")
            {
                Console.Clear();
                Console.Beep();
                Console.WriteLine("Restarting... ");
                System.Threading.Thread.Sleep(1);
                Cosmos.System.Power.Reboot();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Argument, rebooting...");
                Console.ForegroundColor = ConsoleColor.White;
                System.Threading.Thread.Sleep(1);
                Cosmos.System.Power.Reboot();
            }
        }
    }
}
