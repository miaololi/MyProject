using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Tools
{

    public class ModelAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DisplayNameAttribute : Attribute
    {
        public string Name { get; }
        public DisplayNameAttribute(string sValue)
        {
            Name = sValue;
        }
    }
}
