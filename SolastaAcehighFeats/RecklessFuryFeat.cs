using SolastaModApi;
using SolastaModApi.Extensions;
using static FeatureDefinitionSavingThrowAffinity;

namespace SolastaAcehighFeats
{
    internal class RecklessFuryFeatBuilder : BaseDefinitionBuilder<FeatDefinition>
    {
        const string RecklessFuryFeatName = "RecklessFuryFeat";
        const string RecklessFuryFeatNameGuid = "78c5fd76-e25b-499d-896f-3eaf84c711d8";

        protected RecklessFuryFeatBuilder(string name, string guid) : base(DatabaseHelper.FeatDefinitions.FollowUpStrike, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&RecklessFuryFeatTitle";
            Definition.GuiPresentation.Description = "Feat/&RecklessFuryFeatDescription";

            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionPowers.PowerReckless);
            Definition.Features.Add(RagePowerBuilder.RagePower);
            Definition.SetMinimalAbilityScorePrerequisite(false);
        }

        public static FeatDefinition CreateAndAddToDB(string name, string guid)
            => new RecklessFuryFeatBuilder(name, guid).AddToDB();

        public static FeatDefinition RecklessFuryFeat
            => CreateAndAddToDB(RecklessFuryFeatName, RecklessFuryFeatNameGuid);

        public static void AddToFeatList()
        {
            var RecklessFuryFeat = RecklessFuryFeatBuilder.RecklessFuryFeat;//Instantiating it adds to the DB
        }
    }

    internal class RagePowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RagePowerName = "AHRagePower";
        const string RagePowerNameGuid = "a46c1722-7825-4a81-bca1-392b51cd7d97";

        protected RagePowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainElementalFireBurst, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&RagePowerTitle";
            Definition.GuiPresentation.Description = "Feature/&RagePowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetShortTitleOverride("Feature/&RagePowerTitle");

            //Create the power attack effect
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = RageFeatConditionBuilder.RageFeatCondition;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new RagePowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower RagePower
            => CreateAndAddToDB(RagePowerName, RagePowerNameGuid);
    }

    internal class RageFeatConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string RageFeatConditionName = "AHRageFeatCondition";
        const string RageFeatConditionNameGuid = "2f34fb85-6a5d-4a4e-871b-026872bc24b8";

        protected RageFeatConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHeraldOfBattle, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&RageFeatConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&RageFeatConditionDescription";

            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(RageStrengthSavingThrowAffinityBuilder.RageStrengthSavingThrowAffinity);
            Definition.Features.Add(RageDamageBonusAttackModifierBuilder.RageDamageBonusAttackModifier);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);


            Definition.SetDurationType(RuleDefinitions.DurationType.Turn);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new RageFeatConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition RageFeatCondition
            => CreateAndAddToDB(RageFeatConditionName, RageFeatConditionNameGuid);
    }

    internal class RageStrengthSavingThrowAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionSavingThrowAffinity>
    {
        const string RageStrengthSavingThrowAffinityName = "AHRageStrengthSavingThrowAffinity";
        const string RageStrengthSavingThrowAffinityNameGuid = "17d26173-7353-4087-a295-96e1ec2e6cd4";

        protected RageStrengthSavingThrowAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionSavingThrowAffinitys.SavingThrowAffinityCreedOfArun, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&RageStrengthSavingThrowAffinityTitle";
            Definition.GuiPresentation.Description = "Feature/&RageStrengthSavingThrowAffinityDescription";

            Definition.AffinityGroups.Clear();
            var strengthSaveAffinityGroup = new SavingThrowAffinityGroup();
            strengthSaveAffinityGroup.affinity = RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
            strengthSaveAffinityGroup.abilityScoreName = "Strength";
        }

        public static FeatureDefinitionSavingThrowAffinity CreateAndAddToDB(string name, string guid)
            => new RageStrengthSavingThrowAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionSavingThrowAffinity RageStrengthSavingThrowAffinity
            => CreateAndAddToDB(RageStrengthSavingThrowAffinityName, RageStrengthSavingThrowAffinityNameGuid);
    }

    internal class RageDamageBonusAttackModifierBuilder : BaseDefinitionBuilder<FeatureDefinitionAttackModifier>
    {
        const string RageDamageBonusAttackModifierName = "AHRageDamageBonusAttackModifier";
        const string RageDamageBonusAttackModifierNameGuid = "7bc1a47e-9519-4a37-a89a-10bcfa83e48a";

        protected RageDamageBonusAttackModifierBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAttackModifiers.AttackModifierFightingStyleArchery, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&RageDamageBonusAttackModifierTitle";
            Definition.GuiPresentation.Description = "Feature/&RageDamageBonusAttackModifierDescription";

            Definition.SetAttackRollModifier(0);
            Definition.SetDamageRollModifier(2);//Could find a way to up this at level 9 to match barb but that seems like a lot of work right now :)
        }

        public static FeatureDefinitionAttackModifier CreateAndAddToDB(string name, string guid)
            => new RageDamageBonusAttackModifierBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAttackModifier RageDamageBonusAttackModifier
            => CreateAndAddToDB(RageDamageBonusAttackModifierName, RageDamageBonusAttackModifierNameGuid);
    }
}
