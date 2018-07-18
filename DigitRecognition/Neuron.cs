using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitRecognition
{
    class Neuron
    {
        public double Value { get; set; }
        public IList<NeuronWithWeight> neuronWithWeights = new List<NeuronWithWeight>();
        public void Calculate() {
            if (neuronWithWeights.Any()) {
                Value = 0;
                foreach (NeuronWithWeight n in neuronWithWeights)
                    n.NeuronItem.Calculate();
                foreach (NeuronWithWeight n in neuronWithWeights)
                    Value += n.NeuronItem.Value * n.Weight;
            }
        }
    }
    class NeuronWithWeight
    {
        public Neuron NeuronItem { get; set; }
        public double Weight { get; set; }
    }
}
