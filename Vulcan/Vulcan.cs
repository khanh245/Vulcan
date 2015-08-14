using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vulcan.Core;
using Vulcan.Helpers;

namespace Vulcan
{
    /// <summary>
    /// Represents the Neural Network - Vulcan
    /// </summary>
    public class Vulcan
    {
        /// <summary>
        /// List of all neural layers in the network
        /// </summary>
        private readonly List<NeuralLayer> _layers = new List<NeuralLayer>();

        /// <summary>
        /// List of all expected outputs of the network
        /// </summary>
        private readonly List<double> _expectedOutputs = new List<double>();

        /// <summary>
        /// The error of the neural network
        /// </summary>
        public double Error { get; private set; }

        /// <summary>
        /// Initializes an instance of the neural network with X number of hidden layers
        /// </summary>
        /// <param name="hidden">Number of hidden layer(s)</param>
        public Vulcan(int hidden)
        {
            Error = 0.0;

            NeuralLayer input = new NeuralLayer("Input");
            AddLayer(input);

            for (int i = 0; i < hidden; ++i)
            {
                NeuralLayer layer = new NeuralLayer($"Hidden{i + 1}");
                AddLayer(layer);
            }

            NeuralLayer output = new NeuralLayer("Output");
            AddLayer(output);
        }

        /// <summary>
        /// Feeding forward algorithm
        /// </summary>
        /// <param name="datas">The datas to be fed to the network</param>
        public void FeedForward(params double[] datas)
        {
            if (datas.Count() != _layers[0].Neurons.Count) throw new ArgumentException();

            Debug.WriteLine("\n----- Feeding Forward -----");

            int count;
            NeuralLayer input = GetInputLayer();
            NeuralLayer output = GetOutputLayer();

            Debug.WriteLine("Input Layer");

            // Input layer flowthrough
            for (int i = 0; i < input.Neurons.Count; ++i)
            {
                input.Neurons[i].Input = datas[i];
                input.Neurons[i].Output = Activators.SigmoidFunction(input.Neurons[i].Input * input.Neurons[i].Weight);

                Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", i,  input.Neurons[i].Input, input.Neurons[i].Output);
            }

            Debug.Write("\n");

            // Hidden layers flowthrough
            for (int i = 1; i < _layers.Count - 1; ++i)
            {
                Debug.WriteLine("Hidden Layer {0}", i);

                double inputToHidden = _layers[i - 1].Neurons.Sum(neuron => neuron.Output);

                count = 0;
                foreach (var neuron in _layers[i].Neurons)
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
                double hiddenToOutput = GetHiddenLayers().Last().Neurons.Sum(n => n.Output);

                neuron.Input = hiddenToOutput;
                neuron.Output = Activators.SigmoidFunction(neuron.Input * neuron.Weight);

                Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", count, neuron.Input, neuron.Output);
            }

            // Calculating total error for neural network
            for (int i = 0; i < _expectedOutputs.Count; ++i)
            {
                Error += (Math.Pow((output.Neurons[i].Output - _expectedOutputs[i]), 2) / 2.0);
            }

            Debug.Write("\n");
            Debug.WriteLine("Net Error: {0}", Error);
        }

        /// <summary>
        /// Adds a neural layer to the network
        /// </summary>
        /// <param name="layer">The layer to be added</param>
        public void AddLayer(NeuralLayer layer)
        {
            _layers.Add(layer);
        }

        /// <summary>
        /// Trains the neural network
        /// </summary>
        /// <param name="learnRate">The learning rate</param>
        /// <param name="datas">Data to be trained</param>
        public void Train(double learnRate, params double[] datas)
        {
            FeedForward(datas);
            BackPropagate(learnRate);
        }

        /// <summary>
        /// Sets the expecting outputs of the network
        /// </summary>
        /// <param name="expecteds">The expected outputs</param>
        public void Expecting(params double[] expecteds)
        {
            _expectedOutputs.Clear();

            foreach (var e in expecteds)
            {
                _expectedOutputs.Add(e);
            }
        }

        /// <summary>
        /// Back propagating result and update where necessary
        /// </summary>
        /// <param name="learnRate">The learning rate</param>
        private void BackPropagate(double learnRate)
        {
            Debug.WriteLine("\n----- Back Propagating -----");

            NeuralLayer input = GetInputLayer();
            NeuralLayer output = GetOutputLayer();

            // Output layer's weights update
            for(int i = _layers.Count - 1; i >= 1; --i)
            {
                double errorContribution = (output.Neurons[i].Output * (1 - output.Neurons[i].Output)) * (output.Neurons[i].Output - _expectedOutputs[i]) * output.Neurons[i-1].Output;
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
        /// Gets number of layers in the network
        /// </summary>
        public int LayerCount => _layers.Count;

        /// <summary>
        /// Removes a layer from the network
        /// </summary>
        /// <param name="layer">The layer to be removed</param>
        /// <returns></returns>
        public bool RemoveLayer(NeuralLayer layer)
        {
            return _layers.Remove(layer);
        }

        /// <summary>
        /// Gets the input layer of the network
        /// </summary>
        /// <returns></returns>
        public NeuralLayer GetInputLayer()
        {
            return _layers.ElementAt(0);
        }

        /// <summary>
        /// Gets the hidden layer of the network
        /// </summary>
        /// <returns></returns>
        public List<NeuralLayer> GetHiddenLayers()
        {
            return _layers.GetRange(1, _layers.Count - 2);
        }

        /// <summary>
        /// Gets the output layer of the network
        /// </summary>
        /// <returns></returns>
        public NeuralLayer GetOutputLayer()
        {
            return _layers.Last();
        }

        /// <summary>
        /// Returns the current outputs' values
        /// </summary>
        /// <returns></returns>
        public ArrayList GetOutputs()
        {
            ArrayList result = new ArrayList();
            foreach (var n in _layers[LayerCount-1].Neurons)
            {
                result.Add(n.Output);
            }

            return result;
        }
    }
}
