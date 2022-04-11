using System.Net;
using System.Net.Sockets;

System.Console.Write("Введите адресс сервера : ");
IPAddress ipAddress = IPAddress.Parse(System.Console.ReadLine());

System.Console.Write("Введите порт сервера : ");
int port = int.Parse(System.Console.ReadLine());

TcpClient tcpClient = new TcpClient();
tcpClient.Connect(ipAddress, port);

var stream = tcpClient.GetStream();
System.Console.Write("Строка на оброботку : ");
string str = System.Console.ReadLine();

byte[] data = System.Text.Encoding.UTF8.GetBytes(str);

stream.Write(data, 0, data.Length);

byte[] returnData = new byte[256];
int bytes = stream.Read(returnData, 0, returnData.Length);
string str1 = System.Text.Encoding.UTF8.GetString(returnData, 0, bytes);
System.Console.Write("Ответ от сервера : ");
System.Console.WriteLine(str1);

tcpClient.Close();