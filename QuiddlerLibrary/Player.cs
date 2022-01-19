using System;
using System.Collections.Generic;
using Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    internal class Player : IPlayer
    {
        public Player(Deck deck)
        {
            _deck = deck;
        }
        public int CardCount { get => _cardCount; init => _cardCount = CardsAtHand.Count; }
        public int TotalPoints { get => _totalPoints; init => _totalPoints = value; }
        internal List<string> CardsAtHand { get; set; }
        internal Dictionary<string, int> CardsAtHandFreq { get; set; }
        private Deck _deck;
        private int _cardCount;
        private int _totalPoints;

        public bool Discard(string card)
        {
            if (CardsAtHand.Contains(card))
            {
                CardsAtHand.Remove(card);
                _cardCount = CardsAtHand.Count;
                _deck.ChangeTopDiscardCard(card);
                //Remove card from frequency table
                CardsAtHandFreq.DiscardUpdateFreq(card);
                return true;
            }
            return false;
        }

        public string DrawCard()
        {
            string cardToDraw = _deck.DrawCard();//throws exception if deck is empty
            CardsAtHand.Add(cardToDraw);
            CardsAtHandFreq.DrawUpdateFreq(cardToDraw);
            return cardToDraw;
        }

        public string PickUpTopDiscard()
        {
            string topDiscardCard = _deck.TopDiscard;
            CardsAtHand.Add(topDiscardCard);
            _cardCount = CardsAtHand.Count;
            return topDiscardCard;
        }

        public int PlayWord(string candidate)
        {
            int score = TestWord(candidate);
            if(score != 0)
            {
                _totalPoints += score;
                string[] candidateArray = candidate.Split(" ");
                foreach(string card in candidateArray)
                {
                    CardsAtHand.Remove(card);
                    CardsAtHandFreq.DiscardUpdateFreq(card);
                }
            }
            return score;
        }

        public int TestWord(string candidate)
        {
            int score = 0;
            string[] candidateArray = candidate.Split(" ");
            // the player must have not used all their cards to form the word
            if (candidateArray.Length <= CardCount) return score;
            Dictionary<string, int> candidateFreq = new Dictionary<string, int>();
            candidateFreq.UpdateFreq(candidateArray);
            string candidateString = "";
            // test if a real word
            foreach (string card in candidateArray)
            {
                candidateString += card;
            }
            if (!_deck.SpellCheckObject.CheckSpelling(candidateString)) return score;
            //) the letters of the candidate string mustb be a subset of the letters in the current rack object
            foreach (string card in candidateFreq.Keys)
            {
                if (!CardsAtHandFreq.ContainsKey(card)) return score;
                if (CardsAtHandFreq[card] != candidateFreq[card]) return score;
                score += _deck.CardValues[card];
            }
            return score;

        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}

