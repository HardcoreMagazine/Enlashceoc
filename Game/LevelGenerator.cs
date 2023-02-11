namespace Enlashceoc.Game
{
    /// <summary>
    /// Generates text maze level 120 * 28 using home-made 
    /// Kruskals-based algorythm. Note: may generate faulty levels,
    /// supply together with "recreate level" function.
    /// </summary>
    // by "faulty levels" I mean dead ends on player start and/or exit, not "multiple ways out"
    internal class LevelGenerator
    {
        public char[] space;
        public int playerIndex;
        private static char[] field;
        private static int[] edges;
        private static int mazeEntry, mazeExit;
        private static int fieldSizeX = 120, fieldSizeY; //default: 120x30 or 120x28
        private const int multiplier = 2, DENSITY = 4;
        // multimpier: !DANGER ZONE!
        // points on 'edge' area size: 2x2, 3x3,...
        // DENSITY: !DANGER ZONE!
        // 1, 2 -> no walls
        // 3 -> just right @ USE THIS
        // 4 -> medium mode -- works, contains higher rate of dead ends @ OR THIS
        // 5, 6 -> medium mode -- severe lag & a lot of dead ends (including entry & exit)
        // 7+ -> never ending loop OR no physical way to compolete maze (error)

        /// <summary>
        /// Creates random number (more random than Random.Next).
        /// </summary>
        /// <param name="minValue">(optional) minimal value to generate</param>
        /// <param name="maxValue">(optional) maximum value to generate</param>
        /// <returns>Pseudo-random int value</returns>
        static int RNG(int? minValue = null, int? maxValue = null)
        {
            Random num = new Random(Guid.NewGuid().GetHashCode());
            if (minValue == null)
                if (maxValue == null)
                    return num.Next(0, int.MaxValue);
                else
                    return num.Next(0, (int)maxValue);
            else
                if (maxValue == null)
                return num.Next((int)minValue, int.MaxValue);
            else
                return num.Next((int)minValue, (int)maxValue);
        }

        /// <summary>
        /// Calculate element index by given X, Y coordinates
        /// </summary>
        private static int GetPos(int X, int Y)
        {
            if (X == fieldSizeX & Y == fieldSizeY - 1)
                return fieldSizeX * fieldSizeY - 1;
            else if (Y == fieldSizeY)
                if (X == fieldSizeX)
                    return fieldSizeX * fieldSizeY - 1;
                else
                    return (Y - 1) * fieldSizeX + X;
            else
                return Y * fieldSizeX + X;
        }

        /// <summary>
        /// Find X, Y pos by index. 
        /// Always returns pair of values: [0]-X, [1]-Y.
        /// </summary>
        private static int[] ReversePos(int index)
        {
            int posX = index % fieldSizeX;
            int posY = (index - posX) / fieldSizeX;
            return new int[] { posX, posY };
        }

        /// <summary>
        /// Overrides whole field array with given character c
        /// </summary>
        /// <param name="c">Override character</param>
        private static void FillField(char c)
        {
            for (int y = 0; y < fieldSizeY; y++)
            {
                for (int x = 0; x < fieldSizeX; x++)
                {
                    field[GetPos(x, y)] = c;
                }
            }
        }

        /// <summary>
        /// Finds all n*n areas and returns indexes of their centers,
        /// where n - multiplier.
        /// Note: said areas does not overlap.
        /// </summary>
        private static int[] CalculateEdges()
        {
            List<int> indeces = new List<int>();
            for (int y = 0; y < fieldSizeY; y++)
                for (int x = 0; x < fieldSizeX; x++)
                    if (y % multiplier == 0 & x % multiplier == 0)
                        indeces.Add(GetPos(x, y));
            return indeces.ToArray();
        }

        /// <summary>
        /// Select player entry and maze exit based on edges found.
        /// </summary>
        private static void SelectEntryExit()
        {
            Random rng = new Random();
            List<int> possibleEntrances = new List<int>();
            List<int> possibleExits = new List<int>();
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i] % fieldSizeX == 0) possibleEntrances.Add(edges[i]);
                else if (ReversePos(edges[i])[0] == fieldSizeX - multiplier) possibleExits.Add(edges[i]);
            }
            rng.ShuffleList(possibleEntrances);
            rng.ShuffleList(possibleExits);
            mazeEntry = possibleEntrances[0];
            mazeExit = possibleExits[0] + multiplier/2;
        }

        /// <summary>
        /// Creates a wall within provided indeces
        /// </summary>
        /// <param name="from">Start index</param>
        /// <param name="to">End index (inclusive)</param>
        /// <param name="step">Step (+-1 or +-120)</param>
        private static void WalkCells(int from, int to, int step)
        {
            for (int j = from; j <= to; j += step)
            {
                if (j != mazeEntry & j != mazeExit)
                    field[j] = '■';
            }
        }

        /// <summary>
        /// Find possible move directions from index
        /// </summary>
        /// <returns>Directions array (addition to index) with dynamic(!) size</returns>
        private static int[] FindNeighbors(ref int index, ref Dictionary<int, byte> visits)
        {
            List<int> directons = new List<int>();
            int yPos = ReversePos(index)[1];
            if (index + fieldSizeX * multiplier < field.Length)
                if (visits[index + fieldSizeX * multiplier] < DENSITY)
                    directons.Add(multiplier * fieldSizeX);
            if (index + multiplier < field.Length & ReversePos(index + multiplier)[1] == yPos)
                if (visits[index + multiplier] < DENSITY)
                    directons.Add(multiplier);
            if (index - multiplier >= 0 & ReversePos(index - multiplier)[1] == yPos)
                if (visits[index - multiplier] < DENSITY)
                    directons.Add(-multiplier);
            if (index - fieldSizeX * multiplier >= 0)
                if (visits[index - fieldSizeX * multiplier] < DENSITY)
                    directons.Add(-multiplier * fieldSizeX);
            return directons.ToArray();
        }

        /// <summary>
        /// Creates random path based on gathered data.
        /// </summary>
        private static void CreatePathways()
        {
            Random rng = new Random();
            Dictionary<int, byte> visits = new Dictionary<int, byte>(); //index, visit count
            for (int i = 0; i < edges.Length; i++) visits.Add(edges[i], 1);
            
            SelectEntryExit();
            field[mazeEntry] = '@';
            field[mazeExit] = 'E';

            // Create pathways
            while (visits.Values.Min() < DENSITY - 1) //I am a wizard
            {
                int index = edges[RNG(0, edges.Length - 1)];
                int[] neighbors = FindNeighbors(ref index, ref visits);
                if (neighbors.Length > 0)
                {
                    rng.ShuffleArray(neighbors);
                    int nextIndex = neighbors[0]; //always use [0]!!

                    int[] revPos = ReversePos(index + nextIndex);
                    // Pathway correction & printing
                    if (revPos[0] == fieldSizeX - multiplier & nextIndex == multiplier)
                    {
                        // if X == 120-2
                        // and nextIndex == 2 (no abs) -> move to X = 119:
                        WalkCells(index, index + nextIndex + 1, nextIndex / multiplier);
                    }
                    else if (revPos[1] == fieldSizeY - multiplier & nextIndex == fieldSizeX * multiplier)
                    {
                        // if Y = 26 (pre-bottom line)
                        // and nextIndex == 240 (no abs) -> move to Y = 27:
                        WalkCells(index, index + nextIndex + fieldSizeX, nextIndex / multiplier);
                    }
                    else
                    {
                        // default case
                        WalkCells(index, index + nextIndex, nextIndex / multiplier);
                    }
                    visits[index] += 1;
                    index += nextIndex;
                    visits[index] += 1;
                }
            }
        }

        public LevelGenerator(int mazeSizeY = 28)
        {
            fieldSizeY = mazeSizeY;
            field = new char[fieldSizeX * fieldSizeY];
            FillField(' ');
            edges = CalculateEdges();
            CreatePathways();
            space = field;
            playerIndex = mazeEntry;
        }
    }
}
