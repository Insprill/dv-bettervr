using System;
using HarmonyLib;
using UnityModManagerNet;

namespace BetterVR
{
    public static class Main
    {
        private static UnityModManager.ModEntry ModEntry;
        public static bool Enabled => ModEntry.Enabled;
        public static UnityModManager.ModEntry.ModLogger Logger => ModEntry.Logger;

        public static Settings Settings { get; private set; }

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            ModEntry = modEntry;
            ModEntry.Enabled = VRManager.IsVREnabled();
            Settings = UnityModManager.ModSettings.Load<Settings>(ModEntry);
            ModEntry.OnGUI = DrawGUI;
            ModEntry.OnSaveGUI = SaveGUI;

            Harmony harmony = null;

            try
            {
                Logger.Log("Loading BetterVR");

                harmony = new Harmony(ModEntry.Info.Id);
                harmony.PatchAll();

                Logger.Log("Successfully patched");
            }
            catch (Exception ex)
            {
                Logger.LogException("Failed to load BetterVR:", ex);
                harmony?.UnpatchAll();
                Logger.Log("Unpatched");
                return false;
            }

            return true;
        }

        private static void DrawGUI(UnityModManager.ModEntry entry)
        {
            Settings.Draw(entry);
        }

        private static void SaveGUI(UnityModManager.ModEntry entry)
        {
            Settings.Save(entry);
        }
    }
}
