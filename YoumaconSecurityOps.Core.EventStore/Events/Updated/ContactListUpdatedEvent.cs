﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Extensions;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.EventStore.Events.Updated
{
    public class ContactListUpdatedEvent : EventBase
    {
        public ContactListUpdatedEvent(ContactReader contactReader)
        {
            ContactReader = contactReader;
        }

        public ContactReader ContactReader { get; }
    }
}
