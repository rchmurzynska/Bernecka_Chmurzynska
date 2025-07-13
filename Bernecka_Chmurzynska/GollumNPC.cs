namespace Bernecka_Chmurzynska;

class NonPlayerCharacter : Character

{
    public bool isActive { get; set; } = true;
    private Random random = new Random();

    private int actionCounter = 0;

    public NonPlayerCharacter(string name, string symbol) : base(name, symbol) { }

    public override string ChooseAction()
    {
        if (!isActive)
            return "";
        actionCounter++;

        if (actionCounter < 2)
        {
            return "";
        }
        else
        {
            actionCounter = 0;
            string[] moves = { "moveLeft", "moveUp", "moveRight", "MoveDown" };
            int index = random.Next(moves.Length);
            return moves[index];
        }
    }
}