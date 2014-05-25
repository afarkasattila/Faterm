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
using FaTherm.Model.Tokenizer;

namespace FaTherm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DeterministicFiniteAutomaton da = new DeterministicFiniteAutomaton();

            da.ReadCharacter('1');
            da.ReadCharacter('0');
            da.ReadCharacter('9');
            da.ReadCharacter('0');
            da.ReadCharacter('+');
            da.ReadCharacter('9');
            da.ReadCharacter('0');
            da.ReadCharacter('*');
            da.ReadCharacter('/');
        }
    }
}
