using HarmonyLib;
using UnityEngine;

namespace KeyboardKeyer;

[HarmonyPatch(typeof(Input))]
public static class InputKiller
{
    public static bool Active { get; set; }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(Input.GetKeyDown), typeof(KeyCode))]
    [HarmonyPatch(nameof(Input.GetKeyDown), typeof(string))]
    [HarmonyPatch(nameof(Input.GetKey), typeof(KeyCode))]
    [HarmonyPatch(nameof(Input.GetKey), typeof(string))]
    [HarmonyPatch(nameof(Input.GetKeyUp), typeof(KeyCode))]
    [HarmonyPatch(nameof(Input.GetKeyUp), typeof(string))]
    private static bool Kill()
    {
        return !Active;
    }
}
