﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public interface IPlayer
    {
        public int CardCount { get; init; }
        public int TotalPoints { get; init; }

        public string DrawCard();
        public bool Discard(string card);
        public string PickUpTopDiscard();
        public int PlayWord(string candidate);
        public int TestWord(string candidate);
        public string ToString();
    }
}
