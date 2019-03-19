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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoPage : Page
    {
        public PhotoPage()
        {
            this.InitializeComponent();
        }

        private void imgBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

       

        private void OpenFolderControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ViewPage));
        }

        private void NewFolderControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FolderPage));
        }

        private void SaveFolderControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SavePage));
        }
    }
}
