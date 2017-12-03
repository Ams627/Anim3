using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Anim3
{
    public class ViewModel : DependencyObject
    {
        public string V { get; set; } = "hello";
        public Thickness MyMargin { get; set; } = new Thickness(50, 50, 50, 50);

        public bool Flipper
        {
            get { return (bool)GetValue(FlipperProperty); }
            set { SetValue(FlipperProperty, value); }
        }

        public static readonly DependencyProperty FlipperProperty =
            DependencyProperty.Register("Flipper", typeof(bool), typeof(ViewModel), new PropertyMetadata(false));


        public ViewModel()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += (s, e) => {
                Flipper = !Flipper;
            };
            timer.Start();
        }
    }
}
