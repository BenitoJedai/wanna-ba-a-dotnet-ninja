using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotNetNinjaQuiz.Controls
{
    /// <summary>
    /// Interaction logic for GameOverDialog.xaml
    /// </summary>
    public partial class GameOverDialog : UserControl
    {

        public GameOverDialog()
        {
            InitializeComponent();
        }

        public void Show(string levelName)
        {
            _levelName.Text = levelName;
            Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }  
    }
}
