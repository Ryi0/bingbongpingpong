using System.Text.Json;

namespace cYioRypt;

public class Message : IMessage
{
    private readonly AlgoUtils _a = new AlgoUtils();
    public int Seed { get; set; }

    private readonly int _randomizer;
    public List<string> RandomizerAsWordsArray;
    public List<string> ObfuscatedRandomizerAsWordsArray;
    public string InputMessage { get; set; }

    public List<string> ObfuscatingWords { get; set; }

    public string CryptedMessage;
    protected int WordCount = 0;
    private protected char RandomChar = 'A';
    
    
    /// <summary>
    /// /*TODO"{Need to make settings optional and default them to the first and last number of the seed}*/
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="obfuscatorSettings"></param>
    /// <param name="steps"></param>
    /// <param name="words"></param>
    public Message(int seed, int obfuscatorSettings, int steps, params string[] words)
    {
        InputMessage = "";
        this.Seed = seed;


        foreach (string word in words)
        {
            InputMessage += word.ToUpper()[0] + word.Substring(1, word.Length - 1); //ensure camelCase
            WordCount++;
        }

        // ObfuscatingWords = new[] { "Bob", "Yarle", "Stroba", "Jaury", "Lorias", "Teseko", "November", "Zeventytwelve", "Marco","Jennerliras", "The"};
        ObfuscatingWords = new List<string>()
        { "le", "the", "être", "be",
            "avoir",
            "to",
            "de",
            "of",
            "un",
            "and",
            "a",
            "je",
            "in",
            "tu",
            "that",
            "il",
            "have",
            "elle",
            "I",
            "on",
            "it",
            "for",
            "pas",
            "not",
            "aller",
            "on",
            "avec",
            "with",
            "ce",
            "he",
            "que",
            "as",
            "vous",
            "you",
            "do",
            "faire",
            "at",
            "tout",
            "this",
            "pouvoir",
            "but",
            "son",
            "his",
            "venir",
            "by",
            "leur",
            "from",
            "ou",
            "they",
            "dire",
            "we",
            "en",
            "say",
            "ne",
            "her",
            "pour",
            "she",
            "se",
            "or",
            "plus",
            "an",
            "sans",
            "will",
            "contre",
            "my",
            "si",
            "one",
            "après",
            "all",
            "rien",
            "would",
            "où",
            "there",
            "vrai",
            "their",
            "devoir",
            "what",
            "par",
            "so",
            "chez",
            "up",
            "sur",
            "out",
            "homme",
            "if",
            "nouveau",
            "about",
            "femme",
            "who",
            "enfant",
            "get",
            "maintenant",
            "which",
            "autre",
            "go",
            "seulement",
            "me",
            "laisser",
            "when",
            "falloir",
            "make",
            "vouloir",
            "can",
            "comme",
            "like",
            "alors",
            "time",
            "voir",
            "no",
            "bon",
            "just",
            "mot",
            "him",
            "prendre",
            "know",
            "devenir",
            "take",
            "jour",
            "people",
            "savoir",
            "into",
            "donner",
            "year",
            "reste",
            "your",
            "toujours",
            "good",
            "vie",
            "some",
            "chose",
            "could",
            "même",
            "them",
            "aucun",
            "see",
            "deux",
            "other",
            "entre",
            "than",
            "seul",
            "then",
            "fin",
            "now",
            "temps",
            "look",
            "avant",
            "only",
            "encore",
            "come",
            "ainsi",
            "its",
            "grand",
            "over",
            "petit",
            "think",
            "aussi",
            "back",
            "après",
            "use",
            "deux",
            "how",
            "dernier",
            "our",
            "certain",
            "work",
            "first",
            "well",
            "way",
            "even",
            "new",
            "want",
            "because",
            "any",
            "these",
            "give",
            "day",
            "most",
            "us"
        }; //this is why ai is good
        this._randomizer = R1();
        EncryptMessage(obfuscatorSettings, steps);
    }

    /// <summary>
    /// This encrypts a message and sets up the obfuscatedWords array
    /// </summary>
    private void EncryptMessage(int obfuscatorSettings, int steps)
    {
        
        // var r1 = Randomizer;
        // Console.WriteLine(randNum);
        // RandomChar = Convert.ToChar( randNum);
        // var ran = a.Randomized(InputMessage, randNum, RandomChar);
        RandomizerObfuscator(obfuscatorSettings, steps);
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
/// <summary>
/// This decrypts a message using the obfuscator settings
/// </summary>
/// <param name="obfuscatorSettings"></param>
/// <param name="steps"></param>
/// <returns></returns>
    public string DecryptMessage(int obfuscatorSettings, int steps, string[] wordsArray)
    {
        // Console.WriteLine(_randomizer);
        string decryptedMessage = "";
        decryptedMessage = _a.DeCryptWord(CryptedMessage, PositionsArrayToRandomizer(GetRealPositionsArray(obfuscatorSettings, steps, wordsArray)));

        return decryptedMessage;
    }


    /// <summary>
    /// This turns the randomizer associated to the message into an array of strings. Those strings are picked out
    /// from the array of obfuscating words using the obfuscatorsetting and the steps
    /// </summary>
    /// <param name="obfuscatorSetting"></param>
    /// <param name="steps"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void RandomizerObfuscator(int obfuscatorSetting, int steps)
    {
        if (steps>10)
        {
            throw new ArgumentOutOfRangeException("steps", "steps is bigger than 10. Lower it");
        }
        if (obfuscatorSetting > 99||obfuscatorSetting<9)
        {
            throw new ArgumentOutOfRangeException("obfuscatorSetting", "obfuscatorSetting needs to be two digits long");
        }
        string randomizerAsAString = _randomizer.ToString();
        int[] positions = KeyToPosArray(_randomizer);
        if (positions.Length<steps)
        {
            throw new ArgumentOutOfRangeException("steps", "steps Is larger than the key lengt. Lower the step count");
        }

        string[] tmpWordsArray = new string[randomizerAsAString.Length];
        
        //equaly quality vs equal equality dun dun dunnnnnn
        for (int i = 0; i < tmpWordsArray.Length; i++)
        {
            tmpWordsArray[i] = ObfuscatingWords[positions[i]*steps + obfuscatorSetting];
        }
        ObfuscatedRandomizerAsWordsArray = tmpWordsArray.ToList();
        
    }
    
    /// <summary>
    /// This turns a key into an array[int] of positions 
    /// </summary>
    /// <returns></returns>
    private int[] KeyToPosArray(int key)
    {
        string randoAsString = key.ToString();
        int[] posArray = new int[randoAsString.Length];
        
        for (int i = 0; i < posArray.Length; i++)
        {
            posArray[i] = Convert.ToInt32(randoAsString[i].ToString());
        }
        
        return posArray;
    }
    
    /// <summary>
    /// this gives you an array of int that represents the positions of the given words inside
    /// the array of obfuscating words 
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public int[] GetPositionsArray(IEnumerable<string> words)
    {
        var tmpWords = words.ToList();
        int[] tmp = new int[tmpWords.Count];
        int index = 0;
        foreach (string word in tmpWords)
        { 
            tmp[index] = ObfuscatingWords.LastIndexOf(word);
            index++;
        }
        return tmp;
    }
    
    /// <summary>
    /// Using the obfuscator settings and the step count, you can reverse the random words to obtain
    /// the positions of the words in the array of obfuscating words.
    /// These positions can then be turned into the key require to decrypt the message.
    /// I know this is dumb. I'm not exactly sure what's the problem with the logic i'm using but theres a major problem.
    /// I was gonna say that the major problem with the approach is : Why have two methods that give you a position?
    /// But I did it to save a bit of code in this method and because this method transforms a string using the obf settings.
    /// My theory that would explain why I feel a tingle in the back of my head is that I could replace a lot of lines
    /// of code with the method and I haven't done that yet.
    /// </summary>
    /// <param name="obfuscatorSetting"></param>
    /// <param name="steps"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public int[] GetRealPositionsArray(int obfuscatorSetting, int steps, IEnumerable<string> words)
    {

        int[] tmp = GetPositionsArray(words);
        var tmpIndex = 0;
        for (int i = 0; i < tmp.Length; i++)
        {
            tmpIndex = tmp[i];
            tmpIndex = (tmpIndex - obfuscatorSetting) / steps;
            tmp[i]= tmpIndex;
        }
        return tmp;
    }
    
    /// <summary>
    /// Obtain an array of string from an array of positions
    /// </summary>
    /// <param name="posArray"></param>
    /// <returns></returns>
    public IEnumerable<string> GetWordsArray(int[] posArray)
    {
        string[] words = new string[0];
        int i = 0;
        foreach (int pos in posArray)
        {
            words = words.Append(ObfuscatingWords[pos]).ToArray();
        }
        
        return words;
    }

    private int PositionsArrayToRandomizer(int[] positionsArray)
    {
        int randomizer = 0;
        string tmpBuffer = "";
        foreach (int position in positionsArray)
        {
            tmpBuffer += position;
        }

        Console.WriteLine(tmpBuffer);
        randomizer = Convert.ToInt32(tmpBuffer);
        
        return randomizer;
    }
    
}