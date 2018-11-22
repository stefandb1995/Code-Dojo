using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        private static IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          };
            for (int i = 0; i < 15; i++)
            {
                UpdateQuality();
            }

            System.Console.ReadKey();

        }

        private static void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if ((item.Name != "Sulfuras, Hand of Ragnaros") && (item.Quality < 50 && item.Quality >= 0))
                {
                    if (item.Name == "Aged Brie")
                    {
                        ProcessCheese(item);
                    }
                    else if (item.Name.Contains("Backstage"))
                    {
                        ProcessPass(item);
                    }
                    else if (item.Name.Contains("Conjured"))
                    {
                        ProcessConjured(item);
                    }
                    else
                    {
                        ProcessItem(item);
                    }
                }

                if (item.Quality <= 0)
                {
                    item.Quality = 0;
                }
            }
        }

        private static void ProcessConjured(Item item)
        {
            item.Quality = item.Quality - 2;
            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = item.Quality - 2;
            }
        }

        private static void ProcessPass(Item item)
        {
            item.SellIn--;
            if (item.SellIn > 10)
            {
                item.Quality++;
            }
            else if (item.SellIn <= 10 && item.SellIn > 5)
            {
                item.Quality = item.Quality + 2;
            }
            else if (item.SellIn >= 5 && item.SellIn > 0)
            {
                item.Quality = item.Quality + 3;
            }
            else if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }

        private static void ProcessCheese(Item item)
        {
            item.Quality++;
            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality++;
            }
        }

        private static void ProcessItem(Item item)
        {
            item.Quality--;
            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality--;
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
