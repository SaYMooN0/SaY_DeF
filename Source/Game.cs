using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaY_DeF.Source
{
    class Game
    {
        SolidColorBrush brushBack, brushMenu;
        Canvas gameScreen, BottomMenu, fullScreen;
        GameArgs Args;
        Button Btn_GetEnemyScreen;
        ResourceDictionary roundButtons = new ResourceDictionary();
        double sWidth, sHeight;
        Net_Connector Net;
        List<NewTowerTile> ListOfNewTowerTiles = new List<NewTowerTile>();
        Window win;
        Grid TowerGrid;
        List<Button> attackTowers= new List<Button>();
        Label LB_YourTower;

        public Game(GameArgs gArgs, Window w, Net_Connector net)
        {
            win = w;
            Net = net;
            Args = gArgs;
            sWidth = SystemParameters.PrimaryScreenWidth;
            sHeight = SystemParameters.PrimaryScreenHeight;
            roundButtons.Source = new Uri("resources\\ButtonStyle.xaml", UriKind.Relative);



            Net.screenRequstGot += screenRequstGot;
            Net.SetScreen += Net_SetScreen;
            win.Dispatcher.Invoke(() =>
            {
                FormTowersGrid();
                brushBack = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8E8"));
                brushMenu = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
               
                fullScreen = new Canvas() { Background = new SolidColorBrush(Colors.Yellow) };
                gameScreen = new Canvas() { Background = brushBack };
                BottomMenu = new Canvas() { Background = brushMenu };
                fullScreen.Children.Add(gameScreen);
                BottomMenu.Children.Add(TowerGrid);
                fullScreen.Children.Add(BottomMenu);
                
                
                Btn_GetEnemyScreen = new Button() { Style = (Style)roundButtons["ButtonStyle"], Content = "Enemy Scren" };
                Btn_GetEnemyScreen.Click += Btn_GetEnemyScreen_Click;
                fullScreen.Children.Add(Btn_GetEnemyScreen);
                win.MouseRightButtonDown += Win_MouseRightButtonDown;
                win.SizeChanged += Win_SizeChanged;
                win.Content = fullScreen;
            }
            );
            MessageBox.Show("Please maximize the window");
        }

        private void Win_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Point position = e.GetPosition(e.Source as UIElement);
            AddNewTowerTile(position);
        }

        private void Net_SetScreen(object? sender, Command e)
        {
            MessageBox.Show("enemies screen set");
        }

        private void screenRequstGot(object? sender, Command e)
        {
            MessageBox.Show("screen request got");
        }

        private void Btn_GetEnemyScreen_Click(object sender, RoutedEventArgs e)
        {
            string command = CommandManager.GetScreenRequest();
            Net.Send(command, Args.EnIP);
            MessageBox.Show("request sended");
        }

        private void Win_SizeChanged(object sender, SizeChangedEventArgs e)
        {


            fullScreen.Width =sWidth;
            fullScreen.Height= sHeight;

            gameScreen.Width = sWidth;
            gameScreen.Height = sHeight*0.78;
            Canvas.SetTop(gameScreen, 0);
            Canvas.SetLeft(gameScreen, 0);

            BottomMenu.Width = fullScreen.Width;
            BottomMenu.Height = fullScreen.Height * 0.22;
            Canvas.SetTop(BottomMenu, fullScreen.Height * 0.78);

            Btn_GetEnemyScreen.Height = BottomMenu.Height / 4;
            Btn_GetEnemyScreen.Width = Btn_GetEnemyScreen.Height * 3;
            Btn_GetEnemyScreen.FontSize = Btn_GetEnemyScreen.Height / 4.2;
            Btn_GetEnemyScreen.BorderThickness = new Thickness(Btn_GetEnemyScreen.FontSize / 3.8);
            Canvas.SetBottom(Btn_GetEnemyScreen, Btn_GetEnemyScreen.Height * 1.5);
            Canvas.SetRight(Btn_GetEnemyScreen, Btn_GetEnemyScreen.Height / 2);

            TowerGrid.Height = BottomMenu.Height * 0.7;
            TowerGrid.Width = TowerGrid.Height * 2;
            Canvas.SetLeft(TowerGrid, (BottomMenu.Width - TowerGrid.Width) / 4);
            Canvas.SetTop(TowerGrid, (BottomMenu.Height - TowerGrid.Height) / 1.9);
        }
        private void AddNewTowerTile(Point pos)
        {
            NewTowerTile newTowerTile = new NewTowerTile();
            newTowerTile.addBtn.Click += BuildTower;
            Button btn = newTowerTile.addBtn;
            
            ListOfNewTowerTiles.Add(newTowerTile);
            fullScreen.Children.Add(btn);
            pos.X -= btn.Width / 2;
            pos.Y -= btn.Height / 2;
            if (pos.X < 0)
                pos.X = 0;
            if (pos.Y < 0)
                pos.Y = 0;
            Canvas.SetLeft(btn, pos.X);
            Canvas.SetTop(btn, pos.Y);

        }
        private void FormTowersGrid()
        {
            TowerGrid = new Grid() {Background=new SolidColorBrush(Colors.LightCoral), ShowGridLines=true };
            for (int i = 0; i < 6; i++)
            {
                TowerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 3; i++)
            {
                TowerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }


            var image = new Image()
            {
                Source = new BitmapImage(new Uri(@"Towers/GunTower.png", UriKind.Relative)),
                Stretch = Stretch.Uniform,
            };
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);
            Button GunTower = new Button()
            {
                Content = image,
                IsEnabled=false
            };
            GunTower.Click += GunTower_Click;
            attackTowers.Add(GunTower);
            Grid.SetRow(GunTower, 0);
            Grid.SetColumn(GunTower, 0);
            TowerGrid.Children.Add(GunTower);
        }

        private void GunTower_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("GunTower set is building");
        }

        private void BuildTower(object sender, RoutedEventArgs e)
        {
            foreach (Button b in attackTowers)
                b.IsEnabled = true;
        }
    }
}
