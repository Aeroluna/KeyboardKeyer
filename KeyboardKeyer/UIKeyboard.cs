namespace KeyboardKeyer
{
    using HarmonyLib;
    using HMUI;
    using System;
    using System.Reflection;

    [HarmonyPatch(typeof(UIKeyboard))]
    [HarmonyPatch(nameof(UIKeyboard.Awake))]
    internal static class BeatmapDataLoaderGetBeatmapDataFromBeatmapSaveData
    {
        internal static KeyboardKeyerController keyerInstance;

        private static void Postfix(UIKeyboard __instance)
        {
            keyerInstance = __instance.gameObject.AddComponent<KeyboardKeyerController>();
            keyerInstance.Init(__instance);
        }
    }

    [HarmonyPatch]
    internal static class BlockSongCoreRefresh {
        static MethodBase TargetMethod() => AccessTools.Method("SongCore.Loader:Update");
        static Exception Cleanup(Exception _) => null;
        static bool Prefix() {
            if(BeatmapDataLoaderGetBeatmapDataFromBeatmapSaveData.keyerInstance == null)
                return true;
            
            return !BeatmapDataLoaderGetBeatmapDataFromBeatmapSaveData.keyerInstance.isActiveAndEnabled;
        }
        
    }
}
