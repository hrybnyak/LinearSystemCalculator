using System;
using System.Collections.Generic;
using System.IO;

namespace LinearSystemCalculator
{
    internal static class LinearSystemOperator //class to do linear system operations such as read, output to the screen or file
    {
        public static LinearSystem Read() //class to input linear system from a file
        {
            try
            {
                int n = 0;
                int m = 0;
                List<List<double>> A = new List<List<double>>();
                List<double> B = new List<double>();
                Console.WriteLine("Please, enter number of unknowns in your linear system:");
                if (Int32.TryParse(Console.ReadLine(), out n))
                {
                    Console.WriteLine("Please, enter number of equations in your linear system:");
                    if (Int32.TryParse(Console.ReadLine(), out m))
                    {
                        Console.WriteLine("Please, start entering coefficients, after you see '=' enter constant term:");
                        for (int i = 0; i < n; i++)
                        {
                            double temp = 0.0;
                            int consolePosition = 0;
                            var row = new List<double>();
                            if (Double.TryParse(Console.ReadLine(), out temp))
                            {
                                row.Add(temp);
                                Console.CursorTop--;
                                consolePosition += (temp.ToString()).Length;
                                Console.CursorLeft = consolePosition;
                                Console.Write($" * x1 + ");
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                            for (int j = 1; j < m; j++)
                            {

                                if (Double.TryParse(Console.ReadLine(), out temp))
                                {
                                    row.Add(temp);
                                    Console.CursorTop--;
                                    consolePosition += (temp.ToString()).Length + 8;
                                    Console.CursorLeft = consolePosition;
                                    if (j + 1 == m)
                                    {
                                        Console.Write($" * x{j + 1} = ");
                                    }
                                    else
                                    {
                                        Console.Write($" * x{j + 1} + ");
                                    }
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                            if (Double.TryParse(Console.ReadLine(), out temp))
                            {
                                B.Add(temp);
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                            A.Add(row);
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
                LinearSystem system = new LinearSystem(m, n, A, B);
                return system;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You entered inappropriate value, please follow the instructions!");
                LinearSystem system = Read();
                return system;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ ex.GetType()} says { ex.Message}");
                Console.ReadLine();
                LinearSystem system = Read();
                return system;
            }
        }
        //class to output linear system to the screen
        public static void Show(LinearSystem system)
        {
            for (int i = 0; i < system.NumberOfEquations; i++)
            {
                Console.Write($"{system.Coefficients[i][0]} * x1 ");
                for (int j = 1; j < system.NumberOfUnknowns; j++)
                {
                    Console.Write($"+ {system.Coefficients[i][j]} * x{j + 1} ");
                }
                Console.WriteLine($"= {system.ConstantTerms[i]}");
            }
        }
        public static void FileOutput(LinearSystem system, TextWriter w)
        {
            try
            {
                for (int i = 0; i < system.NumberOfEquations; i++)
                {
                    w.Write($"{system.Coefficients[i][0]} * x1 ");
                    for (int j = 1; j < system.NumberOfUnknowns; j++)
                    {
                        w.Write($"+ {system.Coefficients[i][j]} * x{j + 1} ");
                    }
                    w.WriteLine($"= {system.ConstantTerms[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ ex.GetType()} says { ex.Message}");
                Console.ReadLine();
            }
        }
    }

}
