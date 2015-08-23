// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Neuron.cs" company="Ascension">
//     All rights reserved.
// </copyright>
// <author>Khanh Nguyen</author>
// <date>08/23/2015</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Vulcan.Core
{
    using System;

    /// <summary>
    ///     Represents a neuron
    /// </summary>
    public class Neuron
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Neuron" /> class.
        /// </summary>
        public Neuron()
        {
            this.Input = 0.0;
            this.Weight = 0.0;
            this.Output = 0.0;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Neuron" /> class with default values for input, output, and weight.
        /// </summary>
        /// <param name="i">The input value</param>
        /// <param name="w">The weight value</param>
        /// <param name="o">The output value</param>
        public Neuron(double i, double w, double o)
        {
            this.Input = i;
            this.Weight = w;
            this.Output = o;
        }

        /// <summary>
        ///     Input value of the neuron
        /// </summary>
        /// <value>
        ///     Double
        /// </value>
        public double Input { get; set; }

        /// <summary>
        ///     Weight of the neuron
        /// </summary>
        /// <value>
        ///     Double
        /// </value>
        public double Weight { get; set; }

        /// <summary>
        ///     Output value of the neuron
        /// </summary>
        /// <value>
        ///     Double
        /// </value>
        public double Output { get; set; }

        /// <summary>
        ///     Gets a genetic code of the neuron
        /// </summary>
        /// <returns>Genetic code</returns>
        public int GetGeneticCode()
        {
            // TODO: Return Genetic Code
            throw new NotImplementedException();
        }
    }
}