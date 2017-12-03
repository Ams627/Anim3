using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace Anim3
{
    public class ViewModel : DependencyObject
    {
        public bool IsAnimated
        {
            get { return (bool)GetValue(IsAnimatedProperty); }
            set { SetValue(IsAnimatedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAnimated.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAnimatedProperty =
            DependencyProperty.Register("IsAnimated", typeof(bool), typeof(ViewModel), new PropertyMetadata(false, OnIsAnimatedChanged));

        private static void OnIsAnimatedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool b)
            {
                System.Diagnostics.Debug.WriteLine($"IsAnimated is now {b}");
            }
        }
    }
}
