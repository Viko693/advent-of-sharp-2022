using System;
using System.IO;

class Question_1_Solution {
    static void Main() {
        // Read all lines from the input.txt file into an array
        string[] lines = File.ReadAllLines("inputs/Day_1.txt");

        int maxCalories = 0; //  store the maximum calories
        int maxCaloriesElf = 0; //  store the elf number with maximum calories
        int currentElfCalories = 0; //  store the current elf's total calories
        int elfCounter = 1; //  keep track of the elf number

        // Loop through each line in the input
        for (int i = 0; i < lines.Length; i++) {
            // Check if the line is empty (new elf's data starts)
            if (string.IsNullOrEmpty(lines[i])) {
                // Check if the current elf has more calories than the max so far
                if (currentElfCalories > maxCalories) {
                    maxCalories = currentElfCalories;
                    maxCaloriesElf = elfCounter;
                }
                // Reset for the next elf
                currentElfCalories = 0;
                elfCounter++;
                continue;
            }

            // Parse the calories from the line and add to the current elf's total
            try {
                int calories = int.Parse(lines[i]);
                currentElfCalories += calories;
            } catch (FormatException e) {
                Console.WriteLine("Error parsing line: " + lines[i]);
            }
        }

        // Output 
        Console.WriteLine("Elf " + maxCaloriesElf + " has the most calories: " + maxCalories);
    }
}
