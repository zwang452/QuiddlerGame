using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    internal static class Extensions
    {
        internal static void UpdateFreq(this IDictionary<string, int> freq, IEnumerable<string> cards)
        {
            foreach (string card in cards)
            {
                if (freq.ContainsKey(card))
                {
                    freq[card]++;
                }
                else
                {
                    freq.Add(card, 1);
                }
            }
        }
        internal static void DiscardUpdateFreq(this IDictionary<string, int> freq, string card)
        {
            if(freq.TryGetValue(card, out int count)) freq[card] = count - 1;
        }
        internal static void DrawUpdateFreq(this IDictionary<string,int> freq, string card)
        {
            if (freq.ContainsKey(card))
            {
                freq[card]++;
            }
            else
            {
                freq.Add(card, 1);
            }
        }
    }
}
