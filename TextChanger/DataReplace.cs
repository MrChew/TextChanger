using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace TextChanger
{
    public class DataReplace
    {
        private string _content;
        private int _processedFiles;
        private int _foundFiles;
        int _countSubtitud=0;
        private MatchCollection _matchesCount = null;

  
        public int ReplaceTextInFile(string pattern, string newValue, string path)
        {
            try
            {
                using (var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (var reader = new StreamReader(file))
                    {
                        _content = reader.ReadToEnd();
                    }
                 }
                _content=this.Textreplace(_content, pattern, newValue);
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(_content);
                }
                _processedFiles++;
                return _matchesCount.Count;
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

        public int ReplaceInFiles(string pattern, string newValue, IEnumerable<string> source)
        {
           
            foreach (string path in source)
            {
                _countSubtitud=this.ReplaceTextInFile(pattern, newValue, path);
            if (_countSubtitud != 0)
               {
                Console.WriteLine("File '{0}': subtituted {1} entries.", path, _countSubtitud);
               }
           }
            for (int i=1; i<20; i++)
                Console.Write("* ");
            Console.WriteLine();
            Console.WriteLine("Specified: {0} files; Found: {1} files; Processed: {2} files", source.Count(), _foundFiles, _processedFiles);
            return (_processedFiles);
        }

        public string Textreplace(string data, string pattern, string newValue)
        {
            var regText = new Regex(pattern, RegexOptions.ExplicitCapture);
            _matchesCount = regText.Matches(data);
            return (regText.Replace(data, newValue));
        }
        
    }
}
