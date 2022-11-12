namespace Word_Puzzle_Solver
{
    public class UserInput
    {
        /// <summary>
        /// The first argument provided by the User.
        /// Dictionary must exist in available dictionaries.
        /// </summary>
        public string DictionaryFile { get; set; }

        /// <summary>
        /// The second argument provided by the User.
        /// Starting word for the algorithm.
        /// </summary>
        public string StartWord { get; set; }

        /// <summary>
        /// The third argument provided by the User.
        /// Finishing word for the algorithm.
        /// </summary>
        public string EndWord { get; set; }

        /// <summary>
        /// A list of validation errors related to the User input.
        /// </summary>
        public List<string> Errors { get; set; }

        public UserInput() 
        {
            DictionaryFile = string.Empty;
            StartWord = string.Empty;
            EndWord = string.Empty;
            Errors = new List<string>();
        }
    }
}
