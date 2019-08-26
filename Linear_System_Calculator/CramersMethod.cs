using System;
using System.Collections.Generic;
using System.Text;

namespace LinearSystemCalculator
{
    internal class CramersMethod : DirectMethod
    {
        internal override List<double> FinalCalculations(LinearSystem system)
        {
            if (Check(system) == true)
            {
                List<double> x = new List<double>();
                var A = new List<List<double>>(system.Coefficients.Count);
                for (int i = 0; i < system.Coefficients.Count; i++)
                {
                    List<double> row = new List<double>(system.Coefficients[i]);
                    A.Add(row);
                }
                var B = new List<double>(system.ConstantTerms);
                Console.WriteLine("Coefficients matrix (A):");
                ShowMatrixes(A, system.NumberOfUnknowns);
                double determinant = Det(A);
                Console.WriteLine($"Determinant = {determinant}");
                for (int i = 0; i < system.NumberOfUnknowns; i++)
                {
                    List<List<double>> newA = ReplacingColumn(A, B, i);
                    Console.WriteLine("Converted matrix on current step: ");
                    ShowMatrixes(newA, system.NumberOfUnknowns);
                    Console.WriteLine($"Determinant = {Det(newA)}");
                    x.Add(Det(newA) / determinant);
                }
                for (int i = 0; i < system.NumberOfUnknowns; i++)
                {
                    Console.WriteLine($"x{i + 1} = {x[i]}; ");
                }
                return x;
            }
            else
            {
                Console.WriteLine("You can't use this method");
                return null;
            }
        }
        internal override bool Check(LinearSystem system) => (system.NumberOfEquations == system.NumberOfUnknowns && Det(system.Coefficients) != 0);
        internal double Det(List<List<double>> matrix)
        {
            double determinant = 0.0;
            if (matrix.Count != 2)
            {
                for (int i = 0; i < matrix.Count; i++)
                {
                    determinant += Math.Pow((-1), (i + 2)) * matrix[0][i] * Det(Minor(0, i, matrix));
                }
            }
            else
            {
                determinant = matrix[0][0] * matrix[matrix.Count - 1][matrix.Count - 1] - matrix[matrix.Count - 1][0] * matrix[0][matrix.Count - 1];
            }
            return determinant;
        }
        internal List<List<double>> Minor(int z, int x, List<List<double>> matrix)
        {
            List<List<double>> A = new List<List<double>>();
            for (int i = 0; i < matrix.Count - 1; i++)
            {
                List<double> row = new List<double>();
                for (int j = 0; j < matrix.Count - 1; j++)
                {
                    row.Add(0.0);
                }
                A.Add(row);
            }
            for (int h = 0, i = 0; i < matrix.Count - 1; i++, h++)
            {
                if (i == z) h++;
                for (int k = 0, j = 0; j < matrix.Count - 1; j++, k++)
                {
                    if (k == x) k++;
                    A[i][j] = matrix[h][k];
                }
            }
            return A;
        }
        internal List<List<double>> ReplacingColumn(List<List<double>> matrix, List<double> column, int numberOfCol)
        {
            List<List<double>> newMatrix = new List<List<double>>();
            for (int i = 0; i < matrix.Count; i++)
            {
                List<double> row = new List<double>();
                for (int j = 0; j < matrix.Count; j++)
                {
                    if (j == numberOfCol)
                    {
                        row.Add(column[i]);
                    }
                    else
                    {
                        row.Add(matrix[i][j]);
                    }

                }
                newMatrix.Add(row);
            }
            return newMatrix;
        }
        private void ShowMatrixes(List<List<double>> A, int size)
        {
            for (int i = 0; i < size; i++)
            {
                int cursorPosition = 0;
                for (int j = 0; j < size; j++)
                {
                    Console.CursorLeft = cursorPosition;
                    Console.Write($"{A[i][j]}  ");
                    cursorPosition += 9;

                }
                Console.WriteLine("");
            }
        }
    }
}
