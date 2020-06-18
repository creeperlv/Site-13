/////////////////////////////////////
//This tool follows The MIT License//
//Copyright (C) 2020 Creeper Lv    //
/////////////////////////////////////
using System;
using System.IO;

namespace Site_13_Prebuilts
{
    class Program
    {
        static readonly String copyright = "Copyright (C) 2020 Creeper Lv";
        static readonly Version version = new Version(1, 0, 0, 0);
        static void Main(string[] args)
        {
            /////////////////////////////////////////////
            //This tool should be placed at /Publish/  //
            /////////////////////////////////////////////
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Site-13 Tool Set");
            Console.WriteLine("Publish Tools - Prebuild Libs Copy Tool");
            Console.WriteLine("Copyright (C) 2020 Creeper Lv");
            Console.Write("Version: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(version.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            DirectoryInfo WorkingDir = new DirectoryInfo(".");
            DirectoryInfo ProjectDir = WorkingDir.Parent;
            try
            {

                FileInfo fileInfo = new FileInfo(args[0]);
                if (fileInfo.Exists)
                {
                    var lines = File.ReadAllLines(fileInfo.FullName);
                    foreach (var item in lines)
                    {
                        if (item.StartsWith('#')|| item.StartsWith('*')||item.StartsWith("//")|| item.StartsWith('~'))
                        {
                            //Comments
                        }
                        else
                        {
                            try
                            {
                                var fileGrp = item.Split('=');// should be a two-element array.
                                Console.Write($"Copy:{fileGrp[0]}");
                                FileInfo src = new FileInfo(Path.Combine(ProjectDir.FullName, "PrebuildLibs", fileGrp[0]));
                                Console.Write($"...");
                                src.CopyTo(Path.Combine(ProjectDir.FullName, "Build", "x64.Uncompressed", fileGrp[1]),true);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Succeed");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            catch 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Failed");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Finished.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Fatal error:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Rule file does not exist!");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fatal error:"+e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
