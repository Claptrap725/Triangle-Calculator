using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle_Calculator
{
    class Program
    {
        static void Main()
        {
            bool gameRunning = true;

            while (gameRunning)
            {
                //variables that store the lengths of legs and the measeurement of angles in radians
                double legA = 0, legB = 0, legC = 0, angleA = 0, angleB = 0, angleC = 0;
                Console.WriteLine("\n\n\nPlease give us all the information you have about your triangle.");
                Console.WriteLine("If you don't know a specific side or angle, do not enter any number.");

                //ask for known sides
                Console.WriteLine("\nEnter the value of Leg A.");
                legA = GetPositiveNumberInput();
                Console.WriteLine("\nEnter the value of Leg B.");
                legB = GetPositiveNumberInput();
                Console.WriteLine("\nEnter the value of Leg C (This is the hypotenuse for right triangles).");
                legC = GetPositiveNumberInput();

                //ask for known angles
                Console.WriteLine("\nEnter the value of Angle A (Oposite of Leg A).");
                angleA = GetAngleInput();
                Console.WriteLine("\nEnter the value of Angle B (Oposite of Leg B).");
                angleB = GetAngleInput();
                Console.WriteLine("\nEnter the value of Angle C (Oposite of Leg C. This is the 90 degree angle for right triangles).");
                angleC = GetAngleInput();

                //Round angles to the nearest hundreth of a degree
                //angleA = Math.Round(angleA * 100) / 100;
                //angleB = Math.Round(angleB * 100) / 100;
                //angleC = Math.Round(angleC * 100) / 100;


                //Calculate the unknown angles and sides
                bool triangleCalculated = false;
                while (!triangleCalculated)
                {
                    bool didSomethingThisPass = false;
                    //angles inside a triangle always add up to 180 degrees
                    if (angleA != 0 && angleB != 0)
                    {
                        angleC = Math.PI - angleA - angleB;
                        didSomethingThisPass = true;
                    }
                    else if (angleA != 0 && angleC != 0)
                    {
                        angleB = Math.PI - angleA - angleC;
                        didSomethingThisPass = true;
                    }
                    else if (angleB != 0 && angleC != 0)
                    {
                        angleA = Math.PI - angleB - angleC;
                        didSomethingThisPass = true;
                    }


                    //The triangle is a right triangle
                    if (angleC == DegToRad(90))
                    {
                        if (legA != 0 && legB != 0 && legC == 0)//we know legA and legB
                        {
                            legC = Math.Sqrt(legA * legA + legB * legB);
                            if (angleA == 0) angleA = Math.Asin(legA / legC);
                            if (angleB == 0) angleB = Math.Asin(legB / legC);
                            triangleCalculated = true;
                            didSomethingThisPass = true;
                        }
                        else if (legA != 0 && legC != 0 && legB == 0)//we know legA and legC
                        {
                            legB = Math.Sqrt(legC * legC - legA * legA);
                            if (angleA == 0) angleA = Math.Asin(legA / legC);
                            if (angleB == 0) angleB = Math.Asin(legB / legC);
                            triangleCalculated = true;
                            didSomethingThisPass = true;
                        }
                        else if (legB != 0 && legC != 0 && legA == 0)//we know legB and legC
                        {
                            legA = Math.Sqrt(legC * legC - legB * legB);
                            if (angleA == 0) angleA = Math.Asin(legA / legC);
                            if (angleB == 0) angleB = Math.Asin(legB / legC);
                            triangleCalculated = true;
                            didSomethingThisPass = true;
                        }
                        else if (legC != 0 && angleA != 0)//we know the hypotenuse and one angle
                        {
                            legA = legC * Math.Sin(angleA);
                            didSomethingThisPass = true;
                        }
                        else if (legC != 0 && angleB != 0)//we know the hypotenuse and one angle
                        {
                            legB = legC * Math.Sin(angleB);
                            didSomethingThisPass = true;
                        }
                        else if (legA != 0 && angleA != 0)//we know one side and the angle opposite to it
                        {
                            legC = legA / Math.Sin(angleA);
                            didSomethingThisPass = true;
                        }
                        else if (legB != 0 && angleB != 0)//we know one side and the angle opposite to it
                        {
                            legC = legB / Math.Sin(angleB);
                            didSomethingThisPass = true;
                        }
                        else if (legA != 0 && angleB != 0)//we know a leg and the ange adjacent to it
                        {
                            legC = legA / Math.Cos(angleB);
                            didSomethingThisPass = true;
                        }
                        else if (legB != 0 && angleA != 0)//we know a leg and the ange adjacent to it
                        {
                            legC = legB / Math.Cos(angleA);
                            didSomethingThisPass = true;
                        }

                    }
                    //The triangle is not a right triangle
                    else
                    {
                        //Complete Triangle
                        if (angleA != 0 && angleB != 0 && angleC != 0 && legA != 0 && legB != 0 && legC != 0)
                        {
                            if (double.IsNaN(angleA) || double.IsNaN(angleB) || double.IsNaN(angleC) || double.IsNaN(legA) || double.IsNaN(legB) || double.IsNaN(legC))
                            {
                                Console.WriteLine("\n\n\n\nThe triangle you provided is not real and cannot be solved.\nTry again.");
                                break;
                            }
                            triangleCalculated = true;
                            break;
                        }

                        //SSS Triangle
                        else if (legA != 0 && legB != 0 && legC != 0)
                        {
                            if (angleA == 0) angleA = Math.Acos((legB * legB + legC * legC - legA * legA) / (2 * legB * legC));
                            if (angleB == 0) angleB = Math.Acos((legA * legA + legC * legC - legB * legB) / (2 * legA * legC));
                            if (angleC == 0) angleC = Math.PI - angleA - angleB;
                            didSomethingThisPass = true;
                        }

                        //AAA Triangle
                        //These can't be solved any further since we have reference for size

                        //AAS Triangle
                        else if (angleA != 0 && angleC != 0 && legA != 0 && legC == 0)
                        {
                            legC = legA / Math.Sin(angleA) * Math.Sin(angleC);
                            didSomethingThisPass = true;
                        }
                        else if (angleA != 0 && angleC != 0 && legC != 0 && legA == 0)
                        {
                            legA = legC / Math.Sin(angleC) * Math.Sin(angleA);
                            didSomethingThisPass = true;
                        }
                        else if (angleC != 0 && angleB != 0 && legC != 0 && legB == 0)
                        {
                            legB = legC / Math.Sin(angleC) * Math.Sin(angleB);
                            didSomethingThisPass = true;
                        }
                        else if (angleC != 0 && angleB != 0 && legB != 0 && legC == 0)
                        {
                            legC = legB / Math.Sin(angleB) * Math.Sin(angleC);
                            didSomethingThisPass = true;
                        }
                        else if (angleB != 0 && angleA != 0 && legB != 0 && legA == 0)
                        {
                            legA = legB / Math.Sin(angleB) * Math.Sin(angleA);
                            didSomethingThisPass = true;
                        }
                        else if (angleB != 0 && angleA != 0 && legA != 0 && legB == 0)
                        {
                            legB = legA / Math.Sin(angleA) * Math.Sin(angleB);
                            didSomethingThisPass = true;
                        }

                        //SAS Triangle
                        else if (legA != 0 && legB != 0 && angleC != 0)
                        {
                            legC = Math.Sqrt(legA * legA + legB * legB - 2 * legA * legB * Math.Cos(angleC));
                            didSomethingThisPass = true;
                        }
                        else if (legB != 0 && legC != 0 && angleA != 0)
                        {
                            legA = Math.Sqrt(legB * legB + legC * legC - 2 * legB * legC * Math.Cos(angleA));
                            didSomethingThisPass = true;
                        }
                        else if (legC != 0 && legA != 0 && angleB != 0)
                        {
                            legB = Math.Sqrt(legA * legA + legC * legC - 2 * legA * legC * Math.Cos(angleB));
                            didSomethingThisPass = true;
                        }

                        //SSA Triangle
                        else if (legA != 0 && legB != 0 && angleA != 0)
                        {
                            angleB = Math.Asin(legB * Math.Sin(angleA) / legA);
                            didSomethingThisPass = true;
                        }
                        else if (legA != 0 && legB != 0 && angleB != 0)
                        {
                            angleA = Math.Asin(legA * Math.Sin(angleB) / legB);
                            didSomethingThisPass = true;
                        }
                        else if (legB != 0 && legC != 0 && angleB != 0)
                        {
                            angleC = Math.Asin(legC * Math.Sin(angleB) / legB);
                            didSomethingThisPass = true;
                        }
                        else if (legB != 0 && legC != 0 && angleC != 0)
                        {
                            angleB = Math.Asin(legB * Math.Sin(angleC) / legC);
                            didSomethingThisPass = true;
                        }
                        else if (legA != 0 && legC != 0 && angleA != 0)
                        {
                            angleC = Math.Asin(legC * Math.Sin(angleA) / legA);
                            didSomethingThisPass = true;
                        }
                        else if (legA != 0 && legC != 0 && angleC != 0)
                        {
                            angleA = Math.Asin(legA * Math.Sin(angleC) / legC);
                            didSomethingThisPass = true;
                        }

                    }

                    //the triangle can't be solved given the information
                    if (!didSomethingThisPass)
                    {
                        Console.WriteLine("\n\n\n\nYou didn't provide enough information to calculate a triangle with that information.\nTry again.");
                        break;
                    }
                }

                if (triangleCalculated)
                {
                    PrintTriangle(legA, legB, legC, angleA, angleB, angleC);
                }
                Console.ReadLine();
            }
        }

        static string GetTextInput()
        {
            Console.WriteLine("Enter text.");
            string text = Console.ReadLine();
            return text;
        }

        static double GetNumberInput()
        {
            double num = double.MinValue;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Enter a decimal number.");
                string text = Console.ReadLine();
                double.TryParse(text, out num);
                if (num == double.MinValue)
                {
                    Console.WriteLine("\n Inval Input. Try again.");
                }
                else
                {
                    validInput = true;
                }
            }
            
            return num;
        }

        static double GetPositiveNumberInput()
        {
            double num = double.MinValue;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Enter a decimal number.");
                string text = Console.ReadLine();
                double.TryParse(text, out num);
                if (num == double.MinValue)
                {
                    Console.WriteLine("\n Inval Input. Try again.");
                }
                else if (num < 0)
                {
                    Console.WriteLine("\n Input cannot be Negative. Try again.");
                }
                else 
                {
                    validInput = true;
                }
            }

            return num;
        }

        static double GetAngleInput()
        {
            Console.WriteLine("Is your angle in Radians or degrees?");
            while (true)
            {
                Console.WriteLine("Enter \"R\" for radians or \"D\" for degrees.");
                string text = Console.ReadLine().ToLower();
                if (text == "r")
                {
                    return GetPositiveNumberInput();
                }
                else if (text == "d")
                {
                    return DegToRad(GetPositiveNumberInput());
                }
                else if (text == "0" || text == "")
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try again.");
                }
            }
        }

        static double DegToRad(double deg)
        {
            return (deg * Math.PI / 180);
        }

        static double RadToDeg(double rad)
        {
            return (rad * 180 / Math.PI);
        }

        static void PrintTriangle(double legA, double legB, double legC, double angleA, double angleB, double angleC)
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n---------------------------------------------------------------------\n");
            Console.WriteLine("Here's what I figured out about your triangle!");
            Console.WriteLine($"Leg A: {legA}.");
            Console.WriteLine($"Leg B: {legB}.");
            Console.WriteLine($"Leg C: {legC}.");
            Console.WriteLine($"Angle A: {RadToDeg(angleA)} Degrees or {angleA} Radians.");
            Console.WriteLine($"Angle B: {RadToDeg(angleB)} Degrees or {angleB} Radians.");
            Console.WriteLine($"Angle C: {RadToDeg(angleC)} Degrees or {angleC} Radians.");
        }
    }
}
