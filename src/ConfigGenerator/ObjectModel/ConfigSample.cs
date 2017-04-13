using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigGenerator.ObjectModel
{
    /// <summary>
    /// Generates config sample according to the generated xml template
    /// </summary>
    public static class ConfigSample
    {

        /// <summary>
        /// Generates web.config sample
        /// </summary>
        public static void Generate(string nameSpace, string xmlString)
        {

            var xml = new XmlDocument();

            xml.LoadXml(xmlString);

            var script = string.Format(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <configSections>
    <sectionGroup name=""{0}Group"">
      <section name=""{0}"" type=""{1}.Configuration.{2}Section, ConfigGenerator"" />
    </sectionGroup>
  </configSections>
  
  <{0}Group>
      {3}
  </{0}Group>
</configuration>", xml.FirstChild.Name, nameSpace, xml.FirstChild.Name.CapitalizeFirstLetter(), xmlString);

            FileSaver.Save("SampleWeb.config.txt", script, true);

        }


    }
}
