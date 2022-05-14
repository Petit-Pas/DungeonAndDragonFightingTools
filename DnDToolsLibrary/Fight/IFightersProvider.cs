using System;
using DnDToolsLibrary.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Fight
{
    public interface IFightersProvider : INotifyPropertyChanged
    {
        void InvokeFighterSelected(FighterSelectedEventArgs args);
        event FighterSelected FighterSelected;

        List<string> GetFightersNames();
        PlayableEntity GetFighterByDisplayName(string name);
        int CountWithName(string name);

        void AddFighter(PlayableEntity fighter);

        void AddOrUpdateFighter(PlayableEntity fighter);

        bool RemoveFighter(PlayableEntity fighter);
        bool RemoveFighter(string displayName);

        int FighterCount { get; }

        PlayableEntity GetFighterByIndex(int fightContextTurnIndex);
        
        IEnumerable<Monster> Monsters { get; }
        IEnumerable<Character> Characters { get; }
        IEnumerable<PlayableEntity> Fighters { get; }

        IEnumerable<PlayableEntity> GetMonstersByName(string name);

        ObservableCollection<PlayableEntity> GetObservableCollection();

        void Clear();
        PlayableEntity First();
        void OrderFighters(Comparison<PlayableEntity> initiativeSorter);

        PlayableEntity CurrentlyPlaying { get; }
        bool Contains(string entityDisplayName);
    }
}
