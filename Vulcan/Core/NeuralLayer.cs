// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NeuralLayer.cs" company="Ascension">
//     All rights reserved.
// </copyright>
// <author>Khanh Nguyen</author>
// <date>08/23/2015</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Vulcan.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Represents a neural layer consists of neuron(s)
    /// </summary>
    public class NeuralLayer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NeuralLayer" /> class.
        /// </summary>
        /// <param name="name">The name of the layer</param>
        public NeuralLayer(string name)
        {
            this.Neurons = new List<Neuron>();
            this.Name = name;
        }

        /// <summary>
        ///     List of all the neurons
        /// </summary>
        /// <value>
        ///     List of <see cref="Neuron" />.
        /// </value>
        public List<Neuron> Neurons { get; }

        /// <summary>
        ///     The name of the neural layer
        /// </summary>
        /// <value>
        ///     string
        /// </value>
        private string Name { get; set; }

        /// <summary>
        ///     Adds a neuron with random weight
        /// </summary>
        public void AddNeuron()
        {
            var neuron = new Neuron { Weight = this.GetRandomWeight() };
            this.Neurons.Add(neuron);
        }

        /// <summary>
        ///     Adds a neuron with desired weight value
        /// </summary>
        /// <param name="weight">The weight value</param>
        public void AddNeuron(double weight)
        {
            this.Neurons.Add(new Neuron { Weight = weight });
        }

        /// <summary>
        ///     Adds X number of neurons with random weights
        /// </summary>
        /// <param name="count">Number of neurons to add</param>
        public void AddNeurons(int count)
        {
            for (var i = 0; i < count; ++i)
            {
                this.Neurons.Add(new Neuron { Weight = this.GetRandomWeight() });
            }
        }

        /// <summary>
        ///     Adds X number of neurons with X number of weights
        /// </summary>
        /// <param name="weights">Array of weight values</param>
        public void AddNeurons(params double[] weights)
        {
            foreach (var w in weights)
            {
                this.Neurons.Add(new Neuron { Weight = w });
            }
        }

        /// <summary>
        ///     Removes a neuron by Id
        /// </summary>
        /// <param name="id">The Id of a neuron</param>
        public void RemoveNeuron(int id)
        {
            this.Neurons.RemoveAt(id);
        }

        /// <summary>
        ///     Removes a neuron
        /// </summary>
        /// <param name="neuron">The neuron to be removed</param>
        /// <returns>True if it succeeds.</returns>
        public bool RemoveNeuron(Neuron neuron)
        {
            return this.Neurons.Remove(neuron);
        }

        /// <summary>
        ///     Gets a neuron by Id
        /// </summary>
        /// <param name="id">The Id of the neuron</param>
        /// <returns>A <see cref="Neuron" /></returns>
        public Neuron GetNeuron(int id)
        {
            return this.Neurons.ElementAt(id);
        }

        /// <summary>
        ///     Gets a random weight value
        /// </summary>
        /// <returns>Random weight</returns>
        private double GetRandomWeight()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return rand.NextDouble();
        }
    }
}