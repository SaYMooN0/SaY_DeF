using System;
using System.Linq;
using System.Resources;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SaY_DeF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            Button_Play.Resources.Add("Corners", new object());
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LabelReactionToWinSizeChanged();
            PlayButtonReactionToWinSizeChanged();
        }
        private void LabelReactionToWinSizeChanged()
        {
            Lbl_Main.Width = Width / 2;
            Lbl_Main.Height = Height / 6;
            Lbl_Main.FontSize = Math.Min(Lbl_Main.Width / 4.2, Lbl_Main.Height) / 1.2;
            Canvas.SetLeft(Lbl_Main, (this.Width - Lbl_Main.Width) / 2);
            Canvas.SetTop(Lbl_Main, (this.Height - Lbl_Main.Height * 2) / 20);
        }
        private void PlayButtonReactionToWinSizeChanged()
        {

            Button_Play.Width = (Width + Height) / 6.2;
            Button_Play.Height = Button_Play.Width / 2.9;
            Button_Play.BorderThickness = new Thickness(Button_Play.Height / 9);

            //Style borderSt = new Style() { TargetType = typeof(Border) };
            //Setter str = new Setter { Property = Border.CornerRadiusProperty, Value = new CornerRadius((int)(Button_Play.Width / 4)) };
            //borderSt.Setters.Add(str);

            //ResourceDictionary resur = new ResourceDictionary();
            //resur.Add("Corners", borderSt);
            //Button_Play.Resources = resur;
            //Button_Play.ApplyTemplate();


            Canvas.SetLeft(Button_Play, (Width - Button_Play.Width) / 2);
            Canvas.SetTop(Button_Play, (Height - Button_Play.Height) / 3);

        }
    }
}
