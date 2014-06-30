using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextChanger
{
    public class DataReplace
    {
         private string _content="";
        private int _ProcessedFiles = 0;
        private int _FoundFiles=0;


        public int ReplaceTextInFile(string pattern, string newValue, string path)
        {
            Regex RegText = new Regex(pattern, RegexOptions.ExplicitCapture);
            try
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        _content = reader.ReadToEnd();
                    }
                }
                // Counting matching in file
                MatchCollection MatchesCount = RegText.Matches(_content);
                // Replacing %pattern% to %NewValue%
                if (MatchesCount.Count != 0)
                {
                    _content = RegText.Replace(_content, newValue);
                    // Open stream for writing data into file
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.Write(_content);
                    }
                    _ProcessedFiles++;
                    return MatchesCount.Count;
                    //Console.WriteLine("{0}. File <{1}>: subtituted {2} entries.", i, Path, MatchesCount.Count);
                    //ProcessedFiles++;
                }
                else
                {
                    Console.WriteLine("File <{0}> no matches found.",path);
                    return (0);
                }
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine("File <{0}> not found", path);
                _FoundFiles++;
                return (0);
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("File <{0}> access denied. Please run programm as Administrator",path);
                return (0);
            }

            catch (FileLoadException e)
            {
                Console.WriteLine("File <{0}> access denied",path);
                return (0);
            }
        }

        public void Summary(int i)
        {
        Console.WriteLine("Specified: {0} files; Found: {1} files; Processed: {2} files", i, _FoundFiles, _ProcessedFiles);
        return;
        }

        public int FoundFiles()
        {
            return _FoundFiles;
        }
        public int ProcessedFiles()
        {
            return _ProcessedFiles;
        }
    }
}
