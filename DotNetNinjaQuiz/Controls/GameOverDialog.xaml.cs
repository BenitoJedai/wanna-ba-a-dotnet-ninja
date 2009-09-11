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
            _youAreA.Text = getYouAreAText(levelName);
            _levelName.Text = levelName;
            Visibility = Visibility.Visible;
        }

        private string getYouAreAText(string levelName)
        {
            return StartsWithVowel(levelName) ?
                "You are an" :
                "You are a";
        }

        private bool StartsWithVowel(string text)
        {
            char[] vowels = new[] { 'A', 'E', 'I', 'O', 'U', 'Y' };
            for (int i = 0; i < vowels.Length; i++)
            {
                if (text[0] == vowels[i])
                    return true;
            }
            return false;
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }  
    }
}
