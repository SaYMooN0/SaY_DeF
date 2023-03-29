using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static SaY_DeF.Source.FieldGeneration;

namespace SaY_DeF.Source
{
    class GameField
    {
        TyleType[,] Field;
        public Canvas CanvasField { get; }
        public Grid GridField { get; }
        public GameField()
        {   
            SolidColorBrush brushBack = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8E8"));
            Field = FieldGeneration.GenerateField();
            CanvasField = new Canvas() {Background=brushBack };
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
                        NewTowerTile newTowerTile = new NewTowerTile();
                        Grid.SetRow(newTowerTile.addBtn, i);
                        Grid.SetColumn(newTowerTile.addBtn, j);
                        GridField.Children.Add(newTowerTile.addBtn);
                    }
                }
            }
        }

    }
}
