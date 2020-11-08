using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cw2
{
    class Program
    {
        public static void ErrorLogging(Exception ex)
        {
            string logpath = @"C:\Users\Krzysiek\Desktop\log.txt";
            if (!File.Exists(logpath))
            {
                File.Create(logpath).Dispose();
            }
            StreamWriter sw = File.AppendText(logpath);
            sw.WriteLine("START" + DateTime.Now);
            sw.WriteLine("Error Message" + ex.Message);
            sw.WriteLine("END" + DateTime.Now);
            sw.Close();
        }
        static void Main(string[] args)
        {
            try
            {

                string csvp, xmlp, format;
                if (args.Length>0)
                {
                    if (args[0] != null)
                    {
                        csvp = args[0];
                    }
                    else
                    {
                        csvp = @"C:\Users\Krzysiek\Desktop\dane.csv";
                    }
                    if (args[1] != null)
                    {
                        xmlp = args[1];
                    }
                    else
                    {
                        xmlp = @"C:\Users\Krzysiek\Desktop\result.xml";
                    }
                    if (args[2] != null)
                    {
                        format = args[2];
                    }
                    else
                    {
                        format = "xml";
                    } 
                }
                else
                {
                    csvp = @"C:\Users\Krzysiek\Desktop\dane.csv";
                    xmlp = @"C:\Users\Krzysiek\Desktop\";
                    format = "xml";
                }
                if (File.Exists(csvp) && Directory.Exists(xmlp))
                {

                    string[] source = File.ReadAllLines(csvp);
                    System.Xml.Linq.XElement xml = new XElement("Root",
                        from str in source
                        let fields = str.Split(',')
                        select new XElement("studenci",
                            new XAttribute("student_indexnumber", "s" + fields[4]),
                            new XElement("fname", fields[0]),
                            new XElement("lname", fields[1]),
                            new XElement("birthdate", fields[5]),
                            new XElement("email", fields[6]),
                            new XElement("mothersName", fields[7]),
                            new XElement("fathersName", fields[8]),
                            new XElement("studies",
                                new XElement("name", fields[2]),
                                new XElement("mode", fields[3]))
                            )

                    );
                    xml.Save(String.Concat(xmlp + "result.xml"));
                }
                if (!File.Exists(csvp))
                {
                    throw new Exception("File does not exist");
                }
                if (!Directory.Exists(xmlp))
                {
                    throw new Exception("Directory does not exist");
                }
            }
            catch (Exception ex)
            {

                ErrorLogging(ex);
            }
        }
    }
}
