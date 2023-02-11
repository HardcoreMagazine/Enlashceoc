namespace Enlashceoc.Game
{

    /// <summary>
    /// Extends Random class functionality. 
    /// Algorithm used:
    /// <see href="https://en.wikipedia.org/wiki/Fisher-Yates_shuffle"/>
    /// </summary>
    static class RandomExtensions
    {
        /// <summary>
        /// Shuffles selected array.
        /// Use: 
        /// <code>
        /// Random rng = new Random(); 
        /// rng.ShuffleArray(myArray); 
        /// </code>
        /// </summary>
        public static void ShuffleArray<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        /// <summary>
        /// Shuffles selected list.
        /// Use: 
        /// <code>
        /// Random rng = new Random(); 
        /// rng.ShuffleArray(myList); 
        /// </code>
        /// </summary>
        public static void ShuffleList<T>(this Random rng, List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }
    }
}
