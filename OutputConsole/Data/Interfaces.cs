using OutputConsole.Extern;


namespace OutputConsole.Data
{
    public interface ICharInfoProvider
    {
        Kernel.CharInfo[] CharInfos { get; }
    }
}
