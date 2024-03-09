// See https://aka.ms/new-console-template for more information

using cYioRypt;

//Make sure seed is under 10^8
Message mess1 = new Message(8131241, 51, 3, "The","cat","is","out","of","the","hat");
Console.WriteLine($"This is the crypted message : {mess1.CryptedMessage}");
string[] myWords = mess1.ObfuscatedRandomizerAsWordsArray.ToArray();
Console.WriteLine($"This is the obfuscated words array : {string.Join(", ",myWords)}");
Console.WriteLine(mess1.DecryptMessage(71, 3, myWords));