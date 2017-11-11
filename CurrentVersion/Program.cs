using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Data.SqlTypes;
using System.Net;

namespace CurrentVersion
{
    class Program
    {
        static void Main(string[] args)
        {

            //Get's scripts location 
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string dir = currentDirectory + @"\ComputerVerion.csv";

            //OS name 
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
            string pathName = (string)registryKey.GetValue("productName");

            //Gets PCs name
            SqlString strHostName = string.Empty;
            strHostName = Dns.GetHostName();

            //Check csv exists, if not creates it 
            if (!File.Exists(dir))
            {
                StringBuilder csvcontentNOT = new StringBuilder();
                csvcontentNOT.AppendLine("");
                string csvpathNOT = dir;
                File.AppendAllText(csvpathNOT, csvcontentNOT.ToString());
            }

            //Time
            DateTime time = DateTime.Now;
            //Build number
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            int os_version_build = osInfo.Version.Build;

            //Delete lines that include your pc name 
            var oldLines = System.IO.File.ReadAllLines(dir);
            var newLines = oldLines.Where(line => !line.Contains(strHostName.ToString()));
            System.IO.File.WriteAllLines(dir, newLines);

            //Adds all the strings together into one string
            string Complete = time.ToString("dd/MM h:mm tt") + "," + strHostName.ToString() + "," + os_version_build.ToString() + "," + pathName;


            //creates file
            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine(Complete);
            string csvpath = dir;
            File.AppendAllText(csvpath, csvcontent.ToString());

            //this will ask you to click yes before running 

        }


    }



}
