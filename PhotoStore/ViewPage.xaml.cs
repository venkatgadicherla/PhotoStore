using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPage : Page
    {
        public ViewPage()
        {
            this.InitializeComponent();
        }

        
        private async void pnlViewPhoto_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
          

            openPicker.ViewMode = PickerViewMode.Thumbnail;

                         
                openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".png");
                openPicker.FileTypeFilter.Add(".bmp");
            try
            {
                var files = await openPicker.PickMultipleFilesAsync(); // File picker opened to select the files here 



                if (files.Count > 0)//if the no of files selected is greater than zero then they are converted to bitmaps and then displayed in flipview
                {
                 

                    foreach (Windows.Storage.StorageFile file in files)
                    {


                        var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();


                        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {

                            await bitmapImage.SetSourceAsync(stream);// Bitmap source is set to the stream of the file
                        }

                        Image imgSelected = new Image();
                        imgSelected.Height = 350;
                        imgSelected.Width = 350;
                        imgSelected.Source = bitmapImage;
                        ImageView.Items.Add(imgSelected);//Bitmap is added to display in the flipview
                    }
                }
            }
            catch(Exception exp)
            {
                var msg = new MessageDialog(exp.ToString());
                await msg.ShowAsync();
            }
         }

        private void NavigationControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
