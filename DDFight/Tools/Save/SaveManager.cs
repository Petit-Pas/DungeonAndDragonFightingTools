using DDFight.Game.Aggression.Spells;
using DDFight.Game.Entities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace DDFight.Tools.Save
{
    public static class SaveManager
    {


        /// <summary>
        ///     The directory in which to store every config file
        /// </summary>
        private static string config_folder = Environment.GetEnvironmentVariable("LocalAppData") + "\\D&DFightTool\\configs\\";

        /// <summary>
        ///     The file in which to store the characters
        /// </summary>
        private static string monsters_config_file
        {
            get
            {
                return config_folder + "monsters.xml";
            }
        }

        private static string characters_config_file
        {
            get
            {
                return config_folder + "player.xml";
            }
        }

        private static string spells_config_file
        { 
            get 
            {
                return config_folder + "spells.xml";
            }
        }

        /// <summary>
        ///     Creates its own directory if does not exist
        /// </summary>
        public static void Init()
        {
            if (!Directory.Exists(config_folder))
            {
                Directory.CreateDirectory(config_folder);
            }
        }

        #region Spells

        public static void SaveSpells(SpellsList spells)
        {
            GenericList<Spell> list = new GenericList<Spell>();
            foreach (Spell entity in spells.Spells)
            {
                list.AddElementSilent(entity);
            }
            NewSaveManager.SaveGenericList<Spell>(list, NewSaveManager.spells_folder);


            XmlSerializer serializer = new XmlSerializer(typeof(SpellsList));
            StreamWriter writer = new StreamWriter(spells_config_file);

            serializer.Serialize(writer, spells);
            writer.Close();
            Console.WriteLine("Just Saved {0} spells!", spells.Spells.Count);
        }

        public static SpellsList LoadSpells()
        {
            GenericList<Spell> list = NewSaveManager.LoadGenericList<Spell, GenericList<Spell>>(NewSaveManager.spells_folder) as GenericList<Spell>;

            XmlSerializer serializer = new XmlSerializer(typeof(SpellsList));
            SpellsList result = new SpellsList(true);

            try
            {
                StreamReader reader = new StreamReader(spells_config_file);
                result = (SpellsList)serializer.Deserialize(reader);
                reader.Close();
                result.isMainSpellList = true;
                Console.WriteLine("Found {0} spells to load", result.Spells.Count);
            }
            catch (FileNotFoundException)
            {
                Logger.Log("Could not find the spells save file");
            }
            catch (Exception e)
            {
                Logger.Log(String.Format("Unknown error while trying to load the spells file: {0}, {1}", e.Message, e.StackTrace));
            }
            if (result.Spells == null)
                result.Spells = new ObservableCollection<Spell>();
            return result;
        }

        #endregion Spells
    }
}
