using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace ChromaAlpha {
    public class Kernel : Sys.Kernel
    {
        readonly Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        private string dir = "0:\\";
        protected override void BeforeRun()
        {
            Console.WriteLine("\nChroma Alpha v0.1\n");
            Console.WriteLine("Initiating Filesystem...");
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.WriteLine("Done.");
        }

        protected override void Run()
        {
            Console.Write(dir + ">");
            var input = Console.ReadLine();
            var commandarray = input.Split(" ");
            var command = commandarray[0];
            // Console.WriteLine(command);
            switch (command)
            {
                case "about":
                    Console.WriteLine("Chroma Alpha v0.1 (Build 1001)");
                    Console.WriteLine("Written and compiled in 4 January 2022");
                    break;
                case "help":
                    Console.WriteLine("Command list:\n\tabout - Display information about Chroma\n\thelp - Display this help screen\n\tshutdown - Shut down the system\n\treboot - Reboot the system\n\trun - Run application\n\tlist - List current directory's contents\n\tcd - Go to a directory inside the current directory\n\tlistfile - Print a file's contents into the console\n\twritefile - Use commands to write data to a file");
                    break;
                case "shutdown":
                    Sys.Power.Shutdown();
                    break;
                case "reboot":
                    Sys.Power.Reboot();
                    break;
                default:
                    Console.WriteLine("Error: Invalid command");
                    break;
                case "run":
                    try
                    {
                        switch (commandarray[1])
                        {
                            case "diskutils":
                                try
                                {
                                    switch (commandarray[2])
                                    {
                                        default:
                                            Console.WriteLine("There's no disk utility named " + commandarray[2] + ".");
                                            break;
                                        case "format":
                                            Console.WriteLine("Which hard drive do you wanna format?");
                                            Console.WriteLine("[0] Main hard drive");
                                            var driveid = Console.ReadLine();
                                            Console.WriteLine("Are you sure you want to format the drive? Doing so will permanenty erase all the data. (y/N)");
                                            if (Console.ReadLine() == "y")
                                            {
                                                Console.WriteLine("Formatting!");
                                                fs.Format(driveid /*drive id*/, "FAT32" /*fat type*/, true /*use quick format*/);
                                            }
                                            break;
                                        case "getinfo":
                                            Console.WriteLine("Which hard drive's info do you wanna get?");
                                            Console.WriteLine("[0] Main hard drive");
                                            driveid = Console.ReadLine();
                                            driveid += ":\\";
                                            float available_space = (fs.GetAvailableFreeSpace(driveid) / 1048576);
                                            Console.WriteLine(Math.Floor(available_space) + "MiB free (" + Math.Floor((available_space * 1048576) / 1000000) + " MB Free)");
                                            var fs_type = fs.GetFileSystemType(driveid);

                                            Console.WriteLine("File System Type: " + fs_type);
                                            break;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Usage: run diskutils <utility>");
                                }
                                break;
                            default:
                                Console.WriteLine("The application " + commandarray[1] + " isn't installed on your computer.");
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error: Missing application name");
                    }
                    break;
                case "list":
                    var directory_list = Directory.GetFiles(dir);
                    Console.WriteLine("Directory listing for directory \"" + dir + "\":");
                    Console.WriteLine("\tFilename\t\tSize");
                    foreach (string filename in directory_list)
                    {
                        var filecontent = File.ReadAllText(filename);
                        Console.WriteLine("\t" + filename + "\t\t" + filecontent.Length);
                    }
                    break;
                case "cd":
                    try {
                        dir += commandarray[1];
                    } catch (Exception) {
                        Console.WriteLine("Usage: cd <directory>");
                    }
                    break;
                case "listfile":
                    try {
                        Console.WriteLine("\t"+File.ReadAllText(commandarray[1]));
                    } catch (Exception) {
                        Console.WriteLine("Usage: listfile <file>");
                    }
                    break;
            }
        }
    }
}
