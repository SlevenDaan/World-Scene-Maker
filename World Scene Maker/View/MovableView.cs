using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace World_Scene_Maker
{
    class MovableView : View
    {
        //Variables
        private bool blnGrabbed = false;
        private Point pntLastGrabbedPoint;

        private double dblXOffset = 0, dblYOffset = 0;
        
        private double dblZoomMultiplier = 1.2;

        //Constructors
        public MovableView() : base()
        {
            SetupEvents();
        }
        public MovableView(in Canvas pScene) : base(pScene)
        {
            SetupEvents();
        }
        private void SetupEvents()
        {
            this.MouseDown += GrabScene;
            this.MouseUp += ReleaseScene;
            this.MouseLeave += LoseScene;
            this.MouseMove += MoveScene;
            this.MouseWheel += ZoomScene;
        }

        //Properties
        public double ZoomMultiplier
        {
            get
            {
                return dblZoomMultiplier;
            }
            set
            {
                dblZoomMultiplier = value;
            }
        }

        //Functions
        private void UpdateOffset()
        {
            SetPosition(dblXOffset, dblYOffset);
        }
        
        private void GrabScene(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                blnGrabbed = true;
                pntLastGrabbedPoint = e.GetPosition(this);
            }
        }
        private void ReleaseScene(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Released)
            {
                blnGrabbed = false;
            }
        }
        private void LoseScene(object sender, MouseEventArgs e)
        {
            blnGrabbed = false;
        }
        private void MoveScene(object sender, MouseEventArgs e)
        {
            if (blnGrabbed)
            {
                dblXOffset += (e.GetPosition(this).X - pntLastGrabbedPoint.X) * 1;
                dblYOffset += (e.GetPosition(this).Y - pntLastGrabbedPoint.Y) * 1;
                UpdateOffset();
                pntLastGrabbedPoint = e.GetPosition(this);
            }
        }
        private void ZoomScene(object sender, MouseWheelEventArgs e)
        {
            double dblPrevScale = Scale;

            if (e.Delta > 0)
            {
                Scale *= dblZoomMultiplier;
            }
            else
            {
                Scale /= dblZoomMultiplier;
            }

            double dblScaleDiff = Scale - dblPrevScale;
            dblXOffset -= e.GetPosition(Scene).X * dblScaleDiff;
            dblYOffset -= e.GetPosition(Scene).Y * dblScaleDiff;
            UpdateOffset();
        }

    }
}
