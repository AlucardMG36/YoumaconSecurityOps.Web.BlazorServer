﻿using System;
using System.Collections.Generic;

namespace YsecOps.Core.Models.DAO;

public partial class Pronoun
{
    public Pronoun()
    {
        Contacts = new HashSet<Contact>();
        NonStaffPeople = new HashSet<NonStaffPerson>();
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; }
    public virtual ICollection<NonStaffPerson> NonStaffPeople { get; set; }
}
