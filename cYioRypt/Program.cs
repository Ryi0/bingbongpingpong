// See https://aka.ms/new-console-template for more information

using cYioRypt;

//Make sure seed is under 10^8
Message mess1 = new Message(8131241, "The","cat","is","out","of","the","hat");
Console.WriteLine(mess1.InputMessage);
Console.WriteLine(mess1.CryptedMessage);
Console.WriteLine(mess1.DecryptMessage(1));