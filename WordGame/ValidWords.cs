namespace WordGame
{
    using System.Collections;
    using System.IO;
    using System.Reflection;

    public class ValidWords : IValidWords
    {
        /*
         * would like this property name to be more descriptive
         * As well this should be a generic list of strings 
         * instead of a list of objects
         * */
        ArrayList a = new ArrayList();

        /*
         * Might be useful to have an overlodaed constructor that accespt an array of strings.
         * */
        public ValidWords()
        {
            /* 
             * Would prefer to use using statements for stream and reader instead of calling dispose in the finally block. Using statements take care of this in the event of an exception 
             * or successfull completion.
             * */
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                /*
                 * Something seems a bit off about this, but it could be that I've never retrived a file like this before.
                 * I like that it is embedded with the assembly.
                 * */
                stream = Assembly.GetAssembly(typeof(ValidWords)).GetManifestResourceStream("WordGame.wordlist.txt");
                reader = new StreamReader(stream);

                /*
                 * It would be interesting to discover if it would be more efficient to read to end and then split the resulting string by the newline
                 * character to create the array.
                 * */
                while (!reader.EndOfStream)
                {
                    a.Add(reader.ReadLine());
                }

                /* Perhaps we should attempt to filter the array for duplicates */
            }
            finally
            {
                reader.Dispose();
                stream.Dispose();
            }
        }

        /*
         * Seems unecessary we should consider removing
         * */
        public int Size
        {
            get { return a.Count; }
        }

        /*
         * Have grown rather fond of making public methods virtual so they can be mocked in slightly more complicated tests using frameworks like Moq.
         * */
        public bool Contains(string word)
        {

            /*
             * If we had an overloaded constructor taking a list of strings for the a proeprty then we should check for null on that property
             * something like
             * 
             * a == null ? false : a.Contains(word);
             * */
            return a.Contains(word);
        }
    }
}
