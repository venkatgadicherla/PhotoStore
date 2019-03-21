using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
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
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        private async void controlBgProcess_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //The background process is triggered after the button is pressed
            try
            {
                foreach (var t in BackgroundTaskRegistration.AllTasks)//this for loop is used to Unregister any background tasks which are registered
                {

                    t.Value.Unregister(true);
                }
            }
            catch (Exception exp)
            {

                var expmsg = new MessageDialog(exp.ToString());
                await expmsg.ShowAsync();
                return;
            }

            try
            {
                var fileWritingTask = new BackgroundTaskBuilder//Background task is defined here with a name and a entry point
                {
                    Name = "BgTask2",
                    TaskEntryPoint = typeof(BackGroundProcess.BackGroundFileWriter).ToString()

                };
                var backGroundTrigger = new ApplicationTrigger();// A trigger is defined here for the background task
                fileWritingTask.SetTrigger(backGroundTrigger);//The triggger is assigned to the background task
                var condition = new SystemCondition(SystemConditionType.InternetAvailable);//A condition for the background task is set here
                fileWritingTask.Register();//The background task is registered 
                await backGroundTrigger.RequestAsync();//The background task is registered
            }
            catch (Exception exp)
            {

                var expmsg = new MessageDialog(exp.ToString());
                await expmsg.ShowAsync();
                return;
            }
        }

        private async void controlCamera_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //This process is triggered when the camera control is triggered

            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(100, 100);

            try
            {
                StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);//The camera Ui is triggered in this line and the output is stored
                if (photo != null)//If the camera ouput is not null then the output is stored in the system in .bmp format
                {
                    //var currentFolder = ApplicationData.Current.LocalFolder;
                    //var destinationFolder = await currentFolder.CreateFolderAsync("destinationFolder", CreationCollisionOption.OpenIfExists);
                    ////var expmsg = new MessageDialog(currentFolder.ToString());
                    ////await expmsg.ShowAsync();
                    string PhotoName = "cam" + ".jpg";// A name for the picture file is created in this code

                    //await photo.CopyAsync(destinationFolder, PhotoName, NameCollisionOption.ReplaceExisting);
                    //await photo.DeleteAsync();
                    StorageFolder destinationFolder =
                    await ApplicationData.Current.LocalFolder.CreateFolderAsync("ProfilePhotoFolder",
                    CreationCollisionOption.OpenIfExists);

                    await photo.CopyAsync(destinationFolder, PhotoName, NameCollisionOption.ReplaceExisting);
                    await photo.DeleteAsync();
                }

                if (photo == null)//If photo capture operaion is cancelled the following code is executed
                {
                    var msg = new MessageDialog("Photo Capture operation Cancelled");
                    await msg.ShowAsync();
                    return;
                }
            }
            catch (Exception exp)
            {
                var expmsg = new MessageDialog(exp.ToString());
                await expmsg.ShowAsync();
                return;

            }
        }

        private void controlGallery_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PhotoPage));
        }


    }
}
