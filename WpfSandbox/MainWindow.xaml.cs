using DDFight.Tools;
using DDFight.Tools.Save;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfSandbox.Types;

namespace BindValidation
{

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

        public MainWindow()
        {

            Logger.Init();

            DDFight.Tools.Save.GenericList<CharacterType> character_list = SaveManager.LoadGenericList<CharacterType, DDFight.Tools.Save.GenericList<CharacterType>>(SaveManager.players_folder);
            DDFight.Tools.Save.GenericList<MonsterType> monster_list = SaveManager.LoadGenericList<MonsterType, DDFight.Tools.Save.GenericList<MonsterType>>(SaveManager.monsters_folder);
            DDFight.Tools.Save.GenericList<SpellType> spell_list = SaveManager.LoadGenericList<SpellType, DDFight.Tools.Save.GenericList<SpellType>>(SaveManager.spells_folder);



            SaveManager.SaveGenericList<CharacterType>(character_list, "test//characters//");
            SaveManager.SaveGenericList<MonsterType>(monster_list, "test//monsters//");
            SaveManager.SaveGenericList<SpellType>(spell_list, "test//spells//");

            ;

            throw new NotImplementedException();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}