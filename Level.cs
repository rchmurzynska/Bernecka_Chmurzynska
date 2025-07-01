class Level
{
    string[] levelVisuals =
        [
            "###########################################",
            "#.........................................#",
            "#.........................................#",
            "#.........########........................#",
            "#.........................................#",
            "#.........................................#",
            "#........................########.........#",
            "#.........................................#",
            "######....................................#",
            "#.........................................#",
            "#.........................................#",
            "#....................................######",
            "#.........................................#",
            "#.........................................#",
            "#.........................................#",
            "#.......########..........................#",
            "#.........................................#",
            "#........................#####............#",
            "#.........................................#",
            "#.........................................#",
            "###########################################",

        ];

    List<List<Cell>> levelData;
    public Level()
    {
        levelData = new List<List<Cell>>();

        for (int y = 0; y < levelVisuals.Length; y++)
        {
            List<Cell> row = new List<Cell>();
            for (int x = 0; x < levelVisuals[y].Length; x++)
            {
                char cellVisuals = levelVisuals[y][x];
                Cell cell = new Cell(cellVisuals);
                row.Add(cell);
            }
            levelData.Add(row);
        }
    }

    public void Display()
    {
        foreach (List<Cell> row in levelData)
        {
            foreach (Cell cell in row)
            {
                cell.Display();
            }
            Console.WriteLine();
        }
    }
    public char GetCellVisuals(int x, int y)
    {
        return levelVisuals[y][x];
    }
    public int GetHeight()
    {
        return levelVisuals.Length;
    }
    public int GetWidth(int rowNumber)
    {
        return levelVisuals[rowNumber].Length;
    }

    public void RedrawCell(Point position)
    {
        Consol.SetCursorPosition(position.x, position.y);
        char cellValue = GetCellVisuals(position.x, position.y);
        Console.Write(cellValue);
    }
}