using MySqlConnector;
using System.Text;

namespace RagLauncher.Database;

internal class SqlScriptRunner
{
    public async Task ExecuteFileAsync(
        MySqlConnection connection,
        string file)
    {
        Console.WriteLine($"[Schema] Importing {Path.GetFileName(file)}...");

        var script = await File.ReadAllTextAsync(file);

        var commands = SplitCommands(script);

        foreach (var command in commands)
        {
            if (string.IsNullOrWhiteSpace(command))
                continue;

            await using var sql =
                new MySqlCommand(command, connection);

            await sql.ExecuteNonQueryAsync();
        }

        Console.WriteLine($"[Schema] {Path.GetFileName(file)} imported.");
    }

    private static IEnumerable<string> SplitCommands(string script)
    {
        var builder = new StringBuilder();

        foreach (var line in script.Split('\n'))
        {
            var trim = line.Trim();

            if (trim.StartsWith("--"))
                continue;

            builder.AppendLine(line);
        }

        return builder
            .ToString()
            .Split(';')
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x));
    }
}