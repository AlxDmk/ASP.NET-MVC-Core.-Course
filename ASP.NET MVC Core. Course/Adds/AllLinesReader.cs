namespace ASP.NET_MVC_Core._Course.Adds;

public class AllLinesReader
{
    public static async Task<IEnumerable<string[]>> GetLinesFromFiles(params string[] files)
    {
        var getLines = files.Select(x => File.ReadAllLinesAsync(x)).ToList();
        return await Task.WhenAll(getLines);

    }

}