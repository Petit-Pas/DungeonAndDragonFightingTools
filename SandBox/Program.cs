using BaseToolsLibrary.Mediator;
using DDFight;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Entities.EntitiesCommands.HpCommands;
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

            HealEntityCommand command = new HealEntityCommand(new Character(), 10);

            Console.ReadLine();
        }
    }
}
