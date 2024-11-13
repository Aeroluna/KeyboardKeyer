using HarmonyLib;
using HMUI;

namespace KeyboardKeyer;

[HarmonyPatch(typeof(InputFieldView))]
internal static class AssignChatKeyboard
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(InputFieldView.ActivateKeyboard))]
    private static void ActivateKeyboard(InputFieldView __instance, UIKeyboard keyboard)
    {
        if (__instance._hasKeyboardAssigned)
        {
            return;
        }

        InputKiller.Active = true;
        KeyboardKeyer keyboardKeyer = keyboard.gameObject.GetComponent<KeyboardKeyer>();
        if (keyboardKeyer != null)
        {
            keyboardKeyer.enabled = true;
        }
        else
        {
            keyboard.gameObject.AddComponent<KeyboardKeyer>();
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(InputFieldView.DeactivateKeyboard))]
    private static void DeactivateKeyboard(InputFieldView __instance, UIKeyboard keyboard)
    {
        if (!__instance._hasKeyboardAssigned)
        {
            return;
        }

        InputKiller.Active = false;
        KeyboardKeyer keyboardKeyer = keyboard.gameObject.GetComponent<KeyboardKeyer>();
        if (keyboardKeyer != null)
        {
            keyboardKeyer.enabled = false;
        }
    }
}
