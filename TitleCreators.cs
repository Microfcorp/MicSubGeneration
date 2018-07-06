using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MicSubGeneration
{
    class Title
    {
        public string Titles
        {
            get;
            set;
        }

        private int num = 1;

        public void SaveToFile(string path, string name)
        {
            StreamWriter str = File.CreateText(path + "\\" + name);
            str.Write(Titles);
            str.Close();
        }
        public void SaveToFile(string path)
        {
            StreamWriter str = File.CreateText(path);
            str.Write(Titles);
            str.Close();
        }
        public bool Add(string TitleGeneration)
        {
            Titles += num + Environment.NewLine + TitleGeneration + Environment.NewLine + Environment.NewLine;
            num += 1;
            return true;
        }
    }
}
