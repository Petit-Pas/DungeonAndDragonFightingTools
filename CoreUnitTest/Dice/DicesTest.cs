using DnDToolsLibrary.Dice;
using NUnit.Framework;

namespace CoreUnitTest.Dice
{
    [TestFixture]
    public class DicesTest
    {
        [Test]
        public void Roll()
        {
            Dices dices = new Dices("1d1");

            int result = dices.Roll();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Roll_Critical()
        {
            Dices dices = new Dices("1d1");

            int result = dices.Roll(true);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Roll_Negative()
        {
            Dices dices = new Dices("-1d1");

            int result = dices.Roll();

            Assert.AreEqual(-1, result);
        }

    }
}
