using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulcan.Core;
using Vulcan.Helpers;

namespace Vulcan
{
    public class Vulcan
    {
        private List<DefaultNeuralLayer> m_Layers = new List<DefaultNeuralLayer>();
        private List<double> m_ExpectedOutputs = new List<double>();

        public double Error { get; private set; }

        public Vulcan(int hidden)
        {
            Error = 0.0;

            DefaultNeuralLayer input = new DefaultNeuralLayer("Input");
            AddLayer(input);

            for (int i = 0; i < hidden; ++i)
            {
                DefaultNeuralLayer layer = new DefaultNeuralLayer(string.Format("Hidden{0}", i + 1));
                AddLayer(layer);
            }

            DefaultNeuralLayer output = new DefaultNeuralLayer("Output");
            AddLayer(output);
        }

        public void FeedForward(params double[] datas)
        {
            if (datas.Count() != m_Layers[0].Neurons.Count) throw new ArgumentException();

            Debug.WriteLine("\n----- Feeding Forward -----");

            int count = 0;

            /// The input layer        
            DefaultNeuralLayer input = GetInputLayer();

            /// The output layer
            DefaultNeuralLayer output = GetOutputLayer();

            Debug.WriteLine("Input Layer");

            /// Input layer flowthrough
            for (int i = 0; i < input.Neurons.Count; ++i)
            {
                input.Neurons[i].Input = datas[i];
                input.Neurons[i].Output = Activators.SigmoidFunction(input.Neurons[i].Input * input.Neurons[i].Weight);

                Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", i,  input.Neurons[i].Input, input.Neurons[i].Output);
            }

            Debug.Write("\n");

            /// Hidden layers flowthrough
            for (int i = 1; i < m_Layers.Count - 1; ++i)
            {
                double inputToHidden = 0.0;

                Debug.WriteLine("Hidden Layer {0}", i);

                foreach (var neuron in m_Layers[i-1].Neurons)
                {
                    inputToHidden += neuron.Output; // + bias
                }

                count = 0;
                foreach (var neuron in m_Layers[i].Neurons)
                {
                    neuron.Input = inputToHidden;
                    neuron.Output = Activators.SigmoidFunction(neuron.Input * neuron.Weight);

                    Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", count, neuron.Input, neuron.Output);
                    count++;
                }
            }

            count = 0;
            Debug.WriteLine("\nOutput Layer");

            /// Output layer flowthrough
            foreach (var neuron in output.Neurons)
            {
                double hiddenToOutput = 0.0;

                foreach (var n in GetHiddenLayers().Last().Neurons)
                {
                    hiddenToOutput += n.Output; // + bias
                }

                neuron.Input = hiddenToOutput;
                neuron.Output = Activators.SigmoidFunction(neuron.Input * neuron.Weight);

                Debug.WriteLine("Neuron {0} --- Input: {1}, Output {2}", count, neuron.Input, neuron.Output);
            }

            /// Calculating total error for neural network
            for (int i = 0; i < m_ExpectedOutputs.Count; ++i)
            {
                Error += (Math.Pow((output.Neurons[i].Output - m_ExpectedOutputs[i]), 2) / 2.0);
            }

            Debug.Write("\n");
            Debug.WriteLine("Net Error: {0}", Error);
        }

        public void AddLayer(DefaultNeuralLayer layer)
        {
            m_Layers.Add(layer);
        }

        public void Train(double learnRate, params double[] datas)
        {
            FeedForward(datas);
            BackPropagate(learnRate);
        }

        public void Expecting(params double[] expecteds)
        {
            m_ExpectedOutputs.Clear();

            foreach (var e in expecteds)
            {
                m_ExpectedOutputs.Add(e);
            }
        }

        private void BackPropagate(double learnRate)
        {
            Debug.WriteLine("\n----- Back Propagating -----");

            /// The input layer
            DefaultNeuralLayer input = GetInputLayer();

            /// The hidden layers
            List<DefaultNeuralLayer> hiddens = GetHiddenLayers();

            /// The output layer
            DefaultNeuralLayer output = GetOutputLayer();

            /**
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
            **/
        }

        public int LayerCount
        {
            get
            {
                return m_Layers.Count;
            }
        }

        public bool RemoveLayer(DefaultNeuralLayer layer)
        {
            return m_Layers.Remove(layer);
        }

        public DefaultNeuralLayer GetInputLayer()
        {
            return m_Layers.ElementAt(0);
        }

        public List<DefaultNeuralLayer> GetHiddenLayers()
        {
            return m_Layers.GetRange(1, m_Layers.Count - 2);
        }

        public DefaultNeuralLayer GetOutputLayer()
        {
            return m_Layers.Last();
        }

        public ArrayList GetOutputs()
        {
            ArrayList result = new ArrayList();
            foreach (var n in m_Layers[LayerCount-1].Neurons)
            {
                result.Add(n.Output);
            }

            return result;
        }
    }
}
