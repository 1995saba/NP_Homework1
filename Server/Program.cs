using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;

            int port;
            Console.WriteLine("Введите номер порта: ");
            string portAsString = Console.ReadLine();
            Int32.TryParse(portAsString, out port);
            
            IPAddress localAddr;
            Console.WriteLine("Введите IP адрес: ");
            string ipAddressAsString = Console.ReadLine();
            IPAddress.TryParse(ipAddressAsString, out localAddr);

            try
            {
                server = new TcpListener(localAddr, port);
                server.Start();

                var client = server.AcceptTcpClient();
                byte[] buffer = new byte[10];
                StringBuilder globalBuffer = new StringBuilder();

                while (true)
                {
                    var stream = client.GetStream();
                    for (int i = 0; i < stream.Length; i++)
                    {
                        globalBuffer.Append(Convert.ToChar(buffer[i]));
                    }

                    string response = globalBuffer.ToString();
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Отправлено сообщение: {0}", response);
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}
