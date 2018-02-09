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
        private Realm _uiRealm;
        private Realm uiRealm
        {
            get
            {
                if (_uiRealm == null)
                {
                    _uiRealm = Help.RealmDB.RealmForAuthenticatedUser;
                }

                return _uiRealm;
            }
        }

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
            if (await Authenticate())
            {
                CreateTestRealmObject();
                DisplayItem();

                System.Console.WriteLine("Test successful");
            }
            else
            {
                System.Console.WriteLine("Test failed");
            }
        }

        private async Task<bool> Authenticate() => await Help.RealmDB.AuthenticateDebugUser();

        private void CreateTestRealmObject()
        {
            var testItem = new Item() { Name = "Local recall successful" };

            PersistItem(testItem);
        }

        private void PersistItem(Item item)
        {
            using (var realm = Help.RealmDB.RealmForAuthenticatedUser)
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