using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HiQ_Code_Test
{   
    /// <summary>
    /// Validates if the input data results in a crash or success. 
    /// </summary>
    public class Validator
    {

        private int currentX;
        private int currentY;
        private char currentDirection;

        private int xMax;
        private int yMax;
        private char[] commands;
        private Dictionary<char, int[]> directionMap = new Dictionary<char, int[]>()
        {
            { 'N', new int[] { 0, 1 } },
            { 'E', new int[] { 1, 0 } },
            { 'S', new int[] { 0, -1 } },
            { 'W', new int[] { -1, 0 } }
        };

        List<char> directions = new List<char>() { 'N', 'E', 'S', 'W' }; 

        public Validator(InputData inputData) // Assigns default parameters
        {
            // = Board setup = 
            string[] boardSize = inputData.BoardSize.Split(' ');
            xMax = int.Parse(boardSize[0]);
            yMax = int.Parse(boardSize[1]); 

            // = Start Coordinates and rotation = 
            string[] startData = inputData.PosAndRotation.Split(' ');
            currentX = int.Parse(startData[0]);
            currentY = int.Parse(startData[1]);
            currentDirection = startData[2][0]; 

            // = CommandString = 
            commands = inputData.Commands.ToCharArray();

            Validate(); 

        }


        /// <summary>
        /// Runs a complete validation to see if the car crashes or not, based on all given input data. 
        /// </summary>
        public bool Validate()
        {       
            foreach (char command in commands)
            {

                switch (command)
                {
                    case 'F':
                        currentX += directionMap[currentDirection][0];
                        currentY += directionMap[currentDirection][1];
                        break;

                    case 'B':
                        currentX -= directionMap[currentDirection][0];
                        currentY -= directionMap[currentDirection][1];
                        break;

                    case 'R':
                    case 'L':
                        Rotate(command); 
                        break;
                
                }
                if (!IsInBounds()) // This is put BEFORE the loop to check if the car crashes before collision 
                {
                    currentX = Math.Max(0, Math.Min(currentX, xMax));
                    currentY = Math.Max(0, Math.Min(currentY, yMax));
                    Console.WriteLine($"Unsuccessful run. The car crashed into a wall at coordinates: {currentX}, {currentY}");
                    return false;
                }
            }
            Console.WriteLine($"Success! Final coordinates: {currentX}, {currentY}");

            return true;
        }

        public bool IsInBounds() // Checks if the car currently is in bound. 
        {
            if (currentX < 0 || currentX > xMax) return false;
            if (currentY < 0 || currentY > yMax) return false;
            return true; 
        }

        public void Rotate(char Direction) // Changes the current rotation based on the input rotation (R or L)
        {
            int currentDirectionIndex = directions.IndexOf(currentDirection);  

            int newDirectionIndex = Direction == 'R' ? currentDirectionIndex + 1 : currentDirectionIndex - 1;  

            if (newDirectionIndex < 0) newDirectionIndex = directions.Count - 1;  
            else if (newDirectionIndex >= directions.Count) newDirectionIndex = 0;
   
            currentDirection = directions[newDirectionIndex]; 

        }


    }
}
