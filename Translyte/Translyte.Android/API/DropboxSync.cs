using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropboxSync.Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Translyte.Android.Views;

namespace Translyte.Android.API
{
    public class DropboxSync
    {
        const string DropboxSyncKey = "goenzzov1epvl88";
        const string DropboxSyncSecret = "6ms3j2412l0dm8p";

        public DBAccountManager Account { get; private set; }

        public DBDatastore DropboxDatastore { get; set; }

        public DBFileSystem FileSystem { get; private set; }

        public void StartApp(DBAccount account = null)
        {
            
            InitializeDropbox(account);
            FileSystem = DBFileSystem.ForAccount(Account.LinkedAccount);
            var books = FileSystem.ListFolder(new DBPath(""));

            DropboxDatastore.Sync();

        }
        public void BootstrapDropbox(Activity context)
        {
            // Setup Dropbox.
            Account = DBAccountManager.GetInstance(context.ApplicationContext, DropboxSyncKey, DropboxSyncSecret);
            Account.LinkedAccountChanged += (sender, e) =>
            {
                if (e.P1.IsLinked)
                {
                }
                else
                {
                    Account.StartLink(context, 0);
                    return;
                }
                Account = e.P0;
                StartApp(e.P1);
            };
            // TODO: Restart auth flow.
            if (!Account.HasLinkedAccount)
            {
                //StartApp();
                Account.StartLink(context, 0);
            }
            else
            {
                StartApp();
            }
        }

        void InitializeDropbox(DBAccount account)
        {
            Log("InitializeDropbox");
            if (DropboxDatastore == null || !DropboxDatastore.IsOpen || DropboxDatastore.Manager.IsShutDown)
            {
                DropboxDatastore = DBDatastore.OpenDefault(account ?? Account.LinkedAccount);
                DropboxDatastore.DatastoreChanged += HandleStoreChange;
            }
        }

        void HandleStoreChange(object sender, DBDatastore.SyncStatusEventArgs e)
        {
            if (e.P0.SyncStatus.HasIncoming)
            {
                if (!Account.HasLinkedAccount)
                {
                }
                Console.WriteLine("Datastore needs to be re-synced.");
                DropboxDatastore.Sync();
            }
        }
        void UpdateDropbox()
        {
        }
        bool VerifyStore()
        {
            if (!DropboxDatastore.IsOpen)
            {
                //Log("VerifyStore", "Datastore is NOT open.");
                return false;
            }
            if (DropboxDatastore.Manager.IsShutDown)
            {
                //Log("VerifyStore", "Manager is shutdown.");
                return false;
            }
            if (!Account.HasLinkedAccount)
            {
                //Log("VerifyStore", "Account was unlinked while we weren't watching.");
                return false;
            }
            return true;
        }

        void RestartAuthFlow()
        {
            if (Account.HasLinkedAccount)
                Account.Unlink();
            else { }
                //Account.StartLink(this, (int)RequestCode.LinkToDropboxRequest);
        }
        void Log(string location)
        {
            global::Android.Util.Log.Debug(location, String.Empty);
        }
        
    }
}