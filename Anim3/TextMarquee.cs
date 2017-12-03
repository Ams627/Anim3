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

namespace Anim3
{
    public class TextMarquee : Label
    {
        static TextMarquee()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextMarquee), new FrameworkPropertyMetadata(typeof(TextMarquee)));
        }

        public static readonly RoutedEvent StartAnimation = EventManager.RegisterRoutedEvent("StartAnimation", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextMarquee));
        public static void AddStartAnimationHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie)
            {
                uie.AddHandler(TextMarquee.StartAnimation, handler);
            }
        }
        public static void RemoveStartAnimationHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie)
            {
                uie.RemoveHandler(TextMarquee.StartAnimation, handler);
            }
        }

        public static readonly RoutedEvent StopAnimation = EventManager.RegisterRoutedEvent("StopAnimation", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextMarquee));
        public static void AddStopAnimationHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie)
            {
                uie.AddHandler(TextMarquee.StopAnimation, handler);
            }
        }
        public static void RemoveStopAnimationHandler(DependencyObject d, RoutedEventHandler handler)
        {
            if (d is UIElement uie)
            {
                uie.RemoveHandler(TextMarquee.StopAnimation, handler);
            }
        }
        public TextMarquee()
        {
            this.Loaded += TextMarquee_Loaded;
        }

        private void TextMarquee_Loaded(object sender, RoutedEventArgs e)
        {
            var l = new Label() { Content = Content };
            l.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Size s = l.DesiredSize;
            var l1 = l.ActualWidth;
            IsAnimationEnabled = s.Width > Width;
            SendAnimationEvent(IsAnimationEnabled);
        }

        void SendAnimationEvent(bool start)
        {
            var borderElement = GetTemplateChild("border");
            if (borderElement is Border border)
            {
                RoutedEventArgs args;
                args = new RoutedEventArgs
                {
                    RoutedEvent = start ? TextMarquee.StartAnimation : TextMarquee.StopAnimation,
                    Source = this
                };
                border.RaiseEvent(args);
            }
        }

        public bool IsAnimationEnabled
        {
            get { return (bool)GetValue(IsAnimationEnabledProperty); }
            set { SetValue(IsAnimationEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsAnimationEnabledProperty =
            DependencyProperty.Register("IsAnimationEnabled", typeof(bool), typeof(TextMarquee),
                new FrameworkPropertyMetadata(true, OnIsAnimationChanged)
                );

        private static void OnIsAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextMarquee marquee)
            {
                bool start = (bool)e.NewValue;
                marquee.SendAnimationEvent(start);
            }
        }
    }
   
}

