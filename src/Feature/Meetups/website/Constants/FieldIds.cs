using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Disaster.Feature.Meetups.Constants
{
    public class FieldIds
    {
        public static readonly ID MapPoi = new ID("{DF9686EE-85F7-4AD3-BDD2-8A28D31740DF}");

        public static class CreateNewMeetupForm
        {
            public static readonly ID StartDateTime = new ID("{C3B391C8-CBE6-414E-BB1D-1D4EE12715A5}");
            public static readonly ID Title = new ID("{89860BCE-9159-4399-8B7D-73D61EF8ACD9}");
            public static readonly ID Latitude = new ID("{9E34040D-A8F5-4100-B501-3BE77CC5CF45}");
            public static readonly ID Longitude = new ID("{78ABBC13-1334-4A29-8C8E-B94A1F7C32DA}");
            public static readonly ID Description = new ID("{1C1D0380-83DF-488E-923B-2D6BB02A4ACE}");
        }

        public static class MeetupPoi
        {
            public static readonly ID Latitude = new ID("{1DF37811-7355-4EE2-B1C7-ECBD6BE8DF44}");
            public static readonly ID Longitude = new ID("{38732912-D8C3-46C3-9E23-6933552429FA}");
            public static readonly ID Type = new ID("{8E09C5D1-4EC5-48FD-B355-3D1C65DD5C0F}");
            public static readonly ID Title = new ID("{E428DD88-04A5-43C2-BE37-35E0E3CF9890}");
            public static readonly ID Description = new ID("{7DB4985A-73A1-44A3-9B2C-FFEA56093431}");
            public static readonly ID StartDateTime = new ID("{4AAC2F16-E9A0-4184-AD0B-1A2A9A008ED7}");
        }
    }
}