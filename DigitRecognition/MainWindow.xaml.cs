using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool[,] input = new bool[4, 4];
        Neuron[] network1 = new Neuron[16];
        public MainWindow()
        {
            InitializeComponent();
            foreach (Button btn in grdInput.Children)
            {
                btn.Background = Brushes.Red;
                btn.Focusable = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var col = int.Parse(((sender as Button).Content as string)[0].ToString());
            var row = int.Parse(((sender as Button).Content as string)[1].ToString());
            input[row, col] = !input[row, col];
            if (input[row, col]) (sender as Button).Background=Brushes.Green;
            else (sender as Button).Background = Brushes.Red;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 16; i++)
                network1[i]=new Neuron() { Value = (input[i / 4, i % 4]) ? 1 : 0 };
            Neuron UpLine = new Neuron();
            for (int i = 0; i < 4; i++)
                UpLine.neuronWithWeights.Add(new NeuronWithWeight() {NeuronItem=network1[i],Weight=0.25 });
            Neuron DownLine = new Neuron();
            for (int i = 12; i < 16; i++)
                DownLine.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = network1[i], Weight = 0.25 });
            Neuron MidLineZ = new Neuron();
            Neuron MidLineS = new Neuron();
            //TODO: learnning
            for (int i = 4; i < 12; i++)
            {
                double weightS = 0.125;
                double weightZ = 0.125;
                if (i == 9) weightS = 0.25;
                if (i == 5) weightZ = -0.125;
                if (i == 10) weightZ = 0.25;
                if (i == 6) weightS = -0.125;
                MidLineS.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = network1[i], Weight = weightS });
                MidLineZ.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = network1[i], Weight = weightZ });
            }

            Neuron Z = new Neuron();
            Z.neuronWithWeights.Add(new NeuronWithWeight() {NeuronItem = UpLine,Weight=1.0/3 });
            Z.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = MidLineZ, Weight = 1.0 / 3 });
            Z.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = DownLine, Weight = 1.0 / 3 });
            Neuron S = new Neuron();
            S.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = UpLine, Weight = 1.0 / 3 });
            S.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = MidLineS, Weight = 1.0 / 3 });
            S.neuronWithWeights.Add(new NeuronWithWeight() { NeuronItem = DownLine, Weight = 1.0 / 3 });

            S.Calculate();
            Z.Calculate();

            if (Z.Value > S.Value) MessageBox.Show("Z");
            else MessageBox.Show("S");
        }
    }
}
