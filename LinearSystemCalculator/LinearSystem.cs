using System;
using System.Collections.Generic;

namespace LinearSystemCalculator
{
    internal sealed class LinearSystem //class that represents linear system of equations 
    {
        //readonly properties: number of equations in system, number of unknowns, coefficients and constant terms
        public LinearSystem()
        {

        }
        internal int NumberOfEquations { get; }
        internal int NumberOfUnknowns { get; }
        internal List<List<double>> Coefficients { get; } = new List<List<double>>();
        internal List<double> ConstantTerms { get; } = new List<double>();

        //constructor for linear system: once properties set they can't be changed 
        public LinearSystem(int equations, int unknowns, List<List<double>> coefficients, List<double> constantTerms)
        {
            try
            {
                NumberOfEquations = equations;
                NumberOfUnknowns = unknowns;
                Coefficients = coefficients;
                ConstantTerms = constantTerms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ ex.GetType()} says { ex.Message}");
                Console.ReadLine();
            }
        }
    }
}
