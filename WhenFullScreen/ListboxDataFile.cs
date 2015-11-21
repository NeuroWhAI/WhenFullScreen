using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WhenFullScreen
{
    public class ListboxFileInterface
    {
        public static int ReadFile(string fileName, ListBox listbox)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open)))
                {
                    listbox.BeginUpdate();

                    while (!sr.EndOfStream)
                    {
                        string str = sr.ReadLine();

                        if(str.Length > 0)
                            listbox.Items.Add(str);
                    }

                    listbox.EndUpdate();


                    sr.Close();

                    return 0;
                }
            }


            return -1;
        }

        public static int WriteFile(string fileName, ListBox listbox)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create)))
            {
                foreach (var item in listbox.Items)
                {
                    sw.WriteLine(item.ToString());
                }


                sw.Close();
            }


            return 0;
        }
    }
}
