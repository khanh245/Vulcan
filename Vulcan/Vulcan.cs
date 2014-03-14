using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core;

namespace Vulcan
{
    public class Vulcan
    {
        private List<DefaultNeuralLayer> m_Layers = new List<DefaultNeuralLayer>();

        public Vulcan(int hidden)
        {
            DefaultNeuralLayer input = new DefaultNeuralLayer("Input");
            AddLayer(input);

            for (int i = 0; i < hidden; ++i)
            {
                DefaultNeuralLayer layer = new DefaultNeuralLayer(string.Format("Hidden{0}", i+1));
                AddLayer(layer);
            }

            DefaultNeuralLayer output = new DefaultNeuralLayer("Output");
            AddLayer(output);
        }

        public void AddLayer (DefaultNeuralLayer layer)
        {
            m_Layers.Add(layer);
        }

        public int LayerCount 
        { 
            get
            {
                return m_Layers.Count;
            }
        }

        public bool RemoveLayer (DefaultNeuralLayer layer)
        {
            return m_Layers.Remove(layer);
        }

        public DefaultNeuralLayer GetNeuralLayer(int id)
        {
            return m_Layers.ElementAt(id);
        }
    }
}
