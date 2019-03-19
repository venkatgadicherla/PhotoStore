using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PhotoStore
{
    public sealed partial class CameraControl : UserControl
    {
        public CameraControl()
        {
            this.InitializeComponent();
        }


        
        public string tblIconName1
        {
            get { return (string)GetValue(tblIconName1Property); }
            set { SetValue(tblIconName1Property, value); }
        }

        // Using a DependencyProperty as the backing store for tblIconName1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty tblIconName1Property =
            DependencyProperty.Register("tblIconName1", typeof(string), typeof(CameraControl),null );



        public ImageSource imgSource
        {
            get { return (ImageSource)GetValue(imgSourceProperty); }
            set { SetValue(imgSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for imgSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty imgSourceProperty =
            DependencyProperty.Register("imgSource", typeof(ImageSource), typeof(CameraControl), null);
















    }
}
