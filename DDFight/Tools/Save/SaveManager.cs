using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Spells;
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
            XmlSerializer serializer = new XmlSerializer(typeof(SpellsList));
            StreamWriter writer = new StreamWriter(spells_config_file);

            serializer.Serialize(writer, spells);
            writer.Close();
        }

        public static SpellsList LoadSpells()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SpellsList));
            SpellsList result = new SpellsList(true);

            try
            {
                StreamReader reader = new StreamReader(spells_config_file);
                result = (SpellsList)serializer.Deserialize(reader);
                reader.Close();
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

        #region Characters

        /// <summary>
        ///     Save all the characters
        /// </summary>
        /// <param name="characters"></param>
        public static void SavePlayers(CharactersList characters)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CharactersList));
            StreamWriter writer = new StreamWriter(characters_config_file);

            serializer.Serialize(writer, characters);
            writer.Close();
        }

        /// <summary>
        ///     Load all the saved characters
        /// </summary>
        /// <returns></returns>
        public static CharactersList LoadPlayers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CharactersList));
            CharactersList result = new CharactersList();

            try
            {
                StreamReader reader = new StreamReader(characters_config_file);
                result = (CharactersList)serializer.Deserialize(reader);
                reader.Close();

            }
            catch (FileNotFoundException)
            {
                Logger.Log("Could not find the characters save file");
            }
            catch (Exception e)
            {
                Logger.Log(String.Format("Unknown error while trying to load the characters file: {0}, {1}", e.Message, e.StackTrace));
            }
            return result;
        }

        #endregion Players

        #region Monsters

        /// <summary>
        ///     Save all the characters
        /// </summary>
        /// <param name="characters"></param>
        public static void SaveMonsters(MonstersList monsters)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MonstersList));
            StreamWriter writer = new StreamWriter(monsters_config_file);

            serializer.Serialize(writer, monsters);
            writer.Close();
        }

        /// <summary>
        ///     Load all the saved characters
        /// </summary>
        /// <returns></returns>
        public static MonstersList LoadMonsters()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MonstersList));
            MonstersList result = new MonstersList();

            try
            {
                StreamReader reader = new StreamReader(monsters_config_file);
                result = (MonstersList)serializer.Deserialize(reader);
                reader.Close();
                Console.WriteLine("import of monsters went fine");
                Console.WriteLine("Found {0} monsters to load", result.Monsters.Count);
            }
            catch (FileNotFoundException)
            {
                Logger.Log("Could not find the characters save file");
            }
            catch (Exception e)
            {
                Logger.Log(String.Format("Unknown error while trying to load the characters file: {0}, {1}", e.Message, e.StackTrace));
            }
            return result;
        }

        #endregion Monsters
    }
}
