using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace AutoHideHUD
{
    [BepInPlugin("silksong.mods.AutoHideHUD", "Auto Hide HUD", "1.0.0")]
    public class AutoHideHUDMod : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony("silksong.mods.AutoHideHUD").PatchAll();
            Logger.LogInfo("AutoHideHUD initialized.");
            GameObject hudManagerObj = new GameObject("DynamicHudManagerObject");
            hudManagerObj.AddComponent<DynamicHudManager>();
            DontDestroyOnLoad(hudManagerObj);
            Logger.LogInfo("DynamicHudManager successfully injected into the game!");
        }
    }
}
