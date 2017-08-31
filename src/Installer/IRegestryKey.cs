// -----------------------------------------------------------------------
// <copyright file="IRegestryKey.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionEssentials
{
    public interface IRegistryKey : IDisposable
    {
        IRegistryKey CreateSubKey(string subKey);

        void SetValue(string name, object value);

        object GetValue(string name);
    }
}
