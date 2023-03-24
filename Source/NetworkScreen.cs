using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SaY_DeF.Source
{
    class NetworkScreen
    {
        SolidColorBrush brightColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#61C0BF"));
        SolidColorBrush backColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BBDED6"));
        ListBox LB_Request;
        TextBox TB_IPEnter;
        Button ButtonSend;
        Window win;
        Net_Connector NetCon;
        ResourceDictionary roundButtons = new ResourceDictionary(), roundTextBox=new ResourceDictionary();
        public void SpawnConnectionWindow(ref Net_Connector nC)
        {
            NetCon= nC;
            win = new Window();
            win.Height = 400;
            win.Width = 600;
            win.SizeChanged += WinSizeChanged;
            Canvas c = new Canvas() { Background = backColor };

            roundButtons.Source = new Uri("Source\\ButtonStyle.xaml", UriKind.Relative);
            ButtonSend = new Button() { Style = (Style)roundButtons["ButtonStyle"], Content = "Connect", Foreground = brightColor, FontWeight = FontWeights.Bold };
            ButtonSend.Click += sendButtonClicked;
            c.Children.Add(ButtonSend);

            roundTextBox.Source = new Uri("Source\\TextBoxStyle.xaml", UriKind.Relative);
            TB_IPEnter = new TextBox()
            {
                Foreground = brightColor,
                FontWeight = FontWeights.Bold,
                BorderBrush = brightColor,
                Background = backColor,
                TextAlignment = TextAlignment.Center,
                Style = (Style)roundTextBox["RoundTextBox"]
            };
            c.Children.Add(TB_IPEnter);

            win.Content = c;
            win.Show();
        }
        private void sendButtonClicked(object sender, RoutedEventArgs e)
        {
            IPAddress reciever;
            if (!IPAddress.TryParse(TB_IPEnter.Text, out reciever))
            {
                MessageBox.Show("Ip Error");
                return;
            }
            NetCon.Send(CommandManager.GetConnectionRequest(Settings.myNick), reciever);
            
        }
        private void WinSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ButtonReactionToSizeChang();
            TextBoxReactionToSizeChang();
        }
        private void ButtonReactionToSizeChang()
        {
            ButtonSend.Width = (win.Width + win.Height) / 5.85;
            ButtonSend.Height = ButtonSend.Width / 2.9;
            ButtonSend.BorderThickness = new Thickness(ButtonSend.Height / 13 + 2);
            ButtonSend.FontSize = ButtonSend.Width / 7.8;
            Canvas.SetLeft(ButtonSend, (win.Width - ButtonSend.Width) / 2);
            Canvas.SetBottom(ButtonSend, (win.Height) / 40);
        }
        private void TextBoxReactionToSizeChang()
        {
            TB_IPEnter.Width= (win.Width + win.Height) / 5.9;
            TB_IPEnter.Height = TB_IPEnter.Width / 3.9;
            TB_IPEnter.BorderThickness = new Thickness(TB_IPEnter.Height / 13 + 4);
            TB_IPEnter.FontSize = TB_IPEnter.Width / 7.8;

            Canvas.SetLeft(TB_IPEnter, (win.Width - TB_IPEnter.Width) / 2);
            Canvas.SetBottom(TB_IPEnter, win.Height / 40+ButtonSend.Height+10);
        }
    }
}
