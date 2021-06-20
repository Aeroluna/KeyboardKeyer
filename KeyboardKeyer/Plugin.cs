namespace KeyboardKeyer
{
    using System.Reflection;
    using HarmonyLib;
    using IPA;
    using IPALogger = IPA.Logging.Logger;

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal const string HARMONYID = "com.aeroluna.BeatSaber.KeyboardKeyer";

        internal static readonly Harmony _harmonyInstance = new Harmony(HARMONYID);

        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger)
        {
            Log = logger;
        }

        [OnEnable]
        public void OnEnable()
        {
            _harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
