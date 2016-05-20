using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Runner
{
    public class BookMark
    {
        //public string Link { get; set; }
        //public string IconData { get; set; }
        public string Line { get; set; }
    }

    [TestFixture]
    public class Class1
    {
        [Test]
        public void Read_BookMark()
        {
            var pattern = @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)";

            var filePath = "E:\\bookmarks_5_10_16.html";

            var line = "";
            StreamReader str = new StreamReader(filePath);

            var stringb = new StringBuilder();
            stringb.Append("<? xml version =\"1.0\" encoding=\"utf-8\"?>");

            var bookMarkList = new List<BookMark>();

            while ((line = str.ReadLine()) != null)
            {
                if (line.Contains("A HREF"))
                {
                    stringb.Append(line + "</DT>");

                    var line1 = line + "</DT>";
                    var match = Regex.Match(line, pattern);

                    bookMarkList.Add(new BookMark() { Line = line1 });
                }
            }
            var xmlre = ToXML(bookMarkList);


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlre);

            XmlNodeList elemList = doc.GetElementsByTagName("DT");

            for (int i = 0; i < elemList.Count; i++)
            {
                string attrVal = elemList[i].Attributes["HREF"].Value;
            }


            //doc.GetElementsByTagName("HREF")

            string jsonText = JsonConvert.SerializeXmlNode(doc);

            //File.WriteAllText("E:\\Json", jsonText);

            //var formatted = stringb.ToString();

            //XDocument xml = XDocument.Parse(formatted);

            //var query1 = bookMarkList.GroupBy(x => x.Link)
            //    .Where(g => g.Count() > 1)
            //    .Select(y => y.Key)
            //    .ToList();

            //var t = query1;


            //var query2 = bookMarkList.GroupBy(x => x.Link)
            //  .Where(g => g.Count() > 1)
            //  .Select(y => new { Element = y.Key, Counter = y.Count() })
            //  .ToList();


            //var c = query2;

            //foreach (var readLine in str.ReadLine())
            //{
            //    if (line.Contains("A HREF"))
            //    {
            //        var match = Regex.Match(line, pattern);

            //        bookMarkList.Add(new BookMark() {Link = match.Value });
            //    }
            //}


            //var fileContents = File.ReadAllText(filePath);

            //if (!string.IsNullOrEmpty(fileContents))
            //{
            //    Console.WriteLine(fileContents);
            //}
        }

        public string ToXML<T>( T data)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, data);
            return stringwriter.ToString();
        }
    }
}
