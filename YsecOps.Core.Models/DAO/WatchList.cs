﻿using System;
using System.Collections.Generic;

namespace YsecOps.Core.Models.DAO;

public partial class WatchList
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Reason { get; set; }
    public DateTime? LastSeenAt { get; set; }
}