using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core;
using Vulcan.Core.Interfaces;

namespace Vulcan
{
    public class Vulcan  :   INeuralLayer
    {
        private List<INeuralLayer> m_Layers = new List<INeuralLayer>();

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

            Initialize();
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

        private void Initialize()
        {

        }

        #region "Not Supported"
        public INeuron GetNeuron(int id)
        {
            throw new NotSupportedException();
        }

        public void AddNeuron(INeuron neuron)
        {
            throw new NotSupportedException();
        }

        public bool DeleteNeuron(INeuron neuron)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
