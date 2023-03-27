using SaY_DeF.Source.Towers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Point = System.Windows.Point;

namespace SaY_DeF.Source
{
    class Game
    {
        SolidColorBrush brushBack, brushMenu, brushMenuBright, brushMenuTheBrightest;
        Canvas gameScreen, BottomMenu, fullScreen;
        GameArgs Args;
        Button Btn_GetEnemyScreen;
        ResourceDictionary roundButtons = new ResourceDictionary();
        double sWidth, sHeight;
        Net_Connector Net;
        List<NewTowerTile> ListOfNewTowerTiles = new List<NewTowerTile>();
        Window win;
        Grid TowerAtckGrid, TowerResGrid;
        List<Button> UITowers= new List<Button>();
        Rectangle towerGridBack;
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
                FormTowerAtckGrid();
                FormTowerResGrid();
                brushBack = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8E8"));
                brushMenu = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
                brushMenuBright = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BEBEC4"));
                brushMenuTheBrightest = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ADADB2"));

                towerGridBack = new Rectangle() { Fill = brushMenuBright };

                fullScreen = new Canvas() { Background = new SolidColorBrush(Colors.Yellow) };
                gameScreen = new Canvas() { Background = brushBack };
                BottomMenu = new Canvas() { Background = brushMenu };
                fullScreen.Children.Add(gameScreen);
                BottomMenu.Children.Add(towerGridBack);
                BottomMenu.Children.Add(TowerAtckGrid);
                BottomMenu.Children.Add(TowerResGrid);
                fullScreen.Children.Add(BottomMenu);


                Btn_GetEnemyScreen = new Button() { Style = (Style)roundButtons["ButtonStyle"], Content = "Enemy Scren" };
                Btn_GetEnemyScreen.Click += Btn_GetEnemyScreen_Click;
                fullScreen.Children.Add(Btn_GetEnemyScreen);
                win.MouseRightButtonDown += Win_MouseRightButtonDown;
                win.SizeChanged += Win_SizeChanged;
                win.Content = fullScreen;
            });
            
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
            BottomMenu.Height = fullScreen.Height * 0.224;
            Canvas.SetTop(BottomMenu, fullScreen.Height * 0.778);

            Btn_GetEnemyScreen.Height = BottomMenu.Height / 4;
            Btn_GetEnemyScreen.Width = Btn_GetEnemyScreen.Height * 3;
            Btn_GetEnemyScreen.FontSize = Btn_GetEnemyScreen.Height / 4.2;
            Btn_GetEnemyScreen.BorderThickness = new Thickness(Btn_GetEnemyScreen.FontSize / 3.8);
            Canvas.SetBottom(Btn_GetEnemyScreen, Btn_GetEnemyScreen.Height * 1.5);
            Canvas.SetRight(Btn_GetEnemyScreen, Btn_GetEnemyScreen.Height / 2);

            

            

            towerGridBack.Height = BottomMenu.Height * 0.8;
            towerGridBack.Width = towerGridBack.Height * 2.9;
            Canvas.SetLeft(towerGridBack, (BottomMenu.Width - towerGridBack.Height * 2) / 4);
            Canvas.SetTop(towerGridBack, (BottomMenu.Height - towerGridBack.Height) / 3);





            TowerAtckGrid.Height = towerGridBack.Height* 0.9;
            TowerAtckGrid.Width = TowerAtckGrid.Height * 2;
            Canvas.SetLeft(TowerAtckGrid, (BottomMenu.Width - TowerAtckGrid.Width) / 4);
            Canvas.SetTop(TowerAtckGrid, (BottomMenu.Height - TowerAtckGrid.Height) / 2.5);

            TowerResGrid.Height = towerGridBack.Height * 0.9;
            TowerResGrid.Width = TowerResGrid.Height / 2;
            Canvas.SetLeft(TowerResGrid, BottomMenu.Width / 4 - towerGridBack.Height / 2 + TowerAtckGrid.Width * 1.3);
            Canvas.SetTop(TowerResGrid, (BottomMenu.Height - TowerResGrid.Height) / 2.5);
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
        private void FormTowerAtckGrid()
        {
            TowerAtckGrid = new Grid() {ShowGridLines=true};
            for (int i = 0; i < 6; i++)
            {
                TowerAtckGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 3; i++)
            {
                TowerAtckGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            Button UIGunTower = GunTower.UiBtn;
            UIGunTower.BorderBrush = brushMenuTheBrightest;
            UIGunTower.Padding = new Thickness(2);
            UIGunTower.BorderThickness = new Thickness(3);
            UITowers.Add(UIGunTower);
            Grid.SetRow(UIGunTower, 0);
            Grid.SetColumn(UIGunTower,0);
            TowerAtckGrid.Children.Add(UIGunTower);
        }

        private void FormTowerResGrid()
        {
            TowerResGrid = new Grid() { ShowGridLines=true};
            for (int i = 0; i < 2; i++)
            {
                TowerResGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 3; i++)
            {
                TowerResGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }
        }



        private void BuildTower(object sender, RoutedEventArgs e)
        {
            foreach (Button b in UITowers)
                b.IsEnabled = true;
        }
    }
}
