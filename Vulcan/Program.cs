using System;
using System.Collections;
using System.Collections.Generic;
using Vulcan.Core;

namespace Vulcan
{
    class Program
    {
        static void Main(string[] args)
        {
            Vulcan vulcan = new Vulcan(1);
            
            DefaultNeuralLayer input = vulcan.GetInputLayer();
            input.AddNeurons(1.0, 1.0);     // num input neurons

            List<DefaultNeuralLayer> hiddens = vulcan.GetHiddenLayers();
            foreach (var hidden in hiddens)
            {
                hidden.AddNeurons(3);
            }
            
            DefaultNeuralLayer output = vulcan.GetOutputLayer();
            output.AddNeuron();

            // neural network learns
            do
            {
                // And operation
                vulcan.Expecting(1.0);
                vulcan.Train(0.2, 1.0, 1.0);
                vulcan.Train(0.2, 0.5, 0.5);
                vulcan.Train(0.2, 0.75, 0.75);
                vulcan.Train(0.2, 0.9, 0.9);
                vulcan.Train(0.2, 0.85, 0.85);
                vulcan.Train(0.2, 0.65, 0.65);
                vulcan.Train(0.2, 0.55, 0.55);

                vulcan.Expecting(0);
                vulcan.Train(0.2, 1.0, 0.0);
                vulcan.Train(0.2, 0.5, 0);
                vulcan.Train(0.2, 0.75, 0);
                vulcan.Train(0.2, 0.95, 0);
                vulcan.Train(0.2, 0.9, 0);

                vulcan.Train(0.2, 0.0, 1.0);
                vulcan.Train(0.2, 0.0, 0.5);
                vulcan.Train(0.2, 0.0, 0.75);
                vulcan.Train(0.2, 0.0, 0.65);
                vulcan.Train(0.2, 0.0, 0.85);
                vulcan.Train(0.2, 0.0, 0.95);
                vulcan.Train(0.2, 0.0, 0.55);

                vulcan.Train(0.2, 0, 0);
                vulcan.Train(0.2, 0.25, 0.25);
                vulcan.Train(0.2, 0.35, 0.35);
                vulcan.Train(0.2, 0.45, 0.45);
                vulcan.Train(0.2, 0.3, 0.3);
                vulcan.Train(0.2, 0.2, 0.2);
                vulcan.Train(0.2, 0.1, 0.1);
            } while (vulcan.Error > 0.0001);

            vulcan.FeedForward(0.0, 0.0);
            ArrayList result = vulcan.GetOutputs();

            Console.WriteLine("Result: ");
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("testing done.");
            Console.ReadKey();
        }
    }
}
