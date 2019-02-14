using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public MainWindow()
        {
            InitializeComponent();

            //Map View
            MovableView view = new MovableView();
            view.Margin = new Thickness(0, 0, 350, 0);
            view.Background = new SolidColorBrush(Colors.Red);
            grdMain.Children.Add(view);

            //propertyEditor
            PropertyEditor propertyEditor = new PropertyEditor();
            propertyEditor.AddProperty<string>("Name","");
            propertyEditor.AddProperty<double>("Double",0.0);
            propertyEditor.AddProperty<int>("Int", 0);
            propertyEditor.AddProperty<TestEnum>("Enum", 0);
            propertyEditor.AddProperty<DateTime>("Datetime", new DateTime());
            scvEditor.Content = propertyEditor;

            //Elements in map view
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("D:/Users/Gebruiker/Desktop/Backgrounds/Cow1.jpg"));
            view.Scene.Children.Add(image);

            propertyEditor.GetProperty<string>("Name").Value = "Wolla";

            Dictionary<String,Type> arrAll = propertyEditor.GetAllProperties();
        }
    }
}
