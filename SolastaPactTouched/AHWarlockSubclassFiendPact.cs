using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using static FeatureDefinitionSavingThrowAffinity;

namespace SolastaPactTouched
{
    public static class AHWarlockSubclassFiendPact
    {
        public static Guid AHWarlockSubclassFiendPactGuid = new Guid("a21286d4-bb03-40ea-afa9-0ee1324a8ba4");

        const string AHWarlockSubclassFiendPactName = "AHWarlockSubclassFiendPact";
        private static readonly string AHWarlockSubclassFiendPactNameGuid = GuidHelper.Create(AHWarlockSubclassFiendPact.AHWarlockSubclassFiendPactGuid, AHWarlockSubclassFiendPactName).ToString();

        public static CharacterSubclassDefinition Build()
        {
            var subclassGuiPresentation = new GuiPresentationBuilder(
                    "Subclass/&AHWarlockSubclassFiendPactDescription",
                    "Subclass/&AHWarlockSubclassFiendPactTitle")
                    .SetSpriteReference(DatabaseHelper.CharacterSubclassDefinitions.TraditionShockArcanist.GuiPresentation.SpriteReference)
                    .Build();

            CharacterSubclassDefinition definition = new CharacterSubclassDefinitionBuilder(AHWarlockSubclassFiendPactName, AHWarlockSubclassFiendPactNameGuid)
                    .SetGuiPresentation(subclassGuiPresentation)
                    .AddFeatureAtLevel(AHWarlockFiendPactExtendedSpellListMagicAffinityBuilder.AHPactTouchedSpellList, 1) // Extra fiend spells
                    .AddFeatureAtLevel(AHWarlockFiendPactDarkOnesBlessingPowerBuilder.AHWarlockFiendPactDarkOnesBlessingPower, 1) // Dark one's blessing, increase temp hp each level, it gets replaced each level by the next hp number.
                    .AddFeatureAtLevel(AHWarlockFiendPactDarkOnesOwnLuckBuilder.AHWarlockFiendPactDarkOnesOwnLuck, 6) // Dark ones luck replaced with indomitable?
                    .AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityFireResistance, 10) // TODO make this a power that you can use once per rest that assigns resistance
                    .AddToDB();

            return definition;
        }
    }

    internal class AHWarlockFiendPactSpellListBuilder : BaseDefinitionBuilder<SpellListDefinition>
    {
        const string AHWarlockFiendPactSpellListName = "AHWarlockFiendPactSpellList";
        private static readonly string AHWarlockFiendPactSpellListGuid = GuidHelper.Create(AHWarlockSubclassFiendPact.AHWarlockSubclassFiendPactGuid, AHWarlockFiendPactSpellListName).ToString();

        protected AHWarlockFiendPactSpellListBuilder(string name, string guid) : base(DatabaseHelper.SpellListDefinitions.SpellListWizardGreenmage, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockFiendPactSpellListTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockFiendPactSpellListDescription";
            Definition.SpellsByLevel.Clear();

            //List of SRD spells for Warlock implemented in Solasta

            //Seems to need a blank cantrip list?
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>(),
                Level = 0
            });

            //Level 1
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.BurningHands,
                DatabaseHelper.SpellDefinitions.Bane, //Bane instead of command?
                //DatabaseHelper.SpellDefinitions.Command, //Not implemented?  Doesn't seem to work
                },
                Level = 1
            });

            //Level 2
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.Blindness,
                DatabaseHelper.SpellDefinitions.ScorchingRay, },
                Level = 2
            });

            //Level 3
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.Fireball,
                DatabaseHelper.SpellDefinitions.StinkingCloud,
                },
                Level = 3
            });

            //Level 4
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.FireShield,
                DatabaseHelper.SpellDefinitions.WallOfFire,
                },
                Level = 4
            });

            //Level 5
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.FlameStrike,
                DatabaseHelper.SpellDefinitions.ConjureElementalFire,
                },
                Level = 5
            });
        }

        public static SpellListDefinition CreateAndAddToDB(string name, string guid)
            => new AHWarlockFiendPactSpellListBuilder(name, guid).AddToDB();

        public static SpellListDefinition AHWarlockFiendPactSpellList = CreateAndAddToDB(AHWarlockFiendPactSpellListName, AHWarlockFiendPactSpellListGuid);
    }

    internal class AHWarlockFiendPactExtendedSpellListMagicAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionMagicAffinity>
    {
        const string AHPactTouchedSpellListName = "AHWarlockFiendPactExtendedSpellList";
        private static readonly string AHPactTouchedSpellListGuid = GuidHelper.Create(AHWarlockSubclassFiendPact.AHWarlockSubclassFiendPactGuid, AHPactTouchedSpellListName).ToString();

        protected AHWarlockFiendPactExtendedSpellListMagicAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionMagicAffinitys.MagicAffinityGreenmageGreenMagicList, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockFiendPactExtendedSpellListTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockFiendPactExtendedSpellListDescription";
            Definition.SetExtendedSpellList(AHWarlockFiendPactSpellListBuilder.AHWarlockFiendPactSpellList);
        }

        public static FeatureDefinitionMagicAffinity CreateAndAddToDB(string name, string guid)
            => new AHWarlockFiendPactExtendedSpellListMagicAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionMagicAffinity AHPactTouchedSpellList = CreateAndAddToDB(AHPactTouchedSpellListName, AHPactTouchedSpellListGuid);
    }

    /// <summary>
    /// Ideally this would auto-apply but that seems like a major amount of work, and currently not possible without patching.
    /// This unfortunately means that opportunity attacks or similar won't do this
    /// And that players can cheat, but they're already using mods so whatever.
    /// </summary>
    internal class AHWarlockFiendPactDarkOnesBlessingPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockFiendPactDarkOnesBlessingPowerName = "AHWarlockFiendPactDarkOnesBlessingPower";
        private static readonly string AHWarlockFiendPactDarkOnesBlessingPowerGuid = GuidHelper.Create(AHWarlockSubclassFiendPact.AHWarlockSubclassFiendPactGuid, AHWarlockFiendPactDarkOnesBlessingPowerName).ToString();

        protected AHWarlockFiendPactDarkOnesBlessingPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockFiendPactDarkOnesBlessingPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockFiendPactDarkOnesBlessingPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);
            Definition.SetAbilityScore("Charisma");

            //Create the temp hp effect
            EffectForm tempHPEffect = new EffectForm();
            tempHPEffect.FormType = EffectForm.EffectFormType.TemporaryHitPoints;
            var tempHPForm = new TemporaryHitPointsForm();
            tempHPForm.DieType = RuleDefinitions.DieType.D1;
            tempHPForm.BonusHitPoints = -1;
            tempHPEffect.SetTemporaryHitPointsForm(tempHPForm);
            tempHPEffect.SetApplyLevel(EffectForm.LevelApplianceType.Add);
            tempHPEffect.ApplyAbilityBonus = true;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(tempHPEffect);
            newEffectDescription.DurationType = RuleDefinitions.DurationType.UntilLongRest;

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockFiendPactDarkOnesBlessingPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockFiendPactDarkOnesBlessingPower = CreateAndAddToDB(AHWarlockFiendPactDarkOnesBlessingPowerName, AHWarlockFiendPactDarkOnesBlessingPowerGuid);
    }

    internal class AHWarlockFiendPactDarkOnesOwnLuckBuilder : BaseDefinitionBuilder<FeatureDefinitionAttributeModifier>
    {
        const string AHWarlockFiendPactDarkOnesOwnLuckName = "AHWarlockFiendPactDarkOnesOwnLuck";
        private static readonly string AHWarlockFiendPactDarkOnesOwnLuckGuid = GuidHelper.Create(AHWarlockSubclassFiendPact.AHWarlockSubclassFiendPactGuid, AHWarlockFiendPactDarkOnesOwnLuckName).ToString();

        protected AHWarlockFiendPactDarkOnesOwnLuckBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAttributeModifiers.AttributeModifierFighterIndomitable, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockFiendPactDarkOnesOwnLuckTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockFiendPactDarkOnesOwnLuckDescription";
        }

        public static FeatureDefinitionAttributeModifier CreateAndAddToDB(string name, string guid)
            => new AHWarlockFiendPactDarkOnesOwnLuckBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAttributeModifier AHWarlockFiendPactDarkOnesOwnLuck = CreateAndAddToDB(AHWarlockFiendPactDarkOnesOwnLuckName, AHWarlockFiendPactDarkOnesOwnLuckGuid);
    }

    internal class AHWarlockFiendPactFiendishResilienceBuilder : BaseDefinitionBuilder<FeatureDefinitionDamageAffinity>
    {
        const string AHWarlockFiendPactFiendishResilienceName = "AHWarlockFiendPactFiendishResilience";
        private static readonly string AHWarlockFiendPactFiendishResilienceGuid = GuidHelper.Create(AHWarlockSubclassFiendPact.AHWarlockSubclassFiendPactGuid, AHWarlockFiendPactFiendishResilienceName).ToString();

        protected AHWarlockFiendPactFiendishResilienceBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityFireResistance, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockFiendPactFiendishResilienceTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockFiendPactFiendishResilienceDescription";
        }

        public static FeatureDefinitionDamageAffinity CreateAndAddToDB(string name, string guid)
            => new AHWarlockFiendPactFiendishResilienceBuilder(name, guid).AddToDB();

        public static FeatureDefinitionDamageAffinity AHWarlockFiendPactFiendishResilience = CreateAndAddToDB(AHWarlockFiendPactFiendishResilienceName, AHWarlockFiendPactFiendishResilienceGuid);
    }
}
