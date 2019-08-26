using System;
using System.Collections.Generic;
using System.IO;

namespace LinearSystemCalculator
{
    public static class Menu
    {
        public static void RunMenu()
        {
            uint key;
            try
            {
                Console.WriteLine("Hello! This program is designed as a calculator for linear system. This program uses direct methods such as Gauss and Cramer's method." +
                    "To start using this program you have to enter linear system of equations. Please follow instractions!");
                LinearSystem system = LinearSystemOperator.Read();
                do
                {
                    Console.WriteLine("Press 1 to show linear system.");
                    Console.WriteLine("Press 2 to find results using Gauss Method with main element");
                    Console.WriteLine("Press 3 to find results using Gauss Method with unit diagonal");
                    Console.WriteLine("Press 4 to find results using Cramer's Method");
                    Console.WriteLine("Press 5 to enter new linear system");
                    Console.WriteLine("Press 0 to exit program");
                    Console.WriteLine("Enter key: ");
                    key = Convert.ToUInt32(Console.ReadLine());

                    switch (key)
                    {
                        case 1:
                            {
                                LinearSystemOperator.Show(system);
                                break;
                            }
                        case 2:
                            {
                                LinearSystemOperator.Show(system);
                                MainElementGaussMethod gaussMethod = new MainElementGaussMethod();
                                List<double> results = gaussMethod.FinalCalculations(system);
                                if (results != null)
                                {
                                    List<double> error = gaussMethod.R(system, results);
                                    Console.WriteLine("Please, enter 1 if you want to save results to file, press enter to go back to menu: ");
                                    if (Int32.TryParse(Console.ReadLine(), out int saveKey) && saveKey == 1)
                                    {
                                        Console.WriteLine("Please, enter name of file: ");
                                        string fileName = Console.ReadLine();
                                        using (StreamWriter w = new StreamWriter(fileName))
                                        {
                                            gaussMethod.OutputResultsToFile(w, system, results, error);
                                        }

                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                LinearSystemOperator.Show(system);
                                var gaussMethod = new UnitDiagonalGaussMethod();
                                List<double> results = gaussMethod.FinalCalculations(system);
                                if (results != null)
                                {
                                    List<double> error = gaussMethod.R(system, results);
                                    Console.WriteLine("Please, enter 1 if you want to save results to file, press enter to go back to menu: ");
                                    if (Int32.TryParse(Console.ReadLine(), out int saveKey) && saveKey == 1)
                                    {
                                        Console.WriteLine("Please, enter name of file: ");
                                        string fileName = Console.ReadLine();
                                        using (StreamWriter w = new StreamWriter(fileName))
                                        {
                                            gaussMethod.OutputResultsToFile(w, system, results, error);
                                        }
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                LinearSystemOperator.Show(system);
                                var method = new CramersMethod();
                                List<double> results = method.FinalCalculations(system);
                                if (results != null)
                                {
                                    List<double> error = method.R(system, results);
                                    Console.WriteLine("Please, enter 1 if you want to save results to file, press enter to go back to menu: ");
                                    if (Int32.TryParse(Console.ReadLine(), out int saveKey) && saveKey == 1)
                                    {
                                        Console.WriteLine("Please, enter name of file: ");
                                        string fileName = Console.ReadLine();
                                        using (StreamWriter w = new StreamWriter(fileName))
                                        {
                                            method.OutputResultsToFile(w, system, results, error);
                                        }
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                system = LinearSystemOperator.Read();
                                break;
                            }
                        case 0:
                            break;
                    }
                } while (key != 0);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You entered inappropriate value, the program will restart now, please follow instructions.");
                Menu.RunMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured during while program was running, please follow instructions");
                Console.WriteLine($"{ ex.GetType()} says { ex.Message}");
                Menu.RunMenu();
            }
        }
    }
}
