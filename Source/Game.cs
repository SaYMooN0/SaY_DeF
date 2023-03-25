using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace SaY_DeF.Source
{
    class Game
    {
        SolidColorBrush  brushBack, brushMenu;
        Canvas gameScreen, BottomMenu;
        GameArgs Args;
        Button Btn_GetEnemyScreen, Btn_ReturnScreen;
        ResourceDictionary roundButtons = new ResourceDictionary();
        double sWidth, sHeight;
        Net_Connector Net;
        Window win;
        public Game(GameArgs gArgs, Window w, Net_Connector net)
        {
            win = w;
            Net = net;
            Args = gArgs;
            sWidth= SystemParameters.PrimaryScreenWidth;
            sHeight = SystemParameters.PrimaryScreenHeight;
            roundButtons.Source = new Uri("resources\\ButtonStyle.xaml", UriKind.Relative);
            Net.screenRequstGot += screenRequstGot;
            Net.SetScreen += Net_SetScreen;
            win.Dispatcher.Invoke(() => 
            {
                brushBack = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8E8"));
                brushMenu= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
                gameScreen = new Canvas() { Background = brushBack };
                BottomMenu = new Canvas() { Background = brushMenu };
                gameScreen.Children.Add(BottomMenu);

                Btn_GetEnemyScreen = new Button() { Style = (Style)roundButtons["ButtonStyle"], Content="Enemy Scren" };
                Btn_GetEnemyScreen.Click += Btn_GetEnemyScreen_Click;
                gameScreen.Children.Add(Btn_GetEnemyScreen);

                win.SizeChanged += Win_SizeChanged;
                win.Content = gameScreen;
                }
            );
            MessageBox.Show("Please maximize the window");
        }

        private void Net_SetScreen(object? sender, Command e)
        {
            string xamlString = e.CommandArguments[0];
            win.Dispatcher.Invoke(() =>
            {
                Canvas canvas = (Canvas)XamlReader.Parse(xamlString);
                win.Content = canvas;
            });
        }

        private void screenRequstGot(object? sender, Command e)
        {
            var canvasToSend = getScreenToSend();
            var sb = new StringBuilder();
            var xamlWriter = XmlWriter.Create(sb);
            XamlWriter.Save(canvasToSend, xamlWriter);
            string xamlString = sb.ToString();
            if (!Net.Send(CommandManager.GetScreenToSend(xamlString), Args.EnIP))
            {
                MessageBox.Show("Failed to send data");
            }
        }

        private void Btn_GetEnemyScreen_Click(object sender, RoutedEventArgs e)
        {
            string command=CommandManager.GetScreenRequest();
            Net.Send(command, Args.EnIP);
        }

        private void Win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gameScreen.Width = sWidth;
            gameScreen.Height = sHeight;
            BottomMenu.Width = gameScreen.Width;
            BottomMenu.Height= gameScreen.Height*0.25;
            Canvas.SetTop(BottomMenu,gameScreen.Height*0.75);

            Btn_GetEnemyScreen.Height = BottomMenu.Height / 4;
            Btn_GetEnemyScreen.Width = Btn_GetEnemyScreen.Height*2;
        }
        private Canvas getScreenToSend()
        {
            Canvas screen = gameScreen;  
            screen.Children.Remove(Btn_GetEnemyScreen);
            return screen;
        }
    }
}
