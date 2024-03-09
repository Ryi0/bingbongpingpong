using System.Text.Json;

namespace cYioRypt;

public class Message : IMessage
{
    private readonly AlgoUtils _a = new AlgoUtils();
    public int Seed { get; set; }

    private readonly int _randomizer;
    public List<string> RandomizerAsWordsArray;
    public string InputMessage { get; set; }

    public List<string> ObfuscatingWords { get; set; }

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
        RandomizerObfuscator(71, 3);

        return decryptedMessage;
    }

    /**
     * if the array contains an empty string, the final count will be positive (>0).
     * Then, it returns true
     * 
     */
    private bool ContainsEmptyWord(string[] words)
    {
        bool tmp = false;
        int counteur = 0;
        foreach (string word in words)
        {
            if (string.IsNullOrWhiteSpace(word)) 
            {
                counteur++;
            }
        }

        if (counteur>0)
        {
            tmp = true;
        }
        return tmp;
    }
    
    public void RandomizerObfuscator(int obfuscatorSetting, int steps)
    {
        if (obfuscatorSetting > 99||obfuscatorSetting<9)
        {
            throw new ArgumentOutOfRangeException("obfuscatorSetting", " needs to be two digits long");
        }

        string randomFromRandomWords = "";
        List<string> wordsAsList = ObfuscatingWords.ToList(); 
        string randomFromWords = "";
        string randomizerAsAString = _randomizer.ToString();
        
        int[] positions = new int[randomizerAsAString.Length];
        int[] obfPositions = new int[positions.Length];
        if (positions.Length<steps)
        {
            throw new ArgumentOutOfRangeException("steps", " Is larger than the key lengt. Lower the step count");
        }

        if (steps>10)
        {
            throw new ArgumentOutOfRangeException("steps", "is bigger than 10. Lower it");
        }
        string[] tmpWords = new string[randomizerAsAString.Length];
        string[] tmpWordsArray = new string[randomizerAsAString.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Convert.ToInt32(randomizerAsAString[i].ToString());
            tmpWords[i] = ObfuscatingWords[positions[i]];
            
        }
        
        
        
        //equaly quality vs equal equality dun dun dunnnnnn
        for (int i = 0; i < tmpWordsArray.Length; i++)
        {
            tmpWordsArray[i] = wordsAsList[positions[i]*steps + obfuscatorSetting];
            obfPositions[i] = wordsAsList.LastIndexOf(tmpWordsArray[i]);
        }
        
        
        foreach (string tmpWord in tmpWordsArray)
        {
           // Console.WriteLine(wordsAsList.IndexOf(tmpWord)); 
            randomFromRandomWords += wordsAsList.IndexOf(tmpWord);
        }
        
        // Console.WriteLine(a);
        Console.WriteLine($"obfuscatorSetting: {obfuscatorSetting}");
        Console.WriteLine($"steps: {steps}");
        Console.WriteLine($"randomFromWords: {randomFromWords}");
        Console.WriteLine($"randomizerAsAString: {randomizerAsAString}");
        Console.WriteLine($"positions: {string.Join(", ", positions)}");
        Console.WriteLine($"obfPositions: {string.Join(", ", obfPositions)}");
        Console.WriteLine($"tmpWords: {string.Join(", ", tmpWords)}");
        Console.WriteLine($"tmpWordsArray: {string.Join(", ", tmpWordsArray)}");
        Console.WriteLine($"randomFromRandomWord: {string.Join(",  ",randomFromRandomWords)}" );

        RandomizerAsWordsArray = tmpWordsArray.ToList();

        GetRealPositionsArray(obfuscatorSetting, steps);
        Console.WriteLine("\n\nthis the cool shit");
        Console.WriteLine("Word array from RandomizerAsWordsArray : ");
        GetWordsArray(GetPositionsArray(RandomizerAsWordsArray));
        Console.WriteLine("Word array from real pos  : ");
        GetWordsArray(GetRealPositionsArray(obfuscatorSetting, steps));
        Console.WriteLine("\n\n");
    }

    private int[] GetPositionsArray(IEnumerable<string> words)
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

    private int[] GetRealPositionsArray(int obfuscatorSetting, int steps)
    {
        string original = "";
        string obfuscatedString = "";
        string realOg = "";
        int[] tmp = GetPositionsArray(RandomizerAsWordsArray);
        var tmpIndex = 0;
        for (int i = 0; i < tmp.Length; i++)
        {
            tmpIndex = tmp[i];
            obfuscatedString += tmpIndex + ", ";
           // tmpIndex =  steps+ (obfuscatorSetting - tmpIndex); 
           
            realOg += ObfuscatingWords[(tmpIndex - obfuscatorSetting)/steps  ]+", ";
            tmpIndex = (tmpIndex - obfuscatorSetting) / steps;
            original += tmpIndex + ", ";
            tmp[i]= tmpIndex;
        }

        // Console.WriteLine($"ObfuscatedSting = {obfuscatedString}");
        // Console.WriteLine($"originalTry = {original}");
        // Console.WriteLine($"Real original = {_randomizer}");
        // Console.WriteLine($"RealOG = {realOg}");        
        
        
        return tmp;
    }
    public IEnumerable<string> GetWordsArray(int[] posArray)
    {
        string[] words = new string[0];
        int i = 0;
        foreach (int pos in posArray)
        {
            words = words.Append(ObfuscatingWords[pos]).ToArray();

        }

        Console.WriteLine(string.Join(", ", words));
        return words;
    }
    
    
}