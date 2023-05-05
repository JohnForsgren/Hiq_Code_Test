using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiQ_Code_Test
{   


    /// <summary>
    /// Manages the input from the user; Asks for the input, validates it, and returns a DTO. 
    /// </summary>
    public class Input
    {
        private int xMax = 0; 
        private int yMax = 0;

        /// <summary>
        /// Checks if the given input is valid for the given InputType. 
        /// </summary>
        public bool IsInputValid(string InputType, string input)
        {
            switch (InputType)
            {
                case "BoardSize":
                    if (Regex.IsMatch(input, @"^\d+\s\d+$")) // e.g "2 6"
                    {
                        string[] numbers = input.Split(' ');
                        xMax = int.Parse(numbers[0]);
                        yMax = int.Parse(numbers[1]);
                        return true; 
                    }
                    
                    return false; 
                    
                case "PosAndRotation":
                    if (Regex.IsMatch(input, @"^\d+\s\d+\s[NSEW]$")) // e.g "2 6 N"
                    {
                        string[] numbers = input.Split(' ');
                        bool isOutsideBonds = int.Parse(numbers[0]) > xMax || int.Parse(numbers[1]) > yMax; 

                        if(isOutsideBonds)
                        {   
                            Console.WriteLine("Input was not inside of bonds.");
                            return false; 
                        }
                        return true; 
                    }
                    return false; 
                case "Commands":
                    return Regex.IsMatch(input, @"^[FBLR]+$"); // e.g "FFLRB" 
            }
            throw new Exception("No input message was assigned.");

         
        }

        public static string InputMessage(string InputType)
        {
            switch (InputType)
            {
                case "BoardSize":
                    return "Please enter the board size (two numbers separated with a space):"; 
                case "PosAndRotation":
                    return "Please enter the starting coordinates (two numbers followed by a letter [N, W, E, S], separated with one space each):";
                case "Commands":
                    return "Please enter the commands to execute (a string of characters [F, B, L, R] without space):";
            }
            throw new Exception("No input message was assigned.");

        }

        public InputData AskForInput() 
        {
            var inputdata = new InputData(); 

            inputdata.BoardSize = AskForInput("BoardSize");
            inputdata.PosAndRotation = AskForInput("PosAndRotation");
            inputdata.Commands = AskForInput("Commands");

            return inputdata; 
            
        }


        /// <summary>
        /// Asks the user for input of a specific type (e.g board size) until input with the correct format is provided. 
        /// </summary>
        public string AskForInput(string InputType)  
        {
            var success = false;
            var input = ""; 
            while (!success)
            {
                Console.Write(InputMessage(InputType));

                input = Console.ReadLine();
                input = input.ToUpper();

                if (IsInputValid(InputType, input))
                {
                    success = true;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            return input; 
        }
    }
}
