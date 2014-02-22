using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Helpers
{
    internal class Activators
    {
        public static double SigmoidFunction (double x)
        {
            return (1.0 / (1.0 + Math.Exp(-x)));
        }

        public static double TanhFunction (double x)
        {
            return (Math.Tanh(x));
        }

        public static double DerivativeSigmoid (double x)
        {
            return SigmoidFunction(x) - (1.0 / Math.Pow(Math.Exp(x) + 1, 2));
        }

        public static double DerivativeTanh (double x)
        {
            return Math.Pow((2 / (Math.Exp(x) + Math.Exp(-x))), 2);
        }
    }
}
