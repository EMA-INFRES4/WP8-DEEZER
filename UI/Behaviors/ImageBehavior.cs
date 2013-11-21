using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace UI.Behaviors
{
    public class ImageBehavior : Behavior<Image>
    {
        private Storyboard storyboard;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (!DesignerProperties.IsInDesignTool)
            {
                this.AssociatedObject.Opacity = 0;
                this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            }
        }

        void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (storyboard == null)
            {
                storyboard = new Storyboard();
            }
            else
            {
                storyboard.Stop();
                storyboard = new Storyboard();
            }
            storyboard.Children.Add(CreateAnimation(AssociatedObject, UIElement.OpacityProperty, 0, 1, 1000));
            storyboard.Begin();
        }
        public static DoubleAnimation CreateAnimation(DependencyObject obj, DependencyProperty prop, double from, double value, double milliseconds, EasingMode easing = EasingMode.EaseIn)
        {
            CubicEase ease = new CubicEase() { EasingMode = easing };
            DoubleAnimation animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(milliseconds)),
                From = from,
                To = Convert.ToDouble(value),
                FillBehavior = FillBehavior.HoldEnd,
                EasingFunction = ease
            };
            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation, new PropertyPath(prop));
            return animation;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
    }
}
