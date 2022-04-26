﻿namespace YoumaconSecurityOps.Web.Client.Invariants;

public sealed class Errors
{
    public const String UnhandledErrorDebug = @"An unhandled error occurred. {0}";

    public const String UnhandledError = @"An error has occurred in the application" +
                                         "Please contact our support team if the problem persists, citing the correlation id of the Error Message. Correlation Id: {0}";

    public const String ValidationFailure = @"Validation errors have occurred on the server.";
}

