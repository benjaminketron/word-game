namespace WordGame
{
    public interface IValidWords
    {
        /*
         * Have been trying to figure out what this accessor is for. You could use it to short circuit a call to contains, but having
         * if (validWords.Size > 0) {
         *   if (validWords.Contains("...")) {
         *   ...
         *   }
         * }
         * 
         * seems a bit awkward. 
         * 
         * Generally, I prefer not to have accessors in interfaces unless there is a very good reason.
         * */

        /// <summary>
        ///     Gets the size of the valid words collection.
        /// </summary>
        int Size { get; }

        /// <summary>
        ///     Checks if a word is valid
        /// </summary>
        /// <param name="word">the word to check against the valid words collection</param>
        /// <returns>true if the valid words collection contains the word</returns>
        bool Contains(string word);
    }
}
