using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;

namespace RealmCloudTest.Standard
{
    public static class Help
    {
        public static class RealmDB
        {
            private const string InstanceHostName = "workspace.us1.cloud.realm.io/";

            private static Uri authUrl = new Uri("https://" + InstanceHostName);
            private static Uri dataUrl = new Uri("realms://" + InstanceHostName);

            private static SyncConfiguration syncConfigForActiveUser;

            public static async Task<bool> Authenticate(bool needsCreation)
            {
                try
                {
                    var credentials = Credentials.UsernamePassword("nickname", "password", needsCreation);
                    var debugUser = await User.LoginAsync(credentials, authUrl);

                    var config = new SyncConfiguration(debugUser, dataUrl, "notDefault");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }

                return true;
            }

            public static Realm RealmForAuthenticatedUser()
            {
                return Realm.GetInstance(syncConfigForActiveUser);
            }

        }


    }
}
