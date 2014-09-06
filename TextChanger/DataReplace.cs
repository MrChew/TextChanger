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

        public string Textreplace(string data, string pattern, string newValue)
        {
            var regText = new Regex(pattern, RegexOptions.ExplicitCapture);
            _matchesCount = regText.Matches(data);
            return (regText.Replace(data, newValue));
        }
        
    }
}
