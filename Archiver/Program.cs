
using Archiver;

static Dictionary<char, int> CalculateFrequencies(string text)
{
    var frequencies = new Dictionary<char, int>();
    foreach (var ch in text)
    {
        if (frequencies.ContainsKey(ch))
            frequencies[ch]++;
        else
            frequencies[ch] = 1;
    }
    return frequencies;
}
string text = "some mistakes with file stream";
//using (StreamReader file = new StreamReader("C:/Users/uskov/source/repos/Archiver/TextFile1.txt"))
//{
//    text = file.ReadToEnd();

//}
//Console.WriteLine("Текст из файла:", text);

var frequencies = CalculateFrequencies(text);
var huffmanTree = new haffman(frequencies);
var codes = huffmanTree.BuildCodes();

Console.WriteLine("Character\tFrequency\tCode");
int Value = 0;
foreach (var kvp in frequencies)
{
    Value++;
    Console.WriteLine($"{kvp.Key}\t\t{kvp.Value}\t\t{codes[kvp.Key]}");
}


using (StreamWriter file = new StreamWriter("C:/Users/uskov/source/repos/Archiver/Archiving.txt"))
{
    //get meta
    file.Write(Value);
    foreach (var kvp in frequencies)
    {
        string CodeString = codes[kvp.Key];
        int ValueOfBits=CodeString.Length;
        file.Write($"{kvp.Key}{ValueOfBits}{codes[kvp.Key]}");
    }
    //get data
    foreach(char symbol in text)
    {
   
        file.Write(codes[symbol]);
    }
    file.Close();
}
