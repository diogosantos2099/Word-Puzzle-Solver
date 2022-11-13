﻿namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Responsible for the constant values used throughout the program.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Length for the input startWord and endWord.
        /// Also used for filtering dictionary.
        /// </summary>
        public static readonly int DesiredWordLength = 4;

        /// <summary>
        /// Number of expected input arguments.
        /// </summary>
        public static readonly int ArgsExpected = 3;

        /// <summary>
        /// Output file generated by the program.
        /// By default, let it go to /bin/debug folder
        /// </summary>
        public static readonly string OutputDir = "output.txt";
    }
}
