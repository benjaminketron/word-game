namespace WordGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordGame.Model;

    public class WordGameService : IWordGameService
    {
        private List<LeaderBoardEntry> leaderBoard;
        private readonly char[] letters;
        private readonly ILookup<char, char> lettersLookup;
        private readonly IValidWords validWords;
        private readonly HashSet<string> wordsAlreadyPlayed;

        public WordGameService(string letters, IValidWords validWords) : this(letters?.ToCharArray(), validWords) 
        {
        }

        public WordGameService(char[] letters, IValidWords validWords)
        {
            this.leaderBoard = new List<LeaderBoardEntry>();
            this.letters = letters;
            this.lettersLookup = letters?.ToLookup(l => l);
            this.validWords = validWords;
            this.wordsAlreadyPlayed = new HashSet<string>();
        }

        public string GetPlayerNameAtPosition(int position)
        {
            return leaderBoard.Skip(position).FirstOrDefault()?.PlayerName;
        }

        public int? GetScoreAtPosition(int position)
        {
            return leaderBoard.Skip(position).FirstOrDefault()?.Score;
        }

        public string GetWordEntryAtPosition(int position)
        {
            return leaderBoard.Skip(position).FirstOrDefault()?.Word;
        }

        public int? SubmitWord(string playerName, string word)
        {
            var score = null as int?;

            var wordLookup = word?.ToLookup(w => w);

            // only proceed if we are not working with nulls
            if (wordLookup != null &&
                lettersLookup != null)
            {
                // does the starting set contain all the letters of the word?
                var contains = wordLookup.All(xs => xs.Count() <= lettersLookup[xs.Key].Count());
                if (contains)
                {
                    // is this a valid word?
                    if (this.validWords.Contains(word))
                    {
                        score = word.Length;

                        // word has not already been played
                        if (!wordsAlreadyPlayed.Contains(word))
                        {
                            // Does the leaderboard already contain this work?
                            if (!leaderBoard.Any(l => l.Word == word))
                            {
                                var newLeaderBoardEntry = new LeaderBoardEntry()
                                {
                                    PlayerName = playerName,
                                    Word = word,
                                    Score = score.Value
                                };

                                // calculating leaderboard insertion point
                                var entry = leaderBoard.LastOrDefault(l => l.Score >= score);
                                var insertionIndex = 0;
                                if (entry != null)
                                {
                                    insertionIndex = leaderBoard.IndexOf(entry) + 1;
                                }
                                leaderBoard.Insert(insertionIndex, newLeaderBoardEntry);

                                // confining leaderboard to top 10
                                leaderBoard = leaderBoard.Take(10).ToList();
                            }
                        }
                    }                   
                }
            }

            return score;
        }
    }
}
