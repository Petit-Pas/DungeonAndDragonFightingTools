using DDFight.Game.Aggression.Spells;
using DDFight.Game.Entities;
using DDFight.Tools;
using DDFight.Tools.Save;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfSandbox;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged

        public Character transform_characterType(CharacterType to_copy)
        {
            Character result = new Character()
            {
                ActionDescription = to_copy.ActionDescription,
                CA = to_copy.CA,
                Characteristics = to_copy.Characteristics,
                Counters = CounterType.ConvertListCounterType(to_copy.Counters),
                CustomVerboseStatusList = CustomVerboseStatusListType.Convert(to_copy.CustomVerboseStatusList),
                DamageAffinities = to_copy.DamageAffinities,
                DisplayName = to_copy.DisplayName,
                HasBonusAction = to_copy.HasBonusAction,
                HasAction = to_copy.HasAction,
                HasInspiration = to_copy.HasInspiration,
                HasReaction = to_copy.HasReaction,
                HasSpells = to_copy.HasSpells,
                HitAttacks = HitAttackTemplateType.ConvertList(to_copy.HitAttacks),
                Hp = to_copy.Hp,
                HpString = to_copy.HpString,
                InitiativeRoll = to_copy.InitiativeRoll,
                IsFocused = to_copy.IsFocused,
                IsTransformed = to_copy.IsTransformed,
                Level = to_copy.Level,
                MaxHp = to_copy.MaxHp,
                Name = to_copy.Name,
                SpecialAbilities = to_copy.SpecialAbilities,
                SpellHitModifier = to_copy.SpellHitModifier,
                Spells = SpellListType.Convert(to_copy.Spells),
                SpellSave = to_copy.SpellSave,
                TempHp = to_copy.TempHp,
                TurnOrder = to_copy.TurnOrder,
            };

            return result;
        }

        public Monster transform_monsterType(MonsterType to_copy)
        {
            Monster result = new Monster()
            {
                ActionDescription = to_copy.ActionDescription,
                CA = to_copy.CA,
                Characteristics = to_copy.Characteristics,
                Counters = CounterType.ConvertListCounterType(to_copy.Counters),
                CustomVerboseStatusList = CustomVerboseStatusListType.Convert(to_copy.CustomVerboseStatusList),
                DamageAffinities = to_copy.DamageAffinities,
                DisplayName = to_copy.DisplayName,
                HasBonusAction = to_copy.HasBonusAction,
                HasAction = to_copy.HasAction,
                HasReaction = to_copy.HasReaction,
                HasSpells = to_copy.HasSpells,
                HitAttacks = HitAttackTemplateType.ConvertList(to_copy.HitAttacks),
                Hp = to_copy.Hp,
                HpString = to_copy.HpString,
                InitiativeRoll = to_copy.InitiativeRoll,
                IsFocused = to_copy.IsFocused,
                IsTransformed = to_copy.IsTransformed,
                Level = to_copy.Level,
                MaxHp = to_copy.MaxHp,
                Name = to_copy.Name,
                SpecialAbilities = to_copy.SpecialAbilities,
                SpellHitModifier = to_copy.SpellHitModifier,
                Spells = SpellListType.Convert(to_copy.Spells),
                SpellSave = to_copy.SpellSave,
                TempHp = to_copy.TempHp,
                TurnOrder = to_copy.TurnOrder,
            };

            return result;
        }

        public MainWindow()
        {


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
    }
}