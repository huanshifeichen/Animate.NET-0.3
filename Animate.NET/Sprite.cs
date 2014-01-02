using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Animator
{
    public class Sprite<T> : ISprite
        where T : FrameworkElement, new()
    {
        public Sprite(Stage Stage, FrameworkElement SpriteVisual = null)
        {
            this.Stage = Stage;
            this.SpriteVisual = SpriteVisual != null ? (T)SpriteVisual : new T();
            Initialize();
        }

        public Color InnerColor { get; set; }
        public Color OuterColor { get; set; }

        public T SpriteVisual { get; protected set; }
        public UIElement Visual
        {
            get { return (UIElement)SpriteVisual; }
            set { SpriteVisual = (T)value; }
        }
        
        public Stage Stage { get; set; }

        public Point TransformOrigin
        {
            get { return SpriteVisual.RenderTransformOrigin; }
            set { SpriteVisual.RenderTransformOrigin = value; }
        }

        public Point PositionOrigin { get; set; }

        public Size Size
        {
            get { return new Size(SpriteVisual.ActualWidth, SpriteVisual.ActualHeight); }
            set
            {
                SpriteVisual.Width = value.Width;
                SpriteVisual.Height = value.Height; 
            }
        }

        public double Width
        {
            get { return SpriteVisual.Width; }
            set { SpriteVisual.Width = value; }
        }

        public double Height
        {
            get { return SpriteVisual.Height; }
            set { SpriteVisual.Height = value; }
        }

        public Point Position
        {
            get
            {
                return new Point(SpriteVisual.GetLeft(), SpriteVisual.GetTop());
                return new Point(SpriteVisual.GetLeft() + SpriteVisual.ActualWidth * PositionOrigin.X,
                    SpriteVisual.GetTop() + SpriteVisual.ActualHeight * PositionOrigin.Y);
            }
            set
            {
                //SpriteVisual.SetLeft(value.X + SpriteVisual.Width * PositionOrigin.X);
                //SpriteVisual.SetTop(value.Y + SpriteVisual.Height * PositionOrigin.Y);
                SpriteVisual.SetLeft(value.X);
                SpriteVisual.SetTop(value.Y);
            }
        }

        public double Left
        {
            get { return SpriteVisual.GetLeft(); }
            set { SpriteVisual.SetLeft(value); }
        }

        public double Right
        {
            get { return SpriteVisual.GetLeft() + SpriteVisual.ActualWidth; }
            set { Left = value - Width; }
        }

        public double Top
        {
            get { return SpriteVisual.GetTop(); }
            set { SpriteVisual.SetTop(value); }
        }

        public double Bottom
        {
            get { return SpriteVisual.GetTop() + SpriteVisual.ActualHeight; }
            set { Top = value - Height; }
        }

        public Vector2D Velocity { get; set; }

        public double RotateAngle { get; set; }
        public double RotateAngleNormal { get; set; }

        protected virtual void Initialize()
        {
            PositionOrigin = new Point(0.5, 0.5);
            TransformOrigin = new Point(0.5, 0.5);
            Velocity = Vector2D.FromComponents(0, 0);

            if (!Stage.Sprites.Contains(this))
                Stage.Sprites.Add(this);
        }

        public virtual void Update(TimeSpan ElapsedTime)
        {
            Position = new Point(Position.X + Velocity.X * ElapsedTime.TotalSeconds, 
                Position.Y + Velocity.Y * ElapsedTime.TotalSeconds);

            if (Velocity.IsPointingLeft && Left < 0 || Velocity.IsPointingRight && Right > Stage.Size.Width)
                Velocity.InvertX();

            if (Velocity.IsPointingUp && Top < 0 || Velocity.IsPointingDown && Bottom > Stage.Size.Height)
                Velocity.InvertY();
        }
    }
}