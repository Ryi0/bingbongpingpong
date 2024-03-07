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
        Console.WriteLine(aran);
        aran *= (int)inChar;
        aran = aran % 256;
        Console.WriteLine(aran);
        char randomChar = Convert.ToChar(aran);
        Console.WriteLine(randomChar);
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
}

