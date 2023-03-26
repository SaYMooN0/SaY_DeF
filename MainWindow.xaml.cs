using Newtonsoft.Json;
using SaY_DeF.Source;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace SaY_DeF
{
    public partial class MainWindow : Window
    {
        Settings settings;
        Net_Connector netCon;
        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettinsPage.Height = Height;
            SettinsPage.Width = Width;
            MainPage.Height = Height;
            MainPage.Width = Width;
            LabelReactionToWinSizeChanged();
            PlayButtonReactionToWinSizeChanged();
            SettingsButtonReactionToWinSizeChanged();
            SettingsPageReaction();

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


            PlayTB.Width = Button_Play.Width - Button_Play.Height - 10;
            PlayTB.FontSize = Button_Play.Width / 7.8;

            PlayColumnImage.Width = new GridLength(Button_Play.Height);
            PlayImg.Margin = new Thickness((Button_Play.Width - Button_Play.Width) / 220);
            PlayImg.Height = Button_Play.Height * 0.67;
            PlayImg.Width = Button_Play.Height * 0.67;
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
        private void SettingsPageReaction()
        {
            ReturnButtonReactionToWinSizeChanged();
            SettingsParametrsReactionToWinSizeChanged();
        }
        private void SettingsParametrsReactionToWinSizeChanged()
        {
            AllTheSettings.Width = Width *0.9;
            AllTheSettings.Height = Height * 0.8;
            foreach (Control item in AllTheSettings.Children)
            {
                item.FontSize = Math.Min(Height, Width) / 32+6;
                if (item is Button)
                {
                    item.BorderThickness = new Thickness( Math.Min(AllTheSettings.Height, AllTheSettings.Width) *0.01+2);
                    item.Width = AllTheSettings.Width / 4;
                }
            }
            Canvas.SetLeft(AllTheSettings, Width*0.05);
            Canvas.SetTop(AllTheSettings, Height*0.1);
        }
        private void ReturnButtonReactionToWinSizeChanged()
        {
            Button_Return.Height = Height*0.09;
            Button_Return.Width=Button_Return.Height*3.2;
            Button_Return.BorderThickness =new Thickness( Button_Return.Height * 0.12);
            Button_Return.FontSize = Button_Return.Height/2;
            Canvas.SetBottom(Button_Return, Button_Return.Height+16);
            Canvas.SetRight(Button_Return, Button_Return.Height*0.9);
        }
        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            netCon = new Net_Connector();
            NetworkScreen netScreen = new NetworkScreen();
            netScreen.SpawnConnectionWindow(ref netCon);
            netScreen.gameStartIsReady += netScreen.WindowClose;
            netScreen.gameStartIsReady += GameStart;
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            SettinsPage.Visibility = Visibility.Visible;
            Button_Return.Visibility = Visibility.Visible;
            TB_Nick.Text = Settings.myNick;
        }
        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Visible;
            SettinsPage.Visibility = Visibility.Hidden;
        }

        private void Button_ChangeNick_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TB_Nick.Text))
                return;
            Settings.myNick = TB_Nick.Text;
            Task.Run(() => this.Dispatcher.Invoke(new Action(async delegate
            {
                Button_ChangeNick.Content = "Confirmed";
                await Task.Delay(760);
                Button_ChangeNick.Content = "Confirm";
            })));
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveSettings();
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
        }
        private void SaveSettings()
        {
            using (StreamWriter file = File.CreateText("settings.json"))
            {
                string json = JsonConvert.SerializeObject(settings);
                file.WriteLineAsync(json);
            }
        }
        private void LoadSettings()
        {
            using (StreamReader file = File.OpenText("settings.json"))
            {
                string json = file.ReadToEnd();
                settings = JsonConvert.DeserializeObject<Settings>(json);
            }
        }
        private void GameStart(object sender, GameArgs gameArgs)
        {
            Game g = new Game(gameArgs, this, netCon);
        }
    }
}
