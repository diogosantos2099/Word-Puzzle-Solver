string[] dictionaryFile = System.IO.File.ReadAllLines(@"words-english.txt");

foreach (string word in dictionaryFile)
{
    Console.WriteLine(word);
}
System.Console.ReadKey();