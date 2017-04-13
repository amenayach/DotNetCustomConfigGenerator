using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigGenerator.ObjectModel
{
    public class FileSaver
    {

        public static void Save(string filename, string content, bool open)
        {

            var folderPath = Path.Combine(Application.StartupPath, "Configuration");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, filename);

            File.WriteAllText(filePath, content);

            if (open)
            {
                Process.Start(filePath); 
            }
        }

    }
}
