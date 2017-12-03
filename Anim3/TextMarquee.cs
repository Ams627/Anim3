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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anim3
{
    public class TextMarquee : Label
    {
        public enum AnimationRunType
        {
            /// <summary>
            /// Run the animation:
            /// </summary>
            Run,
            /// <summary>
            /// Don't run the animation@
            /// </summary>
            DontRun,
            /// <summary>
            /// Run the animation if the actual width of the text is less than the full required width:
            /// </summary>
            Auto
        };
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
            CheckAnimationRuntype();
        }

        public void CheckAnimationRuntype(bool changed = false)
        {
            if (RunType == AnimationRunType.Run)
            {
                SendAnimationEvent(true);
            }
            else if (RunType == AnimationRunType.Auto)
            {
                var textblock = new TextBlock
                {
                    Text = Content as string,
                    FontSize = FontSize,
                    FontFamily = FontFamily,
                    FontWeight = FontWeight,
                    FontStyle = FontStyle
                };

                textblock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                textblock.Arrange(new Rect(textblock.DesiredSize));
                if (textblock.ActualWidth > ActualWidth)
                {
                    SendAnimationEvent(true);
                }
            }
            else if (changed && RunType == AnimationRunType.DontRun)
            {
                SendAnimationEvent(false);
            }
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

        public AnimationRunType RunType
        {
            get { return (AnimationRunType)GetValue(RunTypeProperty); }
            set { SetValue(RunTypeProperty, value); }
        }

        public static readonly DependencyProperty RunTypeProperty =
            DependencyProperty.Register("RunType", typeof(AnimationRunType), typeof(TextMarquee), new PropertyMetadata(AnimationRunType.Auto, OnAnimationRunTypeChanged));

        private static void OnAnimationRunTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextMarquee marquee)
            {
                if (e.NewValue is AnimationRunType runtype)
                {
                    marquee.CheckAnimationRuntype(true);
                }
            }
        }

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(TextMarquee), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(10))));
    }
}

