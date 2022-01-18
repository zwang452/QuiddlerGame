using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    public class Player : IPlayer
    {
        public Player(Deck deck)
        {
            _deck = deck;
        }
        public int CardCount { get => _cardCount; init => _cardCount = value; }
        public int TotalPoints { get => _totalPoints; init => _totalPoints = value; }
        private int _cardCount;
        private int _totalPoints;
        private Deck _deck;
        bool IPlayer.Discard(string card)
        {
            throw new NotImplementedException();
        }

        string IPlayer.DrawCard()
        {
            throw new NotImplementedException();
        }

        string IPlayer.PickUpTopDiscard()
        {
            throw new NotImplementedException();
        }

        int IPlayer.PlayWord(string candidate)
        {
            throw new NotImplementedException();
        }

        int IPlayer.TestWord(string candidate)
        {
            throw new NotImplementedException();
        }

        string IPlayer.ToString()
        {
            throw new NotImplementedException();
        }
    }
}
