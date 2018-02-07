using System;

using UIKit;
using Realms.Sync;
using Realms;
using System.Threading.Tasks;
using RealmCloudTest.Standard;
using RealmCloudTest.Standard.Models;
using System.Linq;

namespace RealmCloudTest
{
    public partial class FirstViewController : UIViewController
    {
        //change this by hand if you want to redo the test
        private bool firstRun = true;


        private Realm uiRealm;

        protected FirstViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async partial void TestSyncClicked(Foundation.NSObject sender)
        {
            await CreateItem();
            DisplayItem();
        }

        private async Task CreateItem()
        {
            var item = new Item() { Name = "Local recall successful" };

            if (!await Help.RealmDB.Authenticate(firstRun))
            {
                System.Console.WriteLine("Failed to authenticate");
                return;
            }

            System.Console.WriteLine("Authentication successful");
            PersistItem(item);
        }

        private void PersistItem(Item item)
        {
            using (var realm = Help.RealmDB.RealmForAuthenticatedUser())
            {
                try
                {
                    realm.Write(() =>
                    {
                        realm.Add(item);
                    });
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Persistence unsuccessful: {ex.Message}");
                }

                System.Console.WriteLine("Persistence successful");
            }
        }

        private void DisplayItem()
        {
            if (uiRealm == null)
            {
                uiRealm = Help.RealmDB.RealmForAuthenticatedUser();
            }

            var item = uiRealm.All<Item>().FirstOrDefault();

            if (item != null)
            {
                Label.Text = item.Name;
                return;
            }
            else
            {
                Label.Text = "Local recall failure";
            }
        }
    }
}