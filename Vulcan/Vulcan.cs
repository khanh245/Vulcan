using System;
using System.Collections;
using System.Collections.Generic;
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
            if (datas.Count() == m_Layers[0].m_Neurons.Count())
            {
                #region Feed Forward Algorithm
                /// Input Layers
                for (int i = 0; i < m_Layers[0].m_Neurons.Count; ++i)
                {
                    m_Layers[0].m_Neurons[i].Inputs = datas[i];
                    m_Layers[0].m_Neurons[i].Outputs = datas[i] * m_Layers[0].m_Neurons[i].Weights;
                    m_Layers[0].m_Neurons[i].Outputs = Activators.SigmoidFunction(m_Layers[0].m_Neurons[i].Outputs);
                }

                /// Hidden Layers
                for (int i = 1; i < m_Layers.Count - 1; i++)
                {
                    double inputToHidden = 0.0;

                    foreach (var neuron in m_Layers[i - 1].m_Neurons)
                    {
                        inputToHidden += neuron.Outputs;
                    }

                    foreach (var neuron in m_Layers[i].m_Neurons)
                    {
                        neuron.Inputs = inputToHidden;
                        neuron.Outputs = inputToHidden * neuron.Weights;
                        neuron.Outputs = Activators.SigmoidFunction(neuron.Outputs);
                    }
                }

                /// Output Layers
                foreach (var neuron in m_Layers[m_Layers.Count - 1].m_Neurons)
                {
                    double hiddenToOutput = 0.0;

                    foreach (var n in m_Layers[m_Layers.Count - 2].m_Neurons)
                    {
                        hiddenToOutput += n.Outputs;
                    }

                    neuron.Inputs = hiddenToOutput;
                    neuron.Outputs = hiddenToOutput * neuron.Weights;
                    neuron.Outputs = Activators.SigmoidFunction(neuron.Outputs);
                }
                #endregion
            }
            else throw new ArgumentOutOfRangeException();
        }

        public void AddLayer(DefaultNeuralLayer layer)
        {
            m_Layers.Add(layer);
        }

        public void Train(double learnRate, params double[] datas)
        {
            FeedForward(datas);

            /// Back-propagate Errors and Update Weights
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
            double deltaWeight = 0.0;
            double error = 0.0;
            double errorSum = 0.0;
            double tempSum = 0.0;

            // Output Errors
            for (int j = 0; j < m_Layers[LayerCount-1].m_Neurons.Count; ++j)
            {
                Error = ((m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].m_Neurons[j].Outputs) * (m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].m_Neurons[j].Outputs)) / 2;
                error = (m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].m_Neurons[j].Outputs) * m_Layers[LayerCount - 1].m_Neurons[j].Outputs * (1 - m_Layers[LayerCount - 1].m_Neurons[j].Outputs);
                deltaWeight = learnRate * error * m_Layers[LayerCount - 1].m_Neurons[j].Outputs + m_Layers[LayerCount - 1].m_Neurons[j].Weights * 0.1;
                //deltaWeight = Activators.DerivativeSigmoid(deltaWeight);
                m_Layers[LayerCount - 1].m_Neurons[j].Weights += deltaWeight;
                errorSum += m_Layers[LayerCount - 1].m_Neurons[j].Weights * error;
            }

            // Hidden Layers Errors
            for (int i = LayerCount-2; i >= 1; --i)
            {
                for (int j = 0; j < m_Layers[i].m_Neurons.Count; ++j)
                {
                    error = m_Layers[i].m_Neurons[j].Outputs * (1 - m_Layers[i].m_Neurons[j].Outputs) * errorSum;
                    deltaWeight = learnRate * error * m_Layers[i].m_Neurons[j].Inputs + m_Layers[i].m_Neurons[j].Weights * 0.1;
                    m_Layers[i].m_Neurons[j].Weights += deltaWeight;
                    tempSum += m_Layers[i].m_Neurons[j].Weights * error;
                }

                errorSum = tempSum;
                tempSum = 0;
            }

            // Input Layer Errors
            foreach (var neuron in m_Layers[0].m_Neurons)
            {
                error = neuron.Outputs * (1 - neuron.Outputs) * errorSum;
                deltaWeight = learnRate * error * neuron.Inputs + neuron.Weights * 0.1;
                neuron.Weights += deltaWeight;
            }
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

        public DefaultNeuralLayer GetNeuralLayer(int id)
        {
            return m_Layers.ElementAt(id);
        }

        public ArrayList GetOutputs()
        {
            ArrayList result = new ArrayList();
            foreach (var n in m_Layers[LayerCount-1].m_Neurons)
            {
                result.Add(n.Outputs);
            }

            return result;
        }
    }
}
