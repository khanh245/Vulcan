using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Core
{
    public class DefaultNeuron
    {
        public double Input { get; set; }

        public double Weight { get; set; }

        public double Output { get; set; }

        public DefaultNeuron()
        {
            Input = 0.0;
            Weight = 0.0;
            Output = 0.0;
        }

        public DefaultNeuron (double i, double w, double o)
        {
            Input = i;
            Weight = w;
            Output = o;
        }

        public int GetGeneticCode()
        {
            throw new NotImplementedException();
            // TODO: Return Hashed Genetic Code
        }

        public override string ToString()
        {
            throw new NotImplementedException();
            // TODO: Return Genetic Code
        }
    }
}
