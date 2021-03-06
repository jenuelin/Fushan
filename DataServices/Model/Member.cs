﻿using System;

namespace DataServices.Model
{
    public class Member
    {
        public int Id { get; set; }
        public Guid IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
