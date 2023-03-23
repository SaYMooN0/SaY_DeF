using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SaY_DeF.Source
{
    class Game
    {
        SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
        public Game()
        {

        }
        public Canvas RecieveScreen()
        {
            Canvas screen = new Canvas()
            {
                Background = brushWhite
            };
            return screen;
        }
    }
}
