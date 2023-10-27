using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day_1b
{
    private class Elf
    {
        public int Number { get; set; }
        public int Calories { get; set; }
    }
    static void Main_disabled()
    {
        // Prepare the input
        var lines = File.ReadAllLines("inputs/Day_1.txt");

        // Initialize variables
        var elves = new List<Elf>();
        var currentElfCalories = 0;
        var elfCounter = 1;

        // Process each line in the input
        foreach (var line in lines)
        {
            ProcessLine(line, ref currentElfCalories, ref elfCounter, elves);
        }

        // Add the last elf
        elves.Add(new Elf { Number = elfCounter, Calories = currentElfCalories });

        // Sort the elves by calories and take the top 3
        var topElves = elves.OrderByDescending(e => e.Calories).Take(3).ToList();

        // Calculate the total calories of the top 3 elves
        var totalCalories = topElves.Sum(e => e.Calories);

        // Output the results
        Console.WriteLine($"Total calories of top 3 elves: {totalCalories}");

        // Extra output for clarity
        foreach (var elf in topElves)
        {
            Console.WriteLine($"Elf {elf.Number} has {elf.Calories} calories.");
        }
    }

    static void ProcessLine(string line, ref int currentElfCalories, ref int elfCounter, List<Elf> elves)
    {
        if (string.IsNullOrEmpty(line))
        {
            elves.Add(new Elf { Number = elfCounter, Calories = currentElfCalories });
            currentElfCalories = 0;
            elfCounter++;
        }
        else
        {
            currentElfCalories += int.Parse(line);
        }
    }
}
