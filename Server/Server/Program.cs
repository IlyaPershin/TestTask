using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        const int port = 8888;
        static TcpListener listener;
        static void Main(string[] args)
        {
            Console.Write("N = ");
            int n = Convert.ToInt32(Console.ReadLine());
            Thread[] threads = new Thread[n];

            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    bool isStart = false;
                    for (int i = 0; i < n && !isStart; i++)
                    {
                        if (threads[i] == null || threads[i] != null && !threads[i].IsAlive)
                        {
                            isStart = true;
                            // создаем новый поток для обслуживания нового клиента
                            threads[i] = new Thread(new ThreadStart(clientObject.Process));
                            threads[i].Start();
                        }
                    }
                    if (!isStart)
                    {
                        clientObject.SendError();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
