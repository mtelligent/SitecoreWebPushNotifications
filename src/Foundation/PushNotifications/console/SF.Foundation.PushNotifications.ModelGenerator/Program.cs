using Sitecore.XConnect.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Foundation.PushNotifications.ModelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = XdbModelWriter.Serialize(SF.Foundation.PushNotifications.Models.CollectionModel.Model);
            System.IO.File.WriteAllText("PushNotifications.Model.json", json);
        }
    }
}
