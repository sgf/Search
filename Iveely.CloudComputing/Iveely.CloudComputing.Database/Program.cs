﻿using System;
using Iveely.STSdb4.Database;

namespace Iveely.CloudComputing.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (IStorageEngine engine = STSdb.FromFile("Iveely.db"))
                {
                    var server = STSdb.CreateServer(engine, 7182);
                    server.Start();
                    while (true)
                    {
                        string cmd = Console.ReadLine();
                        if (cmd == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter 'exit' to exit program...");
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
