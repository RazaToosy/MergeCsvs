using CommandLine;
using MergeCsvs;
using MergeCsvs.Data;
using MergeCsvs.Services;

Console.WriteLine("Hello, DarkStar!");
Parser.Default.ParseArguments<Options>(args)
    .WithParsed(RunWithOptions)
    .WithNotParsed(HandleParseError);


static void RunWithOptions(Options opts)
{
    var outputFolder = Path.Combine(opts.FolderPath, "Output");
    if (Directory.Exists(outputFolder)) Directory.Delete(outputFolder,true);

    Console.WriteLine($"Creating lists from subfolders in {opts.FolderPath}");
    var repo = new Repo().CreateRepo(opts.FolderPath);

    Directory.CreateDirectory(outputFolder);

    Console.WriteLine($"Merging csvs to {outputFolder}..");
    foreach (var csvFile in repo)
    {
        new CsvManager().ConcatenateCsvFiles(csvFile.Value, Path.Combine(outputFolder, csvFile.Key));
    }

    Console.WriteLine($"Common Csv files exported to {outputFolder}");
    Console.WriteLine("Enjoy...");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void HandleParseError(IEnumerable<Error> errs)
{
    // Handle errors in command-line argument parsing
    foreach (var err in errs)
    {
        Console.WriteLine($"Error: {err.Tag}");
    }
}