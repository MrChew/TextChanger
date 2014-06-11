using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;



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

           string OldValue = (string)(args[0]);
           string NewValue = (string)(args[1]);

            //Reading pathes to files
            string[] Pathes = ConfigurationManager.AppSettings["Path"].Split(';');

            //Create regular 
            Regex RegText = new Regex(OldValue, RegexOptions.ExplicitCapture);

            //V
            int SpecifiedFiles = Pathes.Length;
            int FoundFiles = SpecifiedFiles;
            int ProcessedFiles = 0;
            string content="";
            int i = 0;

            foreach (string Path in Pathes)
            {
                i++;
                try
                {
                    using (FileStream file = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite))
                    { 
                        using (StreamReader reader = new StreamReader(file))
                            {
                             content= reader.ReadToEnd();
                            }
                    }
                    // Counting matching in file
                    MatchCollection MatchesCount = RegText.Matches(content);
                    // Replacing %OldValue% to %NewValue%
                    if (MatchesCount.Count != 0)
                    {
                        content = RegText.Replace(content, NewValue);
                        // Open stream for writing data into file
                        using (StreamWriter writer = new StreamWriter(Path))
                        {
                            writer.Write(content);
                        }
                        Console.WriteLine("{0}. File <{1}>: subtituted {2} entries.", i, Path, MatchesCount.Count);
                        ProcessedFiles++;
                    }
                    else
                    {
                        Console.WriteLine("{0}.File <{1}> no matches found.", i, Path);
                    }
                }
                    
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("{0}. File <{1}> not found", i, Path);
                    FoundFiles--;
                }

                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("{0}. File <{1}> access denied. Please run programm as Administrator", i, Path);
                }

                catch (FileLoadException e)
                {
                    Console.WriteLine("{0}. File <{1}> access denied", i, Path);
                }
                
            }
            Console.WriteLine();
            Console.WriteLine("---Summary---");
            Console.WriteLine("Specified: {0} files; Found: {1} files; Processed: {2} files",SpecifiedFiles,FoundFiles,ProcessedFiles);
            //Console.ReadKey();
        }
    }
}