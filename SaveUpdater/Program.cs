using DDFight;
using DDFight.Game.Entities;
using DDFight.Tools;
using DDFight.Tools.Save;
using System;

namespace SaveUpdater
{
    class Program
    {

        static void Main(string[] args)
        {
            Logger.Init();

            GenericList<Character> character_list = SaveManager.LoadGenericList<Character, GenericList<Character>>(SaveManager.players_folder);

            ;

        }
    }
}
