using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Tools
{
    public static class NewSaveManager
    {
        private static readonly string main_config_folder = Environment.GetEnvironmentVariable("LocalAppData") + "\\D&DFightTool\\configs\\";

        public static readonly string players_folder = "players\\";

        public static readonly string monsters_folder = "monsters\\";

        public static readonly string spells_folder = "spells\\";

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

        /// <summary>
        ///     Saves a list of element of Type T in a file
        ///     1 file per element
        ///     Files are named after INameable.Name property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="genericList"></param>
        /// <param name="subfolder"></param>
        public static void SaveGenericList<T>(GenericList<T> genericList, string subfolder) where T : class, IListable, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string folder_name = main_config_folder + subfolder;

            if (!Directory.Exists(folder_name))
                Directory.CreateDirectory(folder_name);

            foreach (T elem in genericList.Elements)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(folder_name + elem.Name + ".xml");
                    serializer.Serialize(writer, elem);
                    writer.Close();
                }
                catch (Exception e)
                {
                    Logger.Log("Exception occured when trying to load " + folder_name + elem.Name);
                    Logger.Log(e.Message);
                }
            }
            genericList.Elements.Select(x => x.Name).ToList();
            CleanFolder(subfolder, genericList.Elements.Select(x => folder_name + x.Name + ".xml").ToList());
        }

        /// <summary>
        ///     Will load a GenericList of element T with all the files contained in the given subfolder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subfolder"></param>
        /// <returns></returns>
        public static GenericList<T> LoadGenericList<T>(string subfolder) where T : class, IListable, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            GenericList<T> result = new GenericList<T>();

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
                        Logger.Log("Exception occured when trying to load " + fileName);
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
