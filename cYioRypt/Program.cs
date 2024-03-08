// See https://aka.ms/new-console-template for more information

using cYioRypt;

//Make sure seed is under 10^8
Message mess1 = new Message(9143411, "Bon", "Job");
Console.WriteLine(mess1.InputMessage);
mess1.EncryptMessage();