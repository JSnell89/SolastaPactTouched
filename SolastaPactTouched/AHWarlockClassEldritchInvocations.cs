using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolastaPactTouched
{
    internal class AHWarlockEldritchInvocationSetBuilderLevel2 : BaseDefinitionBuilder<FeatureDefinitionFeatureSet>
    {
        const string AHWarlockEldritchInvocationSetLevel2Name = "AHWarlockEldritchInvocationSetLevel2";
        private static readonly string AHWarlockEldritchInvocationSetLevel2Guid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationSetLevel2Name).ToString();

        protected AHWarlockEldritchInvocationSetBuilderLevel2(string name, string guid) : base(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetHunterHuntersPrey, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationSetLevel2Title";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationSetLevel2Description";

            Definition.FeatureSet.Clear();
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationArmorOfShadowsPowerBuilder.AHWarlockEldritchInvocationArmorOfShadowsPower);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationEldritchSightPowerBuilder.AHWarlockEldritchInvocationEldritchSightPower);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationFiendishVigorPowerBuilder.AHWarlockEldritchInvocationFiendishVigorPower);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationDevilsSightPowerBuilder.AHWarlockEldritchInvocationDevilsSightPower);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationBeguilingInfluenceBuilder.AHWarlockEldritchInvocationBeguilingInfluence);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationRepellingBlastBuilder.AHWarlockEldritchInvocationRepellingBlast);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationAgnoizingBlastBuilder.AHWarlockEldritchInvocationAgnoizingBlast);
            Definition.SetUniqueChoices(false); //Seems to be a bug with unique choices where it makes the list smaller but then selects from the wrong index from the master list, using the index of the item in the smaller list.  My tests on higher levels would have RepellingBlast chosen from the master list when choosing ThirstingBlade from the smaller unique list as an example.
        }

        public static FeatureDefinitionFeatureSet CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationSetBuilderLevel2(name, guid).AddToDB();

        public static FeatureDefinitionFeatureSet AHWarlockEldritchInvocationSetLevel2 = CreateAndAddToDB(AHWarlockEldritchInvocationSetLevel2Name, AHWarlockEldritchInvocationSetLevel2Guid);
    }

    internal class AHWarlockEldritchInvocationSetBuilderLevel5 : BaseDefinitionBuilder<FeatureDefinitionFeatureSet>
    {
        const string AHWarlockEldritchInvocationSetLevel5Name = "AHWarlockEldritchInvocationSetLevel5";
        private static readonly string AHWarlockEldritchInvocationSetLevel5Guid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationSetLevel5Name).ToString();

        protected AHWarlockEldritchInvocationSetBuilderLevel5(string name, string guid) : base(AHWarlockEldritchInvocationSetBuilderLevel2.AHWarlockEldritchInvocationSetLevel2, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationSetLevel5Title";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationSetLevel5Description";

            Definition.FeatureSet.Add(AHWarlockEldritchInvocationThirstingBladeBuilder.AHWarlockEldritchInvocationThirstingBlade);
            Definition.SetUniqueChoices(false);
        }

        public static FeatureDefinitionFeatureSet CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationSetBuilderLevel5(name, guid).AddToDB();

        public static FeatureDefinitionFeatureSet AHWarlockEldritchInvocationSetLevel5 = CreateAndAddToDB(AHWarlockEldritchInvocationSetLevel5Name, AHWarlockEldritchInvocationSetLevel5Guid);
    }

    internal class AHWarlockEldritchInvocationSetBuilderLevel7 : BaseDefinitionBuilder<FeatureDefinitionFeatureSet>
    {
        const string AHWarlockEldritchInvocationSetLevel7Name = "AHWarlockEldritchInvocationSetLevel7";
        private static readonly string AHWarlockEldritchInvocationSetLevel7Guid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationSetLevel7Name).ToString();

        protected AHWarlockEldritchInvocationSetBuilderLevel7(string name, string guid) : base(AHWarlockEldritchInvocationSetBuilderLevel5.AHWarlockEldritchInvocationSetLevel5, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationSetLevel7Title";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationSetLevel7Description";

            Definition.SetUniqueChoices(false);
        }

        public static FeatureDefinitionFeatureSet CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationSetBuilderLevel7(name, guid).AddToDB();

        public static FeatureDefinitionFeatureSet AHWarlockEldritchInvocationSetLevel7 = CreateAndAddToDB(AHWarlockEldritchInvocationSetLevel7Name, AHWarlockEldritchInvocationSetLevel7Guid);
    }

    internal class AHWarlockEldritchInvocationSetBuilderLevel9 : BaseDefinitionBuilder<FeatureDefinitionFeatureSet>
    {
        const string AHWarlockEldritchInvocationSetLevel9Name = "AHWarlockEldritchInvocationSetLevel9";
        private static readonly string AHWarlockEldritchInvocationSetLevel9Guid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationSetLevel9Name).ToString();

        protected AHWarlockEldritchInvocationSetBuilderLevel9(string name, string guid) : base(AHWarlockEldritchInvocationSetBuilderLevel5.AHWarlockEldritchInvocationSetLevel5, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationSetLevel9Title";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationSetLevel9Description";

            Definition.FeatureSet.Add(AHWarlockEldritchInvocationAscendentStepPowerBuilder.AHWarlockEldritchInvocationAscendentStepPower);
            Definition.FeatureSet.Add(AHWarlockEldritchInvocationOtherwordlyLeapPowerBuilder.AHWarlockEldritchInvocationOtherwordlyLeapPower);

            Definition.SetUniqueChoices(false);
        }

        public static FeatureDefinitionFeatureSet CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationSetBuilderLevel9(name, guid).AddToDB();

        public static FeatureDefinitionFeatureSet AHWarlockEldritchInvocationSetLevel9 = CreateAndAddToDB(AHWarlockEldritchInvocationSetLevel9Name, AHWarlockEldritchInvocationSetLevel9Guid);
    }

    internal class AHWarlockEldritchInvocationArmorOfShadowsPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationArmorOfShadowsPowerName = "AHWarlockEldritchInvocationArmorOfShadowsPower";
        private static readonly string AHWarlockEldritchInvocationArmorOfShadowsPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationArmorOfShadowsPowerName).ToString();

        protected AHWarlockEldritchInvocationArmorOfShadowsPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationArmorOfShadowsPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationArmorOfShadowsPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.MageArmor.GuiPresentation.SpriteReference);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            Definition.EffectDescription.Copy(DatabaseHelper.SpellDefinitions.MageArmor.EffectDescription);
            Definition.EffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationArmorOfShadowsPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationArmorOfShadowsPower = CreateAndAddToDB(AHWarlockEldritchInvocationArmorOfShadowsPowerName, AHWarlockEldritchInvocationArmorOfShadowsPowerGuid);
    }

    internal class AHWarlockEldritchInvocationEldritchSightPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationEldritchSightPowerName = "AHWarlockEldritchInvocationEldritchSightPower";
        private static readonly string AHWarlockEldritchInvocationEldritchSightPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationEldritchSightPowerName).ToString();

        protected AHWarlockEldritchInvocationEldritchSightPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationEldritchSightPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationEldritchSightPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.DetectMagic.GuiPresentation.SpriteReference);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            Definition.EffectDescription.Copy(DatabaseHelper.SpellDefinitions.DetectMagic.EffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationEldritchSightPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationEldritchSightPower = CreateAndAddToDB(AHWarlockEldritchInvocationEldritchSightPowerName, AHWarlockEldritchInvocationEldritchSightPowerGuid);
    }

    internal class AHWarlockEldritchInvocationFiendishVigorPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationFiendishVigorPowerName = "AHWarlockEldritchInvocationFiendishVigorPower";
        private static readonly string AHWarlockEldritchInvocationFiendishVigorPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationFiendishVigorPowerName).ToString();

        protected AHWarlockEldritchInvocationFiendishVigorPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationFiendishVigorPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationFiendishVigorPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.FalseLife.GuiPresentation.SpriteReference);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            Definition.EffectDescription.Copy(DatabaseHelper.SpellDefinitions.FalseLife.EffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationFiendishVigorPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationFiendishVigorPower = CreateAndAddToDB(AHWarlockEldritchInvocationFiendishVigorPowerName, AHWarlockEldritchInvocationFiendishVigorPowerGuid);
    }

    internal class AHWarlockEldritchInvocationAscendentStepPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationAscendentStepPowerName = "AHWarlockEldritchInvocationAscendentStepPower";
        private static readonly string AHWarlockEldritchInvocationAscendentStepPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationAscendentStepPowerName).ToString();

        protected AHWarlockEldritchInvocationAscendentStepPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationAscendentStepPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationAscendentStepPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.Levitate.GuiPresentation.SpriteReference);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            Definition.EffectDescription.Copy(DatabaseHelper.SpellDefinitions.Levitate.EffectDescription);
            Definition.EffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationAscendentStepPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationAscendentStepPower = CreateAndAddToDB(AHWarlockEldritchInvocationAscendentStepPowerName, AHWarlockEldritchInvocationAscendentStepPowerGuid);
    }

    internal class AHWarlockEldritchInvocationOtherwordlyLeapPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationOtherwordlyLeapPowerName = "AHWarlockEldritchInvocationOtherwordlyLeapPower";
        private static readonly string AHWarlockEldritchInvocationOtherwordlyLeapPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationOtherwordlyLeapPowerName).ToString();

        protected AHWarlockEldritchInvocationOtherwordlyLeapPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationOtherwordlyLeapPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationOtherwordlyLeapPowerDescription";
            Definition.GuiPresentation.SetSpriteReference(DatabaseHelper.SpellDefinitions.Jump.GuiPresentation.SpriteReference);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            Definition.EffectDescription.Copy(DatabaseHelper.SpellDefinitions.Jump.EffectDescription);
            Definition.EffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationOtherwordlyLeapPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationOtherwordlyLeapPower = CreateAndAddToDB(AHWarlockEldritchInvocationOtherwordlyLeapPowerName, AHWarlockEldritchInvocationOtherwordlyLeapPowerGuid);
    }

    //Unfortunately implementing the see through magic darkness is going to be very hard/impossible
    internal class AHWarlockEldritchInvocationDevilsSightPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionSense>
    {
        const string AHWarlockEldritchInvocationDevilsSightPowerName = "AHWarlockEldritchInvocationDevilsSightPower";
        private static readonly string AHWarlockEldritchInvocationDevilsSightPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationDevilsSightPowerName).ToString();

        protected AHWarlockEldritchInvocationDevilsSightPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionSenses.SenseDarkvision12, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationDevilsSightPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationDevilsSightPowerDescription";

            Definition.SetSenseRange(24);
            //Could potentially give blindsight as well to see through magical darkness but that makes this even more OP
        }

        public static FeatureDefinitionSense CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationDevilsSightPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionSense AHWarlockEldritchInvocationDevilsSightPower = CreateAndAddToDB(AHWarlockEldritchInvocationDevilsSightPowerName, AHWarlockEldritchInvocationDevilsSightPowerGuid);
    }

    internal class AHWarlockEldritchInvocationBeguilingInfluenceBuilder : BaseDefinitionBuilder<FeatureDefinitionProficiency>
    {
        const string AHWarlockEldritchInvocationBeguilingInfluenceName = "AHWarlockEldritchInvocationBeguilingInfluence";
        private static readonly string AHWarlockEldritchInvocationBeguilingInfluenceGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationBeguilingInfluenceName).ToString();

        protected AHWarlockEldritchInvocationBeguilingInfluenceBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencySpySkills, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationBeguilingInfluenceTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationBeguilingInfluenceDescription";

            Definition.SetProficiencyType(RuleDefinitions.ProficiencyType.Skill);
            Definition.Proficiencies.Clear();
            Definition.Proficiencies.AddRange(new string[] { "Deception", "Persuasion" });
        }

        public static FeatureDefinitionProficiency CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationBeguilingInfluenceBuilder(name, guid).AddToDB();

        public static FeatureDefinitionProficiency AHWarlockEldritchInvocationBeguilingInfluence = CreateAndAddToDB(AHWarlockEldritchInvocationBeguilingInfluenceName, AHWarlockEldritchInvocationBeguilingInfluenceGuid);
    }

    internal class AHWarlockEldritchInvocationAgnoizingBlastBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationAgnoizingBlastName = "AHWarlockEldritchInvocationAgnoizingBlast";
        private static readonly string AHWarlockEldritchInvocationAgnoizingBlastGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationAgnoizingBlastName).ToString();

        protected AHWarlockEldritchInvocationAgnoizingBlastBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationAgnoizingBlastTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationAgnoizingBlastDescription";

            //A do nothing power, currently the intention is you take Agonizing blast at level 1 and this invocation since there is no way to override a spell.
            Definition.SetCostPerUse(2);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationAgnoizingBlastBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationAgnoizingBlast = CreateAndAddToDB(AHWarlockEldritchInvocationAgnoizingBlastName, AHWarlockEldritchInvocationAgnoizingBlastGuid);
    }

    internal class AHWarlockEldritchInvocationRepellingBlastBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockEldritchInvocationRepellingBlastName = "AHWarlockEldritchInvocationRepellingBlast";
        private static readonly string AHWarlockEldritchInvocationRepellingBlastGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationRepellingBlastName).ToString();

        protected AHWarlockEldritchInvocationRepellingBlastBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationRepellingBlastTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationRepellingBlastDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);

            //Create the summon effect
            EffectForm motionEffect = new EffectForm();
            motionEffect.FormType = EffectForm.EffectFormType.Motion;
            var motionForm = new MotionForm();
            motionForm.SetDistance(2);
            motionEffect.SetMotionForm(motionForm);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(motionEffect);
            newEffectDescription.SetRangeType(RuleDefinitions.RangeType.Distance);
            newEffectDescription.SetRangeParameter(30);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Individuals);
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Enemy);
            newEffectDescription.SetTargetParameter(1);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationRepellingBlastBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockEldritchInvocationRepellingBlast = CreateAndAddToDB(AHWarlockEldritchInvocationRepellingBlastName, AHWarlockEldritchInvocationRepellingBlastGuid);
    }

    internal class AHWarlockEldritchInvocationThirstingBladeBuilder : BaseDefinitionBuilder<FeatureDefinitionAttributeModifier>
    {
        const string AHWarlockEldritchInvocationThirstingBladeName = "AHWarlockEldritchInvocationThirstingBlade";
        private static readonly string AHWarlockEldritchInvocationThirstingBladeGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockEldritchInvocationThirstingBladeName).ToString();

        protected AHWarlockEldritchInvocationThirstingBladeBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAttributeModifiers.AttributeModifierFighterExtraAttack, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockEldritchInvocationThirstingBladeTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockEldritchInvocationThirstingBladeDescription";

            Definition.SetModifiedAttribute("AttacksNumber");
            Definition.SetModifierType2(FeatureDefinitionAttributeModifier.AttributeModifierOperation.Additive);
            Definition.SetModifierValue(1);
            //Just use the extra attack from fighter
        }

        public static FeatureDefinitionAttributeModifier CreateAndAddToDB(string name, string guid)
            => new AHWarlockEldritchInvocationThirstingBladeBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAttributeModifier AHWarlockEldritchInvocationThirstingBlade = CreateAndAddToDB(AHWarlockEldritchInvocationThirstingBladeName, AHWarlockEldritchInvocationThirstingBladeGuid);
    }
}
