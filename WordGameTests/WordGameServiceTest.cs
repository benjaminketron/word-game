namespace WordGameTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordGame;

    [TestClass]
    public class WordGameServiceTest
    {
        private IValidWords validWords = new ValidWords();
        private WordGameService wordGameService;
        
        [TestInitialize]
        public void Initialize()
        {
            this.wordGameService = new WordGameService("areallylongword", this.validWords);
        }

        [TestMethod]
        public void TestSubmissions()
        {
            Assert.AreEqual(3, this.wordGameService.SubmitWord("player1", "all"));
            Assert.AreEqual(4, this.wordGameService.SubmitWord("player2", "word"));
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player3", "tale"));
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player4", "glly"));
            Assert.AreEqual(6, this.wordGameService.SubmitWord("player5", "woolly"));
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player6", "adder"));
        }

        /*
         * Added a few more tests. Hopefully that is alright.
         * */

        [TestMethod]
        public void Test_At_Position_0()
        {
            this.wordGameService.SubmitWord("player1", "all");

            Assert.AreEqual("player1", this.wordGameService.GetPlayerNameAtPosition(0));
            Assert.AreEqual("all", this.wordGameService.GetWordEntryAtPosition(0));
            Assert.AreEqual(3, this.wordGameService.GetScoreAtPosition(0));
        }

        [TestMethod]
        public void Test_Same_Score_At_Position_1_and_2()
        {
            this.wordGameService.SubmitWord("player1", "woolly");
            this.wordGameService.SubmitWord("player2", "all");
            this.wordGameService.SubmitWord("player3", "are");

            Assert.AreEqual(6, this.wordGameService.GetScoreAtPosition(0));
            Assert.AreEqual("player1", this.wordGameService.GetPlayerNameAtPosition(0));
            Assert.AreEqual("woolly", this.wordGameService.GetWordEntryAtPosition(0));
            Assert.AreEqual(3, this.wordGameService.GetScoreAtPosition(1));
            Assert.AreEqual("player2", this.wordGameService.GetPlayerNameAtPosition(1));
            Assert.AreEqual("all", this.wordGameService.GetWordEntryAtPosition(1));
            Assert.AreEqual(3, this.wordGameService.GetScoreAtPosition(2));
            Assert.AreEqual("player3", this.wordGameService.GetPlayerNameAtPosition(2));
            Assert.AreEqual("are", this.wordGameService.GetWordEntryAtPosition(2));
        }

        [TestMethod]
        public void Test_Duplicates_Ignored()
        {
            this.wordGameService.SubmitWord("player1", "woolly");
            this.wordGameService.SubmitWord("player2", "woolly");

            Assert.AreEqual("woolly", this.wordGameService.GetWordEntryAtPosition(0));
            Assert.AreEqual("player1", this.wordGameService.GetPlayerNameAtPosition(0));
            Assert.AreEqual(null, this.wordGameService.GetWordEntryAtPosition(1));
            Assert.AreEqual(null, this.wordGameService.GetPlayerNameAtPosition(1));
        }

        [TestMethod]
        public void Test_Leaderboard_Limited_To_10()
        {
            this.wordGameService.SubmitWord("player1", "woolly");
            this.wordGameService.SubmitWord("player2", "all");
            this.wordGameService.SubmitWord("player3", "are");
            this.wordGameService.SubmitWord("player1", "really");
            this.wordGameService.SubmitWord("player2", "long");
            this.wordGameService.SubmitWord("player3", "word");
            this.wordGameService.SubmitWord("player1", "a");
            this.wordGameService.SubmitWord("player2", "wordy");
            this.wordGameService.SubmitWord("player2", "adder");
            this.wordGameService.SubmitWord("player3", "lard");
            this.wordGameService.SubmitWord("player4", "loan");
            this.wordGameService.SubmitWord("player5", "lag");
            
            /*
             * wooly
             * really
             * wordy
             * long
             * word
             * lard
             * loan
             * all
             * are
             * lag
             * a - get's dropped
             * */
            Assert.AreEqual("woolly", this.wordGameService.GetWordEntryAtPosition(0));
            Assert.AreEqual("really", this.wordGameService.GetWordEntryAtPosition(1));
            Assert.AreEqual("wordy", this.wordGameService.GetWordEntryAtPosition(2));
            Assert.AreEqual("long", this.wordGameService.GetWordEntryAtPosition(3));
            Assert.AreEqual("word", this.wordGameService.GetWordEntryAtPosition(4));
            Assert.AreEqual("lard", this.wordGameService.GetWordEntryAtPosition(5));
            Assert.AreEqual("loan", this.wordGameService.GetWordEntryAtPosition(6));
            Assert.AreEqual("all", this.wordGameService.GetWordEntryAtPosition(7));
            Assert.AreEqual("are", this.wordGameService.GetWordEntryAtPosition(8));
            Assert.AreEqual("lag", this.wordGameService.GetWordEntryAtPosition(9));
            Assert.AreEqual(null, this.wordGameService.GetWordEntryAtPosition(10));
        }

        [TestMethod]
        public void Nothing_In_Leaderboard_Yet()
        {
            Assert.AreEqual(null, this.wordGameService.GetWordEntryAtPosition(0));
        }

        [TestMethod]
        public void SubmitWord_Can_Handle_Nulls()
        {
            Assert.AreEqual(null, this.wordGameService.SubmitWord("player1", null));
            Assert.AreEqual(null, this.wordGameService.GetPlayerNameAtPosition(0));

            Assert.AreEqual(null, this.wordGameService.SubmitWord(null, null));
            Assert.AreEqual(null, this.wordGameService.GetScoreAtPosition(0));
        }
    }
}
