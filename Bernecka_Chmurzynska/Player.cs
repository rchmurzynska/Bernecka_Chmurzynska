namespace Bernecka_Chmurzynska
{
    class Player : Character
    {
        public Inventory inventory = new Inventory();

        Dictionary<ConsoleKey, string> keyMap = new Dictionary<ConsoleKey, string>
        {
            { ConsoleKey.A, "moveLeft" },
            { ConsoleKey.W, "moveUp" },
            { ConsoleKey.D, "moveRight" },
            { ConsoleKey.S, "moveDown" },
            { ConsoleKey.I, "Inventory" }
        };

        public string chosenAction = "pass";

        public Player(string name, string avatar) : base(name, avatar) { }
        public override string ChooseAction()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            chosenAction = keyMap.GetValueOrDefault(key.Key, "pass");
            return chosenAction;
        }
    }
}