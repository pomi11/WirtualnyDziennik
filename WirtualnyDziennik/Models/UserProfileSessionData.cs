using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WirtualnyDziennik.Models
{
    [Serializable]
    public class UserProfileSessionData
    {
        public int UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string Typ { get; set; }
    }
}