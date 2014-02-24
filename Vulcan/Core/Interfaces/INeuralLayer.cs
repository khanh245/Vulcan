using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Core.Interfaces
{
    internal interface INeuralLayer
    {
        INeuron GetNeuron(int id);

        void AddNeuron(INeuron neuron);
        
        bool DeleteNeuron(INeuron neuron);
    }
}
