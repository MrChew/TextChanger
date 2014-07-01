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
        private string _path = "";


        public int ReplaceTextInFile(string pattern, string newValue, string path)
        {
            Regex RegText = new Regex(pattern, RegexOptions.ExplicitCapture);
            _path = path;
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
                    return (0);
                }
            }

            catch (FileNotFoundException e)
            {
                _ErrorLogger(1);
                _FoundFiles++;
                return (0);
            }

            catch (UnauthorizedAccessException e)
            {
                _ErrorLogger(2);
                return (0);
            }

            catch (FileLoadException e)
            {
                _ErrorLogger(3);
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

        private void _ErrorLogger(int i)
        {
            switch(i)
            {
                case 1:
                    Console.WriteLine("File <{0}> not found", _path);
                    break;
                case 2:
                    Console.WriteLine("File <{0}> access denied. Please run programm as Administrator",_path);   
                    break;
                case 3:
                    Console.WriteLine("File <{0}> access denied", _path);
                    break;
            }
        }
    }
}
