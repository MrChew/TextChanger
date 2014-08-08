using System;
using System.Configuration;



namespace TextChanger
{
    class Tch
    {
       static void Main(string[] args)
        {
           //Reading arguments from command line 
           if (args.Length == 0 || args.Length >= 3)
            {
                Console.WriteLine("Invalid parameters. Enter according to example: TextChanger [OldValue] [NewValue]");
                Environment.Exit(0);
            }

           string pattern = args[0];
           string newValue = args[1];

            //Reading pathes from app.config
            string source = ConfigurationManager.AppSettings["Path"];
           
           //Create DataReplace object
           DataReplace dr = new DataReplace();
           dr.ReplaceInFiles(pattern, newValue, source);
           Console.WriteLine();
        }
    }
}