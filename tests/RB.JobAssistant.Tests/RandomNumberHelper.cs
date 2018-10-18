using System;

namespace RB.JobAssistant.Tests
{
    public class RandomNumberHelper
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static int NextInteger()
        {
            return Random.Next();
        }

        public static int NextIntegerInRange(int lowBound, int highBound)
        {
            return Random.Next(lowBound, highBound);
        }
    }

    public class CustomRandomNumberHelper
    {
        private static readonly RandomNumber Random = new LehmerRng(DateTime.Now.Millisecond);

        public static int NextInteger()
        {
            return (int) Random.Next();
        }
    }
}