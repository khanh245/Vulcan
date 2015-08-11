using System;

namespace Vulcan.Core
{
    /// <summary>
    /// Represents a neuron
    /// </summary>
    public class Neuron
    {
        /// <summary>
        /// Input value of the neuron
        /// </summary>
        public double Input { get; set; }

        /// <summary>
        /// Weight of the neuron
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Output value of the neuron
        /// </summary>
        public double Output { get; set; }

        /// <summary>
        /// Initializes an instance of a neuron
        /// </summary>
        public Neuron()
        {
            Input = 0.0;
            Weight = 0.0;
            Output = 0.0;
        }

        /// <summary>
        /// Initializes an instance of a neuron with default values for input, output, and weight
        /// </summary>
        /// <param name="i">The input value</param>
        /// <param name="w">The weight value</param>
        /// <param name="o">The output value</param>
        public Neuron (double i, double w, double o)
        {
            Input = i;
            Weight = w;
            Output = o;
        }

        /// <summary>
        /// Gets a genetic code of the neuron
        /// </summary>
        /// <returns></returns>
        public int GetGeneticCode()
        {
            // TODO: Return Genetic Code
            throw new NotImplementedException();
        }
    }
}
