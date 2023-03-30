using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SaY_DeF.Source.FieldGeneration;

namespace SaY_DeF.Source
{
    class GameField
    {
        TyleType[,] Field;
        public Canvas CanvasField { get; set; }
        public Grid GridField { get; set; }
        public GameField(){  Field = FieldGeneration.GenerateField();}
        public GameField(string json){ Field = JsonConvert.DeserializeObject<TyleType[,]>(json); }
        public void FormVisual()
        {
            SolidColorBrush brushBack = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F7F7F7"));
            CanvasField = new Canvas() { Background = brushBack };
            double mWidth;
            mWidth = SystemParameters.PrimaryScreenWidth;
            GridField = new Grid() { Height = mWidth * 0.42, Width = mWidth, ShowGridLines = true };
            FormFieldCanvas();

        }
        private void FormFieldCanvas()
        {
            for (int i = 0; i < 11; i++)
                GridField.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < 26; i++)
                GridField.ColumnDefinitions.Add(new ColumnDefinition());

            CanvasField.Children.Add(GridField);
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (Field[i, j] == TyleType.UsualTyle)
                    {
                        NewTowerTile newTowerTile = new NewTowerTile(i,j);
                        Grid.SetRow(newTowerTile.addBtn, i);
                        Grid.SetColumn(newTowerTile.addBtn, j);
                        GridField.Children.Add(newTowerTile.addBtn);
                    }
                    if (Field[i, j] == TyleType.Road)
                        SetRoad(i, j);
                }
            }
        }
        private void SetRoad(int i, int j)
        {
            //MessageBox.Show($"ROAD IN {i}  {j}");
            Image road = new Image()
            {
                Stretch = Stretch.Uniform,
                SnapsToDevicePixels = true
            };
            RenderOptions.SetBitmapScalingMode(road, BitmapScalingMode.NearestNeighbor);
            road.Source=new BitmapImage(new Uri(@"images\fieldTyles\road.png", UriKind.Relative));
            Grid.SetRow(road, i);
            Grid.SetColumn(road, j);
            GridField.Children.Add(road);
        }
        public string SerializeField()
        {
            string json = JsonConvert.SerializeObject(Field);
            return json;
        }

    }
}
