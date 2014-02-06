namespace GiveawayApp.Models {
    using System;
    using System.Threading;

    /// <summary>
    /// Implementation by Jon Skeet on this StackOverflow Thread
    /// http://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values
    /// </summary>
    public static class RandomHelper {
        private static int seedCounter = new Random().Next();

        [ThreadStatic]
        private static Random rng;

        public static Random Instance {
            get {
                if (rng != null) { return rng;}
                
                var seed = Interlocked.Increment(ref seedCounter);
                rng = new Random(seed);
                return rng;
            }
        }
    }
}
