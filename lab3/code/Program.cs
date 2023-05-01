static void Wait()
{
  Console.WriteLine("Wait began");
  Thread.Sleep(5000);
  Console.WriteLine("Wait finished");
}

static async Task WaitAsync()
{
  Console.WriteLine("WaitAsync began");
  await Task.Delay(2000);
  Console.WriteLine("WaitAsync finished");
}

static async Task<string> DownloadWebPageAsync(string url)
{
  Console.WriteLine("Downloading web page...");
  HttpClient client = new HttpClient();
  string result = await client.GetStringAsync(url);
  Console.WriteLine("Web page downloaded");
  return result;
}

Console.WriteLine("Main began");
// 1
Thread thread =
  new Thread(new ThreadStart(Wait));
thread.Start();
// 2
await WaitAsync();
// 3
string url = "https://www.google.com";
string result = await DownloadWebPageAsync(url);
Console.WriteLine($"Web page length: {result.Length} characters");
Console.WriteLine("Main finished");
