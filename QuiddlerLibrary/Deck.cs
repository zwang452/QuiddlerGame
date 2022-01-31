using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Word = Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    public class Deck : IDeck
    {
        public Deck()
        {
            _cardsFreq = new SortedDictionary<string, int>(new CardComparer());
            _cards = new List<string>();
            CardValues = new Dictionary<string, int>
            {
                {"a", 2}, {"e", 2}, {"i", 2}, {"o", 2},
                {"l", 3}, {"s", 3}, {"t", 3},
                {"u", 4}, {"y", 4},
                {"d", 5}, {"m", 5}, {"n", 5}, {"r", 5},
                {"f", 6}, {"g", 6}, {"p", 6},
                {"h", 7}, {"er", 7}, {"in", 7},
                {"b", 8}, {"c", 8}, {"k", 8},
                {"qu", 9}, {"th", 9},
                {"w", 10}, {"cl", 10},
                {"v", 11}, {"x", 12}, {"j", 13}, {"z", 14}, {"q", 15}
            };
            spellCheckObject = new Word.Application();
            InitilizeDeck();
            About = "Test Client for: Quiddler (TM) Library, © 2022 Zitong/Behnaz";
            CardCount = _cards.Count;
        }
        public string About { get => _about; init => _about = value; }
        public int CardCount { get => _cards.Count; init => _cardCount = value; }
        public int CardsPerPlayer { 
            get => _cardsPerPlayer; 
            set { 
                if(value < 3 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("Invalid input; Try again with a number between 3-10.");
                }
                _cardsPerPlayer = value;
            }    
        }
        public string TopDiscard { 
            get {
                if (_topDiscard != null) return _topDiscard;
                _topDiscard = DrawCard();
                return _topDiscard;
            } 
            init => _topDiscard = value; }
        internal Word.Application spellCheckObject;
        internal Dictionary<string, int> CardValues { get; init; }
        private string _about;
        private int _cardCount;
        private int _cardsPerPlayer;
        private string _topDiscard;
        private SortedDictionary<string, int> _cardsFreq;
        private List<String> _cards;

        public IPlayer NewPlayer()
        {
            List<string> newPlayerHandCards = DealCards(CardsPerPlayer);
            Dictionary<string, int> newPlayerHandCardsFreq = new Dictionary<string, int>();
            newPlayerHandCardsFreq.UpdateFreq(newPlayerHandCards);


            return new Player(this)
            {
                CardCount = CardsPerPlayer,
                CardsAtHand = newPlayerHandCards,
                CardsAtHandFreq = newPlayerHandCardsFreq,
                TotalPoints = 0
            };
        }

        public override string ToString()
        {
            int entryNumber = 0;
            string output = "";
            foreach (KeyValuePair<string, int> entry in _cardsFreq)
            {
                if (entryNumber++ % 12 == 0) output += "\n";
                output += String.Format("{0,-7}",$"{entry.Key}({entry.Value})");
            }

            return output;
        }
        private void ShuffleList()
        {
            Random rand = new Random();
            _cards = _cards.OrderBy(x => rand.Next()).ToList();
        }

        internal List<string> DealCards(int numberOfCards)
        {
            List<string> dealtCard = new List<string>();
            for (int i = 0; i < numberOfCards; ++i)
            {
                if (CardCount == 0) return dealtCard;
                dealtCard.Add(_cards[0]); //Add the card to hand
                _cardsFreq.DiscardUpdateFreq(_cards[0]); //Remove card from frequency table
                _cards.RemoveAt(0); //Remove the card from deck
            }
            return dealtCard;
        }
        internal string DrawCard()
        {
            if (_cards.Count == 0)
            {
                throw new InvalidOperationException();
            }
            string topCard = _cards[0];
            _cards.RemoveAt(0);
            return topCard;
        }
        //internal Dictionary<String, int> GetDeckCards() { return _cardsFreq; }
        internal void ChangeTopDiscardCard(string card)
        {
            _topDiscard = card;
        }
        private void InitilizeDeck()
        {
            _cardsFreq.Clear();
            _cards.Clear();
            foreach (string card in new[]{ "b", "c", "f", "h", "j", "k", "m", "p", "q", "v", "w", "x", "z",
                "cl", "er", "in", "qu", "th"})
            {
                _cardsFreq.Add(card, 2);
                for (int i = 0; i < 2; ++i)
                {
                    _cards.Add(card);
                }
            }
            foreach (string card in new[] { "d", "g", "l", "s", "y" })
            {
                _cardsFreq.Add(card, 4);
                for (int i = 0; i < 4; ++i)
                {
                    _cards.Add(card);
                }
            }
            foreach (string card in new[] { "n", "r", "t", "u" })
            {
                _cardsFreq.Add(card, 6);
                for (int i = 0; i < 6; ++i)
                {
                    _cards.Add(card);
                }
            }
            _cardsFreq.Add("i", 8);
            _cardsFreq.Add("o", 8);
            for (int i = 0; i < 8; ++i)
            {
                _cards.Add("i");
                _cards.Add("o");
            }
            _cardsFreq.Add("a", 10);
            for (int i = 0; i < 10; ++i)
            {
                _cards.Add("a");
            }
            _cardsFreq.Add("e", 12);
            for (int i = 0; i < 12; ++i)
            {
                _cards.Add("e");
            }
            ShuffleList();
        }

    }
}
