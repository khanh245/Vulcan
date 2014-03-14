using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Core
{
    public class DefaultNeuralLayer
    {
        public List<DefaultNeuron> m_Neurons;

        public string Name { get; set; }

        public DefaultNeuralLayer(string input)
        {
            m_Neurons = new List<DefaultNeuron>();
            Name = input;
        }

        public void AddNeuron()
        {
            DefaultNeuron neuron = new DefaultNeuron();
            if (Name != "Input")
            {
                Random rand = new Random();
                neuron.Weights = (1.0 * rand.Next()) / (Int32.MaxValue * 1.0);
            }
            else
                neuron.Weights = 1.0;

            m_Neurons.Add(neuron);
        }

        public void AddNeurons (params double[] weights)
        {
            foreach (double w in weights)   
            {
                m_Neurons.Add(new DefaultNeuron { Weights = w });
            }
        }

        public bool DeleteNeuron(DefaultNeuron neuron)
        {
            return m_Neurons.Remove(neuron);
        }

        public DefaultNeuron GetNeuron (int id)
        {
            return m_Neurons.ElementAt(id);
        }
    }
}
