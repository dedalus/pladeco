﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pladeco.Domain
{
    public class Role : IdentityRole
    {
        

        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }

        public Role(string roleName, string description, DateTime creationDate) : base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
            this.Active = true;
        }

        public bool Active { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
