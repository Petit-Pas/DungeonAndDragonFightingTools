using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static string players_config_file
        {
            get
            {
                return config_folder + "characters.xml";
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

        /// <summary>
        ///     Save all the characters
        /// </summary>
        /// <param name="characters"></param>
        public static void SavePlayers(CharactersList characters)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CharactersList));
            StreamWriter writer = new StreamWriter(players_config_file);

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
                StreamReader reader = new StreamReader(players_config_file);
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
    }
}
