using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace BetterVR.Patches
{
    /// <summary>
    ///     Where the magic happens. We calculate the height by subtracting the CharacterController's Y position which is at
    ///     the bottom by the camera's Y position.
    /// </summary>
    [HarmonyPatch(typeof(CustomFirstPersonController), "Update")]
    public static class CustomFirstPersonController_Update_Patch
    {
        private static readonly FieldInfo _capsuleProp = AccessTools.DeclaredField(typeof(CustomFirstPersonController), "capsule");

        private static void Postfix(CustomFirstPersonController __instance)
        {
            if (!Main.Enabled || !Main.Settings.AccuratePlayerHeight_Enabled)
                return;

            CharacterController capsule = (CharacterController)_capsuleProp.GetValue(__instance);

            float height = Mathf.Abs(__instance.m_Camera.transform.position.y - capsule.transform.position.y);

            capsule.height = height - capsule.skinWidth * 2;
            capsule.center = new Vector3(0, height / 2, 0);
        }
    }

    /// <summary>
    ///     Since the CharacterController's height is now set to our real height, it won't pass validation.
    ///     This isn't a big deal so we can just disable it.
    /// </summary>
    [HarmonyPatch(typeof(CustomFirstPersonController), "ValidateCharacterControllerValues")]
    public static class CustomFirstPersonController_ValidateCharacterControllerValues_Patch
    {
        private static bool Prefix()
        {
            return !Main.Enabled || !Main.Settings.AccuratePlayerHeight_Enabled;
        }
    }

    /// <summary>
    ///     To disable crouching with the button, we just disable this entire method.
    ///     Is this the best way to do it? Probably not. Does it work? Yes™.
    /// </summary>
    [HarmonyPatch(typeof(CustomFirstPersonController), "InputBasedHeightUpdate")]
    public static class CustomFirstPersonController_InputBasedHeightUpdate_Patch
    {
        private static bool Prefix()
        {
            return !Main.Enabled || !Main.Settings.AccuratePlayerHeight_Enabled || !Main.Settings.AccuratePlayerHeight_DisableCrouching;
        }
    }
}
