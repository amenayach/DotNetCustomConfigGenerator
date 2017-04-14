using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ConfigGenerator.ObjectModel;

namespace ConfigGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    MessageBox.Show(@"Please fill the xml template");
                }
                else if (string.IsNullOrWhiteSpace(tbNamespace.Text))
                {
                    MessageBox.Show(@"Please fill the namespace");
                }
                else
                {

                    var xml = new XmlDocument();

                    xml.LoadXml(tb.Text);

                    var section = new Section();

                    section.GenerateFromXml(xml.FirstChild, tbNamespace.Text.Trim());

                    ConfigSample.Generate(tbNamespace.Text.Trim(), tb.Text);
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
