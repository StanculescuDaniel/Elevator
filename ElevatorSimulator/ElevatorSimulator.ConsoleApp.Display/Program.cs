// See https://aka.ms/new-console-template for more information
using System.IO;

var temp = Path.GetTempPath();
var path = temp + "elevatorSimulator.txt";
var exists = File.Exists(path);

if(!exists)
{

}

// Create a new FileSystemWatcher and set its properties.
FileSystemWatcher watcher = new FileSystemWatcher();
watcher.Path = temp;
/* Watch for changes in LastAccess and LastWrite times, and 
   the renaming of files or directories. */
watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
// Only watch text files.
watcher.Filter = "elevatorSimulator.txt";

// Add event handlers.
watcher.Changed += new FileSystemEventHandler(OnChanged);
watcher.Created += new FileSystemEventHandler(OnChanged);
watcher.Deleted += new FileSystemEventHandler(OnChanged);

// Begin watching.
watcher.EnableRaisingEvents = true;

void OnChanged(object source, FileSystemEventArgs e)
{
    // Specify what is done when a file is changed, created, or deleted.
    Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);

    try
    {
        using (var fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var sr = new StreamReader(fs))
            {
                var str = sr.ReadToEnd();

                Console.Clear();
                Console.WriteLine(str);
            }

            fs.Close();
        }
    }
    catch(Exception ex)
    {

    }

}

Console.ReadKey();

//var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
//using (var sr = new StreamReader(fs))
//{
//    var str = sr.ReadToEnd();
//}
