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
    public class Element : ClassBase
    {

        /// <summary>
        /// Initialize object
        /// </summary>
        public Element()
        {
            Type = ClassType.Element;
        }

    }
}
