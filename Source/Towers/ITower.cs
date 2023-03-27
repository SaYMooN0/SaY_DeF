using System.Windows;
using System.Windows.Controls;

namespace SaY_DeF.Source.Towers
{
    internal interface IAttackingTower
    {
        string Name { get; }
        static Button UiBtn { get;}
        Button ActualTower{ get;}
        void StartToCheckForEnemy();
        void CheckForEnemy();
        void Attack();
        void Build();
        void Upgrade();
        static void UI_Click(object sender, RoutedEventArgs e) { }
    }
}
