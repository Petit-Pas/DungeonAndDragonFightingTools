using BaseToolsLibrary.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged

        public int Integer
        {
            get => _integer;
            set { _integer = value;  NotifyPropertyChanged(); }
        }
        private int _integer = 1;

        public MainWindow()
        {


            this.DataContext = this;
            InitializeComponent();

            Logger.Init();

            /*DDFight.Tools.Save.GenericList<CharacterType> character_list = SaveManager.LoadGenericList<CharacterType, DDFight.Tools.Save.GenericList<CharacterType>>("trash//"+ SaveManager.players_folder);
            DDFight.Tools.Save.GenericList<MonsterType> monster_list = SaveManager.LoadGenericList<MonsterType, DDFight.Tools.Save.GenericList<MonsterType>>("trash//" + SaveManager.monsters_folder);
            DDFight.Tools.Save.GenericList<SpellType> spell_list = SaveManager.LoadGenericList<SpellType, DDFight.Tools.Save.GenericList<SpellType>>("trash//" + SaveManager.spells_folder);


            DDFight.Tools.Save.GenericList<Character> new_character_list = new DDFight.Tools.Save.GenericList<Character>();
            foreach (CharacterType character in character_list.Elements)
            {
                new_character_list.AddElementSilent(transform_characterType(character));
            }

            DDFight.Tools.Save.GenericList<Monster> new_monster_list = new DDFight.Tools.Save.GenericList<Monster>();
            foreach (MonsterType monster in monster_list.Elements)
            {
                new_monster_list.AddElementSilent(transform_monsterType(monster));
            }

            DDFight.Tools.Save.GenericList<Spell> new_spell_list = new DDFight.Tools.Save.GenericList<Spell>();
            foreach (SpellType spell in spell_list.Elements)
            {
                new_spell_list.AddElementSilent(SpellType.Convert(spell));
            }


            SaveManager.SaveGenericList<Character>(new_character_list, SaveManager.players_folder);
            SaveManager.SaveGenericList<Monster>(new_monster_list, SaveManager.monsters_folder);
            SaveManager.SaveGenericList<Spell>(new_spell_list, SaveManager.spells_folder);


            /*SaveManager.SaveGenericList<CharacterType>(character_list, "test//characters//");
            SaveManager.SaveGenericList<MonsterType>(monster_list, "test//monsters//");
            SaveManager.SaveGenericList<SpellType>(spell_list, "test//spells//");*/

            ;


        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonCardControl_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}