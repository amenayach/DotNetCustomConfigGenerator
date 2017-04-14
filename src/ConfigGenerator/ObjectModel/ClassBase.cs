using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigGenerator.ObjectModel
{
    /// <summary>
    /// The common class generator base
    /// </summary>
    public abstract class ClassBase
    {

        /// <summary>
        /// Holds properties keys
        /// </summary>
        protected string PropertyKeys { get; set; }

        /// <summary>
        /// Holds properties
        /// </summary>
        protected string Properties { get; set; }

        /// <summary>
        /// The class namespace
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The class name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The class type
        /// </summary>
        public ClassType Type { get; set; }

        /// <summary>
        /// Generate class as string
        /// </summary>
        public virtual string Generate()
        {
            var script = @"namespace " + Namespace + @".Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Represents the configuration for list.
    /// </summary>
    public class " + Name.CapitalizeFirstLetter() + Type + @" : " + GetInheritanceType() + @"
    {
" + Environment.NewLine + PropertyKeys + Environment.NewLine +
     Properties + OnCodeTail() +
@"    }
}";

            return script;

        }

        /// <summary>
        /// Get inheritance type according to the type
        /// </summary>
        private string GetInheritanceType()
        {
            switch (Type)
            {
                case ClassType.Section:
                    return "ConfigurationSection";
                case ClassType.Collection:
                    return "ConfigurationElementCollection";
                default:
                    return "ConfigurationElement";
            }
        }

        /// <summary>
        /// Fill the bottom of the class
        /// </summary>
        protected virtual string OnCodeTail()
        {
            return string.Empty;
        }

        /// <summary>
        /// Add property
        /// </summary>
        public virtual void AddProperty(string name, string type, string firstChild)
        {

            var capitalName = name.CapitalizeFirstLetter();

            this.PropertyKeys += @"        /// <summary>
        /// The name of the " + name + @" configuration attribute.
        /// </summary>" + Environment.NewLine +
                @"        private const string " + capitalName + @"Key = """ + name + @""";" + Environment.NewLine + Environment.NewLine;

            this.Properties += @"        /// <summary>
        /// The " + name + @" configuration attribute.
        /// </summary>
        [ConfigurationProperty(" + capitalName + @"Key)]" +
        (!string.IsNullOrEmpty(firstChild) ? (Environment.NewLine + @"        [ConfigurationCollection(typeof(" + type + @"), AddItemName = """ + firstChild + @""")]" + Environment.NewLine) : "") + @"
        public " + type + @" " + capitalName + @"
        {
            get
            {
                return (" + type + @")this[" + capitalName + @"Key];
            }
            set
            {
                this[" + capitalName + @"Key] = value;
            }
        }" + Environment.NewLine + Environment.NewLine;

        }

        /// <summary>
        /// Add property
        /// </summary>
        public virtual void AddProperty(XmlAttribute attribute)
        {

            var name = attribute.Name;

            var type = GetAttributeType(attribute);

            AddProperty(name, type, string.Empty);

        }

        /// <summary>
        /// Add property
        /// </summary>
        public virtual void AddProperty(XmlNode node)
        {

            var name = node.Name;

            var type = node.Name.CapitalizeFirstLetter() + (IsCollection(node) ? "Collection" : "Element");

            AddProperty(name, type, IsCollection(node) ? GetFirstChildName(node) : string.Empty);

        }

        /// <summary>
        /// Gets the first child
        /// </summary>
        private string GetFirstChildName(XmlNode node)
        {
            return node.HasChildNodes ? node.ChildNodes[0].Name : string.Empty;
        }

        /// <summary>
        /// Get attribute type according to value
        /// </summary>
        private string GetAttributeType(XmlAttribute attribute)
        {
            bool b;

            if (bool.TryParse(attribute.Value, out b))
            {
                return "bool";
            }

            int i;

            if (int.TryParse(attribute.Value, out i))
            {
                return "int";
            }

            decimal d;

            if (decimal.TryParse(attribute.Value, out d))
            {
                return "decimal";
            }

            DateTime dt;

            if (DateTime.TryParse(attribute.Value, out dt))
            {
                return "DateTime";
            }

            return "string";

        }

        /// <summary>
        /// Generate config section from xml string
        /// </summary>
        public string GenerateFromXml(XmlNode xml, string nameSpace)
        {

            Name = xml.Name;

            Namespace = nameSpace;

            if (Type != ClassType.Collection && xml.Attributes != null)
            {
                foreach (XmlAttribute attribute in xml.Attributes)
                {
                    AddProperty(attribute);
                }
            }

            foreach (XmlNode childNode in xml.ChildNodes)
            {
                if (Type != ClassType.Collection)
                {
                    AddProperty(childNode);
                }

                if (IsCollection(childNode))
                {
                    var collection = new Collection(childNode.ChildNodes[0].Name, childNode.ChildNodes[0].Attributes != null && childNode.ChildNodes[0].Attributes.Count > 0 ? childNode.ChildNodes[0].Attributes[0].Name : "Key");

                    var collectionScript = collection.GenerateFromXml(childNode, nameSpace);

                    foreach (XmlNode node in childNode.ChildNodes)
                    {
                        var element = new Element();

                        var elementScript = element.GenerateFromXml(node, nameSpace);

                        FileSaver.Save(node.Name.CapitalizeFirstLetter() + "Element.cs", elementScript, false);

                    }

                    FileSaver.Save(childNode.Name.CapitalizeFirstLetter() + "Collection.cs", collectionScript, false);
                }
                else if (childNode.HasChildNodes || IsElement(childNode))
                {

                    var element = new Element();

                    var elementScript = element.GenerateFromXml(childNode, nameSpace);

                    FileSaver.Save(childNode.Name.CapitalizeFirstLetter() + "Element.cs", elementScript, false);

                }

            }

            var script = Generate();

            FileSaver.Save(xml.Name.CapitalizeFirstLetter() + Type + ".cs", script, false);

            return script;

        }

        /// <summary>
        /// Checks if a child node is a collection
        /// </summary>
        private bool IsCollection(XmlNode childNode)
        {
            return childNode != null && childNode.HasChildNodes && childNode.ChildNodes.Count > 1 &&
                   childNode.ChildNodes[0].Name == childNode.ChildNodes[1].Name;
        }

        /// <summary>
        /// Checks if a child node is a element
        /// </summary>
        private bool IsElement(XmlNode childNode)
        {
            return childNode != null && childNode.Attributes != null && childNode.Attributes.Count > 0;
        }

    }
}
