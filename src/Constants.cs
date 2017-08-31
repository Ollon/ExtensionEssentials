// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace ExtensionEssentials
{
    public class Constants
    {
        public const string LiveFeedUrl = "http://rawgit.com/ollon/ExtensionEssentials/master/extensions.json";
        public static readonly string LiveFeedCachePath =
            Environment.ExpandEnvironmentVariables("%localAppData%\\" + Vsix.Name + "\\feed.json");
        public static readonly string LogFilePath =
            Environment.ExpandEnvironmentVariables("%localAppData%\\" + Vsix.Name + "\\installer.log");
        public const double UpdateIntervalDays = 1;
        public const string RegistrySubKey = "ExtensionEssentials";
    }
}
