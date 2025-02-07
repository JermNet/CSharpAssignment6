using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Assignment6
{
  
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private const int horizontalOffset = 150, hairOffset = 0, eyesOffset = 200, noseOffset = 400, mouthOffset = 600;
        private const string faceDirectory = "../../images/Guy", chmPath = @"..\..\FaceHelp.chm", webPath = "https://jermnet.github.io/CSharpAssignment5/", infoDirectory = "../../YourInfo.txt";
        private ImageManager imageManager = new ImageManager();

        private List<BitmapImage> hair = new List<BitmapImage>();
        private List<BitmapImage> eyes = new List<BitmapImage>();
        private List<BitmapImage> nose = new List<BitmapImage>();
        private List<BitmapImage> mouth = new List<BitmapImage>();
        private List<CommandHandler> hairCMDList = new List<CommandHandler>();
        private List<CommandHandler> eyesCMDList = new List<CommandHandler>();
        private List<CommandHandler> noseCMDList = new List<CommandHandler>();
        private List<CommandHandler> mouthCMDList = new List<CommandHandler>();
        private List<CommandHandler> helpCMDList = new List<CommandHandler>();

        public MainWindow()
        {
            SetupImages();
            InitializeComponent();
            SetupHandlers();
            firstCanvas.Children.Clear();
        }

        private void SetupHandlers()
        {

            for (int i = 0; i < 4; i++) {
                hairCMDList.Add(new CommandHandler(() => imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, hairOffset, hair[i]), true));
                eyesCMDList.Add(new CommandHandler(() => imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, eyesOffset, eyes[i]), true));
                noseCMDList.Add(new CommandHandler(() => imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, noseOffset, nose[i]), true));
                mouthCMDList.Add(new CommandHandler(() => imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, mouthOffset, mouth[i]), true));
            }

            helpCMDList.Add(new CommandHandler(() => Get_Doc_Help(), true));
            helpCMDList.Add(new CommandHandler(() => Get_Web_Help(), true));

            DataContext = new
            {
                hairCMD1 = hairCMDList[0],
                hairCMD2 = hairCMDList[1],
                hairCMD3 = hairCMDList[2],
                hairCMD4 = hairCMDList[3],
                eyesCMD1 = eyesCMDList[0],
                eyesCMD2 = eyesCMDList[1],
                eyesCMD3 = eyesCMDList[2],
                eyesCMD4 = eyesCMDList[3],
                noseCMD1 = noseCMDList[0],
                noseCMD2 = noseCMDList[1],
                noseCMD3 = noseCMDList[2],
                noseCMD4 = noseCMDList[3],
                mouthCMD1 = mouthCMDList[0],
                mouthCMD2 = mouthCMDList[1],
                mouthCMD3 = mouthCMDList[2],
                mouthCMD4 = mouthCMDList[3],
                helpCMD1 = helpCMDList[0],
                helpCMD2 = helpCMDList[1],
            };
            InputBindings.Add(new KeyBinding(hairCMDList[0], new KeyGesture(Key.D1, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(hairCMDList[1], new KeyGesture(Key.D2, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(hairCMDList[2], new KeyGesture(Key.D3, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(hairCMDList[3], new KeyGesture(Key.D4, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(eyesCMDList[0], new KeyGesture(Key.Q, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(eyesCMDList[1], new KeyGesture(Key.W, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(eyesCMDList[2], new KeyGesture(Key.E, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(eyesCMDList[3], new KeyGesture(Key.R, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(noseCMDList[0], new KeyGesture(Key.A, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(noseCMDList[1], new KeyGesture(Key.S, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(noseCMDList[2], new KeyGesture(Key.D, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(noseCMDList[3], new KeyGesture(Key.F, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(mouthCMDList[0], new KeyGesture(Key.Z, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(mouthCMDList[1], new KeyGesture(Key.X, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(mouthCMDList[2], new KeyGesture(Key.C, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(mouthCMDList[3], new KeyGesture(Key.V, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(helpCMDList[0], new KeyGesture(Key.H, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(helpCMDList[1], new KeyGesture(Key.J, ModifierKeys.Control)));
        }

        private void SetupImages()
        {
            for (int i = 1; i < 5; i++)
            {
                hair.Add(new BitmapImage(new Uri(faceDirectory + i + "P1.png", UriKind.Relative)));
                eyes.Add(new BitmapImage(new Uri(faceDirectory + i + "P2.png", UriKind.Relative)));
                nose.Add(new BitmapImage(new Uri(faceDirectory + i + "P3.png", UriKind.Relative)));
                mouth.Add(new BitmapImage(new Uri(faceDirectory + i + "P4.png", UriKind.Relative)));
            }
        }

        private void Combo_Changed(object sender, RoutedEventArgs e)
        {
            if (Combo.SelectedItem is ComboBoxItem selectedItem)
            {
                int value = int.Parse(selectedItem.Tag.ToString());
                imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, hairOffset, hair[value]);
            }
        }
        private void Eyes_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                int value = int.Parse(checkBox.Tag.ToString());
                imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, eyesOffset, eyes[value]);
            }       
        }

        private void Nose_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                int value = int.Parse(radioButton.Tag.ToString());
                imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, noseOffset, nose[value]);
            }
        }

        private void Value_Changed(object sender, RoutedEventArgs e)
        {
            imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, mouthOffset, mouth[(int)Slider.Value - 1]);
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, hairOffset, hair[random.Next(0, 4)]);
            imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, eyesOffset, eyes[random.Next(0, 4)]);
            imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, noseOffset, nose[random.Next(0, 4)]);
            imageManager.GetImage(firstCanvas, secondCanvas, horizontalOffset, mouthOffset, mouth[random.Next(0, 4)]);
        }

        public void Help_Click(object sender, RoutedEventArgs e)
        {
            Get_Doc_Help();
        }

        public void Help_Click_2(object sender, RoutedEventArgs e)
        {
            Get_Web_Help();
        }

        public void Get_Doc_Help()
        {
            Process.Start(new ProcessStartInfo(chmPath) { UseShellExecute = true });
        }

        private void Get_Web_Help()
        {
            Process.Start(new ProcessStartInfo(webPath) { UseShellExecute = true });
        }

        private string Collect_Info()
        {
            string fn = !string.IsNullOrEmpty(firstName.Text) ? firstName.Text : "No first name given";
            string ln = !string.IsNullOrEmpty(lastName.Text) ? lastName.Text : "No last name given";
            string ad = !string.IsNullOrEmpty(address.Text) ? address.Text : "No address given";
            string j = !string.IsNullOrEmpty(job.Text) ? job.Text : "No job selected";
            string h = !string.IsNullOrEmpty(hobby.Text) ? hobby.Text : "No hobby selected";

            string dogOrCat;
            if (dog.IsChecked == true)
                dogOrCat = "Dog";
            else if (cat.IsChecked == true)
                dogOrCat = "Cat";
            else
                dogOrCat = "No animal selected";

            string elements = "";
            for (int i = 0; i < firstCanvas.Children.Count; i++)
            {
                if (firstCanvas.Children[i] is Image image)
                {
                    elements += image.Source + "\n";
                }
            }

            string info = $"First Name: {fn}\nLast Name: {ln}\nAddress: {ad}\nJob: {j}\nHobby: {h}\nAnimal Preference: {dogOrCat}\nFace Elements: \n{elements}";
            return info;
        }

        private void Save_File(string directory, string content)
        {
            try
            {
                File.WriteAllText(directory, content);
                MessageBox.Show("Information successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            infoText.Content = Collect_Info();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string info = Collect_Info();
            Save_File(infoDirectory, info);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            firstName.Clear();
            lastName.Clear();
            address.Clear();
            job.SelectedItem = null;
            hobby.SelectedItem = null;
            dog.IsChecked = false;
            cat.IsChecked = false;
            firstCanvas.Children.Clear();
            secondCanvas.Children.Clear();
            tabControl.SelectedIndex = 0;
            Save_File(infoDirectory, "");
        }

    }
}
