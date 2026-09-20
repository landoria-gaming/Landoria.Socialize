using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Landoria.Socialize
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class SocializePlugin : BaseUnityPlugin
    {
        private const string PluginGuid = "Landoria.Socialize";
        private const string PluginName = "Landoria.Socialize";
        private const string PluginVersion = "1.0.16";

        internal static ManualLogSource Log { get; private set; }
        internal static SocializeSettings Settings { get; private set; }


        private Harmony _harmony;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            _harmony.PatchAll();
            Settings = new SocializeSettings();
            Log.LogInfo($"{PluginName} {PluginVersion} is loaded.");
        }

        private void Update()
        {
            TextPermissionService.Update();
            GroupService.Update();
            TargetPingService.Update();
            PrivateChat.Update();
        }

        private void OnDestroy()
        {
            GroupService.Reset();
            TargetPingService.Reset();
            PrivateChat.Reset();
            TextPermissionService.Reset();
            Log?.LogInfo($"{PluginName} {PluginVersion} is unloaded.");
            _harmony?.UnpatchSelf();
            _harmony = null;
            Settings = null;
            Log = null;
        }
    }
}
