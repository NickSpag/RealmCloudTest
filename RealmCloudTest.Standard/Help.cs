using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using System.Runtime.CompilerServices;

namespace RealmCloudTest.Standard
{
    public static class Help
    {
        public static class RealmDB
        {
            //formatted as "realmName", "realmName.realm" or either with a "/subdirectory/" beforehand. must specify at least a realm name if the user is not an admin
            private const string realmNameOrPath = "realmName";

            //REQUIRED - typically: "instanceNameYouChose.us1.cloud.realm.io"
            private const string instanceHostName = "";

            private static (string username, string password) debugAccount = (username: "debugUser",
                                                                              password: "debugPassword");

            private static Uri authUrl = new Uri("https://" + instanceHostName);
            //the "/~/" tells Realm Cloud to refer to the users' subdirectory, which is their userId. must use the subdirectory unless the user is an admin
            private static Uri dataUrl = new Uri("realms://" + instanceHostName + $"/~/{realmNameOrPath}");


            private static SyncConfiguration syncConfigForActiveUser;

            public static Realm RealmForAuthenticatedUser => Realm.GetInstance(syncConfigForActiveUser);

            #region Authentication
            private static async Task<bool> TryLogIn(Credentials credentials)
            {
                try
                {
                    var debugUser = await User.LoginAsync(credentials, authUrl);

                    //do not use the optional path overload, specify it above in realmlNameOrPath
                    syncConfigForActiveUser = new SyncConfiguration(debugUser, dataUrl);
                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }

            public static async Task<bool> AuthenticateDebugUser()
            {
                //calling log in with create user flag, and if that fails without the create user flag.
                if (await TryLogIn(CredentialsObject(true)))
                {
                    System.Console.WriteLine("Debug user successfuly logged in");
                }
                else
                {
                    if (await TryLogIn(CredentialsObject(false)))
                    {
                        System.Console.WriteLine("Debug user successfuly logged in");
                    }
                    else
                    {
                        System.Console.WriteLine("Debug user login was unsuccessful");
                        return false;
                    }
                }

                return true;

                //Local function to create Credentials with(out) the "account needs to be created" flag
                Credentials CredentialsObject(bool accountNeedsCreation) => Credentials.UsernamePassword(debugAccount.username,
                                                                                                         debugAccount.password,
                                                                                                         accountNeedsCreation);
            }
            #endregion


        }
    }
}
