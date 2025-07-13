using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using Bernecka_Chmurzynska;

namespace Bernecka_Chmurzynska

{
    class Program
    {
        static bool gameEnded = false;
        static int GetUIBoxStartY(Level level)
        {
            return level.GetHeight() + 2;
        }

        static void ClearInteractionBox(Level level, int height = 6)
        {
            int startY = GetUIBoxStartY(level);
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        static void DrawInventoryHint(Level level)
        {
            int boxY = GetUIBoxStartY(level);
            Console.SetCursorPosition(0, boxY - 1);
            Console.WriteLine("[I] to open inventory");
        }

        static void writeInBox(Level level, params string[] lines)
        {
            int boxY = GetUIBoxStartY(level);
            DrawInventoryHint(level);
            ClearInteractionBox(level);
            for (int i = 0; i < lines.Length && i < 6; i++)
            {
                Console.SetCursorPosition(0, boxY + i);
                Console.WriteLine(lines[i]);
            }
        }

        static void EndGame()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("You're free, be safe in your adventure!");
            Console.WriteLine("Press [E] to exit the game");

            gameEnded = true;
            while (true)
            {
                var endKey = Console.ReadKey(true).Key;
                if (endKey == ConsoleKey.E)
                    Environment.Exit(0);
            }
        }

        static void HandleNPCInteraction(Player player, ref bool isInteracting, ref bool questAccepted, ref bool questCompleted, ref bool CanGaveRing, Level level)
        {
            bool hasRing = player.inventory.HasItem("Ring");

            if (questCompleted && hasRing)
            {
                writeInBox(level,
                "Gollum: You found my precious ring!",
                "Agh, I can't kill you now...",
                "If only... I know! Let's play a game.",
                "If you beat me, you can leave, if you don't well..."
                );

                while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
                var rpsGame = new MoonStarSunGame(level, writeInBox, player.inventory.Count("Tutorial"));
                bool won = rpsGame.Play();

                while (!won)
                {
                    writeInBox(level,
                    "Gollum: I can give you one more chance!",
                    "Let's play agian.",
                    "Press [space] to play again...");
                    while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
                    won = rpsGame.Play();
                }
                EndGame();

            }

            writeInBox(level,
            "NPC Interaction:",
            "[T]alk",
            "[Q]uest",
            "[P]lay",
            "[E]xit");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.E)
                {
                    writeInBox(level, "Gollum: Shoosh!");
                    isInteracting = false;
                    break;
                }
                else if (key == ConsoleKey.T)
                {
                    writeInBox(level,
                    "Gollum: Oh, a visitor!",
                    "It's been a quite time since the last time I saw people...",
                    "Press [space] to continue...");
                    while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
                    isInteracting = false;
                    break;
                }
                else if (key == ConsoleKey.Q)
                {
                    if (!questAccepted)
                    {
                        writeInBox(level,
                        "Gollum: I lost my all golden coins...",
                        "Can you find them for me?",
                        "Will you? [Y]es or [N]o");

                        var questKey = Console.ReadKey(true).Key;

                        if (questKey == ConsoleKey.Y)
                        {
                            writeInBox(level,
                            "Task starting now:",
                            "Gollum: These coins must be lying somewhere...",
                            "Collect three golden coins <C> for me.",
                            "Press [E] to exit.");
                            questAccepted = true;
                        }
                        else if (questKey == ConsoleKey.N)
                        {
                            writeInBox(level,
                            "You chose to not start the task",
                            "Gollum: ... I find them by myself then!",
                            "Press [E] to exit.");
                        }
                    }
                    else
                    {
                        int coinsCount = player.inventory.Count("Coins");
                        if (coinsCount >= 3)
                        {
                            writeInBox(level,
                            "Gollum: Ahaha, my coins! My coins!",
                            "I'm glad to see you again!",
                            "Maybe I can trust you now...",
                            "Press [space] to continue...");

                            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }

                            questCompleted = true;
                            CanGaveRing = true;

                            writeInBox(level,
                            "You: How can I leave this place?",
                            "Press [space] to continue...");

                            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }

                            writeInBox(level,
                            "Gollum: You can find coins, so you can find ring!",
                            "Find me ring and I tell you how to find exit...",
                            "Press [space] to continue...");

                            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
                            isInteracting = false;
                            break;
                        }
                        else
                        {
                            writeInBox(level,
                            $"Gollum: You have only {coinsCount} coins...",
                            "I told you, I have 3 golden coins, bring me all of them!");
                        }
                    }
                }

                else if (key == ConsoleKey.P)
                {
                    var rpsGame = new MoonStarSunGame(level, writeInBox, player.inventory.Count("Coin"));
                    bool won = rpsGame.Play();

                    if (won)
                    {
                        writeInBox(level,
                        "Gollum:You defeated me!",
                        "I'll tell you how to exit this place",
                        "Press [E] to exit.");
                    }
                    else
                    {
                        writeInBox(level,
                        "Gollum: ... now I can kill and eat you?!",
                        "Press any key to exit...");
                        Console.ReadKey(true);
                        isInteracting = false;
                        break;
                    }
                }

            }
        }

        static void HandleMimicInteraction(Player player, ref bool ringCollected, bool CanGaveRing, Level level)
        {
            writeInBox(level,
            "Mimic interaction:",
            "Mimic: A human, look at my ring!",
            "You: Oh, yeah, that ring is mine, you stole it!",
            "Press [space] to continue...");

            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
            if (CanGaveRing && !ringCollected)
            {
                writeInBox(level, "Mimic: I didn't steel it! ...but fine, have it!");
                player.inventory.Add(new Item("Ring", "jewelry"));
                ringCollected = true;
            }
            else
            {
                writeInBox(level, "Mimic: What ring?!");
            }
        }

        static void Main(string[] args)
        {
            Level mainLevel = new Level();
            mainLevel.PlaceRandomItem('C', 3);

            Level CaveLevel = new CaveLevel();
            Level currentLevel = mainLevel;

            bool questAccepted = false;
            bool questCompleted = false;
            bool CanGaveRing = false;
            bool isInteracting = false;

            Console.CursorVisible = false;
            var directions = new Dictionary<string, Point>
            {
                ["moveRight"] = new Point(1, 0),
                ["moveUp"] = new Point(0, -1),
                ["moveLeft"] = new Point(-1, 0),
                ["moveDown"] = new Point(0, 1)

            };

            Point randomNPCPosition = mainLevel.GetRandomEmptyPosition();

            Player player = new Player("Adventurer", "A") { position = new Point(3, 3) };
            NonPlayerCharacter npc = new NonPlayerCharacter("Gollum", "G") { position = randomNPCPosition };
            MimicNPC mimic = new MimicNPC("Mimic", "M") { position = new Point(17, 4) };

            List<Character> mainLevelCharacters = new List<Character> { player, npc };
            List<Character> CaveLevelCharacters = new List<Character> { player, mimic };

            currentLevel.Display();
            DrawInventoryHint(currentLevel);
            foreach (var ch in mainLevelCharacters)
                ch.Display();

            while (true)
            {
                if (gameEnded) continue;
                List<Character> currentCharacters = currentLevel == mainLevel ? mainLevelCharacters : CaveLevelCharacters;

                foreach (var ch in currentCharacters)
                    ch.Display();
                for (int i = 0; i < currentCharacters.Count; i++)
                {
                    Character ch = currentCharacters[i];
                    string action = isInteracting ? "Interact" : ch.ChooseAction();

                    if (action == "Inventory" && ch is Player currentPlayer)
                    {
                        int startY = GetUIBoxStartY(currentLevel);
                        currentPlayer.inventory.Show(startY);

                        while (true)
                        {
                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.E) break;
                        }

                        ClearInteractionBox(currentLevel);
                        continue;
                    }
                    if (!directions.ContainsKey(action))
                        continue;

                    currentLevel.RedrawCell(ch.position);
                    ch.Move(directions[action], currentLevel);

                    char cell = currentLevel.GetCellVisuals(ch.position.x, ch.position.y);

                    if (ch is Player playerAtCell && cell == 'C')
                    {
                        if (questAccepted)
                        {
                            writeInBox(currentLevel, "You found a coin, how nice!");
                            playerAtCell.inventory.Add(new Item("Coin", "money"));
                            currentLevel.SetCell(ch.position.x, ch.position.y, '.');
                        }

                        else
                        {
                            writeInBox(currentLevel, "You don't see any coins...");
                        }

                    }
                    if (ch is Player playerEnter && cell == '>')
                    {
                        Console.Clear();
                        currentLevel = CaveLevel;
                        playerEnter.position = new Point(1, 3);
                        DrawInventoryHint(currentLevel);
                        writeInBox(currentLevel, "You're in small cave!");
                        currentLevel.Display();
                        continue;
                    }
                    else if (ch is Player playerExit && cell == '<')
                    {
                        Console.Clear();
                        currentLevel = mainLevel;
                        playerExit.position = new Point(3, 3);
                        DrawInventoryHint(currentLevel);
                        writeInBox(currentLevel, "You're back, you left cave.");
                        currentLevel.Display();
                        continue;
                    }

                    if (ch is Player PlayerInCave && currentLevel == CaveLevel)
                    {
                        foreach (var character in CaveLevelCharacters)
                        {
                            if (character is MimicNPC mimicNPC)
                            {
                                if (Math.Abs(mimicNPC.position.x - PlayerInCave.position.x) +
                                    Math.Abs(mimicNPC.position.y - PlayerInCave.position.y) <= 2)
                                {
                                    HandleMimicInteraction(PlayerInCave, ref ringCollected, CanGaveRing, currentLevel);
                                }
                            }
                        }
                    }
                }

                if (!isInteracting && player != null && currentLevel == mainLevel)
                {
                    if (Math.Abs(npc.position.x - player.position.x) +
                        Math.Abs(npc.position.y - player.position.y) <= 1)
                    {
                        isInteracting = true;
                        HandleNPCInteraction(player, ref isInteracting, ref questAccepted, ref questCompleted, ref CanGaveRing, currentLevel);
                        }
                }

            }
        }
    }
}