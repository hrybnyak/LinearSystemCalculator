using System;
using System.Collections.Generic;
using System.IO;

namespace LinearSystemCalculator
{
    internal abstract class DirectMethod //class that represents all direct methods to find unknowns in linear system
    {
        abstract internal List<double> FinalCalculations(LinearSystem system); //method that does final calculations to find unknowns
        abstract internal bool Check(LinearSystem system);
        protected internal List<double> R(LinearSystem system, List<double> results) //method to calculate error //b-A*x
        {
            List<double> r = new List<double>();
            List<double> xA = new List<double>();
            Console.WriteLine(" ");
            for (int i = 0; i < system.NumberOfUnknowns; i++)
            {
                xA.Add(0.0);
                r.Add(0.0);
                for (int j = 0; j < system.NumberOfUnknowns; j++)
                {
                    xA[i] += system.Coefficients[i][j] * results[j];
                }
                r[i] = system.ConstantTerms[i] - xA[i];
                Console.WriteLine($"Error for x{i + 1} = {r[i]}");
            }
            return r;
        }
        internal void OutputResultsToFile(TextWriter w, LinearSystem system, List<double> results, List<double> r) //method to output final results to a file
        {
            w.WriteLine("System: ");
            LinearSystemOperator.FileOutput(system, w);
            w.WriteLine("Results: ");
            for (int i = 0; i < system.NumberOfUnknowns; i++)
            {
                w.WriteLine($"x{i + 1} = {results[i]}; ");
            }
            w.WriteLine("Error: ");
            for (int i = 0; i < system.NumberOfUnknowns; i++)
            {
                w.WriteLine($"Error for x{i + 1} = {r[i]}");
            }
        }
        protected void ShowMatrixes(List<List<double>> A, List<double> B, int numberOfRows, int numberOfColums)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                int cursorPosition = 0;
                for (int j = 0; j < numberOfColums; j++)
                {
                    Console.CursorLeft = cursorPosition;
                    Console.Write($"{Math.Round(A[i][j], 6)}  ");
                    cursorPosition += 10;

                }
                Console.CursorLeft = cursorPosition;
                Console.Write($"  |   {B[i]}");
                Console.WriteLine("");
            }
        } //method to output coefficients and constant term matrixes to the screen
    }
}
