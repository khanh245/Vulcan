// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vulcan.cs" company="Ascension">
//     All rights reserved.
// </copyright>
// <author>Khanh Nguyen</author>
// <date>08/23/2015</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Vulcan
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using global::Vulcan.Core;
    using global::Vulcan.Helpers;

    /// <summary>
    ///     Represents the Neural Network - Vulcan
    /// </summary>
    public class Vulcan
    {
        /// <summary>
        ///     List of all expected outputs of the network
        /// </summary>
        private readonly List<double> expectedOutputs = new List<double>();

        /// <summary>
        ///     List of all neural layers in the network
        /// </summary>
        private readonly List<NeuralLayer> layers = new List<NeuralLayer>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vulcan" /> class with X number of hidden layers
        /// </summary>
        /// <param name="hidden">Number of hidden layer(s)</param>
        public Vulcan(int hidden)
        {
            this.Error = 0.0;

            var input = new NeuralLayer("Input");
            this.AddLayer(input);

            for (var i = 0; i < hidden; ++i)
            {
                var layer = new NeuralLayer($"Hidden{i + 1}");
                this.AddLayer(layer);
            }

            var output = new NeuralLayer("Output");
            this.AddLayer(output);
        }

        /// <summary>
        ///     The error of the neural network
        /// </summary>
        /// <value>
        ///     double
        /// </value>
        public double Error { get; private set; }

        /// <summary>
        ///     Gets number of layers in the network
        /// </summary>
        /// <value>
        ///     integer
        /// </value>
        public int LayerCount => this.layers.Count;

        /// <summary>
        ///     Feeding forward algorithm
        /// </summary>
        /// <param name="datas">The datas to be fed to the network</param>
        /// <exception cref="ArgumentException"></exception>
        public void FeedForward(params double[] datas)
        {
            if (datas.Count() != this.layers[0].Neurons.Count)
            {
                throw new ArgumentException();
            }

            Debug.WriteLine("\n----- Feeding Forward -----");

            int count;
            var input = this.GetInputLayer();
            var output = this.GetOutputLayer();

            Debug.WriteLine("Input Layer");

            // Input layer flowthrough
            for (var i = 0; i < input.Neurons.Count; ++i)
            {
                input.Neurons[i].Input = datas[i];
                input.Neurons[i].Output = Activators.SigmoidFunction(input.Neurons[i].Input * input.Neurons[i].Weight);

                Debug.WriteLine(
                    "Neuron {0} --- Input: {1}, Output {2}",
                    i,
                    input.Neurons[i].Input,
                    input.Neurons[i].Output);
            }

            Debug.Write("\n");

            // Hidden layers flowthrough
            for (var i = 1; i < this.layers.Count - 1; ++i)
            {
                Debug.WriteLine("Hidden Layer {0}", i);

                var inputToHidden = this.layers[i - 1].Neurons.Sum(neuron => neuron.Output);

                count = 0;
                foreach (var neuron in this.layers[i].Neurons)
                {
                    neuron.Input = inputToHidden;
                    neuron.Output = Activators.SigmoidFunction(neuron.Input * neuron.Weight);

                    Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", count, neuron.Input, neuron.Output);
                    count++;
                }
            }

            count = 0;
            Debug.WriteLine("\nOutput Layer");

            // Output layer flowthrough
            foreach (var neuron in output.Neurons)
            {
                var hiddenToOutput = this.GetHiddenLayers().Last().Neurons.Sum(n => n.Output);

                neuron.Input = hiddenToOutput;
                neuron.Output = Activators.SigmoidFunction(neuron.Input * neuron.Weight);

                Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", count, neuron.Input, neuron.Output);
            }

            // Calculating total error for neural network
            for (var i = 0; i < this.expectedOutputs.Count; ++i)
            {
                this.Error += Math.Pow(output.Neurons[i].Output - this.expectedOutputs[i], 2) / 2.0;
            }

            Debug.Write("\n");
            Debug.WriteLine("Net Error: {0}", this.Error);
        }

        /// <summary>
        ///     Adds a neural layer to the network
        /// </summary>
        /// <param name="layer">The layer to be added</param>
        public void AddLayer(NeuralLayer layer)
        {
            this.layers.Add(layer);
        }

        /// <summary>
        ///     Trains the neural network
        /// </summary>
        /// <param name="learnRate">The learning rate</param>
        /// <param name="datas">Data to be trained</param>
        public void Train(double learnRate, params double[] datas)
        {
            this.FeedForward(datas);
            this.BackPropagate(learnRate);
        }

        /// <summary>
        ///     Sets the expecting outputs of the network
        /// </summary>
        /// <param name="expecteds">The expected outputs</param>
        public void Expecting(params double[] expecteds)
        {
            this.expectedOutputs.Clear();

            foreach (var e in expecteds)
            {
                this.expectedOutputs.Add(e);
            }
        }

        /// <summary>
        ///     Back propagating result and update where necessary
        /// </summary>
        /// <param name="learnRate">The learning rate</param>
        public void BackPropagate(double learnRate)
        {
            Debug.WriteLine("\n----- Back Propagating -----");

            var input = this.GetInputLayer();
            var output = this.GetOutputLayer();

            // Output layer's weights update
            for (var i = this.layers.Count - 1; i >= 1; --i)
            {
                var errorContribution = (output.Neurons[i].Output * (1 - output.Neurons[i].Output))
                                        * (output.Neurons[i].Output - this.expectedOutputs[i])
                                        * output.Neurons[i].Output;
                output.Neurons[i].Weight = output.Neurons[i].Weight - (learnRate * errorContribution);
            }

            /*
            double deltaWeight = 0.0;
            double error = 0.0;
            double errorSum = 0.0;
            double tempSum = 0.0;

            // Output Errors
            for (int j = 0; j < m_Layers[LayerCount-1].Neurons.Count; ++j)
            {
                Error = ((m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].Neurons[j].Output) * (m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].Neurons[j].Output)) / 2;
                error = (m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].Neurons[j].Output) * m_Layers[LayerCount - 1].Neurons[j].Output * (1 - m_Layers[LayerCount - 1].Neurons[j].Output);
                deltaWeight = learnRate * error * m_Layers[LayerCount - 1].Neurons[j].Output + m_Layers[LayerCount - 1].Neurons[j].Weight * 0.1;
                //deltaWeight = Activators.DerivativeSigmoid(deltaWeight);
                m_Layers[LayerCount - 1].Neurons[j].Weight += deltaWeight;
                errorSum += m_Layers[LayerCount - 1].Neurons[j].Weight * error;
            }

            // Hidden Layers Errors
            for (int i = LayerCount-2; i >= 1; --i)
            {
                for (int j = 0; j < m_Layers[i].Neurons.Count; ++j)
                {
                    error = m_Layers[i].Neurons[j].Output * (1 - m_Layers[i].Neurons[j].Output) * errorSum;
                    deltaWeight = learnRate * error * m_Layers[i].Neurons[j].Input + m_Layers[i].Neurons[j].Weight * 0.1;
                    m_Layers[i].Neurons[j].Weight += deltaWeight;
                    tempSum += m_Layers[i].Neurons[j].Weight * error;
                }

                errorSum = tempSum;
                tempSum = 0;
            }

            // Input Layer Errors
            foreach (var neuron in m_Layers[0].Neurons)
            {
                error = neuron.Output * (1 - neuron.Output) * errorSum;
                deltaWeight = learnRate * error * neuron.Input + neuron.Weight * 0.1;
                neuron.Weight += deltaWeight;
            }
            */
        }

        /// <summary>
        ///     Removes a layer from the network
        /// </summary>
        /// <param name="layer">The layer to be removed</param>
        /// <returns>True if succeeds</returns>
        public bool RemoveLayer(NeuralLayer layer)
        {
            return this.layers.Remove(layer);
        }

        /// <summary>
        ///     Gets the input layer of the network
        /// </summary>
        /// <returns>The <see cref="NeuralLayer" /></returns>
        public NeuralLayer GetInputLayer()
        {
            return this.layers.ElementAt(0);
        }

        /// <summary>
        ///     Gets the hidden layer of the network
        /// </summary>
        /// <returns>List of <see cref="NeuralLayer" /></returns>
        public List<NeuralLayer> GetHiddenLayers()
        {
            return this.layers.GetRange(1, this.layers.Count - 2);
        }

        /// <summary>
        ///     Gets the output layer of the network
        /// </summary>
        /// <returns>The <see cref="NeuralLayer" /></returns>
        public NeuralLayer GetOutputLayer()
        {
            return this.layers.Last();
        }

        /// <summary>
        ///     Returns the current outputs' values
        /// </summary>
        /// <returns><see cref="ArrayList" /> of outputs.</returns>
        public ArrayList GetOutputs()
        {
            var result = new ArrayList();
            foreach (var n in this.layers[this.LayerCount - 1].Neurons)
            {
                result.Add(n.Output);
            }

            return result;
        }
    }
}