// -----------------------------------------------------------------------
// <copyright file="VSCommandTable.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ExtensionEssentials
{
    using System;

    /// <summary>
    ///     Helper class that exposes all GUIDs used across VS Package.
    /// </summary>
    internal sealed partial class PackageGuids
    {
        public const string guidVSPackageString = "f168228c-63f6-4db0-b426-43c30e9d1fc7";
        public const string guidVSPackageCmdSetString = "9232585f-9c24-4dc7-bce8-985261542acf";
        public const string iconsString = "26d84633-82d5-440d-af48-28e57bcab783";
        public static Guid guidVSPackage = new Guid(guidVSPackageString);
        public static Guid guidVSPackageCmdSet = new Guid(guidVSPackageCmdSetString);
        public static Guid icons = new Guid(iconsString);
    }

    /// <summary>
    ///     Helper class that encapsulates all CommandIDs uses across VS Package.
    /// </summary>
    internal sealed partial class PackageIds
    {
        public const int ResetExtensions = 0x0200;
        public const int small = 0x0001;
    }
}
