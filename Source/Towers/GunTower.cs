using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace SaY_DeF.Source.Towers
{
    class GunTower : IAttackingTower
    {
        public string Name { get { return "Gun Tower"; } }
        Image Foundation, Head;
        Button actualTower;
        static public Button UiBtn
        {
            get
            {
                Image UiContent = new Image() { Source = new BitmapImage(new Uri(@"TowersImages\GunTower\GunTower.png", UriKind.Relative)), Stretch = Stretch.Uniform, };
                RenderOptions.SetBitmapScalingMode(UiContent, BitmapScalingMode.NearestNeighbor);
                Button uiBtn = new Button() { Content = UiContent, IsEnabled = false };
                uiBtn.Click += UI_Click;
                return uiBtn;
            }
        }
        public Button ActualTower { get { return actualTower; } }
        public GunTower()
        {
            
            Foundation = new Image() { Source = new BitmapImage(new Uri(@"TowersImages\GunTower\GunTower_Found_lvl1.png", UriKind.Relative)), Stretch = Stretch.Uniform, };
            RenderOptions.SetBitmapScalingMode(Foundation, BitmapScalingMode.NearestNeighbor);
            Head = new Image() { Source = new BitmapImage(new Uri(@"TowersImages\GunTower\GunTowerHead.png", UriKind.Relative)), Stretch = Stretch.Uniform, };
            RenderOptions.SetBitmapScalingMode(Head, BitmapScalingMode.NearestNeighbor);
        }

        static public void UI_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gun Tower Building");
        }
        public void StartToCheckForEnemy()
        {
            
        }
        public void CheckForEnemy()
        {
            
        }
        public void Attack()
        {
            
        }

        public void Build()
        {
            
        }

        public void Upgrade()
        {
            
        }

    }
}
