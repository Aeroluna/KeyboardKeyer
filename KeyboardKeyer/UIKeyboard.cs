namespace KeyboardKeyer
{
    using HarmonyLib;
    using HMUI;

    [HarmonyPatch(typeof(UIKeyboard))]
    [HarmonyPatch(nameof(UIKeyboard.Awake))]
    internal static class BeatmapDataLoaderGetBeatmapDataFromBeatmapSaveData
    {
        private static void Postfix(UIKeyboard __instance)
        {
            KeyboardKeyerController keyboardKeyer = __instance.gameObject.AddComponent<KeyboardKeyerController>();
            keyboardKeyer.Init(__instance);
        }
    }
}
