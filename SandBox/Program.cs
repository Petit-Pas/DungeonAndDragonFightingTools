using BaseToolsLibrary.Mediator;
using DDFight;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands.Heal;
using SimpleInjector;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SandBox
{
    
    class Program
    {
        

        static void Main(string[] args)
        {
            DIConfigurer.ConfigureCore();

            HealCommand command = new HealCommand(new Character(), 10);

            Console.ReadLine();
        }
    }
}
