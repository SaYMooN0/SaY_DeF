using System;
using System.Configuration;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

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
        ResourceDictionary roundButtons = new ResourceDictionary(), roundTextBox=new ResourceDictionary(), roundListBox=new ResourceDictionary();
        public void SpawnConnectionWindow(ref Net_Connector nC)
        {
            NetCon = nC;
            NetCon.requsetGot += NC_requsetGot;
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
                Style = (Style)roundTextBox["RoundTextBox"],
                Text = "26.86.217.127"
            };
            c.Children.Add(TB_IPEnter);

            roundListBox.Source = new Uri("Source\\ListBoxStyle.xaml", UriKind.Relative);
            LB_Request = new ListBox(){ Style = (Style)roundListBox["ListBoxStyle"] };
            c.Children.Add(LB_Request);

            win.Content = c;
            win.Show();
        }

        private void NC_requsetGot(object? sender, Command c)
        {
            string str ="Ip:"+ c.Address.ToString() + "\n" + "Nickname:" + c.CommandArguments[0].ToString();

            //MessageBox.Show(str);
            win.Dispatcher.Invoke(() =>
            {
                AddNewGridToListBox(c.Address.ToString(), c.CommandArguments[0].ToString());
            });

            MessageBox.Show(LB_Request.Items.Count.ToString());
        }
        private void AddNewGridToListBox(string Ip, string Nick)
        {
            Grid item = new Grid
            {
                Width = LB_Request.Width * 0.85,
                Height = LB_Request.Height / 3,
            };
            item.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.6, GridUnitType.Star) });
            item.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.2, GridUnitType.Star) });
            item.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.2, GridUnitType.Star) });

            // добавляем элементы в Grid
            TextBlock tb1 = new TextBlock() { Text = "Столбец 1", VerticalAlignment = VerticalAlignment.Center };
            Image Img_Yes = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri(@"images\check.png", UriKind.Relative)),
                Stretch= Stretch.Uniform
            };
            Image Img_No = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri(@"images\cross.png", UriKind.Relative)),
                Stretch = Stretch.Uniform
            };
            TextBlock tb3 = new TextBlock() { Text = "Столбец 3", VerticalAlignment = VerticalAlignment.Center };

            Grid.SetColumn(tb1, 0); 
            Grid.SetColumn(Img_Yes, 1); 
            Grid.SetColumn(Img_No, 2); 

            item.Children.Add(tb1);
            item.Children.Add(Img_Yes);
            item.Children.Add(Img_No);
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
            NetCon.Send(CommandManager.GetConnectionRequest(Settings.myNick), reciever);
            
        }
        private void WinSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ButtonReactionToSizeChang();
            TextBoxReactionToSizeChang();
            listBoxReactionToSizeChang();
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
        private void listBoxReactionToSizeChang()
        {
            LB_Request.Width = win.Width  / 2.4;
            LB_Request.Height = win.Height / 3.4;
            LB_Request.BorderThickness = new Thickness(TB_IPEnter.Height / 18 + 4);
            LB_Request.FontSize = TB_IPEnter.Width / 7.8;

            Canvas.SetLeft(LB_Request, (win.Width - LB_Request.Width) / 2);
            Canvas.SetTop(LB_Request, win.Height / 12);
        }
    }
}
