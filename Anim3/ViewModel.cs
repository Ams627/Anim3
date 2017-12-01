using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;

namespace Anim3
{
    public class ViewModel
    {
        public string V { get; set; } = "hello";
        public Thickness MyMargin { get; set; } = new Thickness(50, 50, 50, 50);
    }
}
