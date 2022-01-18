using System;
using System.Collections.Generic;

namespace QuiddlerLibrary
{
    public class Deck : IDeck
    {
        public Deck() {
            InitilizeDeck();
            CardCount = _cards.Count;
        }
        public string About { get => _about; init => _about = "Zitong Wang"; }
        public int CardCount { get => _cardCount; init => _cardCount = value; }
        public int CardsPerPlayer { get => _cardsPerPlayer; set => _cardsPerPlayer = value; }
        public string TopDiscard { get => _topDiscard; init => _topDiscard = value; }
        private string _about;
        private int _cardCount;
        private int _cardsPerPlayer;
        private string _topDiscard;
        private Dictionary<string, int> _cards = new Dictionary<string, int>();

        public IPlayer NewPlayer()
        {
            return new Player(new Deck())
            {
                CardCount = 5
            };
        }

        public string toString()
        {
            _about = "sd";
            return "a";
        }

        private void InitilizeDeck()
        {
            _cards.Clear();
            foreach (string card in new []{ "b", "c", "f", "h", "j", "k", "m", "p", "q", "v", "w", "x", "z", 
                "cl", "er", "in", "qu", "th"})
            {
                _cards.Add(card, 2);
            }
            foreach (string card in new[]{ "d", "g", "l", "s", "y" })
            {
                _cards.Add(card, 4);
            }
            foreach (string card in new[]{ "n", "r", "t", "u" })
            {
                _cards.Add(card, 6);
            }
            _cards.Add("i", 8);
            _cards.Add("o", 8);
            _cards.Add("a", 10);
            _cards.Add("e", 12);
        }
    }
}
