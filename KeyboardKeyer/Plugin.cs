using System.Reflection;
using HarmonyLib;
using IPA;
using IPA.Logging;
using JetBrains.Annotations;

namespace KeyboardKeyer;

[Plugin(RuntimeOptions.SingleStartInit)]
public class Plugin
{
    internal const string HARMONY_ID = "dev.aeroluna.KeyboardKeyer";

    internal static readonly Harmony _harmonyInstance = new(HARMONY_ID);

#pragma warning disable CA1822
    [UsedImplicitly]
    [Init]
    public Plugin(Logger pluginLogger)
    {
        Log = pluginLogger;
    }

    internal static Logger Log { get; private set; } = null!;

    [UsedImplicitly]
    [OnEnable]
    public void OnEnable()
    {
        _harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
    }
#pragma warning restore CA1822
}
