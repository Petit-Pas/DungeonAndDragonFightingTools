using DDFight.Game.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {

            string text = "1d4+3d6+8";
            //string pat = @"^((?:\-?[0-9]+)|(?:|\-?[0-9]+d[0-9]+))(?:((?:\+|\-)[0-9]+)|((?:\+|\-)[0-9]+d[0-9]+))*$";
            string pat = @"^((?:\-?[0-9]+)|(?:|\-?[0-9]+d[0-9]+))((?:(?:\+|\-)[0-9]+)|(?:(?:\+|\-)[0-9]+d[0-9]+))*$";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match match = r.Match(text);
            if (match.Success)
                try
                {
                    for (int i = 1; i != match.Groups.Count; i += 1)
                    {
                        foreach (Capture capture in match.Groups[i].Captures)
                        {
                            string captured = capture.ToString();
                            if (captured.Contains("d"))
                            {
                                Console.WriteLine("adding dice {0}", captured);
                            }
                            else
                            {
                                Console.WriteLine("adding mod {0}", captured);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error");
                }

            Console.ReadKey();
        }
    }
}
