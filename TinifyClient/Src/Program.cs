using System;
using System.IO;
using TinifyAPI;
using GameFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinifyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            if(args.Length != 3 )
            {
                Console.WriteLine("参数长度需要为3:当前输入长度:"+args.Length);
                return;
            }
            try
            {


                Tinify.Key = args[0];
                string Dir_Input = args[1];
                string Dir_Output = args[2];
                List<string> Ls_File = new List<string>();
                FileHelper.GetAllFiles(Ls_File, Dir_Input);
                List<Task> Ls_Tasks = new List<Task>();
                foreach (var fp in Ls_File)
                {
                    if(Path.GetExtension(fp) !=".png" && Path.GetExtension(fp) != ".jpg")
                    {
                        continue;
                    }
                    var outpath = Dir_Output + fp.Substring(Dir_Input.Length , fp.Length - Dir_Input.Length );
                    var outpathdir = Path.GetDirectoryName(outpath);
                    FileHelper.CreatPath(outpathdir);
                    Console.WriteLine(outpath);
                    //////////////////
                    Ls_Tasks.Add(Tinify.FromFile(fp).ToFile(outpath));
                }

                Task.WaitAll(Ls_Tasks.ToArray());

                Console.WriteLine("任务完成");
            }
            catch(System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            Console.ReadKey();
        }
    }
}
