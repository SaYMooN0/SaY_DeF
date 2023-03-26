using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        List<IPAddress> sendedRequests = new List<IPAddress>();
        Dictionary<IPAddress, string> requsts=new Dictionary<IPAddress,string>();
        ResourceDictionary roundButtons = new ResourceDictionary(), roundTextBox=new ResourceDictionary(), roundListBox=new ResourceDictionary();
        public event EventHandler<GameArgs> gameStartIsReady;
        public void SpawnConnectionWindow(ref Net_Connector nC)
        {
            NetCon = nC;
            NetCon.requestGot += NC_requsetGot;
            NetCon.positiveResponse += Nc_PositiveResponse;
            win = new Window();
            win.Height = 400;
            win.Width = 600;
            win.SizeChanged += WinSizeChanged;
            Canvas c = new Canvas() { Background = backColor };

            roundButtons.Source = new Uri("resources\\ButtonStyle.xaml", UriKind.Relative);
            ButtonSend = new Button()
            {
                Style = (Style)roundButtons["ButtonStyle"],
                Content = "Connect",
                Foreground = brightColor,
                FontWeight = FontWeights.Bold,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            ButtonSend.Click += sendButtonClicked;
            c.Children.Add(ButtonSend);

            roundTextBox.Source = new Uri("resources\\TextBoxStyle.xaml", UriKind.Relative);
            TB_IPEnter = new TextBox()
            {
                Foreground = brightColor,
                FontWeight = FontWeights.Bold,
                BorderBrush = brightColor,
                Background = backColor,
                TextAlignment = TextAlignment.Center,
                Style = (Style)roundTextBox["RoundTextBox"],
                Text = "26.86.217.127"
            };
            c.Children.Add(TB_IPEnter);

            roundListBox.Source = new Uri("resources\\ListBoxStyle.xaml", UriKind.Relative);
            LB_Request = new ListBox(){ Style = (Style)roundListBox["ListBoxStyle"] };
        
            c.Children.Add(LB_Request);

            win.Content = c;
            win.Show();
        }

        private void NC_requsetGot(object? sender, Command c)
        {
            if (requsts.ContainsKey(c.Address))
            {
                return;
            }
            requsts.Add(c.Address, c.CommandArguments[0]);
            win.Dispatcher.Invoke((Delegate)(() =>
            {
                AddNewGridToListBox(c.Address.ToString(), c.CommandArguments[0].ToString());
                listBoxReactionToSizeChange();
            }));
        }
        private void Nc_PositiveResponse(object? sender, Command c)
        {
            string nick = c.CommandArguments[0].Replace("\0","");
            GameArgs ga = new GameArgs(Settings.IP, Settings.myNick, c.Address, nick);
          
            gameStartIsReady.Invoke(this, ga);
        }
        private void AddNewGridToListBox(string Ip, string Nick)
        {
            Grid item = new Grid
            {
                Width = LB_Request.Width * 0.85,
                Height = LB_Request.Height / 3.8,
                
            };
            item.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.6, GridUnitType.Star) });
            item.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.2, GridUnitType.Star) });
            item.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.2, GridUnitType.Star) });

            TextBlock tb1 = new TextBlock() { Text = Nick,FontWeight=FontWeights.Bold, VerticalAlignment = VerticalAlignment.Center, Foreground=brightColor };
            Button btnYes = new Button()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Style = (Style)roundButtons["ButtonStyle"],
                BorderBrush = Brushes.Transparent,
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri(@"images\check.png", UriKind.Relative)),
                    Stretch = Stretch.Uniform
                }
            };
            btnYes.Click += ButtonYesClicked;
            Button btnNo = new Button()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Style= (Style)roundButtons["ButtonStyle"],
                BorderBrush = Brushes.Transparent,
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri(@"images\cross.png", UriKind.Relative)),
                    Stretch = Stretch.Uniform
                }
            };
            btnNo.Click+=ButtonNoClicked;
            Grid.SetColumn(tb1, 0);
            Grid.SetColumn(btnYes, 1);
            Grid.SetColumn(btnNo, 2);

            item.Children.Add(tb1);
            item.Children.Add(btnYes);
            item.Children.Add(btnNo);
            LB_Request.Items.Add(item);
        }
        private void sendButtonClicked(object sender, RoutedEventArgs e)
        {
            IPAddress reciever;
            if (!IPAddress.TryParse(TB_IPEnter.Text, out reciever))
            {
                MessageBox.Show("Ip Error");
                return;
            }
            TB_IPEnter.Text = "";
            if (sendedRequests.Contains(reciever))
            {
                Task.Run(() => win.Dispatcher.Invoke(new Action(async delegate
                {
                    ButtonSend.FontSize*=0.7;
                    ButtonSend.Content = "You've already send";
                    await Task.Delay(960);
                    ButtonSend.Content = "Connect ";
                    ButtonSend.FontSize /=0.7;
                })));
                return;
            }
            if (reciever.ToString() == Settings.IP.ToString())
            {
                Task.Run(() => win.Dispatcher.Invoke(new Action(async delegate
                {
                    ButtonSend.FontSize *=0.62;
                    ButtonSend.Content = "You can't send yourself";
                    await Task.Delay(960);
                    ButtonSend.Content = "Connect ";
                    ButtonSend.FontSize/=0.62;
                })));
                return;
            }
            sendedRequests.Add(reciever);
            if (NetCon.Send(CommandManager.GetConnectionRequest(Settings.myNick), reciever))
            {

                Task t = Task.Run(() => win.Dispatcher.Invoke(new Action(async delegate
                {
                    ButtonSend.FontSize *= 0.75;
                    ButtonSend.Content = "Request sended";
                    await Task.Delay(600);
                    ButtonSend.Content = "Connect ";
                    ButtonSend.FontSize /= 0.75;
                })));
            }
        }
        private void ButtonYesClicked(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Grid gr = b.Parent as Grid;
            TextBlock txt = gr.Children[0] as TextBlock;
            string nick = txt.Text;
            IPAddress ip = requsts.FirstOrDefault(x => x.Value == nick).Key;
            nick = nick.Replace("\0","");
            NetCon.Send(CommandManager.GetRequestAgreed(Settings.myNick), ip);
            GameArgs ga = new GameArgs(Settings.IP, Settings.myNick, ip, nick);
            gameStartIsReady.Invoke(this, ga);

        }
        private void ButtonNoClicked(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Grid gr = b.Parent as Grid;
            LB_Request.Items.Remove(gr);
            TextBlock txt = gr.Children[0] as TextBlock;
            string nick = txt.Text;
            IPAddress ip = requsts.FirstOrDefault(x => x.Value == nick).Key;
            requsts.Remove(ip);

        }
        private void WinSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ButtonReactionToSizeChang();
            TextBoxReactionToSizeChang();
            listBoxReactionToSizeChange();
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
        private void listBoxReactionToSizeChange()
        {
            LB_Request.Width = win.Width  / 2.4;
            LB_Request.Height = win.Height / 3.2;
            LB_Request.BorderThickness = new Thickness(TB_IPEnter.Height / 18 + 4);
            LB_Request.FontSize = TB_IPEnter.Width / 7.8;
            foreach (Grid item in LB_Request.Items)
            {
                item.Width = LB_Request.Width * 0.85;
                item.Height = LB_Request.Height / 3.8;
                TextBlock tb1 = item.Children[0] as TextBlock;
                tb1.FontSize = (item.Width + item.Height) / 10;
                
            }
            Canvas.SetLeft(LB_Request, (win.Width - LB_Request.Width) / 2);
            Canvas.SetTop(LB_Request, win.Height / 12);
        }
        public void WindowClose(object sender, GameArgs gameArgs) {
            win.Dispatcher.Invoke((Delegate)(() =>
            {
                win.Close();
            }));
        }
    }
}
