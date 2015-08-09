using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Core
{
    public class DefaultNeuralLayer
    {
        public string Name { get; set; }

        public List<DefaultNeuron> Neurons { get; }

        public DefaultNeuralLayer(string input)
        {
            Neurons = new List<DefaultNeuron>();
            Name = input;
        }

        private double GetRandomWeight()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return rand.NextDouble();
        }

        public void AddNeuron()
        {
            DefaultNeuron neuron = new DefaultNeuron() { Weight = GetRandomWeight() };
            Neurons.Add(neuron);
        }

        public void AddNeuron(double weight = 1.0)
        {
            Neurons.Add(new DefaultNeuron { Weight = weight });
        }

        public void AddNeurons(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Neurons.Add(new DefaultNeuron { Weight = GetRandomWeight() });
            }
        }

        public void AddNeurons(params double[] weights)
        {
            foreach (double w in weights)   
            {
                Neurons.Add(new DefaultNeuron { Weight = w });
            }
        }

        public void RemoveNeuron(int id)
        {
            Neurons.RemoveAt(id);
        }

        public bool RemoveNeuron(DefaultNeuron neuron)
        {
            return Neurons.Remove(neuron);
        }

        public DefaultNeuron GetNeuron (int id)
        {
            return Neurons.ElementAt(id);
        }
    }
}
