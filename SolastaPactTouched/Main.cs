using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;
using I2.Loc;
using SolastaModApi;
using System.Collections.Generic;

namespace SolastaPactTouched
{
    public class Main
    {
        [Conditional("DEBUG")]
        internal static void Log(string msg) => Logger.Log(msg);
        internal static void Error(Exception ex) => Logger?.Error(ex.ToString());
        internal static void Error(string msg) => Logger?.Error(msg);
        internal static UnityModManager.ModEntry.ModLogger Logger { get; private set; }

        internal static void LoadTranslations()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo($@"{UnityModManager.modsPath}/SolastaPactTouched");
            FileInfo[] files = directoryInfo.GetFiles($"Translations-??.txt");

            foreach (var file in files)
            {
                var filename = $@"{UnityModManager.modsPath}/SolastaPactTouched/{file.Name}";
                var code = file.Name.Substring(13, 2);
                var languageSourceData = LocalizationManager.Sources[0];
                var languageIndex = languageSourceData.GetLanguageIndexFromCode(code);

                if (languageIndex < 0)
                    Main.Error($"language {code} not currently loaded.");
                else
                    using (var sr = new StreamReader(filename))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var splitted = line.Split(new[] { '\t', ' ' }, 2);
                            var term = splitted[0];
                            var text = splitted[1];
                            languageSourceData.AddTerm(term).Languages[languageIndex] = text;
                        }
                    }
            }
        }

        internal static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Logger = modEntry.Logger;

                LoadTranslations();

                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }

            return true;
        }

        internal static void ModEntryPoint()
        {
            //var ebSpellCantrip = new SpellListDefinition.SpellsByLevelDuplet();
            //ebSpellCantrip.Level = 0;
            //ebSpellCantrip.Spells = new List<SpellDefinition>();
            //ebSpellCantrip.Spells.Add(AHEldritchBlastSpellBuilder.AHEldritchBlastSpell);

            //var hellishRebukeLevel1Spell = new SpellListDefinition.SpellsByLevelDuplet();
            //hellishRebukeLevel1Spell.Level = 1;
            //hellishRebukeLevel1Spell.Spells = new List<SpellDefinition>();
            //hellishRebukeLevel1Spell.Spells.Add(AHHellishRebukeSpellBuilder.AHHellishRebukeSpell);
            //DatabaseHelper.SpellListDefinitions.SpellListWizard.SpellsByLevel.ToArray()[0].Spells.Add(AHEldritchBlastSpellBuilder.AHEldritchBlastSpell);
            //DatabaseHelper.SpellListDefinitions.SpellListWizard.SpellsByLevel.ToArray()[1].Spells.Add(AHPactMarkSpellBuilder.AHPactMarkSpell);
            //DatabaseHelper.SpellListDefinitions.SpellListWizard.SpellsByLevel.ToArray()[1].Spells.Add(AHHellishRebukeSpellBuilder.AHHellishRebukeSpell);
            //DatabaseHelper.CharacterClassDefinitions.Wizard.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHHellishRebukePowerBuilder.AHHellishRebukeSpell, 1));
            //PactTouchedFeatBuilder.AddToFeatList(); //Unfortunately doesn't work well as feat, adding cantrips doesn't work through feats :(

            var pactTouchedWizardSubclass = AHWizardSubclassPactTouched.Build();
            DatabaseHelper.FeatureDefinitionSubclassChoices.SubclassChoiceWizardArcaneTraditions.Subclasses.Add(pactTouchedWizardSubclass.Name);

            AHWarlockClassBuilder.BuildAndAddClassToDB();
        }
    }
}

