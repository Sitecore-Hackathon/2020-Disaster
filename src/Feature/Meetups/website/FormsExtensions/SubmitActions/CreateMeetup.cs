using System;
using System.Linq;
using Disaster.Feature.Meetups.Constants;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.SecurityModel;
using Sitecore.Data.Fields;
using static System.FormattableString;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using Sitecore.Publishing;
using Sitecore.Configuration;
using Disaster.Feature.Meetups.Helpers;

namespace Disaster.Feature.Meetups.FormsExtensions.SubmitActions
{
    public class CreateMeetup : SubmitActionBase<string>
    {
        public CreateMeetup(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }

        protected override bool TryParse(string value, out string target)
        {
            target = string.Empty;
            return true;
        }

        protected override bool Execute(string data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));

            //bouncers
            if (!formSubmitContext.HasErrors) { Logger.Info(Invariant($"Form {formSubmitContext.FormId} submitted successfully."), this); } else { Logger.Warn(Invariant($"Form {formSubmitContext.FormId} submitted with errors: {string.Join(", ", formSubmitContext.Errors.Select(t => t.ErrorMessage))}."), this); }

            var db = Factory.GetDatabase("master");
            using (new SecurityDisabler())
            {
                var meetupsRootItem = db.GetItem(ItemIds.MeetupsPoisRoot);
                if (meetupsRootItem == null) { Log.Error("Meetups root item does not exist", this); return false; }

                //create Meetup POI item
                var newMeetupItem = meetupsRootItem.Add("Meetup " + DateTime.Now.ToString("hh-mm-ss"), TemplateIds.MeetupPoi);

                //add the new POI item to the map item
                var mapItem = db.GetItem(ItemIds.MeetupsMap);
                if (mapItem == null) { Log.Error("Meetups map item does not exist", this); return false; }

                var poiFieldOnMapItem = (MultilistField)mapItem.Fields[FieldIds.MapPoi];
                if (poiFieldOnMapItem == null) { Log.Error("Poi field does not exist. You may have forgetten to install SXA!", this); return false; }

                using (new EditContext(mapItem)) { poiFieldOnMapItem.Add(newMeetupItem.ID.ToString()); }

                //copy form values onto the newly created item
                //TODO replace with the GUI mapper unless need computed fields
                MapMeetupFormValuesToItem(newMeetupItem, formSubmitContext);

                //publish the updated and the created items
                PublishingHelper.Publish(mapItem);
                PublishingHelper.Publish(newMeetupItem);
            }

            return true;
        }

        private void MapMeetupFormValuesToItem(Item newMeetupItem, FormSubmitContext formSubmitContext)
        {
            var dateFieldValueFromForm = FormsHelper.ParseFieldValue(FormsHelper.GetFieldById(FieldIds.CreateNewMeetupForm.StartDateTime.ToString(), formSubmitContext.Fields));
            var titleFieldValueFromForm = FormsHelper.ParseFieldValue(FormsHelper.GetFieldById(FieldIds.CreateNewMeetupForm.Title.ToString(), formSubmitContext.Fields));
            var latitudeFieldValueFromForm = FormsHelper.ParseFieldValue(FormsHelper.GetFieldById(FieldIds.CreateNewMeetupForm.Latitude.ToString(), formSubmitContext.Fields));
            var longitudeFieldValueFromForm = FormsHelper.ParseFieldValue(FormsHelper.GetFieldById(FieldIds.CreateNewMeetupForm.Longitude.ToString(), formSubmitContext.Fields));
            var descriptionFieldValueFromForm = FormsHelper.ParseFieldValue(FormsHelper.GetFieldById(FieldIds.CreateNewMeetupForm.Description.ToString(), formSubmitContext.Fields));

            using (new EditContext(newMeetupItem))
            {
                newMeetupItem[FieldIds.MeetupPoi.StartDateTime] = Sitecore.DateUtil.ToIsoDate(DateTime.Parse(dateFieldValueFromForm, new CultureInfo("en-US")));
                newMeetupItem[FieldIds.MeetupPoi.Title] = titleFieldValueFromForm;
                newMeetupItem[FieldIds.MeetupPoi.Latitude] = latitudeFieldValueFromForm;
                newMeetupItem[FieldIds.MeetupPoi.Longitude] = longitudeFieldValueFromForm;
                newMeetupItem[FieldIds.MeetupPoi.Description] = descriptionFieldValueFromForm;
                newMeetupItem[FieldIds.MeetupPoi.Type] = ItemIds.MeetupPoiType.ToString();
            }
        }


    }
}