using System.Text.Json;

namespace cYioRypt;
public class Message : IMessage
{
    private AlgoUtils a = new AlgoUtils();  
    public int Seed { get; set; }

    protected int Randomizer;
    
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
        this.Randomizer = R1();
    }
    
    
    public void EncryptMessage()
    {
        
        // var r1 = Randomizer;
        // Console.WriteLine(randNum);
        // RandomChar = Convert.ToChar( randNum);
        // var ran = a.Randomized(InputMessage, randNum, RandomChar);
        
        Console.WriteLine(a.CryptWord("Hola", Randomizer));
        Console.WriteLine(a.DeCryptWord(a.CryptWord("Hola", Randomizer), Randomizer));
    }

    private int R1()
    {
        int r1;
        var totalLength = 0;
        int randomizer = 0;
        foreach (var word in ObfuscatingWords)
        { 
            totalLength += word.Length; //even with the same seed, if the users are not on the same server with the same obfuscating words, they cannot decrpyt the message.
        }
        randomizer = totalLength * (Seed);
        r1 = a.FindRelativePrimeTo256(randomizer);
        return r1;
    }

    public void DecryptMessage()
    {
        
    }

}