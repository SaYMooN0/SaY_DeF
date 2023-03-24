using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SaY_DeF.Source
{
    class Game
    {
        SolidColorBrush brushWhite = new SolidColorBrush(Colors.White);
        SolidColorBrush brushYellow = new SolidColorBrush(Colors.Yellow);
        Canvas currentScreen;
        Canvas gameScreen;
        IPAddress myIP, enIP;
        string myNick, enNick;
        public Game(IPAddress m, IPAddress en, string mN, string enN)
        {
            myIP = m;
            enIP = en;
            myNick = mN;
            enNick = enN;
        }
        public Canvas RecieveScreen() { return currentScreen; }
        public void SetGameScreen(){ currentScreen = gameScreen;}
    }
}
