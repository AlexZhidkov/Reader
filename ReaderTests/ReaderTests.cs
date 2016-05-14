using System;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderLib;

namespace ReaderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetBlog()
        {
            var reader = new Reader();
            var blog = reader.GetBlog("http://sethgodin.typepad.com");
            Assert.IsTrue(blog.Any());
        }

        [TestMethod]
        public void ReadFirstBlogEntry()
        {
            var reader = new Reader();
            var blog = reader.GetBlog("http://sethgodin.typepad.com");

            reader.Read(blog.First());
        }

        [TestMethod]
        public void ReadAllBlogEntries()
        {
            var reader = new Reader();
            var blog = reader.GetBlog("http://sethgodin.typepad.com");

            foreach (var entry in blog)
            {
                reader.Read(entry);
            }
        }

        [TestMethod]
        public void ShowInstalledVoices()
        {
            var installedVoices = new StringBuilder();

            using (var synthesizer = new SpeechSynthesizer())
            {
                foreach (var voice in synthesizer.GetInstalledVoices().Select(v => v.VoiceInfo))
                {
                    installedVoices.AppendFormat("Name:{0}, Gender:{1}, Age:{2}",
                        voice.Description, voice.Gender, voice.Age);
                    installedVoices.AppendLine();
                }
            }
        }
    }
}
