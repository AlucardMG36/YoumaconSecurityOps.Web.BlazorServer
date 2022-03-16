﻿namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class HostingEnvironmentExtensions
{
    public static Boolean IsDevelopmentOrStaging(this IWebHostEnvironment env)
    {
        return env.IsDevelopment() || env.IsStaging();
    }
}