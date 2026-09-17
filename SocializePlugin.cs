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

        private void RegisterPatches()
        {
            _harmony.CreateClassProcessor(typeof(RegisterSocialCommandsPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(RequestSocialStateOnSpawnPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(DisablePrivateWorldTextPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(LimitMapPingToGroupPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(PersistentChatInputPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(PersistentChatChannelPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(SocialChatRangePatch)).Patch();
            _harmony.CreateClassProcessor(typeof(ChatPresentationPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(AutoDisplaySimpleChatPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FormatTitleChatPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(FormatUserChatPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(GroupNewConnectionPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(GroupDisconnectPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(UpdateMapPingVisibilityPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(DisablePublicPositionPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(ShowGroupMembersOnMapPatch)).Patch();
            _harmony.CreateClassProcessor(typeof(HidePublicPositionTogglePatch)).Patch();
        }

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"AssemblyVersion: {GetType().Assembly.GetName().Version}.");
            _harmony = new Harmony(PluginGuid);
            RegisterPatches();
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
