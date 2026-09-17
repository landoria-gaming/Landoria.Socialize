namespace Landoria.Socialize
{
    internal static class ServerRole
    {
        public static bool IsDedicatedServer => ZNet.instance != null &&
            ZNet.instance.IsServer() && ZNet.instance.IsDedicated();
    }
}
