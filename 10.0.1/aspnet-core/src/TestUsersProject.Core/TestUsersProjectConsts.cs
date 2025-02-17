using TestUsersProject.Debugging;

namespace TestUsersProject;

public class TestUsersProjectConsts
{
    public const string LocalizationSourceName = "TestUsersProject";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ec6997bc64884a3bb7726d3f77794834";
}
