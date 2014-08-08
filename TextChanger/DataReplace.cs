using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TextChanger
{
    public class DataReplace
    {
        private string _content;
        private int _processedFiles;
        private int _foundFiles;
        private string[] _source;
        int _countSubtitud;

 
        public int ReplaceTextInFile(string pattern, string newValue, string path)
        {
            Regex regText = new Regex(pattern, RegexOptions.ExplicitCapture);
            try
            {
                using (var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (var reader = new StreamReader(file))
                    {
                        _content = reader.ReadToEnd();
                    }
                }
                // Counting matching in file
                MatchCollection matchesCount = regText.Matches(_content);
                // Replacing %pattern% to %NewValue%
                if (matchesCount.Count != 0)
                {
                    _content = regText.Replace(_content, newValue);
                    // Open stream for writing data into file
                    using (var writer = new StreamWriter(path))
                    {
                        writer.Write(_content);
                    }
                    _processedFiles++;
                    return matchesCount.Count;
                }
                return (0);
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                _foundFiles++;
                return (0);
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                return (0);
            }

            catch (FileLoadException e)
            {
                Console.WriteLine(e.Message);
                return (0);
            }
        }

        public int ReplaceInFiles(string pattern, string newValue, string source)
        {
            _source = source.Split(';');
            
            foreach (string path in _source)
            {
                _countSubtitud=this.ReplaceTextInFile(pattern, newValue, path);
            if (_countSubtitud != 0)
               {
                Console.WriteLine("File '{0}': subtituted {1} entries.", path, _countSubtitud);
               }
           }
            for (int i=1; i<20; i++)
            {
            Console.Write("* ");
            }
            Console.WriteLine();
            Console.WriteLine("Specified: {0} files; Found: {1} files; Processed: {2} files", _source.Length, _foundFiles, _processedFiles);
            return (_processedFiles);
        }

    }
}
