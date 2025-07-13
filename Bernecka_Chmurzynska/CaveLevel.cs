namespace Bernecka_Chmurzynska
{
    public class CaveLevel : Level
    {
        public CaveLevel()
        {
            levelVisuals = new string[]
            {
                "##############################",
                "<............................#",
                "#............................#",
                "#...................##########",
                "#######.............#######",
                      "#.............#",
                      "###############"

            };

            InitializeLevelData();
        }
    }
}