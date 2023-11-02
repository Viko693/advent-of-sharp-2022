using System;
using System.IO;

// TO DO
// Read each line from the input file, 
// Split each line  
// Find the character that appears in both compartments.
// Calculate and assign  priority
// Sum up 
// Half string error check


class Day_3a
{
    static void Main_disabled()
    {
        // Read lines 
        var lines = File.ReadAllLines("inputs/Day_3.txt");
        int totalPriority = 0;

        // Loop through each line
        foreach (string line in lines)
        {

            // Check if the line has an even number of characters
            if (line.Length % 2 != 0)
            {
                Console.WriteLine("Error: Line does not have an even number of characters.");
                continue;  // Skip to the next iteration
            }


            // Split the line into two halves
            int halfLength = line.Length / 2;
            string firstHalf = line.Substring(0, halfLength);
            string secondHalf = line.Substring(halfLength, halfLength);

            // Find the common character
            // loop logic :[ if the same character in the loop is contained in the second string ] , give priority points\
            // could make a separate list with the info caught here if needed
            char commonItem = FindCommonItem(firstHalf, secondHalf);

            // If no common item, continue to the next iteration
            if (commonItem == '\0')
            {
                Console.WriteLine("No common item found.");
                continue;
            }
             // Calculate the priority of the common item
            int priority = GetPriority(commonItem);

            // Add the priority to the total
            totalPriority += priority;
        }

        // Output the total priority
        Console.WriteLine("Total Priority: " + totalPriority);
    }

    static char FindCommonItem(string firstHalf, string secondHalf)
    {
        foreach (char c in firstHalf)
        {
            if (secondHalf.Contains(c.ToString()))
            {
                return c;
            }
        }
        return '\0';  // Return null character if no common item
    }

    // Function to get the priority of a character based on the characters' innate number code in the system 
    static int GetPriority(char c)
    {
        if (c >= 'a' && c <= 'z')
        {
            return c - 'a' + 1;
        }
        else if (c >= 'A' && c <= 'Z')
        {
            return c - 'A' + 27;
        }
        else
        {
            return 0;  // Invalid item
        }
        
    }
}