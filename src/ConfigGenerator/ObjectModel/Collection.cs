using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigGenerator.ObjectModel
{

    /// <summary>
    /// Generates collection class
    /// </summary>
    public class Collection : ClassBase
    {

        /// <summary>
        /// Holds the child name
        /// </summary>
        private readonly string _childName;

        /// <summary>
        /// Holds the key name
        /// </summary>
        private readonly string _childKey;
        
        /// <summary>
        /// Initialize object
        /// </summary>
        public Collection(string childName, string childKey)
        {
            Type = ClassType.Collection;
            _childName = childName;
            _childKey = childKey;
        }

        protected override string OnCodeTail()
        {
            return @"        /// <summary>
        /// Retrieves configuration instance that matches the specified list key.
        /// </summary>
        public " + _childName.CapitalizeFirstLetter() + @"Element GetConfigurationById(string key)
        {
            foreach (" + _childName.CapitalizeFirstLetter() + @"Element instanceConfig in this)
            {
                if (string.Equals(instanceConfig." + _childKey.CapitalizeFirstLetter() + @", key.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return instanceConfig;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a new instance of a configuration element that belongs to the collection.
        /// </summary>
        protected override ConfigurationElement CreateNewElement()
        {
            return new " + _childName.CapitalizeFirstLetter() + @"Element();
        }

        /// <summary>
        /// Retrieves the key of the configuration element.
        /// </summary>
        /// <returns>The key for the configuration element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((" + _childName.CapitalizeFirstLetter() + @"Element)element)." + _childKey.CapitalizeFirstLetter() + @";
        }" + Environment.NewLine + Environment.NewLine;

        }

    }
}
