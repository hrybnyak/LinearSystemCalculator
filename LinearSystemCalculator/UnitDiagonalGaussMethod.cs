using System;
using System.Collections.Generic;

namespace LinearSystemCalculator
{
    internal class UnitDiagonalGaussMethod : GaussMethod //class that represents gauss method with unit diagonal methods to find unknowns in linear system
    {
        internal override void PreCalculations(LinearSystem system, out List<List<double>> A, out List<double> B)
        {
            A = new List<List<double>>(system.Coefficients.Count);
            for (int i = 0; i < system.Coefficients.Count; i++)
            {
                List<double> row = new List<double>(system.Coefficients[i]);
                A.Add(row);
            }
            B = new List<double>(system.ConstantTerms);
            int k = 0;
            double Amax;
            while (k < system.NumberOfEquations)
            {
                Amax = A[k][k];
                int p = k;
                for (int i = k + 1; i < system.NumberOfEquations; i++)
                {
                    if (Math.Abs(A[i][k]) > Amax)
                    {
                        Amax = Math.Abs(A[i][k]);
                        p = i;
                    }
                }
                for (int i = 0; i < system.NumberOfUnknowns; i++)
                {
                    double tempA = A[k][i];
                    A[k][i] = A[p][i];
                    A[p][i] = tempA;
                }
                double temp = B[k];
                B[k] = B[p];
                B[p] = temp;
                temp = A[k][k];
                for (int i = 0; i < system.NumberOfEquations; i++)
                {
                    A[k][i] = A[k][i] / temp;
                }
                B[k] = B[k] / temp;
                Console.WriteLine("Converted matrixes on current step: ");
                ShowMatrixes(A, B, system.NumberOfEquations, system.NumberOfUnknowns);
                Console.WriteLine("");
                for (int i = k + 1; i < system.NumberOfEquations; i++)
                {
                    double M = A[i][k];
                    for (int j = k; j < system.NumberOfEquations; j++)
                    {
                        A[i][j] = A[i][j] - M * A[k][j];
                    }
                    B[i] = B[i] - M * B[k];
                }
                Console.WriteLine("Converted matrixes on current step: ");
                ShowMatrixes(A, B, system.NumberOfEquations, system.NumberOfUnknowns);
                Console.WriteLine("");
                k++;
            }
        } //method that convertes matrixes to triagonal form
    }
}
