using System;
using System.Collections.Generic;
using Extensions;
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
        public int CardCount { get => _cardCount; init => _cardCount = CardsAtHand.Count; }
        public int TotalPoints { get => _totalPoints; init => _totalPoints = value; }
        internal List<string> CardsAtHand { get; set; }
        internal Dictionary<string, int> CardsAtHandFreq { get; set; }
        internal Deck _deck;
        private int _cardCount;
        private int _totalPoints;

        bool IPlayer.Discard(string card)
        {
            if (CardsAtHand.Contains(card))
            {
                _deck.ChangeTopDiscardCard(card);
                _cardCount--;
                //Remove card from frequency table
                CardsAtHandFreq.DiscardUpdateFreq(card);
                return true;
            }
            return false;
        }

        string IPlayer.DrawCard()
        {
            string cardToDraw = _deck.DrawCard();//throws exception if deck is empty
            CardsAtHand.Add(cardToDraw);
            CardsAtHandFreq.DrawUpdateFreq(cardToDraw);
            return cardToDraw;
        }

        string IPlayer.PickUpTopDiscard()
        {
            string topDiscardCard = _deck.TopDiscard;
            CardsAtHand.Add(topDiscardCard);
            _cardCount = CardsAtHand.Count;
            return topDiscardCard;
        }

        int IPlayer.PlayWord(string candidate)
        {
            throw new NotImplementedException();
        }

        int IPlayer.TestWord(string candidate)
        {
            //string[] words = candidate.Split(" ");
            //Dictionary<string, int> candidateFreq = new Dictionary<string, int>();
            //candidateFreq.UpdateFreq(words);
            throw new NotImplementedException();
        }

        string IPlayer.ToString()
        {
            throw new NotImplementedException();
        }
    }
}
