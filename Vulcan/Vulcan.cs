using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core;
using Vulcan.Core.Interfaces;

namespace Vulcan
{
    public class Vulcan
    {
        private List<INeuralLayer> m_Layers = new List<INeuralLayer>();

        public Vulcan(int input, int hidden, int output)
        {
            m_Layers.Add(new DefaultNeuralLayer());
            m_Layers.Add(new DefaultNeuralLayer());
            m_Layers.Add(new DefaultNeuralLayer());
        }
    }
}
