using System;
using System.Collections.Generic;
using System.Text;

namespace LinearSystemCalculator
{
    internal abstract class GaussMethod : DirectMethod
    {
        internal override List<double> FinalCalculations(LinearSystem system)
        {
            if (Check(system) == true)
            {
                List<double> x = new List<double>();
                for (int i = 0; i < system.NumberOfUnknowns; i++)
                {
                    x.Add(0.0);
                }
                List<List<double>> A = new List<List<double>>();
                List<double> B = new List<double>();
                PreCalculations(system, out A, out B);
                for (int k = system.NumberOfUnknowns - 1; k >= 0; k--)
                {
                    x[k] = B[k];
                    for (int i = 0; i < k; i++)
                    {
                        B[i] = B[i] - A[i][k] * x[k];
                    }
                }
                for (int i = 0; i < system.NumberOfUnknowns; i++)
                {
                    Console.WriteLine($"x{i + 1} = {x[i]}; ");
                }
                return x;
            }
            else
            {
                Console.WriteLine("System has endless number of solutions or none, please enter another system");
                return null;
            }
        }
        internal abstract void PreCalculations(LinearSystem system, out List<List<double>> A, out List<double> B);
        internal override bool Check(LinearSystem system)
        {
            bool check = true;
            List<List<double>> A = new List<List<double>>();
            List<double> B = new List<double>();
            PreCalculations(system, out A, out B);
            int rangA = 0, rangB = 0;
            for (int i = 0; i < B.Count; i++)
            {
                if (A[i][i] != 0.0)
                {
                    rangA++;
                }
                if (B[i] != 0.0)
                {
                    rangB++;
                }
            }
            if (rangA != rangB) check = false;
            return check;
        }
    }
}
