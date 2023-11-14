using System;
using System.IO;
// read from three lines at a time instead of separating 1 line in half and disecting it
class Day_3b
{
    static void Main_disabled()
    {
        var lines = System.IO.File.ReadAllLines("inputs/Day_3.txt");
        int totalPriority = 0;

        for (int i = 0; i < lines.Length; i += 3)
        {
            string firstElf = lines[i];
            string secondElf = lines[i + 1];
            string thirdElf = lines[i + 2];

            char commonItem = FindCommonItem(firstElf, secondElf, thirdElf);
            int priority = GetItemPriority(commonItem);

            totalPriority += priority;
        }

        Console.WriteLine("Total Priority: " + totalPriority);
    }

    static char FindCommonItem(string first, string second, string third)
    {
        foreach (char c1 in first)
        {
            if (second.Contains(c1) && third.Contains(c1))
            {
                return c1;
            }
        }
        return ' ';
    }

    static int GetItemPriority(char item)
    {
        if (item >= 'a' && item <= 'z')
        {
            return item - 'a' + 1;
        }
        else if (item >= 'A' && item <= 'Z')
        {
            return item - 'A' + 27;
        }
        return 0;
    }
}
