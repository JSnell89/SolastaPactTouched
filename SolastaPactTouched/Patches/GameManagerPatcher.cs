using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace SolastaPactTouched.Patches
{
    class GameManagerPatcher
    {
        [HarmonyPatch(typeof(GameManager), "BindPostDatabase")]
        internal static class GameManager_BindPostDatabase_Patch
        {
            internal static void Postfix()
            {
                Main.ModEntryPoint();
            }
        }


        [HarmonyPatch(typeof(RulesetSpellRepertoire), "get_MaxSpellLevelOfSpellCastingLevel")]
        internal static class RulesetSpellRepertoire_get_MaxSpellLevelOfSpellCastingLevel_Patch
        {
            internal static void Postfix(RulesetSpellRepertoire __instance, ref int __result)
            {
                if(__instance.SpellCastingFeature.SlotsRecharge == RuleDefinitions.RechargeRate.ShortRest)
                {
                    if (__instance.SpellCastingFeature == null)
                    {
                        Trace.LogWarning("Invalid SpellCastingFeature in RulesetSpellRepertoire.ComputeSpellSlots()");
                        return;
                    }
                    if (__instance.SpellCastingLevel < 1 || __instance.SpellCastingFeature != null && __instance.SpellCastingLevel > __instance.SpellCastingFeature.SlotsPerLevels.Count - 1)
                    {
                        Trace.LogWarning("Invalid spellcasting level in RulesetSpellRepertoire.ComputeSpellSlots()");
                        return;
                    }
                    if (__instance.SpellCastingFeature == null)
                    {
                        return;
                    }
                    FeatureDefinitionCastSpell.SlotsByLevelDuplet item = __instance.SpellCastingFeature.SlotsPerLevels[__instance.SpellCastingLevel -1];
                    if (item == null || item.Slots == null || item.Slots.Count == 0)
                    {
                        Trace.LogWarning("Invalid duplet in RulesetSpellRepertoire.ComputeSpellSlots()");
                        return;
                    }
                    //************************************************************
                    //Only real change - Switch to Last non-zero index (plus 1 since arrays start a 0) so that spell slots aren't required lower level slots to allow higher level spellcasting
                    //************************************************************
                    int num = item.Slots.FindLastIndex(i => i > 0) + 1;
                    if (num == -1)
                    {
                        num = (item.Slots.Count > 0 ? item.Slots.Count : __instance.SpellCastingFeature.SpellListDefinition.MaxSpellLevel);
                    }
                    __result = num;
                }
            }
        }
        //TODO ComputeHighestSpellLevel patch

        [HarmonyPatch(typeof(FeatureDefinitionCastSpell), "ComputeHighestSpellLevel")]
        internal static class FeatureDefinitionCastSpell_ComputeHighestSpellLevel_Patch
        {
            internal static void Postfix(FeatureDefinitionCastSpell __instance, int classLevel, ref int __result)
            {
                List<int> slots = __instance.SlotsPerLevels[classLevel - 1].Slots;
                __result = slots.FindLastIndex(i => i > 0) + 1;//Switch to Last non-zero index (plus 1 since arrays start a 0)
            }
        }


        [HarmonyPatch(typeof(RulesetSpellRepertoire), "RestoreAllSpellSlots")]
        internal static class RulesetSpellRepertoire_RestoreAllSpellSlots_Patch
        {
            internal static void Postfix(RulesetSpellRepertoire __instance)
            {
                //Only apply to Warlock Short rest spells to not mess with other potential patches
                if (__instance.SpellCastingFeature.SlotsRecharge == RuleDefinitions.RechargeRate.ShortRest)
                {

                    if (__instance.SpellCastingLevel - 1 >= __instance.SpellCastingFeature.SlotsPerLevels.Count)
                    {
                        Trace.LogError(string.Format("Spellcasting feature {0} does not contain an entry for spell slots at spellcasting level {1}", __instance.SpellCastingFeature.Name, __instance.SpellCastingLevel));
                        return;
                    }
                    int maxSpellLevel = __instance.SpellCastingFeature.SlotsPerLevels[__instance.SpellCastingLevel - 1].Slots.FindLastIndex(i => i > 0) + 1; //Change to find last non-zero index +1
                    if (maxSpellLevel == -1)
                    {
                        maxSpellLevel = __instance.SpellCastingFeature.SpellListDefinition.MaxSpellLevel;
                        if (maxSpellLevel > __instance.SpellCastingFeature.SlotsPerLevels[__instance.SpellCastingLevel - 1].Slots.Count)
                        {
                            maxSpellLevel = __instance.SpellCastingFeature.SlotsPerLevels[__instance.SpellCastingLevel - 1].Slots.Count;
                        }
                    }
                    for (int i = 0; i < maxSpellLevel; i++)
                    {
                        __instance.AvailableSpellsSlots[i + 1] = __instance.SpellCastingFeature.SlotsPerLevels[__instance.SpellCastingLevel - 1].Slots[i];
                    }
                    RulesetSpellRepertoire.RepertoireRefreshedHandler repertoireRefreshed = __instance.RepertoireRefreshed;
                    if (repertoireRefreshed == null)
                    {
                        return;
                    }
                    repertoireRefreshed(__instance);
                }
            }
        }
    }
}
