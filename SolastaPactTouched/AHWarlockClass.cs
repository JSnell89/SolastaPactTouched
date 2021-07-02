using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolastaPactTouched
{
    internal class AHWarlockClassBuilder : CharacterClassDefinitionBuilder
    {
        const string AHWarlockClassName = "AHWarlockClass";
        const string AHWarlockClassNameGuid = "307a723c-1f2b-41b4-a830-afb3eed194c0";
        const string AHWarlockClassSubclassesGuid = "51e32250-bf3c-4874-be12-bf27595c403d";

        protected AHWarlockClassBuilder(string name, string guid) : base(name, guid)
        {
            var wizard = DatabaseHelper.CharacterClassDefinitions.Wizard;
            Definition.GuiPresentation.Title = "Class/&AHWarlockClassTitle";
            Definition.GuiPresentation.Description = "Class/&AHWarlockClassDescription";
            Definition.GuiPresentation.SetSpriteReference(wizard.GuiPresentation.SpriteReference);

            Definition.SetClassAnimationId(AnimationDefinitions.ClassAnimationId.Wizard);
            Definition.SetClassPictogramReference(wizard.ClassPictogramReference);
            Definition.SetDefaultBattleDecisions(wizard.DefaultBattleDecisions);
            Definition.SetHitDice(RuleDefinitions.DieType.D8);
            Definition.SetIngredientGatheringOdds(wizard.IngredientGatheringOdds);
            Definition.SetRequiresDeity(true);

            Definition.AbilityScoresPriority.AddRange(new List<string>() { "Charisma", "Constitution", "Dexterity", "Wisdom", "Intelligence", "Strength" });
            Definition.FeatAutolearnPreference.AddRange(wizard.FeatAutolearnPreference);
            Definition.PersonalityFlagOccurences.AddRange(wizard.PersonalityFlagOccurences);
            Definition.SkillAutolearnPreference.AddRange(new string[] { "Arcana", "Deception", "History", "Intimidation", "Investigation", "Nature", "Religion" });
            Definition.ToolAutolearnPreference.AddRange(wizard.ToolAutolearnPreference);

            Definition.EquipmentRows.Clear();
            List<CharacterClassDefinition.HeroEquipmentOption> list = new List<CharacterClassDefinition.HeroEquipmentOption>();
            List<CharacterClassDefinition.HeroEquipmentOption> list2 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            list.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.LightCrossbow, EquipmentDefinitions.OptionWeapon, 1));
            list.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Bolt, EquipmentDefinitions.OptionWeapon, 20));
            list2.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Quarterstaff, EquipmentDefinitions.OptionWeaponSimpleChoice, 1));
            List<CharacterClassDefinition.HeroEquipmentOption> list3 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            List<CharacterClassDefinition.HeroEquipmentOption> list4 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            list3.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.ComponentPouch, EquipmentDefinitions.OptionGenericItem, 1));
            list4.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.ArcaneFocusOrb, EquipmentDefinitions.OptionArcaneFocusChoice, 1));
            List<CharacterClassDefinition.HeroEquipmentOption> list5 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            List<CharacterClassDefinition.HeroEquipmentOption> list6 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            list5.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.ScholarPack, EquipmentDefinitions.OptionStarterPack, 1));
            list6.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.DungeoneerPack, EquipmentDefinitions.OptionStarterPack, 1));
            this.AddEquipmentRow(list, list2);
            this.AddEquipmentRow(list3, list4);
            this.AddEquipmentRow(list5, list6);
            this.AddEquipmentRow(new List<CharacterClassDefinition.HeroEquipmentOption>
            {
                EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Leather, EquipmentDefinitions.OptionArmor, 1),
                EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Mace, EquipmentDefinitions.OptionWeaponSimpleChoice, 1),
                EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Dagger, EquipmentDefinitions.OptionWeapon, 2),
               // EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Spellbook, EquipmentDefinitions.OptionGenericItem, 1),
            });

            Definition.FeatureUnlocks.Clear();
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyPaladinSavingThrow, 1)); //Same saves as Paladin :)
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyRogueArmor, 1)); //Same armor as rogue :)
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyClericWeapon, 1)); //Same weapons as cleric :)
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockClassSkillPointPoolBuilder.AHWarlockClassSkillPointPool, 1)); //Custom skills
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockShortRestSpellFeatureBuilder.AHWarlockSpellCastFeature, 1));
            var subclassChoicesGuiPresentation = new GuiPresentation();
            subclassChoicesGuiPresentation.Title = "Subclass/&AHWarlockSubclassPactTitle";
            subclassChoicesGuiPresentation.Description = "Subclass/&AHWarlockSubclassPactDescription";
            //Cheat and use the Domain suffix
            WarlockFeatureDefinitionSubclassChoice = this.BuildSubclassChoice(1, "Domain", true, "SubclassChoiceWarlockSpecialistArchetypes", subclassChoicesGuiPresentation, AHWarlockClassSubclassesGuid);
            //Eldritch invocations at level 2, going to be a lot of work to implement them all
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockEldritchInvocationSetBuilderLevel2.AHWarlockEldritchInvocationSetLevel2, 2));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockEldritchInvocationSetBuilderLevel2.AHWarlockEldritchInvocationSetLevel2, 2));

            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockClassPactBoonSetBuilder.AHWarlockClassPactBoonSet, 3));
            //No longer needed with the patchers to change spell recharging
            //Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockSpendExcessSpellSlotPowerBuilder.AHWarlockSpendExcessSpellSlotPower, 3)); //Hopefully can be removed at some point.  Should spend the lowest level slot.
            

            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 4));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockEldritchInvocationSetBuilderLevel5.AHWarlockEldritchInvocationSetLevel5, 5));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockEldritchInvocationSetBuilderLevel7.AHWarlockEldritchInvocationSetLevel7, 7));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 8));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHWarlockEldritchInvocationSetBuilderLevel9.AHWarlockEldritchInvocationSetLevel9, 9));

            //Above level 10 features
            //Level 11 Relentless Rage
            //Level 12 Rage use increase
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 12));
            //Level 13 Brutal Critical (2 dice)
            //Level 14 Path feature
            //Level 15 Persistent Rage
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 16));
            //Level 16 Rage damage increase
            //Level 16 Brutal Critical (3 dice)
            //Level 17 Rage use increase
            //Level 18 Indomitable Might
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 19));
            //Level 20 	Primal Champion
            //Level 20 Unlimited Rages

        }

        public static void BuildAndAddClassToDB()
        {
            var warlockClass = new AHWarlockClassBuilder(AHWarlockClassName, AHWarlockClassNameGuid).AddToDB();
            //Might need to add subclasses after the class is in the DB?
            CharacterSubclassDefinition fiendPactSubclass = AHWarlockSubclassFiendPact.Build();
            WarlockFeatureDefinitionSubclassChoice.Subclasses.Add(fiendPactSubclass.Name);
            CharacterSubclassDefinition soulBladePactSubclass = AHWarlockSubclassSoulBladePact.Build();
            WarlockFeatureDefinitionSubclassChoice.Subclasses.Add(soulBladePactSubclass.Name);

            var deities = DatabaseRepository.GetDatabase<DeityDefinition>().GetAllElements();
            foreach (var deity in deities)
            {
                deity.Subclasses.Add(fiendPactSubclass.Name);
                deity.Subclasses.Add(soulBladePactSubclass.Name);
            }
            
        }

        private static FeatureDefinitionSubclassChoice WarlockFeatureDefinitionSubclassChoice;
    }

    internal class AHWarlockClassSkillPointPoolBuilder : BaseDefinitionBuilder<FeatureDefinitionPointPool>
    {
        const string AHWarlockClassSkillPointPoolName = "AHWarlockClassSkillPointPool";
        private static readonly string AHWarlockClassSkillPointPoolGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockClassSkillPointPoolName).ToString();

        protected AHWarlockClassSkillPointPoolBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPointPools.PointPoolFighterSkillPoints, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockClassSkillPointPoolTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockClassSkillPointPoolDescription";

            Definition.SetPoolAmount(2);
            Definition.SetPoolType(HeroDefinitions.PointsPoolType.Skill);
            Definition.RestrictedChoices.Clear();
            Definition.RestrictedChoices.AddRange(new string[] { "Arcana", "Deception", "History", "Intimidation", "Investigation", "Nature", "Religion" });
        }

        public static FeatureDefinitionPointPool CreateAndAddToDB(string name, string guid)
            => new AHWarlockClassSkillPointPoolBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPointPool AHWarlockClassSkillPointPool = CreateAndAddToDB(AHWarlockClassSkillPointPoolName, AHWarlockClassSkillPointPoolGuid);
    }

    internal class AHWarlockShortRestSpellFeatureBuilder : CastSpellBuilder
    {
        const string AHWarlockShortRestSpellFeatureName = "AHWarlockShortRestSpellFeature";
        private static readonly string AHWarlockShortRestSpellFeatureGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockShortRestSpellFeatureName).ToString();

        protected AHWarlockShortRestSpellFeatureBuilder(string name, string guid) : base(name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHWarlockShortRestSpellFeatureTitle";
            Definition.GuiPresentation.Description = "Feat/&AHWarlockShortRestSpellFeatureDescription";
            SetKnownCantrips(new List<int>() {
                2, //1
                2,
                2,
                3,
                3, //5
                3,
                3,
                3,
                3,
                4, //10
                4,
                4,
                4,
                4,
                4, //15
                4,
                4,
                4,
                4,
                4, //20
            });
            Definition.KnownSpells.Clear();

            //Proper spells known but Solasta doesn't take into account the fact that you can retrain a prior known spell each level up
            //Definition.KnownSpells.AddRange(new List<int>() {
            //    2, //1
            //    3,
            //    4,
            //    5,
            //    6, //5
            //    7,
            //    8,
            //    9,
            //    10,
            //    10, //10
            //    11,
            //    11,
            //    12,
            //    12,
            //    13, //15
            //    13,
            //    14,
            //    14,
            //    15,
            //    15, //20

            //});

            //Adjust the spells known table to allow 2 new spells when you gain a new spell slot level until level 10 to help account for Warlocks usually being able to swap spells known
            Definition.KnownSpells.AddRange(new List<int>() {
                2, //1
                3,
                5,
                6,
                8, //5
                9,
                11,
                12,
                14,
                14, //10
                15,
                15,
                16,
                17,
                18, //15
                18,
                19,
                19,
                20,
                20, //20
            });

            //Unfortunately lots of features break if there are no spell slots available.  So even though you have spell slots at higher levels they don't get recognized if you don't have lower level slots
            SetSlotsPerLevel(new List<FeatureDefinitionCastSpell.SlotsByLevelDuplet>()
            {
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 1, Slots = new List<int> () { 1, 0, 0, 0, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 2, Slots = new List<int> () { 2, 0, 0, 0, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 3, Slots = new List<int> () { 0, 2, 0, 0, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 4, Slots = new List<int> () { 0, 2, 0, 0, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 5, Slots = new List<int> () { 0, 0, 2, 0, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 6, Slots = new List<int> () { 0, 0, 2, 0, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 7, Slots = new List<int> () { 0, 0, 0, 2, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 8, Slots = new List<int> () { 0, 0, 0, 2, 0 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 9, Slots = new List<int> () { 0, 0, 0, 0, 2 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 10, Slots = new List<int> () { 0, 0, 0, 0, 2 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 11, Slots = new List<int> () { 0, 0, 0, 0, 3 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 12, Slots = new List<int> () { 0, 0, 0, 0, 3 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 13, Slots = new List<int> () { 0, 0, 0, 0, 3 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 14, Slots = new List<int> () { 0, 0, 0, 0, 3 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 15, Slots = new List<int> () { 0, 0, 0, 0, 3 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 16, Slots = new List<int> () { 0, 0, 0, 0, 3 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 17, Slots = new List<int> () { 0, 0, 0, 0, 4 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 18, Slots = new List<int> () { 0, 0, 0, 0, 4 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 19, Slots = new List<int> () { 0, 0, 0, 0, 4 } },
                new FeatureDefinitionCastSpell.SlotsByLevelDuplet() { Level = 20, Slots = new List<int> () { 0, 0, 0, 0, 4 } },
            });
            SetSpellCastingOrigin(FeatureDefinitionCastSpell.CastingOrigin.Class);
            SetSpellCastingAbility("Charisma");
            SetSlotsRecharge(RuleDefinitions.RechargeRate.ShortRest);
            SetSpellKnowledge(RuleDefinitions.SpellKnowledge.Selection); //Incorrect, but maybe this will work properly?
            SetSpellList(AHWarlockSpellListBuilder.AHWarlockSpellList);
            SetSpellReadyness(RuleDefinitions.SpellReadyness.AllKnown);
        }

        public static FeatureDefinitionCastSpell CreateAndAddToDB(string name, string guid)
            => new AHWarlockShortRestSpellFeatureBuilder(name, guid).AddToDB();

        public static FeatureDefinitionCastSpell AHWarlockSpellCastFeature = CreateAndAddToDB(AHWarlockShortRestSpellFeatureName, AHWarlockShortRestSpellFeatureGuid);
    }

    internal class AHWarlockSpellListBuilder : BaseDefinitionBuilder<SpellListDefinition>
    {
        const string AHWarlockSpellListName = "AHWarlockSpellList";
        private static readonly string AHWarlockSpellListGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockSpellListName).ToString();

        protected AHWarlockSpellListBuilder(string name, string guid) : base(DatabaseHelper.SpellListDefinitions.SpellListWizardGreenmage, name, guid)
        {
            Definition.GuiPresentation.Title = "Feat/&AHWarlockSpellListTitle";
            Definition.GuiPresentation.Description = "Feat/&AHWarlockSpellListDescription";
            Definition.SpellsByLevel.Clear();

            //List of SRD spells for Warlock implemented in Solasta

            //Cantrips
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet() { Spells = new List<SpellDefinition>() { 
                AHEldritchBlastSpellBuilder.AHEldritchBlastSpell,
                AHAgonizingBlastSpellBuilder.AHAgonizingBlastSpell,
                DatabaseHelper.SpellDefinitions.ChillTouch, 
                DatabaseHelper.SpellDefinitions.PoisonSpray, 
                DatabaseHelper.SpellDefinitions.TrueStrike }, Level = 0 });

            //Level 1
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet() { Spells = new List<SpellDefinition>() { 
                AHPactMarkSpellBuilder.AHPactMarkSpell, //Custom hex like spell
                DatabaseHelper.SpellDefinitions.CharmPerson, 
                DatabaseHelper.SpellDefinitions.ComprehendLanguages, 
                DatabaseHelper.SpellDefinitions.ExpeditiousRetreat, 
                DatabaseHelper.SpellDefinitions.ProtectionFromEvilGood }, Level = 1 });

            //Level 2
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet() { Spells = new List<SpellDefinition>() { 
                DatabaseHelper.SpellDefinitions.Darkness, 
                DatabaseHelper.SpellDefinitions.HoldPerson, 
                DatabaseHelper.SpellDefinitions.Invisibility,
                //DatabaseHelper.SpellDefinitions.MirrorImage, //Not implemented?
                DatabaseHelper.SpellDefinitions.MistyStep, 
                DatabaseHelper.SpellDefinitions.RayOfEnfeeblement, 
                DatabaseHelper.SpellDefinitions.Shatter, 
                DatabaseHelper.SpellDefinitions.SpiderClimb }, Level = 2 });

            //Level 3
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.Counterspell,
                DatabaseHelper.SpellDefinitions.DispelMagic,
                DatabaseHelper.SpellDefinitions.Fear,
                DatabaseHelper.SpellDefinitions.Fly,
                DatabaseHelper.SpellDefinitions.HypnoticPattern,
                DatabaseHelper.SpellDefinitions.RemoveCurse,
                DatabaseHelper.SpellDefinitions.Tongues,
                //DatabaseHelper.SpellDefinitions.VampiricTouchIntelligence //Wonder if this works?  Prob not :(
                },
                Level = 3
            });

            //Level 4
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.Banishment,
                DatabaseHelper.SpellDefinitions.Blight,
                DatabaseHelper.SpellDefinitions.DimensionDoor,
                },
                Level = 4
            });

            //Level 5
            Definition.SpellsByLevel.Add(new SpellListDefinition.SpellsByLevelDuplet()
            {
                Spells = new List<SpellDefinition>() {
                DatabaseHelper.SpellDefinitions.HoldMonster,
                },
                Level = 5
            });
        }

        public static SpellListDefinition CreateAndAddToDB(string name, string guid)
            => new AHWarlockSpellListBuilder(name, guid).AddToDB();

        public static SpellListDefinition AHWarlockSpellList = CreateAndAddToDB(AHWarlockSpellListName, AHWarlockSpellListGuid);
    }

    internal class AHWarlockClassPactBoonSetBuilder : BaseDefinitionBuilder<FeatureDefinitionFeatureSet>
    {
        const string AHWarlockClassPactBoonSetName = "AHWarlockClassPactBoonSet";
        private static readonly string AHWarlockClassPactBoonSetGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockClassPactBoonSetName).ToString();

        protected AHWarlockClassPactBoonSetBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetHunterHuntersPrey, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockClassPactBoonSetTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockClassPactBoonSetDescription";

            Definition.FeatureSet.Clear();
            Definition.FeatureSet.Add(AHWarlockClassPactOfTheBladeSetBuilder.AHWarlockClassPactOfTheBladeSet);
            Definition.FeatureSet.Add(AHWarlockClassPactOfTheChainPowerBuilder.AHWarlockClassPactOfTheChainPower);
            Definition.SetUniqueChoices(true);
        }

        public static FeatureDefinitionFeatureSet CreateAndAddToDB(string name, string guid)
            => new AHWarlockClassPactBoonSetBuilder(name, guid).AddToDB();

        public static FeatureDefinitionFeatureSet AHWarlockClassPactBoonSet = CreateAndAddToDB(AHWarlockClassPactBoonSetName, AHWarlockClassPactBoonSetGuid);
    }

    internal class AHWarlockClassPactOfTheBladeSetBuilder : BaseDefinitionBuilder<FeatureDefinitionFeatureSet>
    {
        const string AHWarlockClassPactOfTheBladeSetName = "AHWarlockClassPactOfTheBladeSet";
        private static readonly string AHWarlockClassPactOfTheBladeSetGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockClassPactOfTheBladeSetName).ToString();

        protected AHWarlockClassPactOfTheBladeSetBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetHunterHuntersPrey, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockClassPactOfTheBladeSetTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockClassPactOfTheBladeSetDescription";

            Definition.FeatureSet.Clear();
            Definition.FeatureSet.Add(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyFighterWeapon);
            Definition.FeatureSet.Add(DatabaseHelper.FeatureDefinitionAttackModifiers.AttackModifierMartialSpellBladeMagicWeapon);
            Definition.SetMode(FeatureDefinitionFeatureSet.FeatureSetMode.Union);
            Definition.SetUniqueChoices(false);
        }

        public static FeatureDefinitionFeatureSet CreateAndAddToDB(string name, string guid)
            => new AHWarlockClassPactOfTheBladeSetBuilder(name, guid).AddToDB();

        public static FeatureDefinitionFeatureSet AHWarlockClassPactOfTheBladeSet = CreateAndAddToDB(AHWarlockClassPactOfTheBladeSetName, AHWarlockClassPactOfTheBladeSetGuid);
    }

    internal class AHWarlockClassPactOfTheChainPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockClassPactOfTheChainPowerName = "AHWarlockClassPactOfTheChainPower";
        private static readonly string AHWarlockClassPactOfTheChainPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockClassPactOfTheChainPowerName).ToString();

        protected AHWarlockClassPactOfTheChainPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockClassPactOfTheChainPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockClassPactOfTheChainPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.ShortRest);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(1);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);

            //Create the summon effect
            EffectForm summonEffect = new EffectForm();
            summonEffect.FormType = EffectForm.EffectFormType.Summon;
            var summonForm = new SummonForm();
            summonForm.SetMonsterDefinitionName(DatabaseHelper.MonsterDefinitions.Flying_Snake.Name);
            summonForm.SetDecisionPackage(DatabaseHelper.DecisionPackageDefinitions.IdleGuard_Default);
            summonForm.SetConditionDefinition(DatabaseHelper.ConditionDefinitions.ConditionMindControlledByCaster);
            summonForm.SetNumber(1);
            summonEffect.SetSummonForm(summonForm);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(summonEffect);
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetRangeType(RuleDefinitions.RangeType.Distance);
            newEffectDescription.SetRangeParameter(6);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Position);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockClassPactOfTheChainPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockClassPactOfTheChainPower = CreateAndAddToDB(AHWarlockClassPactOfTheChainPowerName, AHWarlockClassPactOfTheChainPowerGuid);
    }

    /// <summary>
    /// Power to spend the extra spell slots currently in the class to help reduce potential cheating for reaction spells etc.
    /// </summary>
    internal class AHWarlockSpendExcessSpellSlotPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHWarlockSpendExcessSpellSlotPowerName = "AHWarlockSpendExcessSpellSlotPower";
        private static readonly string AHWarlockSpendExcessSpellSlotPowerGuid = GuidHelper.Create(PactTouchedFeatBuilder.PactTouchedMainGuid, AHWarlockSpendExcessSpellSlotPowerName).ToString();

        protected AHWarlockSpendExcessSpellSlotPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHWarlockSpendExcessSpellSlotPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHWarlockSpendExcessSpellSlotPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.SpellSlot);
            Definition.SetSpellcastingFeature(AHWarlockShortRestSpellFeatureBuilder.AHWarlockSpellCastFeature);
            Definition.SetFixedUsesPerRecharge(9999);
            Definition.SetCostPerUse(1);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.NoCost);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHWarlockSpendExcessSpellSlotPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower AHWarlockSpendExcessSpellSlotPower = CreateAndAddToDB(AHWarlockSpendExcessSpellSlotPowerName, AHWarlockSpendExcessSpellSlotPowerGuid);
    }
}
