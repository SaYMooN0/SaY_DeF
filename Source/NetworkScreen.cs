using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SaY_DeF.Source
{
    class NetworkScreen
    {
        SolidColorBrush brightColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#61C0BF"));
        SolidColorBrush backColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBDED6"));
        public void SpawnConnectionWindow(ref Net_Connector nC)
        {
            Window win= new Window();
            Canvas c = new Canvas() { Background = backColor };
            win.Content = c;
            win.Height = 400;
            win.Width = 600;
            win.SizeChanged += WinSizeChanged;
            win.Show();
        }
        private void senfButtonClicked(object sender, RoutedEventArgs e)
        {
            //nC.Send(CommandManager.GetGameStartRequest(Settings.myNick));
        }
        private void WinSizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
