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
                if(__instance?.SpellCastingFeature?.SlotsRecharge != null && __instance.SpellCastingFeature.SlotsRecharge == RuleDefinitions.RechargeRate.ShortRest)
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

        [HarmonyPatch(typeof(FeatureDefinitionCastSpell), "ComputeHighestSpellLevel")]
        internal static class FeatureDefinitionCastSpell_ComputeHighestSpellLevel_Patch
        {
            internal static void Postfix(FeatureDefinitionCastSpell __instance, int classLevel, ref int __result)
            {
                List<int> slots = __instance.SlotsPerLevels[classLevel - 1].Slots;
                __result = slots.FindLastIndex(i => i > 0) + 1;//Switch to Last non-zero index (plus 1 since arrays start a 0)
            }
        }

        [HarmonyPatch(typeof(RulesetSpellRepertoire), "ComputeSpellSlots")]
        internal static class RulesetSpellRepertoire_ComputeSpellSlots_Patch
        {
            internal static void Postfix(RulesetSpellRepertoire __instance, List<FeatureDefinition> spellCastingAffinities)
            {
                //Don't do anything for non-short rest classes (non-Warlocks)
                if (__instance.SpellCastingFeature.SlotsRecharge != RuleDefinitions.RechargeRate.ShortRest)
                    return;

                int maxSpellLevel = __instance.MaxSpellLevelOfSpellCastingLevel;
                var currentInstanceSpellsSlotCapacities = (Dictionary<int, int>)AccessTools.Field(__instance.GetType(), "spellsSlotCapacities").GetValue(__instance);
                currentInstanceSpellsSlotCapacities.Clear();
                for (int i = 0; i < maxSpellLevel; i++)
                {
                    currentInstanceSpellsSlotCapacities[i + 1] = __instance.SpellCastingFeature.SlotsPerLevels[__instance.SpellCastingLevel - 1].Slots[i];

                    //Not going to bother with used spell slots for Warlock, this only helps between patch saves and may give a spell slot back
                    //if (this.legacyAvailableSpellsSlots.ContainsKey(i + 1))
                    //{
                    //    this.usedSpellsSlots.Add(i + 1, currentInstanceSpellsSlotCapacities[i + 1] - this.legacyAvailableSpellsSlots[i + 1]);
                    //    this.legacyAvailableSpellsSlots.Remove(i + 1);
                    //}
                }
                if (spellCastingAffinities != null && spellCastingAffinities.Count > 0)
                {
                    foreach (FeatureDefinition spellCastingAffinity in spellCastingAffinities)
                    {
                        foreach (AdditionalSlotsDuplet additionalSlot in ((ISpellCastingAffinityProvider)spellCastingAffinity).AdditionalSlots)
                        {
                            if (!currentInstanceSpellsSlotCapacities.ContainsKey(additionalSlot.SlotLevel))
                            {
                                currentInstanceSpellsSlotCapacities[additionalSlot.SlotLevel] = additionalSlot.SlotsNumber;
                            }
                            else
                            {
                                Dictionary<int, int> item = currentInstanceSpellsSlotCapacities;
                                int slotLevel = additionalSlot.SlotLevel;
                                item[slotLevel] = item[slotLevel] + additionalSlot.SlotsNumber;
                            }
                        }
                    }
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
