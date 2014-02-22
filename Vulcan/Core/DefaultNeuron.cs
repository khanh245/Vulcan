using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core.Interfaces;

namespace Vulcan.Core
{
    public class DefaultNeuron  :   INeuron
    {
        private ArrayList m_Inputs;

        private ArrayList m_Weights;

        public DefaultNeuron()
        {
            m_Inputs = new ArrayList();
            m_Weights = new ArrayList();
        }

        public double GetWeight(int id)
        {
            if (id < 0 || id >= m_Weights.Count)
                throw new IndexOutOfRangeException();

            return (double)m_Weights[id];
        }

        public double GetInput(int id)
        {
            if (id < 0 || id >= m_Inputs.Count)
                throw new IndexOutOfRangeException();

            return (double)m_Inputs[id];
        }
    }
}
