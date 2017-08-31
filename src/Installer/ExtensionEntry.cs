// -----------------------------------------------------------------------
// <copyright file="ExtensionEntry.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace ExtensionEssentials
{
    public class ExtensionEntry
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public Version MinVersion { get; set; }

        public Version MaxVersion { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
