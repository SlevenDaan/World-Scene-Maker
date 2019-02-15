using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DS_PropertyEditor;

namespace World_Scene_Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum TestEnum
        {
            True,False,None
        }

        PropertyEditor propertyEditor;

        public MainWindow()
        {
            InitializeComponent();

            //Map View
            MovableView view = new MovableView();
            view.Margin = new Thickness(0, 0, 350, 0);
            view.Background = new SolidColorBrush(Colors.Red);
            grdMain.Children.Add(view);

            //propertyEditor
            propertyEditor = new PropertyEditor();
            propertyEditor.AddProperty<bool>("bool", true);
            propertyEditor.AddProperty<byte>("byte", 1);
            propertyEditor.AddProperty<char>("char", 'h');
            propertyEditor.AddProperty<DateTime>("datetime", new DateTime());
            propertyEditor.AddProperty<double>("double", 15.1789);
            propertyEditor.AddProperty<TestEnum>("enum", TestEnum.None);
            propertyEditor.AddProperty<int>("int", 898);
            propertyEditor.AddProperty<sbyte>("Sbyte", -25);
            propertyEditor.AddProperty<float>("float", 0.171451f);
            propertyEditor.AddProperty<UInt32>("Uint", 1787919);
            scvEditor.Content = propertyEditor;

            //Elements in map view
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("D:/Users/Gebruiker/Desktop/Backgrounds/Cow1.jpg"));
            view.Scene.Children.Add(image);

            //Propertyeditor
            propertyEditor.GetProperty<bool>("bool").Value = false;

            propertyEditor.ValueChanged += PropertyEditor_ValueChanged;

            Dictionary<string, Type> arrAll = propertyEditor.GetAllProperties();

            propertyEditor.RemoveProperty("Sbyte");
        }

        private void PropertyEditor_ValueChanged(PropertyEditor propertyEditor)
        {
            Dictionary<String, Type> arrAll = propertyEditor.GetAllProperties();
            TestEnum testEnum = propertyEditor.GetProperty<TestEnum>("enum").Value;
            bool testBool = propertyEditor.GetProperty<bool>("bool").Value;
        }
    }
}
