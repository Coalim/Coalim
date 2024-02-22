using CoalimConsoleClient;

using CoalimWebSocketClient ws = new(new Uri("ws://localhost:10060/ws"));

await Task.Factory.StartNew(async () =>
{
    while (true)
    {
        Console.WriteLine(await ws.ReceiveTextAsync());
    }
});

while (true)
{
    string input = ReadLine.Read(">");
    ReadLine.AddHistory(input);
    await ws.SendTextAsync(input);
}