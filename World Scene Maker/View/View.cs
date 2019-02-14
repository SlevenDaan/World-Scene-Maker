using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace World_Scene_Maker
{
    class View : Canvas
    {
        //Variables
        private Canvas scene;

        private double dblScale = 1;
        private const double SCALE_MIN = 0.01;
        private const double SCALE_MAX = 10;

        //Constructors
        public View()
        {
            scene = new Canvas();
            base.Children.Add(scene);
            base.ClipToBounds = true;
        }
        public View(in Canvas pScene)
        {
            scene = pScene;
            base.Children.Add(scene);
            base.ClipToBounds = true;
        }

        //Properties
        public Canvas Scene
        {
            get
            {
                return scene;
            }
        }
        
        public double Scale
        {
            get
            {
                return dblScale;
            }
            set
            {
                dblScale = value;
                if (dblScale > SCALE_MAX)
                {
                    dblScale = SCALE_MAX;
                }
                else if (dblScale < SCALE_MIN)
                {
                    dblScale = SCALE_MIN;
                }

                ScaleTransform scale = new ScaleTransform(dblScale, dblScale);
                scene.RenderTransform = scale;
            }
        }

        //Functions
        public void SetPosition(double pX, double pY)
        {
            scene.Margin = new Thickness(pX, pY, 0, 0);
        }
        
    }
}
