using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core;
using Vulcan.Core.Interfaces;

namespace Vulcan
{
    public class Vulcan :   INeuralLayer
    {
        private List<INeuralLayer> m_Layers = new List<INeuralLayer>();

        public Vulcan(int input, int hidden, int output)
        {

        }

        public void AddLayer (INeuralLayer layer)
        {
            m_Layers.Add(layer);
        }

        public bool RemoveLayer (INeuralLayer layer)
        {
            return m_Layers.Remove(layer);
        }

        public INeuralLayer GetNeuralLayer(int id)
        {
            return m_Layers.ElementAt(id);
        }
    }
}
