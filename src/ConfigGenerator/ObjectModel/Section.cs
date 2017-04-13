using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigGenerator.ObjectModel
{

    /// <summary>
    /// Generates section class
    /// </summary>
    public class Section : ClassBase
    {

        /// <summary>
        /// Initialize object
        /// </summary>
        public Section()
        {
            Type = ClassType.Section;
        }

        protected override string OnCodeTail()
        {
            return @"        /// <summary>
        /// Loads the configuration section.
        /// </summary>
        /// <returns>The configuration section for " + Name + @".</returns>
        public static " + Name.CapitalizeFirstLetter() + Type + @" Load()
        {
            object section = ConfigurationManager.GetSection(""" + Name + @"Group/" + Name + @""");

            return (" + Name.CapitalizeFirstLetter() + Type + @")section;
        }" + Environment.NewLine + Environment.NewLine;

        }
        
    }
}
