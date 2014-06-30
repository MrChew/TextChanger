using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Configuration;



namespace TextChanger
{
    class Tch
    {
       static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length >= 3)
            {
                Console.WriteLine("Invalid parameters. Enter according to example: TextChanger [OldValue] [NewValue]");
                Environment.Exit(0);
            }

           string Pattern = (string)(args[0]);
           string NewValue = (string)(args[1]);

            //Reading pathes to files
            string[] Pathes = ConfigurationManager.AppSettings["Path"].Split(';');
           
           DataReplace DR = new DataReplace();
           int CountSubtitud=0;
           foreach (string Path in Pathes)
            {
                CountSubtitud = DR.ReplaceTextInFile(Pattern, NewValue, Path);
                if (CountSubtitud != 0)
                {
                    Console.WriteLine("File <{0}>: subtituted {1} entries.",Path, CountSubtitud);
                }
            }
            Console.WriteLine();
            DR.Summary(Pathes.Length);
            //Console.ReadKey();
        }
    }
}