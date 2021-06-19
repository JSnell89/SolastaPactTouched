using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;

namespace SolastaAcehighFeats
{
    /*********************************************/
    /***************  Feats **********************/
    /*********************************************/

    internal class PactTouchedFeatBuilder : BaseDefinitionBuilder<FeatDefinition>
    {
        public static readonly Guid PactTouchedMainGuid = new Guid("05ff22e3-1709-4081-9147-1df506b0507e");
        const string PactTouchedFeatName = "PactTouchedFeat";
        private static readonly string PactTouchedFeatNameGuid = GuidHelper.Create(PactTouchedMainGuid, PactTouchedFeatName).ToString();

        protected PactTouchedFeatBuilder(string name, string guid) : base(DatabaseHelper.FeatDefinitions.FollowUpStrike, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&PactTouchedFeatTitle";
            Definition.GuiPresentation.Description = "Feat/&PactTouchedFeatDescription";

            Definition.Features.Clear();
            Definition.Features.Add(AHPactTouchedEldritchBlastFeatPowerBuilder.AHPactEldricthBlastPower);
            Definition.Features.Add(AHPactMarkedFeatPowerBuilder.AHPactMarkedPower);
            Definition.Features.Add(AHPactTouchedShatterFeatPowerBuilder.AHPactShatter);
            foreach (var characterClass in DatabaseRepository.GetDatabase<CharacterClassDefinition>().GetAllElements())
                Definition.Features.Add(AHPactTouchedBuildAutoPreparedSpellsBuilder.GetOrAdd(characterClass));
            //Try as I might, these do not actually work :(
            //Definition.Features.Add(AHPactTouchedBonusCantripsBuilder.AHPactTouchCantrips); 
            //Definition.Features.Add(AHPactTouchedMagicAffinityBuilder.AHPactTouchedSpellList);

            Definition.SetMinimalAbilityScorePrerequisite(false);
        }

        public static FeatDefinition CreateAndAddToDB(string name, string guid)
            => new PactTouchedFeatBuilder(name, guid).AddToDB();

        public static FeatDefinition PactTouchedFeat = CreateAndAddToDB(PactTouchedFeatName, PactTouchedFeatNameGuid);

        public static void AddToFeatList()
        {
            var PactTouchedFeat = PactTouchedFeatBuilder.PactTouchedFeat;//Instantiating it adds to the DB
        }
    }

    internal class AHPactTouchedAutoPreparedSpellsBuilder : BaseDefinitionBuilder<FeatureDefinitionAutoPreparedSpells>
    {
        const string AHPactTouchedAutoPreparedSpellsName = "AHPactTouchedAutoPreparedSpells";
        private static readonly string AHPactTouchedAutoPreparedSpellsNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedAutoPreparedSpellsName).ToString();

        protected AHPactTouchedAutoPreparedSpellsBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAutoPreparedSpellss.AutoPreparedSpellsDomainBattle, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedAutoPreparedSpellsTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedAutoPreparedSpellsDescription";
        }

        public static FeatureDefinitionAutoPreparedSpells CreateAndAddToDB(string name, string guid)
            => new AHPactTouchedAutoPreparedSpellsBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAutoPreparedSpells AHPactTouchedAutoPreparedSpells = CreateAndAddToDB(AHPactTouchedAutoPreparedSpellsName, AHPactTouchedAutoPreparedSpellsNameGuid);
    }

    internal class AHEldritchBlastSpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string AHEldritchBlastSpellName = "AHEldritchBlastSpell";
        private static readonly string AHEldritchBlastSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHEldritchBlastSpellName).ToString();

        protected AHEldritchBlastSpellBuilder(string name, string guid) : base(DatabaseHelper.SpellDefinitions.MagicMissile, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&AHEldritchBlastSpellTitle";
            Definition.GuiPresentation.Description = "Spell/&AHEldritchBlastSpellDescription";
            Definition.SetSpellLevel(0);
            Definition.SetSomaticComponent(true);
            Definition.SetVerboseComponent(true);

            //D10 damage
            var damageForm = new DamageForm();
            damageForm.DiceNumber = 1;
            damageForm.DamageType = "DamageForce";
            damageForm.DieType = RuleDefinitions.DieType.D10;

            var damageEffectForm = new EffectForm();
            damageEffectForm.FormType = EffectForm.EffectFormType.Damage;
            damageEffectForm.DamageForm = damageForm;

            //Additional blast every 5 caster levels
            var advancement = new EffectAdvancement();
            advancement.SetEffectIncrementMethod(RuleDefinitions.EffectIncrementMethod.CasterLevelTable);
            advancement.SetIncrementMultiplier(5);
            advancement.SetAdditionalTargetsPerIncrement(1);

            var effectDescription = Definition.EffectDescription;
            effectDescription.SetRangeType(RuleDefinitions.RangeType.RangeHit);
            effectDescription.SetRangeParameter(30);
            effectDescription.SetTargetParameter(1);
            effectDescription.EffectForms.Clear();
            effectDescription.EffectForms.Add(damageEffectForm);
            effectDescription.SetEffectAdvancement(advancement);
        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new AHEldritchBlastSpellBuilder(name, guid).AddToDB();

        public static SpellDefinition AHEldritchBlastSpell = CreateAndAddToDB(AHEldritchBlastSpellName, AHEldritchBlastSpellNameGuid);
    }

    internal class AHAgonizingBlastSpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string AHAgonizingBlastSpellName = "AHAgonizingBlastSpell";
        private static readonly string AHAgonizingBlastSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHAgonizingBlastSpellName).ToString();

        protected AHAgonizingBlastSpellBuilder(string name, string guid) : base(AHEldritchBlastSpellBuilder.AHEldritchBlastSpell, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&AHAgonizingBlastSpellTitle";
            Definition.GuiPresentation.Description = "Spell/&AHAgonizingBlastSpellDescription";
            Definition.EffectDescription.EffectForms.Find(ef => ef.DamageForm != null).ApplyAbilityBonus = true;
        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new AHAgonizingBlastSpellBuilder(name, guid).AddToDB();

        public static SpellDefinition AHAgonizingBlastSpell = CreateAndAddToDB(AHAgonizingBlastSpellName, AHAgonizingBlastSpellNameGuid);
    }

    internal class AHHellishRebukeSpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string AHHellishRebukeSpellName = "AHHellishRebukeSpell";
        private static readonly string AHHellishRebukeSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHHellishRebukeSpellName).ToString();

        protected AHHellishRebukeSpellBuilder(string name, string guid) : base(DatabaseHelper.SpellDefinitions.SacredFlame, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHHellishRebukeSpellTitle";
            Definition.GuiPresentation.Description = "Feat/&AHHellishRebukeSpellDescription";
            Definition.SetSpellLevel(1);
            Definition.SetSomaticComponent(true);
            Definition.SetVerboseComponent(true);
            Definition.SetCastingTime(RuleDefinitions.ActivationTime.Reaction);

            //D10 damage
            var damageForm = new DamageForm();
            damageForm.DiceNumber = 2;
            damageForm.DamageType = "DamageFire";
            damageForm.DieType = RuleDefinitions.DieType.D10;

            var damageEffectForm = new EffectForm();
            damageEffectForm.HasSavingThrow = true;
            damageEffectForm.FormType = EffectForm.EffectFormType.Damage;
            damageEffectForm.SavingThrowAffinity = RuleDefinitions.EffectSavingThrowType.HalfDamage;
            damageEffectForm.SaveOccurence = RuleDefinitions.TurnOccurenceType.EndOfTurn;
            damageEffectForm.DamageForm = damageForm;
            

            //Additional die per spell level
            var advancement = new EffectAdvancement();
            advancement.SetEffectIncrementMethod(RuleDefinitions.EffectIncrementMethod.PerAdditionalSlotLevel);
            advancement.SetAdditionalDicePerIncrement(1);

            var effectDescription = Definition.EffectDescription;
            effectDescription.SetRangeParameter(12);
            effectDescription.EffectForms.Clear();
            effectDescription.SetTargetParameter(1);
            damageEffectForm.HasSavingThrow = true;
            effectDescription.SavingThrowAbility = "Dexterity";
            effectDescription.SetEffectAdvancement(advancement);
            effectDescription.EffectForms.Add(damageEffectForm);

            Definition.SetEffectDescription(effectDescription);
        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new AHHellishRebukeSpellBuilder(name, guid).AddToDB();

        public static SpellDefinition AHHellishRebukeSpell = CreateAndAddToDB(AHHellishRebukeSpellName, AHHellishRebukeSpellNameGuid);
    }


    internal class AHHellishRebukePowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHHellishRebukeSpellName = "AHHellishRebukePowerSpell";
        private static readonly string AHHellishRebukeSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHHellishRebukeSpellName).ToString();

        protected AHHellishRebukePowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerRangerPrimevalAwareness, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHHellishRebukeSpellTitle";
            Definition.GuiPresentation.Description = "Feat/&AHHellishRebukeSpellDescription";
            Definition.SetSpellcastingFeature(DatabaseHelper.FeatureDefinitionCastSpells.CastSpellWizard);
            Definition.SetEffectDescription(AHHellishRebukeSpellBuilder.AHHellishRebukeSpell.EffectDescription);
            Definition.SetReactionContext(RuleDefinitions.ReactionTriggerContext.HitByMelee);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Reaction);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHHellishRebukePowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHHellishRebukeSpell = CreateAndAddToDB(AHHellishRebukeSpellName, AHHellishRebukeSpellNameGuid);
    }


    internal class AHPactMarkSpellBuilder : BaseDefinitionBuilder<SpellDefinition>
    {
        const string AHPactMarkSpellName = "AHPactMarkSpellBuilder";
        private static readonly string AHPactMarkSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkSpellName).ToString();

        protected AHPactMarkSpellBuilder(string name, string guid) : base(DatabaseHelper.SpellDefinitions.HuntersMark, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&AHPactMarkSpellTitle";
            Definition.GuiPresentation.Description = "Spell/&AHPactMarkSpellDescription";
            Definition.SetSpellLevel(1);
            Definition.SetSomaticComponent(true);
            Definition.SetVerboseComponent(true);
            Definition.SetSchoolOfMagic("SchoolEnchantment");
            Definition.SetMaterialComponentType(RuleDefinitions.MaterialComponentType.Mundane);
            Definition.SetCastingTime(RuleDefinitions.ActivationTime.BonusAction);

            var markedByPactEffectForm = new EffectForm();
            markedByPactEffectForm.FormType = EffectForm.EffectFormType.Condition;
            markedByPactEffectForm.ConditionForm = new ConditionForm();
            markedByPactEffectForm.ConditionForm.ConditionDefinition = AHPactMarkMarkedByPactConditionBuilder.MarkedByPactCondition;
            markedByPactEffectForm.SetCreatedByCharacter(true);

            var pactMarkEffectForm = new EffectForm();
            pactMarkEffectForm.FormType = EffectForm.EffectFormType.Condition;
            pactMarkEffectForm.ConditionForm = new ConditionForm();
            pactMarkEffectForm.ConditionForm.ConditionDefinition = AHPactMarkPactMarkConditionBuilder.PactMarkCondition;
            pactMarkEffectForm.ConditionForm.SetApplyToSelf(true);
            pactMarkEffectForm.SetCreatedByCharacter(true);

            var effectDescription = Definition.EffectDescription;
            effectDescription.SetRangeType(RuleDefinitions.RangeType.Distance);
            effectDescription.SetRangeParameter(24);
            effectDescription.SetTargetParameter(1);
            effectDescription.EffectForms.Clear();
            effectDescription.EffectForms.Add(markedByPactEffectForm);
            effectDescription.EffectForms.Add(pactMarkEffectForm);

            Definition.SetEffectDescription(effectDescription);
        }

        public static SpellDefinition CreateAndAddToDB(string name, string guid)
            => new AHPactMarkSpellBuilder(name, guid).AddToDB();

        public static SpellDefinition AHPactMarkSpell = CreateAndAddToDB(AHPactMarkSpellName, AHPactMarkSpellNameGuid);
    }

    internal class AHPactMarkMarkedByPactConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHPactMarkMarkedByPactConditionName = "AHPactMarkMarkedByPactCondition";
        private static readonly string AHPactMarkMarkedByPactConditionGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkMarkedByPactConditionName).ToString();

        protected AHPactMarkMarkedByPactConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionMarkedByHunter, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&AHPactMarkMarkedByPactConditionTitle";
            Definition.GuiPresentation.Description = "Spell/&AHPactMarkMarkedByPactConditionDescription";
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHPactMarkMarkedByPactConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition MarkedByPactCondition = CreateAndAddToDB(AHPactMarkMarkedByPactConditionName, AHPactMarkMarkedByPactConditionGuid);
    }

    internal class AHPactMarkPactMarkConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHPactMarkPactMarkConditionName = "AHPactMarkPactMarkCondition";
        private static readonly string AHPactMarkPactMarkConditionGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkPactMarkConditionName).ToString();

        protected AHPactMarkPactMarkConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHuntersMark, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&AHPactMarkPactMarkConditionTitle";
            Definition.GuiPresentation.Description = "Spell/&AHPactMarkPactMarkConditionDescription";
            Definition.Features.Clear();
            Definition.Features.Add(AHPactMarkAdditionalDamageBuilder.AHPactMarkAdditionalDamage);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHPactMarkPactMarkConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition PactMarkCondition = CreateAndAddToDB(AHPactMarkPactMarkConditionName, AHPactMarkPactMarkConditionGuid);
    }

    internal class AHPactMarkAdditionalDamageBuilder : BaseDefinitionBuilder<FeatureDefinitionAdditionalDamage>
    {
        const string AHPactMarkAdditionalDamageBuilderName = "AHPactMarkAdditionalDamage";
        private static readonly string AHPactMarkAdditionalDamageGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkAdditionalDamageBuilderName).ToString();

        protected AHPactMarkAdditionalDamageBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalDamages.AdditionalDamageHuntersMark, name, guid)
        {
            Definition.GuiPresentation.Title = "Spell/&AHPactMarkAdditionalDamageTitle";
            Definition.GuiPresentation.Description = "Spell/&AHPactMarkAdditionalDamageDescription";
            Definition.SetAttackModeOnly(false);
            Definition.SetRequiredTargetCondition(AHPactMarkMarkedByPactConditionBuilder.MarkedByPactCondition);
            Definition.SetNotificationTag("PactMarked");
        }

        public static FeatureDefinitionAdditionalDamage CreateAndAddToDB(string name, string guid)
            => new AHPactMarkAdditionalDamageBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAdditionalDamage AHPactMarkAdditionalDamage = CreateAndAddToDB(AHPactMarkAdditionalDamageBuilderName, AHPactMarkAdditionalDamageGuid);
    }

    internal class AHPactMarkedFeatPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHPactMarkedFeatPowerName = "AHPactMarkedFeatPower";
        private static readonly string AHHellishRebukeSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkedFeatPowerName).ToString();

        protected AHPactMarkedFeatPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerRangerPrimevalAwareness, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactMarkedFeatPowerTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactMarkedFeatPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.HuntersMark.GuiPresentation.SpriteReference);
            Definition.SetEffectDescription(AHPactMarkSpellBuilder.AHPactMarkSpell.EffectDescription);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(1);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHPactMarkedFeatPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHPactMarkedPower = CreateAndAddToDB(AHPactMarkedFeatPowerName, AHHellishRebukeSpellNameGuid);
    }

    internal class AHPactTouchedShatterFeatPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHPactMarkedFeatPowerName = "AHPactTouchedShatterFeatPower";
        private static readonly string AHHellishRebukeSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkedFeatPowerName).ToString();

        protected AHPactTouchedShatterFeatPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerRangerPrimevalAwareness, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedShatterFeatPowerTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedShatterFeatPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.Shatter.GuiPresentation.SpriteReference);
            Definition.SetEffectDescription(DatabaseHelper.SpellDefinitions.Shatter.EffectDescription);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);
            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(1);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHPactTouchedShatterFeatPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHPactShatter = CreateAndAddToDB(AHPactMarkedFeatPowerName, AHHellishRebukeSpellNameGuid);
    }

    internal class AHPactTouchedEldritchBlastFeatPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHPactMarkedFeatPowerName = "AHPactTouchedEldritchBlastFeatPower";
        private static readonly string AHHellishRebukeSpellNameGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactMarkedFeatPowerName).ToString();

        protected AHPactTouchedEldritchBlastFeatPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerRangerPrimevalAwareness, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedEldritchBlastFeatPowerTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedEldritchBlastFeatPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.MagicMissile.GuiPresentation.SpriteReference);
            Definition.SetEffectDescription(AHEldritchBlastSpellBuilder.AHEldritchBlastSpell.EffectDescription);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);
            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHPactTouchedEldritchBlastFeatPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHPactEldricthBlastPower = CreateAndAddToDB(AHPactMarkedFeatPowerName, AHHellishRebukeSpellNameGuid);
    }

    internal class AHPactTouchedSpellListBuilder : BaseDefinitionBuilder<SpellListDefinition>
    {
        const string AHPactTouchedSpellListName = "AHPactTouchedSpellList";
        private static readonly string AHPactTouchedSpellListGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedSpellListName).ToString();

        protected AHPactTouchedSpellListBuilder(string name, string guid) : base(DatabaseHelper.SpellListDefinitions.SpellListWizardGreenmage, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedSpellListTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedSpellListDescription";
            Definition.SpellsByLevel.Clear();
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet() { Spells = new List<SpellDefinition>() { AHEldritchBlastSpellBuilder.AHEldritchBlastSpell }, Level = 0 });
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet() { Spells = new List<SpellDefinition>() { AHPactMarkSpellBuilder.AHPactMarkSpell }, Level = 1 });
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet() { Spells = new List<SpellDefinition>() { DatabaseHelper.SpellDefinitions.Shatter }, Level = 2 });
        }

        public static SpellListDefinition CreateAndAddToDB(string name, string guid)
            => new AHPactTouchedSpellListBuilder(name, guid).AddToDB();

        public static SpellListDefinition AHPactTouchedSpellList = CreateAndAddToDB(AHPactTouchedSpellListName, AHPactTouchedSpellListGuid);
    }

    internal class AHPactTouchedMagicAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionMagicAffinity>
    {
        const string AHPactTouchedSpellListName = "AHPactTouchedSpellList";
        private static readonly string AHPactTouchedSpellListGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedSpellListName).ToString();

        protected AHPactTouchedMagicAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionMagicAffinitys.MagicAffinityGreenmageGreenMagicList, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedMagicAffinityTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedMagicAffinityDescription";
            Definition.SetExtendedSpellList(AHPactTouchedSpellListBuilder.AHPactTouchedSpellList);
        }

        public static FeatureDefinitionMagicAffinity CreateAndAddToDB(string name, string guid)
            => new AHPactTouchedMagicAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionMagicAffinity AHPactTouchedSpellList = CreateAndAddToDB(AHPactTouchedSpellListName, AHPactTouchedSpellListGuid);
    }

    internal class AHPactTouchedBuildAutoPreparedSpellsBuilder : BaseDefinitionBuilder<FeatureDefinitionAutoPreparedSpells>
    {
        const string AHPactTouchedBuildAutoPreparedSpellsName = "AHPactTouchedBuildAutoPreparedSpells";

        protected AHPactTouchedBuildAutoPreparedSpellsBuilder(CharacterClassDefinition characterClass, string name, string guid) : base(DatabaseHelper.FeatureDefinitionAutoPreparedSpellss.AutoPreparedSpellsDomainBattle, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedBuildAutoPreparedSpellsTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedBuildAutoPreparedSpellsDescription";
            Definition.SetSpellcastingClass(characterClass);
            Definition.AutoPreparedSpellsGroups.Clear();
            Definition.AutoPreparedSpellsGroups.Add(new FeatureDefinitionAutoPreparedSpells.AutoPreparedSpellsGroup() { SpellsList = new List<SpellDefinition> { AHPactMarkSpellBuilder.AHPactMarkSpell, AHEldritchBlastSpellBuilder.AHEldritchBlastSpell, DatabaseHelper.SpellDefinitions.Shatter }, ClassLevel = 0 });
        }

        public static FeatureDefinitionAutoPreparedSpells CreateAndAddToDB(CharacterClassDefinition characterClass)
            => new AHPactTouchedBuildAutoPreparedSpellsBuilder(characterClass, AHPactTouchedBuildAutoPreparedSpellsName+characterClass.Name, GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedBuildAutoPreparedSpellsName+characterClass.Name).ToString()).AddToDB();

        public static FeatureDefinitionAutoPreparedSpells GetOrAdd(CharacterClassDefinition characterClass)
        {
            var db = DatabaseRepository.GetDatabase<FeatureDefinitionAutoPreparedSpells>();
            return db.TryGetElement(AHPactTouchedBuildAutoPreparedSpellsName + characterClass.Name, GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedBuildAutoPreparedSpellsName + characterClass.Name).ToString()) ?? CreateAndAddToDB(characterClass);
        }
    }

    internal class AHPactTouchedBonusCantripsBuilder : BaseDefinitionBuilder<FeatureDefinitionBonusCantrips>
    {
        const string AHPactTouchedBonusCantripsName = "AHPactTouchedBonusCantrips";
        private static readonly string AHPactTouchedBonusCantripsGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedBonusCantripsName).ToString();

        protected AHPactTouchedBonusCantripsBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionBonusCantripss.BonusCantripsDomainSun, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHPactTouchedBonusCantripsTitle";
            Definition.GuiPresentation.Description = "Feat/&AHPactTouchedBonusCantripsDescription";
            Definition.BonusCantrips.Clear();
            Definition.BonusCantrips.Add(AHEldritchBlastSpellBuilder.AHEldritchBlastSpell);
        }

        public static FeatureDefinitionBonusCantrips CreateAndAddToDB(string name, string guid)
            => new AHPactTouchedBonusCantripsBuilder(name, guid).AddToDB();

        public static FeatureDefinitionBonusCantrips AHPactTouchCantrips = CreateAndAddToDB(AHPactTouchedBonusCantripsName, AHPactTouchedBonusCantripsGuid);
    }

    public static class AHWizardSubclassPactTouched
    {
        public static Guid AHWizardSubclassPactTouchedGuid = new Guid("9ac9c013-eeab-4960-9548-ddf713093032");

        const string AHWizardSubclassPactTouchedName = "AHWizardSubclassPactTouched";
        private static readonly string AHWizardSubclassPactTouchedNameGuid = GuidHelper.Create(AHWizardSubclassPactTouched.AHWizardSubclassPactTouchedGuid, AHWizardSubclassPactTouchedName).ToString();

        public static CharacterSubclassDefinition Build()
        {
            var subclassGuiPresentation = new GuiPresentationBuilder(
                    "Subclass/&AHWizardSubclassPactTouchedDescription",
                    "Subclass/&AHWizardSubclassPactTouchedTitle")
                    .SetSpriteReference(DatabaseHelper.CharacterSubclassDefinitions.RoguishDarkweaver.GuiPresentation.SpriteReference)
                    .Build();

            CharacterSubclassDefinition definition = new CharacterSubclassDefinitionBuilder(AHWizardSubclassPactTouchedName, AHWizardSubclassPactTouchedNameGuid)
                    .SetGuiPresentation(subclassGuiPresentation)
                    .AddFeatureAtLevel(AHPactTouchedSubclassAgonizingBlassBonusCantripBuilder.AHAgonizingBlastBonusCantrip, 2)
                    .AddFeatureAtLevel(AHPactTouchedSubclassBuildAutoPreparedSpellsBuilder.GetOrAdd(DatabaseHelper.CharacterClassDefinitions.Wizard), 2)
                    //.AddFeatureAtLevel(AHPactMarkedFeatPowerBuilder.AHPactMarkedPower, 2)
                    .AddFeatureAtLevel(AHPactTouchedSummonPactWeaponPowerBuilder.SummonPactWeaponPower, 6)
                    .AddFeatureAtLevel(AHPactTouchedSoulTakerPowerBuilder.SoulTakerPower, 10)
                    .AddToDB();

            return definition;
        }
        internal class AHPactTouchedSubclassBuildAutoPreparedSpellsBuilder : BaseDefinitionBuilder<FeatureDefinitionAutoPreparedSpells>
        {
            const string AHPactTouchedBuildAutoPreparedSpellsName = "AHPactTouchedSubclassBuildAutoPreparedSpells";

            protected AHPactTouchedSubclassBuildAutoPreparedSpellsBuilder(CharacterClassDefinition characterClass, string name, string guid) : base(DatabaseHelper.FeatureDefinitionAutoPreparedSpellss.AutoPreparedSpellsDomainBattle, name, guid)
            {
                Definition.GuiPresentation.Title = "Feature/&AHPactTouchedBuildAutoPreparedSpellsTitle";
                Definition.GuiPresentation.Description = "Feature/&AHPactTouchedBuildAutoPreparedSpellsDescription";
                Definition.SetSpellcastingClass(characterClass);
                Definition.AutoPreparedSpellsGroups.Clear();
                Definition.AutoPreparedSpellsGroups.Add(new FeatureDefinitionAutoPreparedSpells.AutoPreparedSpellsGroup() { SpellsList = new List<SpellDefinition> 
                    { AHAgonizingBlastSpellBuilder.AHAgonizingBlastSpell, AHPactMarkSpellBuilder.AHPactMarkSpell, DatabaseHelper.SpellDefinitions.Blindness, DatabaseHelper.SpellDefinitions.Fear, DatabaseHelper.SpellDefinitions.BlackTentacles, DatabaseHelper.SpellDefinitions.MindTwist}, ClassLevel = 0 });
            }

            public static FeatureDefinitionAutoPreparedSpells CreateAndAddToDB(CharacterClassDefinition characterClass)
                => new AHPactTouchedSubclassBuildAutoPreparedSpellsBuilder(characterClass, AHPactTouchedBuildAutoPreparedSpellsName + characterClass.Name, GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedBuildAutoPreparedSpellsName + characterClass.Name).ToString()).AddToDB();

            public static FeatureDefinitionAutoPreparedSpells GetOrAdd(CharacterClassDefinition characterClass)
            {
                var db = DatabaseRepository.GetDatabase<FeatureDefinitionAutoPreparedSpells>();
                return db.TryGetElement(AHPactTouchedBuildAutoPreparedSpellsName + characterClass.Name, GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedBuildAutoPreparedSpellsName + characterClass.Name).ToString()) ?? CreateAndAddToDB(characterClass);
            }
        }

        internal class AHPactTouchedSubclassAgonizingBlassBonusCantripBuilder : BaseDefinitionBuilder<FeatureDefinitionBonusCantrips>
        {
            const string AHPactTouchedBonusCantripsName = "AHPactTouchedSubclassAgonizingBlassBonusCantrip";
            private static readonly string AHPactTouchedBonusCantripsGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedBonusCantripsName).ToString();

            protected AHPactTouchedSubclassAgonizingBlassBonusCantripBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionBonusCantripss.BonusCantripsDomainSun, name, guid)
            {
                Definition.GuiPresentation.Title = "Feature/&AHPactTouchedSubclassAgonizingBlassBonusCantripTitle";
                Definition.GuiPresentation.Description = "Feature/&AHPactTouchedSubclassAgonizingBlassBonusCantripDescription";
                Definition.BonusCantrips.Clear();
                Definition.BonusCantrips.Add(AHAgonizingBlastSpellBuilder.AHAgonizingBlastSpell);
            }

            public static FeatureDefinitionBonusCantrips CreateAndAddToDB(string name, string guid)
                => new AHPactTouchedSubclassAgonizingBlassBonusCantripBuilder(name, guid).AddToDB();

            public static FeatureDefinitionBonusCantrips AHAgonizingBlastBonusCantrip = CreateAndAddToDB(AHPactTouchedBonusCantripsName, AHPactTouchedBonusCantripsGuid);
        }

        internal class AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilder : BaseDefinitionBuilder<FeatureDefinitionAdditionalDamage>
        {
            const string AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilderName = "AHPactTouchSubclassPactSoulTakerAdditionalDamage";
            private static readonly string AHPactTouchSubclassPactSoulTakerAdditionalDamageGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilderName).ToString();

            protected AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalDamages.AdditionalDamageHuntersMark, name, guid)
            {
                Definition.GuiPresentation.Title = "Feature/&AHPactTouchSubclassPactSoulTakerAdditionalDamageTitle";
                Definition.GuiPresentation.Description = "Feature/&AHPactTouchSubclassPactSoulTakerAdditionalDamageDescription";
                Definition.SetAttackModeOnly(false);
                Definition.SetNotificationTag("PactSoulTaker");
                Definition.SetTriggerCondition(RuleDefinitions.AdditionalDamageTriggerCondition.TargetIsWounded);
                Definition.SetDamageDieType(RuleDefinitions.DieType.D8);
            }

            public static FeatureDefinitionAdditionalDamage CreateAndAddToDB(string name, string guid)
                => new AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilder(name, guid).AddToDB();

            public static FeatureDefinitionAdditionalDamage AHPactTouchSubclassPactSoulTakerAdditionalDamage = CreateAndAddToDB(AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilderName, AHPactTouchSubclassPactSoulTakerAdditionalDamageGuid);
        }

        internal class AHPactTouchedSoulTakerConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
        {
            const string AHPactTouchedSoulTakerConditionName = "AHPactTouchedSoulTakerCondition";
            private static readonly string AHPactTouchedSoulTakerConditionGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedSoulTakerConditionName).ToString();

            protected AHPactTouchedSoulTakerConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHeraldOfBattle, name, guid)
            {
                Definition.GuiPresentation.Title = "Feature/&AHPactTouchedSoulTakerConditionTitle";
                Definition.GuiPresentation.Description = "Feature/&AHPactTouchedSoulTakerConditionDescription";
                Definition.Features.Clear();
                Definition.Features.Add(AHPactTouchSubclassPactSoulTakerAdditionalDamageBuilder.AHPactTouchSubclassPactSoulTakerAdditionalDamage);
                Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
                Definition.SetDurationParameter(1);
            }

            public static ConditionDefinition CreateAndAddToDB(string name, string guid)
                => new AHPactTouchedSoulTakerConditionBuilder(name, guid).AddToDB();

            public static ConditionDefinition SoulTakerCondition = CreateAndAddToDB(AHPactTouchedSoulTakerConditionName, AHPactTouchedSoulTakerConditionGuid);
        }

        internal class AHPactTouchedSoulTakerPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
        {
            const string AHPactTouchedSoulTakerPowerName = "AHPactTouchedSoulTakerPower";
            private static readonly string AHPactTouchedSoulTakerPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedSoulTakerPowerName).ToString();

            protected AHPactTouchedSoulTakerPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainElementalFireBurst, name, guid)
            {
                Definition.GuiPresentation.Title = "Feature/&AHPactTouchedSoulTakerPowerTitle";
                Definition.GuiPresentation.Description = "Feature/&AHPactTouchedSoulTakerPowerDescription";
                Definition.SetShortTitleOverride("Feature/&AHPactTouchedSoulTakerPowerTitle");

                Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
                Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
                Definition.SetCostPerUse(1);
                Definition.SetFixedUsesPerRecharge(1);

                //Create the power attack effect
                EffectForm soulTakerConditionEffectForm = new EffectForm();
                soulTakerConditionEffectForm.ConditionForm = new ConditionForm();
                soulTakerConditionEffectForm.FormType = EffectForm.EffectFormType.Condition;
                soulTakerConditionEffectForm.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
                soulTakerConditionEffectForm.ConditionForm.ConditionDefinition = AHPactTouchedSoulTakerConditionBuilder.SoulTakerCondition;

                //Add to our new effect
                EffectDescription newEffectDescription = new EffectDescription();
                newEffectDescription.Copy(Definition.EffectDescription);
                newEffectDescription.EffectForms.Clear();
                newEffectDescription.EffectForms.Add(soulTakerConditionEffectForm);
                newEffectDescription.HasSavingThrow = false;
                newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
                newEffectDescription.DurationParameter = 1;
                newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
                newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
                newEffectDescription.SetCanBePlacedOnCharacter(true);

                Definition.SetEffectDescription(newEffectDescription);
            }

            public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
                => new AHPactTouchedSoulTakerPowerBuilder(name, guid).AddToDB();

            public static FeatureDefinitionPower SoulTakerPower
                = CreateAndAddToDB(AHPactTouchedSoulTakerPowerName, AHPactTouchedSoulTakerPowerGuid);
        }


        internal class AHPactTouchedSummonPactWeaponPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
        {
            const string AHPactTouchedSummonPactWeaponPowerName = "AHPactTouchedSummonPactWeaponPower";
            private static readonly string AHPactTouchedSummonPactWeaponPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHPactTouchedSummonPactWeaponPowerName).ToString();

            protected AHPactTouchedSummonPactWeaponPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerTraditionShockArcanistArcaneFury, name, guid)
            {
                Definition.GuiPresentation.Title = "Feature/&AHPactTouchedSummonPactWeaponPowerTitle";
                Definition.GuiPresentation.Description = "Feature/&AHPactTouchedSummonPactWeaponPowerDescription";
                Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.SpiritualWeapon.GuiPresentation.SpriteReference);
                Definition.SetEffectDescription(DatabaseHelper.SpellDefinitions.SpiritualWeapon.EffectDescription);
                Definition.SetRechargeRate(RuleDefinitions.RechargeRate.ShortRest);
                Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);
            }

            public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
                => new AHPactTouchedSummonPactWeaponPowerBuilder(name, guid).AddToDB();

            public static FeatureDefinitionPower SummonPactWeaponPower = CreateAndAddToDB(AHPactTouchedSummonPactWeaponPowerName, AHPactTouchedSummonPactWeaponPowerGuid);
        }

    }
}
