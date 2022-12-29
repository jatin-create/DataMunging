using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataMunging
{
    class Program
    {
        static void Main(string[] args)
        {
            string minDiffTeam = PrintMinDiff(1,6,8, @"football.dat");

            Console.WriteLine("Mininum Goal Diff Team = {0}", minDiffTeam);

            string minWeatherSpread = PrintMinDiff(0,1,2, @"weather.dat");

            Console.WriteLine("Min. Weather Spread on = {0}", minWeatherSpread);

            Console.ReadLine();
        }

        private static string PrintMinDiff(int paramNum, int col1, int col2, string filename)
        {
            int? minDiff = null;
            int maxCol = col1 > col2 ? col1 : col2;
            string filePath = filename;
            string teamName = "";
            string[] qa = File.ReadAllLines(filePath);
            for (int i = 1; i < qa.Length; i++)
            {
                string[] cols = qa[i].Split(' ');
                cols = cols.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int a = 0;
                int b = 0;
                if (cols.Length > maxCol && ParseStringToNum(cols[col1],out a))
                {
                    if(ParseStringToNum(cols[col2],out b))
                    {
                        int diff = GetDiff(a, b);
                        if(!minDiff.HasValue || diff < minDiff)
                        {
                            minDiff = diff;
                            teamName = cols[paramNum];
                        }
                    }
                }
            }

            return teamName;
        }

        private static bool ParseStringToNum(string number, out int num)
        {
            string resultString = Regex.Match(number, @"\d+").Value;
            return int.TryParse(resultString, out num);
        }

        private static int GetDiff(int a, int b)
        {
            return a > b ? a - b : b - a;
        }
    }
}
