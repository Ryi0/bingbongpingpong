namespace cYioRypt;

public class AlgoUtils
{
    public static string[] MyStrings = Array.Empty<string>();

    public string Randomized(string input, int randomizer, char randChar)
    {
        string message = "";
        var lenght = input.Length;
        MyStrings = new String[lenght];

        for (int i = 0; i < MyStrings.Length; i++)
        {
            MyStrings[i] += randChar.ToString() + randomizer + i.ToString() + (i * i + randomizer).ToString() +
                            randChar.ToString();
        }

        for (int i = 0; i < MyStrings.Length; i++)
        {
            message += MyStrings[i] + " ";
        }

        return message;
    }

    public char CryptedChar(char inChar, int randomizer)
    {
        var aran = randomizer % 256;
        // Console.WriteLine(aran);
        aran *= (int)inChar;
        aran = aran % 256;
        // Console.WriteLine(aran);
        char randomChar = Convert.ToChar(aran);
        // Console.WriteLine(randomChar);
        return randomChar;
    }

    public char DecryptedChar(char randomChar, int randomizer)
    {
        int aran = Convert.ToInt32(randomChar);
        int inverseMultiplier = ModInverse(randomizer, 256);
        aran *= inverseMultiplier;
        aran = aran % 256;
        return Convert.ToChar(aran);
    }

    public int ModInverse(int a, int m)
    {
        a = a % m;
        for (int x = 1; x < m; x++)
        {
            if ((a * x) % m == 1)
                return x;
        }

        return 1;
    }

    public int GreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public int FindRelativePrimeTo256(int number)
    {
        while (GreatestCommonDivisor(number, 256) != 1)
        {
            number--;
        }

        return number;
    }

//after this, find an algo that can obfuscate the randomizer and add that layer of obfuscation
//This will permit me to separate my strings with longer strings of text. Only the correct initial letters are to be parsed using the right randomizer. every 5 letters, use the randomizer. if i%5!=0, randomizer+i for the decryptions
    public string CryptWord(string inputWord, int randomizer)
    {
        if (inputWord==null)
        {
            throw new Exception("Steph Charray will be null");
        }
        string tmp = inputWord;
        char[] stephChArray = new char[tmp.Length];

        for (int i = 0; i < tmp.Length; i++)
        {
            stephChArray[i] = this.CryptedChar(tmp[i], randomizer);
        }

        tmp = "";
        for (int i = 0; i < stephChArray.Length; i++)
        {
            tmp += stephChArray[i];
        }
        return tmp;
    }
    
    public string DeCryptWord(string inputWord, int randomizer)
    {
        if (inputWord==null)
        {
            throw new Exception("Steph Charray will be null");
        }
        string tmp = inputWord;
        List<char> stephChArray = new List<char>();

        for (int i = 0; i < tmp.Length; i++)
        {
            stephChArray.Add(tmp[i]);
        }

        tmp = "";
        for (int i = 0; i < stephChArray.Count; i++)
        {
            Console.WriteLine(this.DecryptedChar(stephChArray[i], randomizer));
            tmp += this.DecryptedChar(stephChArray[i], randomizer);
        }
        
        return tmp;
    }
} 