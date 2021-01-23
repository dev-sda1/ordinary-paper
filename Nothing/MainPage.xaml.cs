﻿using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Storage;

namespace Nothing{
    public sealed partial class MainPage : Page
    {
        public int counter = 0;
        public int angers;
        public string keysPressed = "";
        public string btnsPressed = "";
        public string[] randstrings = { ">w<", "owch!", "get off me >:(" };
        public bool angerinProgress = false;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (!(localSettings.Values["Angers"] is int localValue))
            {
                Debug.WriteLine("No previous angers found");
                angers = 0;
            }
            else
            {
                Debug.WriteLine("Angers previously: "+(int)localSettings.Values["Angers"]);
                angers = (int)localSettings.Values["Angers"];
                Anger.Text = "Angered: " + (int)localSettings.Values["Angers"];
            }

            ButtonLayout.Visibility = Visibility.Collapsed;
            Debug.WriteLine("Hello there");
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }


        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key was pressed:"+e.VirtualKey);

            keysPressed = keysPressed + e.VirtualKey;
            keysPressed = keysPressed.Replace("Number", "");
            keysPressed = keysPressed.Replace("GamepadLeftThumbstick", "");
            keysPressed = keysPressed.Replace("GamepadRightThumbstick", "");
            keysPressed = keysPressed.Replace("GamepadDPad", "");
            // counter = counter + 1;

            Debug.WriteLine(keysPressed);
            //DebugTxt.Text = ""+e.VirtualKey;

            string hi = keysPressed.Substring(Math.Max(0, keysPressed.Length - 30));
            if (hi == "UpUpDownDownLeftRightLeftRight" ^ hi == "")
            {
                //DisplayNineThousand();
                //counter = 0;
                //AButton.Focus(FocusState.Programmatic);
                ButtonLayout.Visibility = Visibility.Visible;
                keysPressed = "";
                Task.Factory.StartNew(
                    () => Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () => AButton.Focus(FocusState.Programmatic)));
            }

            Debug.WriteLine(keysPressed.Substring(Math.Max(0, keysPressed.Length - 30)));
        }

        private async void CheckOrder()
        {
            Debug.WriteLine(btnsPressed);
            if(counter == 3)
            {
                if (btnsPressed == "BASTART")
                {
                    //Open edge here
                    counter = 0;
                    string uriToLaunch = @"https://stackoverflow.com/admin.php";

                    //Rickroll chance thingy
                    //10% chance of launching rickroll

                    Random rnd = new Random();
                    int num = rnd.Next(0, 10);
                    Debug.WriteLine(num);
                    if (num == 7)
                    {
                        uriToLaunch = @"https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                    }

                    Main.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    ButtonLayout.Visibility = Visibility.Collapsed;

                    angerinProgress = true;

                    Title.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 101, 101));
                    Title.Text = "You fool.";
                    await Task.Delay(3000);

                    Title.Text = "You blongous.";
                    await Task.Delay(3000);

                    Title.Text = "You absolute utter clampongus.";
                    await Task.Delay(3000);

                    Title.Text = "Think about the consequences of your actions.";
                    await Task.Delay(3000);

                    Title.Text = "Prepare for transportation";

                    angers = angers + 1;
                    ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                    localSettings.Values["Angers"] = angers;
                    angerinProgress = false;

                    var uri = new Uri(uriToLaunch);
                    DefaultLaunch();

                    async void DefaultLaunch()
                    {
                        var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                        if (success)
                        {
                            Debug.WriteLine("URL Launched!");
                        }
                        else
                        {
                            Debug.WriteLine("URL Failiure :(");
                        }
                    }

                    Title.Text = "Just ordinary paper";
                    Anger.Text = "Angered: " + angers;
                    Title.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Paper.png", UriKind.Absolute));
                    Main.Background = myBrush;
                }
                btnsPressed = "";
                counter = 0;
                ButtonLayout.Visibility = Visibility.Collapsed;
            }
        }

        
        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            btnsPressed += "A";
            counter = counter + 1;
            CheckOrder();
        }

        private void BButton_Click(object sender, RoutedEventArgs e)
        {
            btnsPressed += "B";
            counter = counter + 1;
            CheckOrder();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            btnsPressed += "START";
            counter = counter + 1;
            CheckOrder();
        }

        private async void Title_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(angerinProgress == true)
            {
                return;
            }
            else
            {
                Random r = new Random();
                Title.Text = randstrings[r.Next(0, randstrings.GetLength(0) - 1)];
                await Task.Delay(500);
                Title.Text = "Just ordinary paper";
            }
        }
    }
}
