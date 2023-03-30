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
        SolidColorBrush backColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DCDCDC"));
        public Point Coordinate { get; }
        public NewTowerTile(int y, int x)
        {
            roundButtons.Source = new Uri("resources\\ButtonStyle.xaml", UriKind.Relative);
            Coordinate = new Point(y, x);   
            Image btnContent = new Image()
            {
                Source = new BitmapImage(new Uri(@"images\fieldTyles\AddNewTowerTyle.png", UriKind.Relative)),
                Stretch = Stretch.Uniform,
                SnapsToDevicePixels = true
            };
            RenderOptions.SetBitmapScalingMode(btnContent, BitmapScalingMode.NearestNeighbor);
            addBtn = new Button()
            {
                Content = btnContent,
                Style = (Style)roundButtons["ButtonStyle"],
                Background=backColor
            };
            
            addBtn.Click += AddBtn_Click;

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => addBtn.Dispatcher.Invoke(new Action(async delegate
            {
                MessageBox.Show($"New tower button clicked on y:{Coordinate.Y} x:{Coordinate.X}");
                await Task.Delay(560);
                addBtn.Background=backColor;
            })));
        }
    }
}
