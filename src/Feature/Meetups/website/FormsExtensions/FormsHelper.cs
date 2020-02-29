using System;
using System.Linq;
using Sitecore.ExperienceForms.Models;
using System.Collections.Generic;
using System.Reflection;

namespace Disaster.Feature.Meetups.FormsExtensions
{
    public static class FormsHelper
    {
        public static IViewModel GetFieldById(string id, IList<IViewModel> fields)
        {
            return fields.SingleOrDefault<IViewModel>((IViewModel x) => x.ItemId == new Guid(id).ToString());
        }

        public static string ParseFieldValue(IViewModel field)
        {
            if (field == null) { return null; }

            PropertyInfo property = field?.GetType()?.GetProperty("Value");
            if (property == null)
                return null;

            var value = property.GetValue(field);

            return value.ToString();
        }
    }
}