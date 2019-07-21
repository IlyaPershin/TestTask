using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class ClientObject
    {
        const int port = 8888;
        const string address = "127.0.0.1";

        string fileName;
        string text;

        public ClientObject(string fileName, string text)
        {
            this.fileName = fileName;
            this.text = text + ' ';
        }

        public void SendMessage()
        {
            TcpClient client = null;
            try
            {
                bool correctAnswer = false;
                while (!correctAnswer)
                {
                    client = new TcpClient(address, port);
                    NetworkStream stream = client.GetStream();

                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.Unicode.GetBytes(text);
                    // отправка сообщения
                    stream.Write(data, 0, data.Length);

                    // получаем ответ
                    data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    if (message != "Error")
                    {
                        correctAnswer = true;
                        Console.WriteLine("{0}: {1}", fileName, message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (client != null)
                    client.Close();
            }

            return;
        }
    }
}
