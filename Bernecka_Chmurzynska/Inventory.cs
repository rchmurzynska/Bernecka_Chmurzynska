using System;
using System.Collections.Generic;

namespace Bernecka_Chmurzynska
{
    //Represents an item tat can be stored in inventory
    class Item
    {
        public string Name { get; } //Item name
        public string Type { get; } //Item type

        public Item(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    class Inventory
    {
        private Dictionary<string, (Item item, int count)> items = new();

        public void Add(Item item)
        {
            if (items.ContainsKey(item.Name))
            {
                var (existingItem, count) = items[item.Name];
                items[item.Name] = (existingItem, count + 1);
            }
            else
            {
                items[item.Name] = (item, 1);
            }
        }

        public void Use(string name)
        {
            if (!items.ContainsKey(name)) return;
            var (item, count) = items[name];

            if (count > 1)
                items[name] = (item, count - 1);
            else
                items.Remove(name);
        }

        public void Show(int startY, int maxLines = 10)
        {
            ClearArea(startY, maxLines);

            Console.SetCursorPosition(0, startY);
            Console.WriteLine("Inventory");

            int line = 1;
            foreach (var kv in items)
            {
                if (line >= maxLines) break;

                var (item, count) = kv.Value;
                Console.SetCursorPosition(0, startY + line);
                Console.WriteLine($"- {item.Name} ({item.Type}) x{count}");
                line++;
            }
        }

        private void ClearArea(int startY, int maxLines)
        {
            for (int i = 0; i < maxLines + 2; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        public void OpenInventory(int startY, int maxLines = 10)
        {
            Show(startY, maxLines);

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.I)
                {
                    ClearArea(startY, maxLines);
                    break;
                }
            }
        }

        public bool HasItem(string name)
        {
            return items.ContainsKey(name);
        }

        public int Count(string name)
        {
            return items.ContainsKey(name) ? items[name].count : 0;
        }
    }
}