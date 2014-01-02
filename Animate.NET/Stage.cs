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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Animator
{
    public class Stage
    {
        public Canvas StageVisual { get; protected set; }
        public ObservableCollection<ISprite> Sprites { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        public Stage(Canvas StageVisual = null)
        {
            this.StageVisual = StageVisual == null ? new Canvas() : StageVisual;
            Initialize();
        }

        public Size Size
        {
            get { return new Size(StageVisual.Width, StageVisual.Height); }
            set
            {
                StageVisual.Width = value.Width;
                StageVisual.Height = value.Height;
            }
        }

        protected void Initialize()
        {
            Sprites = new ObservableCollection<ISprite>();
            Sprites.CollectionChanged += new NotifyCollectionChangedEventHandler(Sprites_CollectionChanged);

            LastUpdate = DateTime.Now;
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        public void Begin()
        {
            // avoid duplicate delegates
            CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering); 
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        public void End()
        {
            CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var ElapsedTime = DateTime.Now - LastUpdate;
            LastUpdate = DateTime.Now;
            Update(ElapsedTime);
        }

        public virtual void Update(TimeSpan ElapsedTime)
        {
            for (int i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].Update(ElapsedTime);
            }

            for (int first = 0; first < Sprites.Count; first++)
            {
                for (int second = 0; second < Sprites.Count; second++)
                {
                    if (first == second)
                        continue;

                    // overlap checking
                    if (Sprites[second].Left >= Sprites[first].Left && Sprites[second].Left <= Sprites[second].Right
                        && Sprites[second].Top <= Sprites[first].Bottom && Sprites[second].Bottom >= Sprites[first].Top)
                    {
                        Sprites[first].Velocity.InvertX();
                        Sprites[second].Velocity.InvertY();
                    }
                }
                Sprites[first].Update(ElapsedTime);
            }
        }

        private void Sprites_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (int i = 0; i < e.NewItems.Count; i++)
                    {
                        ISprite NewSprite = (ISprite)e.NewItems[i];
                        StageVisual.Children.Add(NewSprite.Visual);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    for (int i = 0; i < e.OldItems.Count; i++)
                    {
                        StageVisual.Children.Remove((UIElement)e.OldItems[i]);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    throw new NotSupportedException("Replace in Sprites collection not supported");
                case NotifyCollectionChangedAction.Reset:
                    for (int i = 0; i < e.OldItems.Count; i++)
                    {
                        StageVisual.Children.Remove((UIElement)e.OldItems[i]);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}