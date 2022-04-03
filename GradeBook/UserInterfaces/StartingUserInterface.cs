﻿using GradeBook.GradeBooks;
using System;

namespace GradeBook.UserInterfaces
{
    public static class StartingUserInterface
    {
        public static bool Quit = false;
        public static void CommandLoop()
        {
            while (!Quit)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(">> What would you like to do?");
                var command = Console.ReadLine().ToLower();
                CommandRoute(command);
            }
        }

        public static void CommandRoute(string command)
        {
            if (command.StartsWith("create"))
                CreateCommand(command);
            else if (command.StartsWith("load"))
                LoadCommand(command);
            else if (command == "help")
                HelpCommand();
            else if (command == "quit")
                Quit = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }

        public static BaseGradeBook CreateCommand(string command)
        {
            var parts = command.Split(' ');
            
                if (parts.Length != 4)
                {
                   
                    Console.WriteLine("Command not valid, Create requires a name, if it's weighted (true / false).");
                return null;
                }
            var name = parts[1];

            bool isWeight = Convert.ToBoolean(parts[3]);

            StandardGradeBook gradeBook = new StandardGradeBook(name, isWeight);

            Console.WriteLine("Created gradebook {0}.", name);

            GradeBookUserInterface.CommandLoop(gradeBook);



            if (parts[2] == "standard")
            {
                return new StandardGradeBook(name, isWeight);
            }
            if (parts[2] == "ranked")
            {
                return new RankedGradeBook(name, isWeight);
            }
            if (parts[2] != "standard" && parts[2] != "ranked")
            {
                parts[2] = "is not a supported type od gradeboook, please try again";
            }
            return null;
        }

        public static void LoadCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Command not valid, Load requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = BaseGradeBook.Load(name);

            if (gradeBook == null)
                return;

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void HelpCommand()
        {
            Console.WriteLine();
            Console.WriteLine("GradeBook accepts the following commands:");
            Console.WriteLine();
            Console.WriteLine("Create 'Name' 'Type' - Creates a new gradebook where 'Name' is the name of the gradebook and 'Type' is what type of grading it should use.");
            Console.WriteLine();
            Console.WriteLine("Load 'Name' - Loads the gradebook with the provided 'Name'.");
            Console.WriteLine();
            Console.WriteLine("Help - Displays all accepted commands.");
            Console.WriteLine();
            Console.WriteLine("Quit - Exits the application");
        }
    }
}
