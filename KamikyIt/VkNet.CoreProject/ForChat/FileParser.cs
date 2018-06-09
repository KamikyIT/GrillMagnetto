using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkNet.Examples.ForChat
{
    public static class FileParser
    {
        public static List<String> getBans()
        {
            List<String> bans = new List<string>();
			string path = GetSpecialFilePath(FilesEnum.banlist);
            StreamReader fs = new StreamReader(path);
            string s = "";
            while (s != null)
            {
                s = fs.ReadLine();
                if (!String.IsNullOrEmpty(s))
                {
                    bans.Add(s);
                }
            }
            fs.Close();
            return bans;
        }

        public static string getLogPath()
        {

            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);

			root_path = @"C:\Users\Kamiky\source\repos\borat2\";

			return root_path + "Log\\";


        }

        public static List<String> getAdvise(string filename)
        {
			return File.ReadAllLines(GetSpecialFilePath(FilesEnum.quastins)).ToList();
			
        }


        public static List<String> getAnswer()
        {
			return File.ReadAllLines(GetSpecialFilePath(FilesEnum.answer_databse)).ToList();
        }

        public static string getAdviseFilePath(String name)
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);

			root_path = @"C:\Users\Kamiky\source\repos\borat2\";


			return root_path + "Data\\"+name+".txt";

        }

        public static string getBanFilePath()
        {
            string path = Directory.GetCurrentDirectory();
            int index = path.IndexOf("Kami");
            string root_path = path.Substring(0, index);

			root_path = @"C:\Users\Kamiky\source\repos\borat2\";


			return root_path + "Data\\banlist.txt";

        }



		public static void setBanList(List<String> domains)
        {
			string path = GetSpecialFilePath(FilesEnum.banlist);//getBanFilePath();

			File.AppendAllLines(path, domains);
        }


		public static string GetSpecialFilePath(FilesEnum file)
		{
			var directory = Directory.GetCurrentDirectory();
			directory = Directory.GetParent(directory).Parent.Parent.Parent.FullName;

			return Path.Combine(directory, "Data", file.ToString() + ".txt");
		}

		public enum FilesEnum
		{
			banlist,
			hobby,
			quastins,
			answer_databse,
		}
    }
}
