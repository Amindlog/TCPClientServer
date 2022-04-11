using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
TcpListener? server = null;
var isStartingServer = true;

System.Console.Write("Введите ipaddress server : ");
string? ip = System.Console.ReadLine();
IPAddress localAddr = IPAddress.Parse(ip);

System.Console.Write("Введите порт для приложения : ");
int port = int.Parse(System.Console.ReadLine());


try
{
    server = new TcpListener(localAddr, port);
    server.Start();
    while (isStartingServer)
    {
        Console.WriteLine("Ожидание подключений... ");

        // получаем входящее подключение
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Подключен клиент. Выполнение запроса...");

        // получаем сетевой поток для чтения и записи
        NetworkStream stream = client.GetStream();
        byte[] data = new byte[256];
        int bytes = stream.Read(data, 0, data.Length);
        string message = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
        var result = new string(message.ToCharArray().Where(n => !char.IsDigit(n)).Where(n => !char.IsSymbol(n)).ToArray());
        string backResult = "";

        for (int i = 0; i < result.Length; i++)
        {
            if(result[i] != '*'){
                backResult += result[i];
            }
        }

        byte[] returnData = System.Text.Encoding.UTF8.GetBytes(backResult);
        stream.Write(returnData, 0, returnData.Length);

        System.Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Отправлено сообщение: {0}", backResult);
        System.Console.ResetColor();
        stream.Close();
        client.Close();
    }
}
catch (System.Exception e)
{
    System.Console.WriteLine(e.Message);
}
finally
{
    if (server != null) server.Stop();
}