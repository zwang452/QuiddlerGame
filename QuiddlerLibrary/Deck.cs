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
            _cardsFreq = new Dictionary<string, int>();
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
            InitilizeDeck();
            CardCount = _cards.Count;
        }
        public string About { get => _about; init => _about = value; }
        public int CardCount { get => _cardCount; init => _cardCount = value; }
        public int CardsPerPlayer { get => _cardsPerPlayer; set => _cardsPerPlayer = value; }
        public string TopDiscard { get => _topDiscard; init => _topDiscard = value; }
        internal Word.Application SpellCheckObject { get; }
        internal Dictionary<string, int> CardValues { get; init; }
        private string _about;
        private int _cardCount;
        private int _cardsPerPlayer;
        private string _topDiscard;
        private Dictionary<string, int> _cardsFreq;
        private List<String> _cards;

        public IPlayer NewPlayer()
        {
            List<string> newPlayerHandCards = DealCards(CardsPerPlayer);
            Dictionary<string, int> newPlayerHandCardsFreq = new Dictionary<string, int>();
            newPlayerHandCardsFreq.UpdateFreq(newPlayerHandCards);
            return new Player(new Deck())
            {
                CardCount = CardsPerPlayer,
                CardsAtHand = newPlayerHandCards,
                CardsAtHandFreq = newPlayerHandCardsFreq,
                TotalPoints = 0
            };
        }

        public override string ToString()
        {
            _about = "sd";
            return "a";
        }
        private void ShuffleList(List<string> list)
        {
            Random rand = new Random();
            list = list.OrderBy(x => rand.Next()).ToList();
        }

        internal List<string> DealCards(int numberOfCards)
        {
            List<string> dealtCard = new List<string>();
            foreach (string card in _cards)
            {
                _cards.Remove(card); //Remove the card from deck
                dealtCard.Add(card); //Add the card to hand

                //Remove card from frequency table
                _cardsFreq.DiscardUpdateFreq(card);
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
            ShuffleList(_cards);
           
        }

    }
}
