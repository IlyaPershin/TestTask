using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            Thread[] threads = new Thread[n];
            //Key- Имя файла
            //Value- Путь к файлу
            Dictionary<string, string> files = new Dictionary<string, string>();

            Console.Write("Путь к директории: ");
            string directory = Console.ReadLine();

            FilesReader filesReader = new FilesReader(directory);
            files = filesReader.GetFilesPaths();

            foreach (var file in files)
            {
                bool isStart = false;
                while (!isStart)
                {
                    ClientObject clientObject = new ClientObject(file.Key, FilesReader.ReadFile(file.Value));

                    int i = 0;
                    while (i < n && !isStart)
                    {
                        if (threads[i] == null || threads[i] != null && !threads[i].IsAlive)
                        {
                            isStart = true;
                            // создаем новый поток для обслуживания нового клиента
                            threads[i] = new Thread(new ThreadStart(clientObject.SendMessage));
                            threads[i].Start();
                        }
                        i = (i + 1) % n;
                    }
                    if (!isStart)
                        Thread.Sleep(100);
                }
            }

            bool isSomeAlive = true;
            while (isSomeAlive)
            {
                bool isAlive = false;
                for (int i = 0; i < n && !isAlive; i++)
                {
                    isAlive = isAlive || threads[i].IsAlive;
                }
                isSomeAlive = isAlive;
            }

            Console.Write("Нажмие Enter для завершения");
            Console.ReadLine();
        }
    }
}
