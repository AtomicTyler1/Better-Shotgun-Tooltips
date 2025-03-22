using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using System.Linq;

[BepInDependency("Entity378.BuyableShotgunPlus", BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin("com.atomic.shotgunsafety", "Shotgun Safety", "1.4.0")]
public class ShotgunPatch : BaseUnityPlugin
{
    public static ShotgunPatch Instance { get; private set; }

    internal ConfigEntry<bool> colorizeTextConfig;
    internal ConfigEntry<bool> colorizeFullTextConfig;
    internal ConfigEntry<bool> ammoIndicator;
    internal ConfigEntry<bool> numberRepresentsAmmo;
    internal ConfigEntry<string> colorOnConfig;
    internal ConfigEntry<string> colorOffConfig;
    internal ConfigEntry<string> textOffConfig;
    internal ConfigEntry<string> textOnConfig;

    private bool CheckIfModInstalled(string modGUID)
    {
        string modGUID2 = modGUID;
        return Chainloader.PluginInfos.Values.Any((PluginInfo info) => info.Metadata.GUID == modGUID2);
    }

    void Awake()
    {
        Instance = this;

        colorizeTextConfig = Config.Bind("Text Color", "Colorize Text", false, "Enable or disable colorized text for shotgun safety.");
        colorizeFullTextConfig = Config.Bind("Text Color", "Colorize Full-Text", false, "Enable or disable coloring the entire text instead of just the 'on' or 'off' word.");
        colorOnConfig = Config.Bind("Text Color", "On-Text Color", "green", "The color for the 'on' text.");
        colorOffConfig = Config.Bind("Text Color", "Off-Text Color", "red", "The color for the 'off' text.");
        textOffConfig = Config.Bind("Text Settings", "Safety Text Off", "", "Custom text for safety off. Must contain the keyword 'off'.");
        textOnConfig = Config.Bind("Text Settings", "Safety Text On", "", "Custom text for safety on. Must contain the keyword 'on'.");
        ammoIndicator = Config.Bind("Miscellaneous", "Ammo Indicator", false, "Shows how much ammo you have in the shotgun.");
        numberRepresentsAmmo = Config.Bind("Miscellaneous", "Number Represents Ammo", false, "False is more compact, True is more wordy. You can always change this mid game using LethalConfig without a restart");

        Harmony harmony = new Harmony("com.atomic.shotgunsafety");
        harmony.PatchAll();

        if (CheckIfModInstalled("Entity378.BuyableShotgunPlus"))
        {
            harmony.Unpatch(AccessTools.Method(typeof(ShotgunItem), nameof(ShotgunItem.SetControlTipsForItem)), HarmonyPatchType.Postfix, "Entity378.BuyableShotgunPlus");
        }

        Logger.LogInfo("Shotgun Safety Plugin loaded successfully.");
    }

    public static string FormatSafetyText(string text, string keyword, string color, bool fullColor, bool shouldColor)
    {
        if (string.IsNullOrWhiteSpace(text) || !text.ToLower().Contains(keyword.ToLower()))
            return "Incorrect setup! Missing keyword: " + keyword;

        string colorTag = fullColor ? $"<color={color}>" : "<color=white></color><color=" + color + ">";
        if (!shouldColor) { return text; }
        if (fullColor) { return text.Replace(text, $"{colorTag}{text}</color>"); }
        if (!fullColor) { return text.Replace(keyword, $"{colorTag}{keyword}</color>"); }
        return null;
    }
}

[HarmonyPatch(typeof(ShotgunItem))]
public class ShotgunItemPatches
{
    [HarmonyPostfix]
    [HarmonyPatch("SetControlTipsForItem")]
    [HarmonyPatch("SetSafetyControlTip")]
    [HarmonyPatch("ItemActivate")]
    [HarmonyPatch("ReloadGunEffectsServerRpc")]
    static void UpdateTooltips(ShotgunItem __instance, int ___shellsLoaded)
    {
        string safetyText = __instance.safetyOn
            ? ShotgunPatch.FormatSafetyText(
                string.IsNullOrWhiteSpace(ShotgunPatch.Instance.textOnConfig.Value) ? "The safety is on: [Q]" : $"{ShotgunPatch.Instance.textOnConfig.Value}: [Q]",
                "on", ShotgunPatch.Instance.colorOnConfig.Value, ShotgunPatch.Instance.colorizeFullTextConfig.Value, ShotgunPatch.Instance.colorizeTextConfig.Value ? true : false)
            : ShotgunPatch.FormatSafetyText(
                string.IsNullOrWhiteSpace(ShotgunPatch.Instance.textOffConfig.Value) ? "The safety is off: [Q]" : $"{ShotgunPatch.Instance.textOffConfig.Value}: [Q]",
                "off", ShotgunPatch.Instance.colorOffConfig.Value, ShotgunPatch.Instance.colorizeFullTextConfig.Value, ShotgunPatch.Instance.colorizeTextConfig.Value ? true : false
            );

        if (__instance.IsOwner)
        {
            string[] toolTips = __instance.itemProperties.toolTips;
            toolTips[2] = ShotgunPatch.Instance.colorizeTextConfig.Value ? safetyText : (ShotgunPatch.Instance.textOnConfig.Value ?? "The safety is on: [Q]");
            HUDManager.Instance.ChangeControlTip(3, safetyText, false);
            HUDManager.Instance.ChangeControlTipMultiple(toolTips, true, __instance.itemProperties);
        }

        if (ShotgunPatch.Instance.ammoIndicator.Value)
        {

            if (ShotgunPatch.Instance.numberRepresentsAmmo.Value)
            {
                int shellsRemaining = ___shellsLoaded;

                string[] toolTips = __instance.itemProperties.toolTips;
                toolTips[0] = $"Fire ({shellsRemaining} Loaded): [RMB]";
                if (__instance.IsOwner) { HUDManager.Instance.ChangeControlTipMultiple(toolTips, true, __instance.itemProperties); }
            }
            else
            {
                char c1 = ___shellsLoaded >= 1 ? 'O' : ' ';
                char c2 = ___shellsLoaded >= 2 ? 'O' : ' ';

                string[] toolTips = __instance.itemProperties.toolTips;
                toolTips[0] = $"Fire ({c1})({c2}): [RMB]";
                if (__instance.IsOwner) { HUDManager.Instance.ChangeControlTipMultiple(toolTips, true, __instance.itemProperties); }
            }
        }
        else
        {
            string[] toolTips = __instance.itemProperties.toolTips;
            toolTips[0] = $"Fire: [RMB]";
            if (__instance.IsOwner) { HUDManager.Instance.ChangeControlTipMultiple(toolTips, true, __instance.itemProperties); }
        }
    }
}
