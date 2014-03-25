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

        public void Train(params double[] datas)
        {
            FeedForward(datas);

            /// Back-propagate Errors and Update Weights
            BackPropagate();
        }

        public void Expecting(params double[] expecteds)
        {
            m_ExpectedOutputs.Clear();

            foreach (var e in expecteds)
            {
                m_ExpectedOutputs.Add(e);
            }
        }

        private void BackPropagate()
        {
            double deltaWeight = 0.0;
            double error = 0.0;

            // Output Errors
            for (int j = 0; j < m_Layers[LayerCount-1].m_Neurons.Count; ++j)
            {
                error = m_ExpectedOutputs[j] - m_Layers[LayerCount-1].m_Neurons[j].Outputs;
                Error = ((m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].m_Neurons[j].Outputs) * (m_ExpectedOutputs[j] - m_Layers[LayerCount - 1].m_Neurons[j].Outputs)) / 2;
                deltaWeight = error * m_Layers[LayerCount - 1].m_Neurons[j].Outputs * (1 - m_Layers[LayerCount-1].m_Neurons[j].Outputs);
                m_Layers[LayerCount - 1].m_Neurons[j].Weights += deltaWeight * 0.01;
            }

            // Hidden Layers Errors
            for (int i = LayerCount-2; i >= 1; --i)
            {
                double hError = 0.0;

                // From output's weight
                for (int j = 0; j <= m_Layers[LayerCount - 1].m_Neurons.Count - 1; ++j )
                {
                    hError += m_Layers[LayerCount-1].m_Neurons[j].Weights * m_Layers[i].m_Neurons[j].Weights;
                }

                foreach (var neuron in m_Layers[i].m_Neurons)
                {
                    neuron.Weights = hError;
                }
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
