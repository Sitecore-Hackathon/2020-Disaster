using System;
using Sitecore.Data.Items;
using Sitecore.Publishing;
using Sitecore.Configuration;
namespace Disaster.Feature.Meetups.Helpers
{
    public class PublishingHelper
    {
        public static void Publish(Item item)
        {
            var sourceDb = Factory.GetDatabase("master");
            var targetDb = Factory.GetDatabase("web");
            PublishOptions myOptions = new PublishOptions(sourceDb, targetDb, PublishMode.Smart, Sitecore.Data.Managers.LanguageManager.DefaultLanguage, DateTime.Now);
            myOptions.RootItem = item;
            Publisher myPublisher = new Publisher(myOptions);
            var myPublishJob = myPublisher.PublishAsync();
        }
    }
}