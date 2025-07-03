class Cell

{
    char visuals;

    public Cell(char visuals)
    {
        this.visuals = visuals;
    }
    internal void Display()
    {
        Console.Write(visuals);
    }   
}