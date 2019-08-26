using System;
using System.Collections.Generic;
using System.Text;

namespace LinearSystemCalculator
{
    internal class MainElementGaussMethod : GaussMethod
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
            double Amax;
            int k = 0;
            int p;
            while (k < system.NumberOfEquations)
            {
                Amax = Math.Abs(A[k][k]);
                p = k;
                for (int i = k + 1; i < system.NumberOfEquations; i++)
                {
                    if (Math.Abs(A[i][k]) > Amax)
                    {
                        Amax = Math.Abs(A[i][k]);
                        p = i;
                    }
                }

                for (int j = 0; j < system.NumberOfUnknowns; j++)
                {
                    double tempA = A[k][j];
                    A[k][j] = A[p][j];
                    A[p][j] = tempA;
                }
                double tempB = B[k];
                B[k] = B[p];
                B[p] = tempB;
                Console.WriteLine("Converted matrixes on current step: ");
                ShowMatrixes(A, B, system.NumberOfEquations, system.NumberOfUnknowns);
                Console.WriteLine("");
                for (int i = k; i < system.NumberOfEquations; i++)
                {
                    double temp = A[i][k];
                    if (Math.Abs(temp) == 0.0)
                    {
                        continue;
                    }
                    for (int j = 0; j < system.NumberOfUnknowns; j++)
                    {
                        A[i][j] = A[i][j] / temp;
                    }
                    B[i] = B[i] / temp;
                    if (i == k)
                    {
                        continue;
                    }
                    for (int j = 0; j < system.NumberOfUnknowns; j++)
                    {
                        A[i][j] = A[i][j] - A[k][j];
                    }
                    B[i] = B[i] - B[k];
                }
                Console.WriteLine("Converted matrixes on current step: ");
                ShowMatrixes(A, B, system.NumberOfEquations, system.NumberOfUnknowns);
                Console.WriteLine("");
                k++;
            }

        }

    }
}
