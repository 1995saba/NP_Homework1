using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            Console.WriteLine("Введите номер порта: ");
            string portAsString = Console.ReadLine();
            Int32.TryParse(portAsString, out port);

            IPAddress serverAddr;
            Console.WriteLine("Введите IP адрес: ");
            string ipAddressAsString = Console.ReadLine();
            IPAddress.TryParse(ipAddressAsString, out serverAddr);

            try
            {
                TcpClient client = new TcpClient();
                client.Connect(serverAddr, port);

                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                Console.WriteLine(response.ToString());

                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            Console.WriteLine("Запрос завершен...");
            Console.Read();
        }
    }
}
