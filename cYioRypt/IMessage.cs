namespace cYioRypt;

public interface IMessage
{
    public int Seed { get; set; }
    private protected string InputMessage { get; set; }
    public List<string> ObfuscatingWords { get; set; }
    
}