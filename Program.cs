using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Triangle Calculator
//
// Authour: Lee Burdett
// Created: 13.09.2019
// Updated: 13.09.2019
//
// Purpose: Takes input of the 3 side lengths of a triangle in metres (m), and
//          outputs the resultant area of the triangle in metres squared (m^2)
//
// Notes:   The commonly taught formula for calculating the area of a triangle is:
//
//                                      A = b*h
//
//          Where 'A' is the area of the triangle (in m^2), 'b' the length of the 
//          longest side (m) and 'h' the height of the triangle when the longest 
//          side is placed horizontally at the bottom. 
//
//          For an input of 3 side lengths, though, it isn't immediately obvious
//          what the value of 'h' is without some more calculations.
//          The method is useful when calculating on paper with a diagram, but I 
//          would suggest for the purposes of a computer program, an alternative
//          method be used.
//          
//          Heron's formula is the perfect solution. It gives the area of a triangle
//          based on side length only, with only one intermediate calculation.
//          
//                                  S = (a+b+c) / 2      
//
//                           A = Sqrt( S(S-a)(S-b)(S-c) )
//          
//          Where 'S' is half the perimeter of the triangle (m), 'A' is the area of the 
//          triangle (m^2), and a, b and c are the 3 side lengths of the triangle (m).
//
//          For this reason, I decided to use Heron's formula for this task.

namespace TriangleCalculator
{
    class Program
    {
        static bool reset; // Used to check whether program should be reset
        static void Main(string[] args)
        {
            reset = false; // Checks whether the program has been reset

            while (reset == false) // Runs while reset and error are false
            {
                // Variables to store side length values input by user for the 3 sides of a triangle (A, B and C)
                double sideA = 0;
                double sideB = 0;
                double sideC = 0;

                // Variable to store the area calculated using the side lengths.
                double Area = 0;

                // Prints Title, Description, and a break line
                Console.WriteLine("Triangle Calculator");
                Console.WriteLine("");
                Console.WriteLine("This program uses Heron's formula to calculate the area of a triangle given the length of all 3 sides");
                Console.WriteLine("");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");

                // Prints first instruction, for the user to input the length of a side
                Console.WriteLine("Please type the length of one of your triangle's sides (in metres) and press Enter.");
                                              
                // ProcessInput Converts user's input to double, displays as "Side a = %sideA%m", 
                // and stores sideA double for area calculation later.
                sideA = ProcessInput("a");
                if (reset == true) { break; } // Checks if error has occured

                // Prints second instruction, for the user to pick another side and input its length
                Console.WriteLine("Great! Now input the length (in metres) of another side.");
                

                sideB = ProcessInput("b"); // Performs ProcessInput for side "b"
                if (reset == true) { break; } // Checks if error has occured

                // Prints final instruction for the remaining side
                Console.WriteLine("Almost there! You just need to input the length of the remaining side (in metres)");
                

                sideC = ProcessInput("c"); // Performs ProcessInput for side "c"
                if (reset == true) { break; } // Checks if error has occured

                // Prints message about Heron's formula explaining what the program will do with the user inputs.
                Console.WriteLine("Perfect! Now the program will calculate the area of a triangle using Heron's formula.");
                Console.WriteLine("For reference, Heron's formula is shown below:");
                Console.WriteLine("");
                Console.WriteLine("     S = (a+b+c) / 2             (S is half the perimeter of the triangle (m)");
                Console.WriteLine("");
                Console.WriteLine("A = Sqrt( S(S-a)(S-b)(S-c) )     (A is the area of the triangle (m^2)");
                Console.WriteLine("");
                Console.WriteLine("Where a, b and c are the three side lengths of the triangle in metres");
                Console.WriteLine("");
                Console.WriteLine("Press Enter to reveal result...");

                Console.ReadLine(); // Waits for key press to reveal result
                Area = HeronsMethod(sideA, sideB, sideC); // Calculates Area of triangle using Heron's method

                if (Area.ToString() == "NaN" || Area.ToString() == "0") // Checks if the triangle of side lengths a,b,c is possible
                {
                    // Error Message
                    Console.WriteLine("----------------------------------------------------------------------------------------");
                    Console.WriteLine("This is an impossible triangle, check your measurements and try again by pressing Enter.");
                    Console.WriteLine("----------------------------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("The area of your triangle is " + Area.ToString() + "m^2"); // Outputs Area of triangle in m^2
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("To calculate the area of another triangle, press Enter"); // Instruction on how to reset.
                }

                Console.ReadLine(); // Waits for user to press Enter
                reset = true; // Sets program to reset
            }

            Console.Clear(); // Clears console
            Main(args); // Restarts Main()
        }

        static double ProcessInput(string side)
        {
            // ProcessInput takes the user's input on the current line, converts it to double, prints the input value
            // for the user to see which side they have input and its value, and then returns the side length as a double.

            int line = 0; // Stores which line of the console the output text will be written on
            double SideLength = 0; // Stores input side string converted to double

            try
            {
                SideLength = Convert.ToDouble(Console.ReadLine()); // Converts Side string to double and stores as SideLength

                switch (side)
                {
                    case "a": // side a
                        line = 5; // assigned to overwrite line 5
                        break;
                    case "b": // side b
                        line = 6; // assigned to overwrite line 6
                        break;
                    case "c": // side c
                        line = 7; // assigned to overwrite line 7
                        break;
                }

                // This section of code moves the cursor to the assigned line, overwrites the text in that line with the value that
                // the user input, then deletes the instruction that was given.

                Console.SetCursorPosition(0, line); // Sets cursor position to the assigned line

                // If it is the input for side a, of example, 10 metres, it will display as "Side a = 10m"
                Console.Write("Side " + side + " = " + SideLength.ToString() + "m" + new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, line + 1); // Sets cursor position to one below the assigned line (where the instruction is)
                Console.Write(new string(' ', Console.WindowWidth)); // Replaces instruction with blank space characters

                return SideLength; // Returns SideLength as a double
            }

            catch // In case error occurs due to invalid user input
            {
                Console.WriteLine("Input must be a number, press Enter to try again."); // Error message
                reset = true; // Sets program to reset
                SideLength = 0; // Sets side length to default value
                Console.ReadLine(); // Waits for user to press Enter
                return SideLength; // returns sidelength
            }
        }

        static double HeronsMethod(double sideA, double sideB, double sideC)
        {
            // Calculates the Area of a triangle using Heron's Method given 3 triangle side lengths

            double HalfPerimeter = (sideA + sideB + sideC) / 2; // Calculates half of the perimeter of the triangle

            // A = Sqrt( S(S-a)(S-b)(S-c) ): A = Area, S = half perimeter, a,b and c are the triangle side lengths.
            double Area = Math.Sqrt(HalfPerimeter * ((HalfPerimeter - sideA) * (HalfPerimeter - sideB) * (HalfPerimeter - sideC)));
            return Area; // Returns the area value
        }
    }
}
