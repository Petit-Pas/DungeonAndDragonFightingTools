using DDFight.Game.Aggression.Spells;
using DDFight.Game.Entities;
using DDFight.Tools.Save;
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
        public static readonly string main_config_folder = Environment.GetEnvironmentVariable("LocalAppData") + "\\D&DFightTool\\configs\\";

        public static readonly string players_folder = "players\\";

        public static readonly string monsters_folder = "monsters\\";

        public static readonly string spells_folder = "spells\\";

        private static string get_subfolder<T>(T elem)
        {
            if (elem.GetType() == typeof(Character))
                return players_folder;
            else if (elem.GetType() == typeof(Monster))
                return monsters_folder;
            else if (elem.GetType() == typeof(Spell))
                return spells_folder;
            return "default\\";
        }

        /// <summary>
        ///     As the config are not in a single file that gets erased every save, this function erases any files unwanted file
        /// </summary>
        /// <param name="sub_folder">
        ///     The folder to clean
        /// </param>
        /// <param name="authorized">
        ///     The list of files (Absolute Path) authorized.
        ///     /!\ any files that is not contained in this list will be erased
        /// </param>
        private static void CleanFolder(string sub_folder, List<string> authorized)
        {
            string folder_name = main_config_folder + sub_folder;
            if (Directory.Exists(folder_name))
            {
                string[] fileEntries = Directory.GetFiles(folder_name);
                foreach (string fileName in fileEntries)
                {
                    if (!authorized.Contains(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }
        }

        public static void DeleteUnique<T>(T elem) where T : class, INameable, new()
        {
            DeleteUnique<T>(elem, get_subfolder(elem));
        }

        public static void DeleteUnique<T>(T elem, string subfolder) where T : class, INameable, new ()
        {
            string folder_name = main_config_folder + subfolder;
            string filename = folder_name + elem.Name + ".xml";
            if (Directory.Exists(folder_name))
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }

        public static void SaveUnique<T>(T elem) where T : class, INameable, new()
        {
            SaveUnique<T>(elem, get_subfolder(elem));
        }

        public static void SaveUnique<T>(T elem, string subfolder) where T : class, INameable, new ()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string folder_name = main_config_folder + subfolder;

            if (!Directory.Exists(folder_name))
                Directory.CreateDirectory(folder_name);

            try
            {
                StreamWriter writer = new StreamWriter(folder_name + elem.Name + ".xml");
                serializer.Serialize(writer, elem);
                writer.Close();
            }
            catch (Exception e)
            {
                Logger.Log("WARNING: Exception occured when trying to save " + folder_name + elem.Name);
                Logger.Log(e.Message);
            }
        }

        public static T LoadUnique<T>(string fileName) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result;

            if (File.Exists(fileName))
            {
                try
                {
                    StreamReader reader = new StreamReader(fileName);
                    result = serializer.Deserialize(reader) as T;
                    reader.Close();
                    return result;
                }
                catch (Exception e)
                {
                    Logger.Log("WARNING: Exception occured when trying to load " + fileName);
                    Logger.Log(e.Message);
                }
            }
            else
                Logger.Log("Could not find file " + fileName);
            return null;
        }

        /// <summary>
        ///     Saves a list of element of Type T in a file
        ///     1 file per element
        ///     Files are named after INameable.Name property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="genericList"></param>
        /// <param name="subfolder"></param>
        public static void SaveGenericList<T>(GenericList<T> genericList, string subfolder = null) where T : class, ICloneable, INameable, new()
        {
            if (genericList.Elements.Count == 0)
                return;
            if (subfolder == null)
                subfolder = get_subfolder<T>(genericList.Elements[0]);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string folder_name = main_config_folder + subfolder;

            if (!Directory.Exists(folder_name))
                Directory.CreateDirectory(folder_name);

            foreach (T elem in genericList.Elements)
            {
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(folder_name + elem.Name + ".xml");
                    serializer.Serialize(writer, elem);
                }
                catch (Exception e)
                {
                    Logger.Log("WARNING: Exception occured when trying to save " + folder_name + elem.Name);
                    Logger.Log(e.Message);
                }
                writer?.Close();
            }
            //genericList.Elements.Select(x => x.Name).ToList();
            CleanFolder(subfolder, genericList.Elements.Select(x => folder_name + x.Name + ".xml").ToList());
        }

        /// <summary>
        ///     Will load a GenericList of element T with all the files contained in the given subfolder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subfolder"></param>
        /// <returns></returns>
        public static U LoadGenericList<T, U>(string subfolder) where T : class, INameable, ICloneable, new() where U : GenericList<T>, new ()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            U result = new U();

            if (Directory.Exists(main_config_folder + subfolder))
            {
                string[] fileEntries = Directory.GetFiles(main_config_folder + subfolder);
                foreach (string fileName in fileEntries)
                {
                    try
                    {
                        StreamReader reader = new StreamReader(fileName);
                        T element = serializer.Deserialize(reader) as T;
                        reader.Close();
                        result.AddElementSilent(element);
                    }
                    catch (Exception e)
                    {
                        Logger.Log("WARNING: Exception occured when trying to load " + fileName);
                        Logger.Log(e.Message);
                    }
                }
            }
            else
                Logger.Log("Found nothing to load in folder " + main_config_folder + subfolder);
            return result;
        }
    }
}
