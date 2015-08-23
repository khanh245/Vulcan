// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Activators.cs" company="Ascension">
//     All rights reserved.
// </copyright>
// <author>Khanh Nguyen</author>
// <date>08/23/2015</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Vulcan.Helpers
{
    using System;

    /// <summary>
    ///     Internal activator functions
    /// </summary>
    internal static class Activators
    {
        /// <summary>
        ///     Sigmoid logistic function
        /// </summary>
        /// <param name="x">At time x.</param>
        /// <returns>The output of the function</returns>
        public static double SigmoidFunction(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        /// <summary>
        ///     Tanh function
        /// </summary>
        /// <param name="x">Input x</param>
        /// <returns>Output y</returns>
        public static double TanhFunction(double x)
        {
            return Math.Tanh(x);
        }

        /// <summary>
        ///     Derivative of Sigmoid function
        /// </summary>
        /// <param name="x">Input x</param>
        /// <returns>Output y</returns>
        public static double DerivativeSigmoid(double x)
        {
            return SigmoidFunction(x) * (1 - SigmoidFunction(x));
        }

        /// <summary>
        ///     Derivative of Tanh function
        /// </summary>
        /// <param name="x">Input x</param>
        /// <returns>Output y</returns>
        public static double DerivativeTanh(double x)
        {
            return Math.Pow(2 / (Math.Exp(x) + Math.Exp(-x)), 2);
        }
    }
}