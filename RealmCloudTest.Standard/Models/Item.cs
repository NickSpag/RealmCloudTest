using System;
using Realms;
namespace RealmCloudTest.Standard.Models
{
    public class Item : RealmObject
    {
        public Item()
        {
        }

        public string Name { get; set; }
    }
}
