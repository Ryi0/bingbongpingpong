namespace cYioRypt;

public interface IMessage
{
    public int Seed { get; set; }
    private protected string InputMessage { get; set; }
    public string[] ObfuscatingWords { get; set; }
}