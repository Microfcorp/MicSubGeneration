using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicSubGeneration
{
    public static class Validater
    {
        public static string TimeValid(string time)
        {
            string tmp = time;

            tmp += ",000";
            if (time.Split(':').Length < 3)
            {
                tmp = "00:" + tmp;
            }

            return tmp;
        }
        public static string TitleValid(string Time_Start, string Time_Stop, string Title)
        {
            string tmp = "";
            /*
                01:20:45,138 --> 01:20:48,164
                You'd say anything now
                to get what you want.
            */

            tmp += Time_Start + " --> " + Time_Stop;
            tmp += Environment.NewLine + Title;
            return tmp;
        }
    }
}
