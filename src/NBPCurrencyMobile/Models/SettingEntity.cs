using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NBPCurrencyMobile.Models
{
    public class SettingEntity
    {
        [PrimaryKey]
        public string Name { get; set; }

        [NotNull]
        public string Value { get; set; }

        public SettingEntity()
        {
        }

        public SettingEntity(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}