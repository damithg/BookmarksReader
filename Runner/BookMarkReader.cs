using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookmarksManager;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Runner
{
    [TestFixture]
    public class BookMarkReader
    {
        [Test]
        public void GetBookMark()
        {
            var bookmarksString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Roaming\\Google\\Chrome\\Default\\Bookmarks");
            var bookmarksfile = "E:\\bookmarks_5_17_16.html";


            //var reader = new NetscapeBookmarksReader();
            //var bookmarks = reader.Read(bookmarksString);
            //foreach (var b in bookmarks.AllLinks)
            //{
            //    Console.WriteLine("Url: {0}; Title: {1}", b.Url, b.Title);
            //}

            //Read bookmarks from file
            using (var file = File.OpenRead(bookmarksfile))
            {
                var reader1 = new NetscapeBookmarksReader();
                //supports encoding detection when reading from stream
                var bookmarks2 = reader1.Read(file);
                foreach (var b in bookmarks2.AllLinks.Where(l => l.LastVisit < DateTime.Today))
                {
                    Console.WriteLine("Type {0}, Title: {1}", b.GetType().Name, b.Title);
                }
            }

        }

        [Test]
        public void GetBookMarkStream()
        {
            var bookmarksString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Roaming\\Google\\Chrome\\Default\\Bookmarks");
            var bookmarksfile = "E:\\bookmarks_5_17_16.html";

            var reader = new NetscapeBookmarksReader();

            using (var s = new FileStream(bookmarksfile, FileMode.Open))
            {
                var c = reader.Read(s);

                var r = JsonConvert.SerializeObject(c);

                File.WriteAllText("E:\\Json", r);


                Assert.AreEqual(c.Count, 10);
            }

            //var reader = new NetscapeBookmarksReader();
            //var bookmarks = reader.Read(bookmarksString);
            //foreach (var b in bookmarks.AllLinks)
            //{
            //    Console.WriteLine("Url: {0}; Title: {1}", b.Url, b.Title);
            //}

            //Read bookmarks from file
            using (var file = File.OpenRead(bookmarksfile))
            {
                var reader1 = new NetscapeBookmarksReader();
                //supports encoding detection when reading from stream
                var bookmarks2 = reader1.Read(file);
                foreach (var b in bookmarks2.AllLinks.Where(l => l.LastVisit < DateTime.Today))
                {
                    Console.WriteLine("Type {0}, Title: {1}", b.GetType().Name, b.Title);
                }
            }

        }

    }
}
