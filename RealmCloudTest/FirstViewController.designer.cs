// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RealmCloudTest
{
    [Register("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        UIKit.UILabel Label { get; set; }

        [Action("TestSyncClicked:")]
        partial void TestSyncClicked(Foundation.NSObject sender);

        void ReleaseDesignerOutlets()
        {
            if (Label != null)
            {
                Label.Dispose();
                Label = null;
            }
        }
    }
}
