using System;
using System.IO;
// TO DO
//  Read the puzzle input
// Parse the info and loop through it
// check for overlap
// output result

class Day_4a
{
    static void Main_disabled()
    {
        var lines = System.IO.File.ReadAllLines("inputs/Day_4.txt");
        int totalOverlapNumber = 0;

        foreach (string line in lines)
        {
            try
            {
                // Split the line into two parts to get the section assignments for each Elf
                string[] assignments = line.Split(',');

                // Parse the range for the first Elf
                string[] range1 = assignments[0].Split('-');
                int elf1Min = int.Parse(range1[0]);
                int elf1Max = int.Parse(range1[1]);

                // Parse the range for the second Elf
                string[] range2 = assignments[1].Split('-');
                int elf2Min = int.Parse(range2[0]);
                int elf2Max = int.Parse(range2[1]);

                // Check if one range fully contains the other
                if ((elf1Min <= elf2Min && elf1Max >= elf2Max) || (elf2Min <= elf1Min && elf2Max >= elf1Max))
                {
                    totalOverlapNumber++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing line: " + line);
                Console.WriteLine(ex.Message);
            }
        }

        // Output the result
        Console.WriteLine("Total Overlap Number: " + totalOverlapNumber);
    }
}
