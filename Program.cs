Consol.CursorVisible = false;

Dctionary<string, Point> directionsMap = new Dictionary<string, Point>();
directionsMap.Add("moveLeft", new Point(-1, 0));
directionsMap.Add("moveRight", new Point(1, 0));
directionsMap.Add("moveUp", new Point(0, -1));
directionsMap.Add("moveDown", new Point(0, 1));

Point startingPoint = new Point(18, 1);
Player hero = new Player("", "");
hero.speed = 1;
hero.position = startingPoint;

List<Character> characters = new List<Character>();
characters.Add(hero);