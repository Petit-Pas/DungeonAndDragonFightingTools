using System;
using DnDToolsLibrary.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;

namespace DnDToolsLibrary.Fight
{
    public interface IFightManager : INotifyPropertyChanged
    {
        List<string> GetFightersNames();

        PlayableEntity GetFighterByDisplayName(string name);

        int CountWithName(string name);

        void AddFighter(PlayableEntity fighter);

        void AddOrUpdateFighter(PlayableEntity fighter);

        bool RemoveFighter(PlayableEntity fighter);
        bool RemoveFighter(string displayName);

        int FighterCount { get; }

        PlayableEntity GetFighterByIndex(int fightContextTurnIndex);
        IEnumerable<PlayableEntity> GetAllFighters();
        IEnumerable<PlayableEntity> GetAllMonsters();
        IEnumerable<Character> GetAllCharacters();

        IEnumerable<PlayableEntity> GetMonstersByName(string name);

        ObservableCollection<PlayableEntity> GetObservableCollection();

        void SetTurnOrdersMiddleFight();

        // 
        int GetCurrentTurnIndex();
        void Clear();
        PlayableEntity First();
        void SetTurnOrders();
    }
}
