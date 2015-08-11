using System;
using System.Collections.Generic;
using System.Linq;

namespace Vulcan.Core
{
    /// <summary>
    /// Represents a neural layer consists of neuron(s)
    /// </summary>
    public class NeuralLayer
    {
        /// <summary>
        /// The name of the neural layer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of all the neurons
        /// </summary>
        public List<Neuron> Neurons { get; }

        /// <summary>
        /// Initializes an instance of a neural layer
        /// </summary>
        /// <param name="name">The name of the layer</param>
        public NeuralLayer(string name)
        {
            Neurons = new List<Neuron>();
            Name = name;
        }

        /// <summary>
        /// Gets a random weight value
        /// </summary>
        /// <returns></returns>
        private double GetRandomWeight()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return rand.NextDouble();
        }

        /// <summary>
        /// Adds a neuron with random weight
        /// </summary>
        public void AddNeuron()
        {
            Neuron neuron = new Neuron { Weight = GetRandomWeight() };
            Neurons.Add(neuron);
        }

        /// <summary>
        /// Adds a neuron with desired weight value
        /// </summary>
        /// <param name="weight">The weight value</param>
        public void AddNeuron(double weight)
        {
            Neurons.Add(new Neuron { Weight = weight });
        }

        /// <summary>
        /// Adds X number of neurons with random weights
        /// </summary>
        /// <param name="count">Number of neurons to add</param>
        public void AddNeurons(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Neurons.Add(new Neuron { Weight = GetRandomWeight() });
            }
        }

        /// <summary>
        /// Adds X number of neurons with X number of weights
        /// </summary>
        /// <param name="weights">Array of weight values</param>
        public void AddNeurons(params double[] weights)
        {
            foreach (double w in weights)   
            {
                Neurons.Add(new Neuron { Weight = w });
            }
        }

        /// <summary>
        /// Removes a neuron by Id
        /// </summary>
        /// <param name="id">The Id of a neuron</param>
        public void RemoveNeuron(int id)
        {
            Neurons.RemoveAt(id);
        }

        /// <summary>
        /// Removes a neuron
        /// </summary>
        /// <param name="neuron">The neuron to be removed</param>
        /// <returns></returns>
        public bool RemoveNeuron(Neuron neuron)
        {
            return Neurons.Remove(neuron);
        }

        /// <summary>
        /// Gets a neuron by Id
        /// </summary>
        /// <param name="id">The Id of the neuron</param>
        /// <returns></returns>
        public Neuron GetNeuron (int id)
        {
            return Neurons.ElementAt(id);
        }
    }
}
