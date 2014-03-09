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
        public ArrayList Inputs { get; set; }

        public ArrayList Weights { get; set; }

        public ArrayList Outputs { get; set; }

        public DefaultNeuron()
        {
            Inputs = new ArrayList();
            Weights = new ArrayList();
            Outputs = new ArrayList();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
            // TODO: Return Hashed Genetic Code
        }

        public override string ToString()
        {
            return base.ToString();
            // TODO: Return Genetic Code
        }
    }
}
