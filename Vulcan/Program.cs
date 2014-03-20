﻿using System;
using System.Collections;
using Vulcan.Core;

namespace Vulcan
{
    class Program
    {
        static void Main(string[] args)
        {
            Vulcan vulcan = new Vulcan(2);  // 2 hidden layers
            
            DefaultNeuralLayer input = vulcan.GetNeuralLayer(0);
            input.AddNeurons(1.0, 1.0);     // num input neurons

            DefaultNeuralLayer hidden = vulcan.GetNeuralLayer(1);
            hidden.AddNeuron();
            hidden.AddNeuron();

            hidden = vulcan.GetNeuralLayer(2);  // 2nd h layer
            hidden.AddNeuron();
            hidden.AddNeuron();

            DefaultNeuralLayer output = vulcan.GetNeuralLayer(vulcan.LayerCount - 1);
            output.AddNeuron();

            double i = 0;

            // neural network learns
            while (i != 1000)
            {
                // And operation
                vulcan.Expecting(1.0);
                vulcan.Train(1.0, 1.0);

                vulcan.Expecting(0);
                vulcan.Train(1.0, 0.0);
                vulcan.Train(0.0, 1.0);
                vulcan.Train(0, 0);
                i++;
            }

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
