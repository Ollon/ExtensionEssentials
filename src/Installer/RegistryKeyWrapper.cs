// -----------------------------------------------------------------------
// <copyright file="RegistryKeyWrapper.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Win32;

namespace ExtensionEssentials
{
    public class RegistryKeyWrapper : IRegistryKey
    {
        private readonly RegistryKey _key;

        public RegistryKeyWrapper(RegistryKey key)
        {
            _key = key;
        }

        public IRegistryKey CreateSubKey(string subKey)
        {
            return new RegistryKeyWrapper(_key.CreateSubKey(subKey));
        }

        public object GetValue(string name)
        {
            return _key.GetValue(name);
        }

        public void SetValue(string name, object value)
        {
            _key.SetValue(name, value);
        }

        public void Dispose()
        {
            _key.Dispose();
        }
    }
}
