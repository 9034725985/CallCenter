using System.Diagnostics;
using MathNet.Numerics.Statistics;

int iterations = 100;
List<double> results = [];
string url = "https://localhost:7109/openperson";

for (int i = 0; i < iterations; i++)
{
    using HttpClient client = new();
    Stopwatch sw = Stopwatch.StartNew();
    var response = await client.GetAsync(url);
    sw.Stop();
    long x = sw.ElapsedTicks;
    Console.WriteLine($"Iteration {i} took {sw.ElapsedTicks} ticks or about {sw.ElapsedMilliseconds} milliseconds");

    results.Add(x);
}

double median = results.Median();
for (int i = 0; i < results.Count; i++)
{
}
Console.WriteLine($"The median time taken to get {url} out of {iterations} attempts was {median} ticks.");
