using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core.Interfaces;

namespace Vulcan.Core
{
    internal class DefaultNeuralLayer : INeuralLayer
    {
        public List<INeuron> m_Neurons;

        public DefaultNeuralLayer()
        {
            m_Neurons = new List<INeuron>();
        }

        public void AddNeuron(INeuron neuron)
        {
            m_Neurons.Add(neuron);
        }

        public bool DeleteNeuron(INeuron neuron)
        {
            return m_Neurons.Remove(neuron);
        }
    }
}
