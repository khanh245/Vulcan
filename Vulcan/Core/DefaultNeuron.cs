using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Core
{
    public class DefaultNeuron
    {
        public double Inputs { get; set; }

        public double Weights { get; set; }

        public double Outputs { get; set; }

        public DefaultNeuron()
        {
            Inputs = 0.0;
            Weights = 0.0;
            Outputs = 0.0;
        }

        public DefaultNeuron (double i, double w, double o)
        {
            Inputs = i;
            Weights = w;
            Outputs = o;
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
