using System.Text.Json;

namespace cYioRypt;

public class Message : IMessage
{
    private readonly AlgoUtils _a = new AlgoUtils();
    public int Seed { get; set; }

    private readonly int _randomizer;

    public string InputMessage { get; set; }

    public string[] ObfuscatingWords { get; set; }

    public string CryptedMessage;
    protected int WordCount = 0;
    private protected char RandomChar = 'A';

    public Message(int seed, params string[] words)
    {
        InputMessage = "";
        this.Seed = seed;


        foreach (string word in words)
        {
            InputMessage += word.ToUpper()[0] + word.Substring(1, word.Length - 1); //ensure camelCase
            WordCount++;
        }

        // ObfuscatingWords = new[] { "Bob", "Yarle", "Stroba", "Jaury", "Lorias", "Teseko", "November", "Zeventytwelve", "Marco","Jennerliras", "The"};
        ObfuscatingWords = new[]
        {
            "le", "the", "de", "be", "un", "to", "à", "of", "être", "and",
            "et", "a", "en", "in", "avoir", "that", "que", "have", "pour", "I",
            "dans", "it", "ce", "for", "il", "not", "qui", "on", "ne", "with",
            "sur", "do", "se", "at", "pas", "this", "plus", "but", "pouvoir", "his",
            "par", "by", "je", "from", "avec", "they", "we", "say", "nous", "her",
            "comme", "she", "or", "ou", "an", "si", "will", "leur", "my", "lui",
            "one", "devoir", "all", "sans", "would", "mon", "what", "dire", "so",
            "vous", "up", "fois", "out", "même", "if", "y", "about", "pendant",
            "who", "encore", "get", "elle", "dont", "make", "où", "can", "when",
            "après", "like", "aussi", "you", "tout", "time", "faire", "no", "but",
            "just", "pas", "him", "know", "sur", "take", "their", "people", "do",
            "into", "year", "your", "good", "its", "some", "over", "could", "think",
            "them", "also", "see", "back", "other", "than", "after", "then", "use",
            "now", "two", "look", "how", "only", "our", "come", "work", "its", "first",
            "well", "way", "even", "grand", "allons", "trouver", "l'autre", "temps", "bien",
            "aussi", "jamais", "entre", "peu", "vouloir", "cette", "ainsi", "heure", "quelque",
            "deux", "chose", "année", "avant", "après", "ça", "donner", "premier", "partir",
            "voir", "dernier", "tant", "grâce", "jour", "moins", "venir", "prendre", "suite",
            "monde", "puis", "rien", "abord", "passer", "trois", "nouveau", "tous", "suite"
        }; //this is why ai is good
        this._randomizer = R1();
        EncryptMessage();
    }


    private void EncryptMessage()
    {
        // var r1 = Randomizer;
        // Console.WriteLine(randNum);
        // RandomChar = Convert.ToChar( randNum);
        // var ran = a.Randomized(InputMessage, randNum, RandomChar);
        CryptedMessage = _a.CryptWord(InputMessage, _randomizer);
        //Console.WriteLine(a.CryptWord("Hola", Randomizer));
        //Console.WriteLine(a.DeCryptWord(a.CryptWord("Hola", Randomizer), Randomizer));
        InputMessage = "";
    }


    private int R1()
    {
        int r1;
        var totalLength = 0;
        int randomizer = 0;
        foreach (var word in ObfuscatingWords)
        {
            totalLength +=
                word.Length; //even with the same seed, if the users are not on the same server with the same obfuscating words, they cannot decrpyt the message.
        }

        randomizer = totalLength * (Seed);
        r1 = _a.FindRelativePrimeTo256(randomizer);
        return r1;
    }

    public string DecryptMessage(int randoKey)
    {
        // Console.WriteLine(_randomizer);
        string decryptedMessage = "";
        decryptedMessage = _a.DeCryptWord(CryptedMessage, _randomizer);
        RandomizerObfuscator(11, 4);

        return decryptedMessage;
    }

    public void RandomizerObfuscator(int obfuscatorSetting, int steps)
    {
        if (obfuscatorSetting > 99 || obfuscatorSetting < 9)
            throw new ArgumentOutOfRangeException("obfuscatorSetting", " needs to be two digits long");

        var wordsAsList = ObfuscatingWords.ToList();
        string randomFromWords = "";
        Console.WriteLine(_randomizer);
        string randomizerAsAString = _randomizer.ToString();
        int[] positions = new int[randomizerAsAString.Length];
        if (positions.Length < steps)
            throw new ArgumentOutOfRangeException("steps", " Is larger than the key lengt. Lower the step count");

        string[] tmpWords = new string[randomizerAsAString.Length];
        Console.WriteLine(positions.Length);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Convert.ToInt32(randomizerAsAString[i].ToString());
            Console.WriteLine("positions[" + i + "] = " + positions[i]);
            tmpWords[i] = ObfuscatingWords[positions[i]];
        }

        string[] tmpWordsArray = new string[randomizerAsAString.Length];
        int accu = obfuscatorSetting;
        int tmpIndex = 0;
        while (accu > 0)
        {
            if (tmpIndex > tmpWordsArray.Length - 1)
                tmpIndex -= tmpWordsArray.Length - 1;
            else if (tmpIndex < 0)
                tmpIndex = 0;

            tmpWordsArray[tmpIndex] = wordsAsList[tmpIndex + obfuscatorSetting];
            tmpIndex += steps;
            accu--;
        }

        foreach (string tmpWord in tmpWords)
        {
            randomFromWords += wordsAsList.IndexOf(tmpWord);
            Console.WriteLine(tmpWord);
            Console.WriteLine(randomFromWords);
        }

        foreach (string word in tmpWordsArray)
            Console.WriteLine(word);
    }
}