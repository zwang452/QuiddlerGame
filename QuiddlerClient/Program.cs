using System;
using System.Collections.Generic;
using QuiddlerLibrary;

namespace QuiddlerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IDeck deck = new Deck();

            Console.WriteLine(deck.About);
            Console.WriteLine($"Deck initialized with the following {deck.CardCount} cards...");
            Console.WriteLine(deck.ToString());
            //int age = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine(deck.CardsPerPlayer);

            bool valid = false;
            int numberOfPlayers = 0;
            while (!valid)
            {
                Console.Write("How many players are there? (1-8): ");
                valid = int.TryParse(Console.ReadLine(), out numberOfPlayers);
                if (!valid || numberOfPlayers < 1 || numberOfPlayers > 8)
                {
                    valid = false;
                    Console.WriteLine("Invalid input");
                }
            }


            valid = false;
            int cardsPerPlayer = 0;
            while (!valid)
            {
                Console.Write("How many cards will be dealt to each player? (3-10): ");
                try
                {
                    valid = int.TryParse(Console.ReadLine(), out cardsPerPlayer);
                    deck.CardsPerPlayer = cardsPerPlayer;
                }
                catch(Exception e) {
                    valid = false;
                    Console.WriteLine(e.Message);
                }
            }

            List<IPlayer> playerList = new List<IPlayer>();
            for(int i = 0; i < numberOfPlayers; ++i)
            {
                playerList.Add(deck.NewPlayer());
            }
            Console.WriteLine($"Cards were dealt to {numberOfPlayers} player(s).");
            Console.WriteLine($"The top card which was '{deck.TopDiscard}' was moved to the discard pile\n");

            bool endOfGame = false;
            while (!endOfGame)
            {
                for(int i = 0; i < playerList.Count; ++i)
                {
                    IPlayer currentPlayer = playerList[i];
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"Player {i+1} ({currentPlayer.TotalPoints} points)");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"\nThe deck now contains the following {deck.CardCount} cards...");
                    Console.WriteLine(deck.ToString());
                    Console.WriteLine();
                    Console.WriteLine($"Your cards are {currentPlayer.ToString()}");
                    PickFromDiscardPile(currentPlayer, deck);
                    TestAndPlayWord(currentPlayer, deck);
                    DiscardCard(currentPlayer);

                }

                endOfGame = EndRound(playerList);
            }


        }
        public static void PickFromDiscardPile(IPlayer player, IDeck deck)
        {
            bool validInput = false;
            do
            {
                Console.Write($"Do you want the top card in the discard pile which is '{deck.TopDiscard}'? (y/n): ");
                string command = Console.ReadLine();
                if(command.ToLower() == "y")
                {
                    player.PickUpTopDiscard();
                    validInput = true;

                }
                else if(command.ToLower() == "n")
                {
                    Console.WriteLine($"The dealer dealt '{player.DrawCard()}' to you from the deck.");
                    Console.WriteLine($"The deck contains {deck.CardCount} cards.");
                    validInput = true;
                }
                else
                {
                     Console.WriteLine($"{Environment.NewLine}Invalid input, please try again!");
                    
                }
            } while (!validInput);
            Console.WriteLine($"Your cards are {player.ToString()}");
        }
        public static void TestAndPlayWord(IPlayer player, IDeck deck)
        {
            bool validInput = false;
            do
            {
                Console.Write($"Test a word for its points value? (y/n): ");
                string command = Console.ReadLine();
                if (command.ToLower() == "y")
                {
                    Console.Write($"Enter a word using {player.ToString()} leaving a space between cards: ");
                    string candidate = Console.ReadLine();
                    int score = player.TestWord(candidate);
                    Console.WriteLine($"The word {candidate} is worth {score} points.");
                    if (score > 0) {
                        validInput = PlayWord(player, deck, candidate);
                    }
                }
                else if (command.ToLower() == "n")
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Invalid input, please try again!");

                }
            } while (!validInput);
        }

        public static bool PlayWord(IPlayer player, IDeck deck, string word)
        {
            bool validInput = false;
            bool playedCard = false;
            int score = 0;
            do
            {
                Console.Write($"Do you want to play the word {word}? (y/n): ");
                string playCommand = Console.ReadLine();
                if (playCommand == "y")
                {
                    score = player.PlayWord(word);
                    Console.WriteLine($"Your card are {player.ToString()} and you have {player.TotalPoints} points");
                    validInput = true;
                    playedCard = true;
                }
                else if (playCommand.ToLower() == "n")
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Invalid input, please try again!");

                }
            } while (!validInput);
            return playedCard;
        }
        public static void DiscardCard(IPlayer player)
        {
            bool validInput = false;
            do
            {
                Console.Write($"Enter a card from your hand to drop on the discard pile: ");
                string card = Console.ReadLine();
                if (validInput = player.Discard(card))
                {
                    Console.WriteLine($"Your cards are {player.ToString()}.");
                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Invalid input, please try again!");

                }
            } while (!validInput);
        }
        public static bool EndRound(List<IPlayer> players)
        {
            foreach(IPlayer player in players)
            {
                if(player.CardCount == 0)
                {
                    Stats(players);
                    return true;
                }
            }
            bool validInput = false;
            do
            {
                Console.Write($"Would you like each player to take another turn? (y/n): ");
                string command = Console.ReadLine();
                if (command.ToLower() == "n")
                {
                    Stats(players);
                    return true;

                }
                else if (command.ToLower() == "y")
                {
                    validInput = true;
                }
                else{
                    Console.WriteLine($"{Environment.NewLine}Invalid input, please try again!");

                }
            } while (!validInput);
            return false;
        }
        public static void Stats(List<IPlayer> players)
        {
            Console.WriteLine($"\nRetiring the game.\n\n" +
                                     $"The final scores are...\n" +
                                     $"{new string('-', 80)}\n");
            for (int i = 0; i < players.Count; ++i)
            {
                Console.WriteLine($"Player {i + 1}: {players[i].TotalPoints} points");
            }
        }
    }
}
