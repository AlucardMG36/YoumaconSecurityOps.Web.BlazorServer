﻿namespace YoumaconSecurityOps.Core.Mediatr.Queries;

public record GetRadioScheduleWithParameters(RadioScheduleQueryStringParameter Parameters) : StreamQueryBase<RadioScheduleReader>;