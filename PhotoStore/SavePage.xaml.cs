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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SavePage : Page
    {
        public SavePage()
        {
            this.InitializeComponent();
        }

        List<String> selectedFileNames = new List<String>();//This list is used to store selected file names which are later save the files with that name
        List<SoftwareBitmap> selectedSoftwareBmps = new List<SoftwareBitmap>();//This list is used to store the selected files as software bitmaps
       
        private void NavigationControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void iconSavePhoto_Tapped(object sender, TappedRoutedEventArgs e)
          {
            //This event is triggered when the savecontrol is tapped
            FileSavePicker saveFileP = new FileSavePicker();// A save file picker is uded to select a destination and save the files

            saveFileP.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            saveFileP.FileTypeChoices.Add("Jpeg", new List<string>() { ".jpeg" });
            saveFileP.FileTypeChoices.Add("Jpg", new List<string>() { ".jpg" });
            saveFileP.FileTypeChoices.Add("Png", new List<string>() { ".png" });
            saveFileP.FileTypeChoices.Add("Bmp", new List<string>() { ".bmp" });
         

            if (selectedSoftwareBmps.Count > 0)// if there are photos is in bitmap list the following code executes
            {
                int photoCounter = 0;
                try
                {
                    foreach (SoftwareBitmap bmp in selectedSoftwareBmps)//
                    {

                        saveFileP.SuggestedFileName = selectedFileNames[photoCounter].ToString();
                        var outputfile = await saveFileP.PickSaveFileAsync();
                        if (outputfile != null)
                        {
                            using (IRandomAccessStream stream = await outputfile.OpenAsync(FileAccessMode.ReadWrite))
                            {
                                // Create an encoder with the desired format
                                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

                                encoder.SetSoftwareBitmap(bmp);// Set the software bitmap
                                encoder.IsThumbnailGenerated = true;

                                try
                                {
                                    await encoder.FlushAsync();
                                }

                                catch (Exception err)
                                {

                                }

                                if (encoder.IsThumbnailGenerated == false)
                                {
                                    await encoder.FlushAsync();
                                }


                            }
                            var saveMessage = new MessageDialog("Photo Saved");
                            await saveMessage.ShowAsync();

                            photoCounter++;

                        }
                        else
                        {
                            var saveMessage = new MessageDialog("File Save operation cancelled");
                            await saveMessage.ShowAsync();


                        }


                    }

                }
                catch (Exception exp)
                {

                    var expmsg = new MessageDialog(exp.ToString());
                    await expmsg.ShowAsync();
                    return;
                }

                selectedSoftwareBmps.Clear();
            }
            else
            {
                var saveMessage = new MessageDialog("No Photos selected");
                await saveMessage.ShowAsync();
            }
        }


        private async void iconSelectPhotos_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //This event is triggered when the select icon is clicked.
            //This method the FileOpenPicker is used to select Photos in .jpeg .jpg and .png and .bmp formats and convert them into
            // software bitmaps.The sofware bitmaps are stored in a List. which can be later accessed
            selectedFileNames.Clear();// Old pictures are cleared if any
            selectedSoftwareBmps.Clear();//old softwareBmpn cleared if any

            FileOpenPicker photoSelectPicker = new FileOpenPicker();
            
            photoSelectPicker.ViewMode = PickerViewMode.Thumbnail;

            photoSelectPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            photoSelectPicker.FileTypeFilter.Add(".jpeg");
            photoSelectPicker.FileTypeFilter.Add(".jpg");
            photoSelectPicker.FileTypeFilter.Add(".png");
            photoSelectPicker.FileTypeFilter.Add(".bmp");
           var selectedFiles = await photoSelectPicker.PickMultipleFilesAsync();// Files are picked here
            
            if(selectedFiles.Count>0)// if the  no of files selected are greater than zero then convert the picture files into bitmaps
            {
                try
                {
                    foreach (Windows.Storage.StorageFile file in selectedFiles)
                    {
                        selectedFileNames.Add(file.Name);//This seleted file name is stored in a array for later use when saving the file 

                        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))//The file is being read into a stream
                        {
                            // Create the decoder from the stream
                            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream); // Bitmap decoder is used to convert the stream into a sofware bitmap

                            selectedSoftwareBmps.Add(await decoder.GetSoftwareBitmapAsync());// The Software bitmap is added to the List for later use

                        }
                    }
                }
                catch (Exception exp)
                {

                    var expmsg = new MessageDialog(exp.ToString());
                    await expmsg.ShowAsync();
                    return;
                }

            }
                var saveMessage = new MessageDialog(selectedFiles.Count+" Photos Selected");
               
                await saveMessage.ShowAsync();
            }
        }
    }

