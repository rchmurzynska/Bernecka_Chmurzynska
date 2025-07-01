class Player : Character
{
    Dictionary<ConsoleKey, string> keyActionMap = new Dictionary<ConsoleKey, string>
        {
            { ConsoleKey.A, "moveLeft" },
            { ConsoleKey.D, "moveRight" },
            { ConsoleKey.W, "moveUp" },
            { ConsoleKey.S, "moveDown" },
            { ConsoleKey.C, "clone" }
    };
    
}