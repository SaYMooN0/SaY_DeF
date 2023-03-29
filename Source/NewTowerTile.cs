using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Threading.Tasks;

namespace SaY_DeF.Source
{
    class NewTowerTile
    {
        public Button addBtn { get; }
        ResourceDictionary roundButtons = new ResourceDictionary();
        SolidColorBrush brightColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8C8C8C"));
        SolidColorBrush backColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DCDCDC"));
        public NewTowerTile()
        {
            roundButtons.Source = new Uri("resources\\ButtonStyle.xaml", UriKind.Relative);
            addBtn = new Button()
            {
                BorderThickness = new Thickness(4),
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri(@"images\plus.png", UriKind.Relative)),
                    Stretch = Stretch.Uniform
                },
                Style = (Style)roundButtons["ButtonStyle"],
                BorderBrush = brightColor,
                Background=backColor
            };
            addBtn.Click += AddBtn_Click;

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => addBtn.Dispatcher.Invoke(new Action(async delegate
            {
                addBtn.Background = brightColor;
                await Task.Delay(560);
                addBtn.Background=backColor;
            })));
        }
    }
}
