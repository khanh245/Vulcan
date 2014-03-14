using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core;

namespace Vulcan
{
    class Program
    {
        static void Main(string[] args)
        {
            Vulcan vulcan = new Vulcan(2);
            
            DefaultNeuralLayer input = vulcan.GetNeuralLayer(0);
            input.AddNeurons(1.0, 1.0);

            DefaultNeuralLayer hidden = vulcan.GetNeuralLayer(1);
            hidden.AddNeuron();
            hidden.AddNeuron();

            hidden = vulcan.GetNeuralLayer(2);
            hidden.AddNeuron();
            hidden.AddNeuron();
            hidden.AddNeuron();

            DefaultNeuralLayer output = vulcan.GetNeuralLayer(vulcan.LayerCount - 1);
            output.AddNeurons(1.0, 1.0);


            Console.WriteLine("testing done.");
            Console.ReadKey();
        }
    }
}
