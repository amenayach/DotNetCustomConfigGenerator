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
    /// <summary>
    /// Handle files operations
    /// </summary>
    public class FileSaver
    {

        /// <summary>
        /// Saves a file in a configuration folder
        /// </summary>
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

        /// <summary>
        /// Open windows explorer where configuration folder is selected
        /// </summary>
        public static void BrowsePath(string filename)
        {
            var filePath = GetFilePath(filename);

            Process.Start("explorer.exe", $"/select,\"{filePath}\"");
        }

        /// <summary>
        /// Gets a file path in the configuration folder
        /// </summary>
        private static string GetFilePath(string filename)
        {
            var folderPath = Path.Combine(Application.StartupPath, "Configuration");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, filename);
        }

    }
}
