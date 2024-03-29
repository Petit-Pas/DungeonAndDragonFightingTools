using DnDToolsLibrary.Dice;
using NUnit.Framework;
using System;

namespace CoreUnitTest.Dice
{
    public class DiceRollTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private void test(string format, string soluce)
        {
            try
            {
                DiceRoll test = new DiceRoll(format);
                Console.WriteLine(format);
                Console.WriteLine(test.ToString());
                Assert.IsTrue(test.ToString() == soluce);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().ToString());
                Console.WriteLine(e.Message);
                Assert.Fail();
            }
        }

        private void test(string format)
        {
            try
            {
                DiceRoll test = new DiceRoll(format);
                Console.WriteLine(format);
                Console.WriteLine(test.ToString());
                Assert.IsTrue(test.ToString() == format);
            }
            catch (Exception e) {
                Console.WriteLine(e.GetType().ToString());
                Console.WriteLine(e.Message);
                Assert.Fail();
            }
        }

        [Test]
        public void EmptyConstructor()
        {
            DiceRoll diceRoll = new DiceRoll();
        }

        [Test]
        public void TestCombination()
        {
            test("1d6+1d6+1+1", "2d6+2");
            test("2d6-1d6", "1d6");
        }

        [Test]
        public void TestUniquePositiveDice()
        {
            test("1d6");
            test("2d4");
            test("3d20");
        }

        [Test]
        public void TestMultiplePositiveDice()
        {
            test("1d6+1d4");
        }

        [Test]
        public void TestMultipleNegativeDice()
        {
            test("1d18-1d6-1d4");
        }

        [Test]
        public void TestMultipleMixedDice()
        {
            test("1d8+1d6-1d4-2");
        }

        [Test]
        public void TestPositiveModifierOnly()
        {
            test("4");
        }

        [Test]
        public void TestPositiveRollAndModifier()
        {
            test("1d6+4");
        }

        [Test]
        public void TestPositiveRollNegativeModifier()
        {
            test("1d6-4");
        }

        [Test]
        public void BigTest()
        {
            test("2d20-1d6+33d44+1d2-3");
        }

        private void failing_test(string format)
        {
            try
            {
                DiceRoll test = new DiceRoll(format);
                Assert.Fail();
            }
            catch (Exception) { Assert.Pass(); }
        }

        [Test]
        public void TestBadFormat()
        {
            failing_test("-1d4");
            failing_test("d4");
            failing_test("1d");
            failing_test("-");
            failing_test("1d4+");
            failing_test("1dd4");
            failing_test("");
        }

        [Test]
        public void TestIsEquivalentTo_Ok()
        {
            DiceRoll diceRoll = new DiceRoll("2d8+1");

            Assert.IsTrue(diceRoll.IsEquivalentTo(diceRoll));
        }

        [Test]
        public void TestIsEquivalentTo_NotOk()
        {
            DiceRoll diceRoll = new DiceRoll("2d8+1");
            DiceRoll diceRoll2 = new DiceRoll("2d8+2");

            Assert.IsFalse(diceRoll.IsEquivalentTo(diceRoll2));
        }

        [Test]
        public void TestReset()
        {
            DiceRoll diceRoll = new DiceRoll("2d8+1");
            DiceRoll diceRoll2 = new DiceRoll("2d8+1");

            Assert.IsTrue(diceRoll.IsEquivalentTo(diceRoll2));
            diceRoll.Roll();
            Assert.IsFalse(diceRoll.IsEquivalentTo(diceRoll2));
            diceRoll.Reset();
            Assert.IsTrue(diceRoll.IsEquivalentTo(diceRoll2));
        }

        [Test]
        public void TestStaticRoll()
        {
            DiceRoll diceRoll = new DiceRoll("1d1+9");

            int result = DiceRoll.Roll("1d1+9");
            diceRoll.Roll();

            Assert.AreEqual(diceRoll.LastResult, result);
        }
    }
}