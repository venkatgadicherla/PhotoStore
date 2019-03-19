using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class FolderPage : Page
    {
        public FolderPage()
        {
            this.InitializeComponent();
        }

        private async void panelNewFolder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // The follwing code is executed when the new aln=bum button is pressed.
            //A content dialog is invoked with a text box , a ok button and a cancel button
            var albumNameDialog = new ContentDialog();// This content dialog is used to take input for the album name.
            albumNameDialog.Width = 450;
            StackPanel dialogPanel = new StackPanel();//This Stackpanel provides the content for the content dialog
            
            TextBox tbAlbumName = new TextBox();

            TextBlock tblAlbumNameLabel = new TextBlock();
            tblAlbumNameLabel.Text = "Album name";
            dialogPanel.Children.Add(tblAlbumNameLabel);
            dialogPanel.Children.Add(tbAlbumName);
            albumNameDialog.Content = dialogPanel;
            albumNameDialog.PrimaryButtonText = "Ok";
            albumNameDialog.IsPrimaryButtonEnabled = false;//Intially the primary button is in disabled
            tbAlbumName.TextChanged += delegate// But when text is changed then the primary button is set enabled
            {
                if (!String.IsNullOrWhiteSpace(tbAlbumName.Text))// when a name in entered in the album name text box primary button is activated
                    {
                    albumNameDialog.IsPrimaryButtonEnabled = true;
                }
            };

            string albumName=null;
            albumNameDialog.PrimaryButtonClick += delegate
            {
                albumName = tbAlbumName.Text;
            };
            albumNameDialog.SecondaryButtonText = "Cancel";

             await albumNameDialog.ShowAsync();
          
            if (!String.IsNullOrWhiteSpace(albumName))//When the albumName is entered  the album is created
            {
                StorageFolder testFolder = await StorageFolder.GetFolderFromPathAsync(@"C:\Users\user\Pictures");// The initial folder is set to pictures folder

                bool FileExists = false;

                try
                {
                    IStorageItem item = await testFolder.TryGetItemAsync(albumName);
                    if (item != null)//The album name is cheked if it exists
                    {
                        FileExists = true;
                    }
                }
                catch (Exception exp)
                {
                    var expMsg = new MessageDialog(exp.ToString());
                    await expMsg.ShowAsync();

                }
                if (!FileExists)// if album doesnt exist a albUm is created
                {
                    try
                    {
                        await testFolder.CreateFolderAsync(albumName);
                        var Msg = new MessageDialog("New Album created");
                        await Msg.ShowAsync();
                    }
                    catch (Exception exp)
                    {
                        var expMsg = new MessageDialog(exp.ToString());
                        await expMsg.ShowAsync();

                    }

                }
                else
                {
                    var Msg = new MessageDialog("Album already exists");
                    await Msg.ShowAsync();
                }
            }
        }

        private void NavigationControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
