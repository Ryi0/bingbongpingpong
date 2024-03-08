using System.Text.Json;

namespace cYioRypt;
public class Message : IMessage
{
    protected AlgoUtils a = new AlgoUtils();  
    public int Seed { get; set; }

    public string InputMessage { get; set; }

    public string[] ObfuscatingWords { get; set; }

    public string? CryptedMessage;
    protected int WordCount = 0;
    private protected char RandomChar = 'A';

    public Message( int seed, params string[] words)
    {
        InputMessage = "";
        this.Seed = seed;
        foreach (string word in words)
        {
            InputMessage += word.ToUpper()[0]+word.Substring(1,word.Length-1); //ensure camelCase
            WordCount++;
        }
        ObfuscatingWords = new[] { "Bob", "Yarle", "Stroba", "Jaury", "Lorias", "Teseko", "November", "Zeventytwelve"};
    }
    
    public void EncryptMessage()
    {
        var randNum = 0;
        var totalLength = 0;
        int randomizer = 0;
        foreach (var word in ObfuscatingWords)
        {
            totalLength += word.Length;
        }

        randomizer = totalLength * (Seed);
        Console.WriteLine(randomizer);
        // Console.WriteLine(randNum);
        var r1 = a.FindRelativePrimeTo256(randomizer);
        RandomChar = Convert.ToChar( randNum);
        // var ran = a.Randomized(InputMessage, randNum, RandomChar);
        var c1 = a.CryptedChar('N', r1);
        Console.WriteLine(c1);
        Console.WriteLine(a.DecryptedChar(c1, r1));
        
        Console.WriteLine(a.CryptWord("Hola", r1));
        Console.WriteLine(a.DeCryptWord("Hola", r1));
        List<char> testCharArrayCrypted = new List<char>();
        List<char> testCharArrayDecrypted = new List<char>();
        
        // for (int i = 0; i < "hola".Length; i++)
        // {
        //     Console.WriteLine(a.CryptedChar("Hola"[i], r1));
        //     testCharArrayCrypted.Add(a.CryptedChar("Hola"[i], r1));
        // }
        //
        // for (int i = 0; i < testCharArrayCrypted.Count; i++)
        // {
        //     Console.WriteLine(a.DecryptedChar(testCharArrayCrypted[i], r1));
        // }
                
    }

}