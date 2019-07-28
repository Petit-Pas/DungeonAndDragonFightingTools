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
        private static string config_folder = Environment.GetEnvironmentVariable("LocalAppData") + "\\D&DFightTool\\configs\\";

        private static string players_config_file
        {
            get
            {
                return config_folder + "characters.xml";
            }
        }

        public static void Init ()
        {
            if (!Directory.Exists (config_folder))
            {
                Directory.CreateDirectory(config_folder);
            }
        }

        public static void SavePlayers (CharactersList characters)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CharactersList));
            StreamWriter writer = new StreamWriter(players_config_file);

            serializer.Serialize(writer, characters);
        }

        public static CharactersList LoadPlayers ()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CharactersList));
            try
            {
                StreamReader reader = new StreamReader(players_config_file);
                return (CharactersList)serializer.Deserialize(reader);
            }catch (FileNotFoundException)
            {
                Logger.Log("Could not find the characters save file");
            }
            catch (Exception e)
            {
                Logger.Log(String.Format("Unknown error while trying to load the characters file: {0}, {1}", e.Message, e.StackTrace));
            }
            return null;
        }
    }
}
