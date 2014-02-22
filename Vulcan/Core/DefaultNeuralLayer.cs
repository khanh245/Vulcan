using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core.Interfaces;

namespace Vulcan.Core
{
    public class DefaultNeuralLayer : INeuralLayer
    {
        private List<INeuron> m_Neurons;

        public DefaultNeuralLayer()
        {
            m_Neurons = new List<INeuron>();
        }

        public INeuron GetNeuron(int id)
        {
            if (m_Neurons.Count > 0)
                return m_Neurons.ElementAt(id);
            else return null;
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
