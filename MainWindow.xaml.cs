using System;
using System.Linq;
using System.Resources;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SaY_DeF.Source;

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
            SettingsButtonReactionToWinSizeChanged();
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
            Button_Play.Width = (Width + Height) / 5.85;
            Button_Play.Height = Button_Play.Width / 2.9;
            Button_Play.BorderThickness = new Thickness(Button_Play.Height / 13 + 2);


            PlayTB.Width = Button_Play.Width-Button_Play.Height-10;
            PlayTB.FontSize = Button_Play.Width /7.8;

            PlayColumnImage.Width = new GridLength(Button_Play.Height);
            PlayImg.Margin=new Thickness((Button_Play.Width-Button_Play.Width)/220);
            PlayImg.Height = Button_Play.Height* 0.67;
            PlayImg.Width = Button_Play.Height* 0.67;
            Canvas.SetLeft(Button_Play, (Width - Button_Play.Width) / 2);
            Canvas.SetTop(Button_Play, (Height - Button_Play.Height) / 3);
        }
        private void SettingsButtonReactionToWinSizeChanged()
        {
            Button_Settings.Width = (Width + Height) / 5.85;
            Button_Settings.Height = Button_Play.Width / 2.9;
            Button_Settings.BorderThickness = new Thickness(Button_Play.Height / 13 + 2);


            SetTB.Width = Button_Play.Width - Button_Play.Height - 10;
            SetTB.FontSize = Button_Play.Width / 7.8;

            SetColumnImage.Width = new GridLength(Button_Play.Height);
            SetImg.Margin = new Thickness((Button_Play.Width - Button_Play.Width) / 220);
            SetImg.Height = Button_Play.Height * 0.67;
            SetImg.Width = Button_Play.Height * 0.67;
            Canvas.SetLeft(Button_Settings, (Width - Button_Play.Width) / 2);
            Canvas.SetTop(Button_Settings, (Height - Button_Play.Height) / 1.82);
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            Game g = new Game();
            this.Content = g.RecieveScreen();
        }
    }
}
