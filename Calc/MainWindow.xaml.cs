using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Calc
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        double result;
        string prevOps;
        double prevNum;
        StringBuilder token;
        int sign;
        public MainWindow()
        {
            InitializeComponent();
            token = new StringBuilder();
            prevOps = "";
            sign = 1;
        }

        private void updateDisplay()
        {
            string text = result.ToString();
            if (token.Length > 0)
            {
                text = token.ToString();
                if (sign < 0)
                {
                    text = "-" + text;
                }
            }
            resultDisplay.Text = text.ToString();
        }

        private double calc(double lhs, double rhs, string ops)
        {
            switch (ops)
            {
                case "+":
                    return lhs + rhs;
                case "-":
                    return lhs - rhs;
                case "×":
                    return lhs * rhs;
                case "÷":
                    return lhs / rhs;
                case "%":
                    return lhs % rhs;
                default:
                    return rhs;
            }
        }

        private void ops_Click(object sender, RoutedEventArgs e)
        {
            string currentOps = ((Button)sender).Content.ToString();
            Debug.Print(token.ToString());
            if (token.Length > 0)
            {
                double currentNum = double.Parse(token.ToString()) * sign;
                result = calc(result, currentNum, prevOps);
                prevNum = currentNum;
            } else if (currentOps == "=")
            {
                result = calc(result, prevNum, prevOps);
            }
            if (currentOps != "=")
            {
                prevOps = currentOps;
            }
            token.Clear();
            updateDisplay();
            sign = 1;
        }

        private void numbutton_Click(object sender, RoutedEventArgs e)
        {
            token.Append(((Button)sender).Content);
            updateDisplay();
        }

        private void buttonAC_Click(object sender, RoutedEventArgs e)
        {
            result = 0;
            token.Clear();
            prevNum = 0;
            prevOps = "";
            updateDisplay();
        }

        private void buttonSign_Click(object sender, RoutedEventArgs e)
        {
            if (sign > 0)
            {
                sign = -1;
            } else
            {
                sign = 1;
            }
            updateDisplay();
        }
    }
}
