using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Notifications;

namespace BackGroundProcess
{
    public sealed class BackGroundFileWriter : IBackgroundTask
    {

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //This Method is run when the background process is run.Here the File Save method is called and SendToat method is also called to send a message
            FileSave();
            SendToast("Text is written");
        }
        private async void FileSave()
        {
            //This method is  is used create a txt file and write to it 
            Windows.Storage.StorageFolder storageFolder =
             Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile ticketsFile =
                await storageFolder.CreateFileAsync("BackGroundFile.txt",
                    CreationCollisionOption.ReplaceExisting);// A text file is created here replaced if such file exists

            //Write data to the file
            await FileIO.WriteTextAsync(ticketsFile, "This is text written by back ground process");




        }
        public static void SendToast(string message)
        {
            var xmlToastTemplate = "<toast launch=\"app-defined-string\">" +
                          "<visual>" +
                            "<binding template =\"ToastGeneric\">" +
                              "<text>BackGround Notification</text>" +
                              "<text>" +
                                message +
                              "</text>" +
                              "</binding>" +
                          "</visual>" +
                        "</toast>";

            // load the template as XML document
            var xmlDocument = new Windows.Data.Xml.Dom.XmlDocument();

            xmlDocument.LoadXml(xmlToastTemplate);

            // create the toast notification and show to user
            var toastNotification = new ToastNotification(xmlDocument);
            var notification = ToastNotificationManager.CreateToastNotifier();

            notification.Show(toastNotification);
        }
    };


}
