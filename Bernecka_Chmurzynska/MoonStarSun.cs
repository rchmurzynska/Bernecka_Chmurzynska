using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Bernecka_Chmurzynska
{
// handles the Moon-Star-Sun minigame logic
    class MoonStarSunGame
    {
        private Level currentLevel;
        private Action<Level, string[]> writeInBox;
        private int tutorialCount;

        public MoonStarSunGame(Level level, Action<Level, string[]> writeInBoxMethod, int tutorialCount)
        {
            currentLevel = level;
            writeInBox = writeInBoxMethod;
            this.tutorialCount = tutorialCount;
        }

        public bool Play()
        {
            writeInBox(currentLevel, new string[]
            {
                "Let's start a game called Moon-Star-Sun!",
                "Choose your move:",
                "[1] Moon",
                "[2] Star",
                "[3] Sun"
            });

            Dictionary<int, string> moves = new Dictionary<int, string>
            {
                {1, "Moon"},
                {2, "Star"},
                {3, "Sun" }

            };

            int playerChoice = 0;
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1) playerChoice = 1;
                else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2) playerChoice = 2;
                else if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad2) playerChoice = 3;

                if (playerChoice != 0) break;

            }

            Random rng = new Random();
            int npcChoice = rng.Next(1, 4); //1 to 3

            writeInBox(currentLevel, new string[]
            {
                $"You chose: {moves[playerChoice]}",
                $"Gollum chose: {moves[npcChoice]}"
            });

            if (playerChoice == npcChoice)
            {
                writeInBox(currentLevel, new string[]
                {
                    "It's a tie! Try again...",
                    "Press any key to continue..."
                });
                Console.ReadKey(true);
                return Play();
            }

            if (tutorialCount < 3)
            {
                writeInBox(currentLevel, new string[]
                {
                    "You lose this time!",
                    "Press any key to continue..."
                });
                Console.ReadKey(true);
                return false;
            }

            bool playerWins =
            (playerChoice == 1 && npcChoice == 3) ||
            (playerChoice == 2 && npcChoice == 1) ||
            (playerChoice == 3 && npcChoice == 2);

            if (playerWins)
            {
                writeInBox(currentLevel, new string[]
                {
                    "Excellent, you win!",
                    "Press any key to continue..."
                });
                Console.ReadKey(true);
                return true;
            }
            else
            {
                writeInBox(currentLevel, new string[]
                {
                    "Damn, you lose!",
                    "Press any key to continue..."
                });
                Console.ReadKey(true);
                return false;
            }

        }
    }
}