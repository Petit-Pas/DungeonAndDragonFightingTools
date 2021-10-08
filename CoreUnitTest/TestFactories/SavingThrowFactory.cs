using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.TestFactories
{
    public static class SavingThrowFactory
    {
        public static SavingThrow Successful(PlayableEntity target) => new SavingThrow()
        {
            Difficulty = 10,
            Modifier = 5,
            SavingRoll = 5,
            Target = target,
        };

        public static SavingThrow Failed(PlayableEntity target) => new SavingThrow()
        {
            Difficulty = 20,
            Modifier = 5,
            SavingRoll = 5,
            Target = target,
        };
    }
}
