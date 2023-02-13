using UnityEngine;
using UnityModManagerNet;

namespace BetterVR
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool AccuratePlayerHeight_Enabled = true;
        public bool AccuratePlayerHeight_DisableCrouching = true;

        public void Draw(UnityModManager.ModEntry modEntry)
        {
            GUILayout.BeginVertical();

            GUILayout.Label("Accurate Player Height:");
            AccuratePlayerHeight_Enabled = GUILayout.Toggle(AccuratePlayerHeight_Enabled, "Enabled");
            AccuratePlayerHeight_DisableCrouching = GUILayout.Toggle(AccuratePlayerHeight_DisableCrouching, "Disable Crouching");
            GUILayout.Space(2);

            GUILayout.EndVertical();
        }

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
