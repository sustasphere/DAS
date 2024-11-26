using static System.String;

namespace DAS.GoT.Types.Utils;
/// <summary>
/// 
/// </summary>
public class ServerSetup
{
    /// <summary>
    /// 
    /// </summary>
    public static string Key = nameof(ServerSetup);
    /// <summary>
    /// 
    /// </summary>
    public string Host { get; init; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Path { get; init; } = Empty;
    /// <summary>
    /// 
    /// </summary>
    public int PollingDelayMinutes { get; init; } = 12;
}
