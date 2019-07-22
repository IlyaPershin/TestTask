using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    public class ClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {
            NetworkStream stream = null;

            stream = client.GetStream();
            byte[] data = new byte[64]; // буфер для получаемых данных

            // получаем сообщение
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            string message = builder.ToString();

            Console.WriteLine(message);

            // отправляем обратно сообщение в верхнем регистре
            string answer = PalindromeChecker.IsPalindrome(message).ToString();
            data = Encoding.Unicode.GetBytes(answer);
            stream.Write(data, 0, data.Length);

            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();

            return;
        }

        public void SendError()
        {
            NetworkStream stream = null;
            stream = client.GetStream();
            byte[] data = new byte[64];
            data = Encoding.Unicode.GetBytes("Error");
            stream.Write(data, 0, data.Length);
            stream.Close();
            client.Close();
        }
    }
}
