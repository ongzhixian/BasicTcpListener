using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Configuration;

TcpListener? server = null;

try
{
    ReadIPAddressPortFromConfig(out IPAddress ipAddress, out int port);

    server = new TcpListener(ipAddress, port);
    
    Console.WriteLine($"Listening for connections on {ipAddress}:{port}");

    server.Start();

    while (true)
    {
        Console.Write("Waiting for a connection... ");

        // Perform a blocking call to accept requests.
        // You could also use server.AcceptSocket() here.
        using (TcpClient client = server.AcceptTcpClient())
        using (NetworkStream stream = client.GetStream())
        {
            Console.WriteLine("Connected!");

            byte[] bytes = new byte[256];

            string? data = null;

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
        }
    }
}
catch (SocketException e)
{
    Console.WriteLine("SocketException: {0}", e);
}
finally
{
    if (server != null)
    {
        server.Stop();
        Console.WriteLine("Server stopped.");
    }
}

void ReadIPAddressPortFromConfig(out IPAddress ipAddress, out int port)
{
    const string HOST_CONFIGURATION_KEY = "TcpListener:Host";
    const string PORT_CONFIGURATION_KEY = "TcpListener:Port";

    ipAddress = IPAddress.Loopback;
    port = 3092;

    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    if (IPAddress.TryParse(configuration[HOST_CONFIGURATION_KEY], out IPAddress? parsedIPAddress))
    {
        ipAddress = parsedIPAddress;
    }

    if (int.TryParse(configuration[PORT_CONFIGURATION_KEY], out int parsedPort))
    {
        port = parsedPort;
    }
}