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

namespace Wpf2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += LoadedWindow;
            System.Windows.Media.CompositionTarget.Rendering += UpdateScene;
        }

        private void UpdateScene(object sender, EventArgs e)
        {
            Translate.X += 1;
        }
        private void LoadedWindow(object sender, RoutedEventArgs e)
        {

            Rotate.Angle = 90;
            Translate.X = 200;
            Translate.Y = -150;
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {

        }

        private void OnRestart(object sender, RoutedEventArgs e)
        {

        }

        private void OnPause(object sender, RoutedEventArgs e)
        {

        }
    }

	
}
