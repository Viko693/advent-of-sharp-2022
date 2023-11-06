using System;
using System.IO;
// TO DO
// Change Overlap Logic from 4a
// For them to overlap one side's minimum has to always be lower than the other side's maximum 
// and have the same apply in reverse to them . 
// The Elf1 Min has to be lower than the Elf2 max AND     Elf 1: 2 3 4 5 6
// The Elf 2 Min has to be lower than the Elf1 Max        Elf 2:     4 5 6 7 8

class Day_4b
{
    static void Main_disabled()
    {
        var lines = File.ReadAllLines("inputs/Day_4.txt");
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
                if (elf1Min <= elf2Max && elf1Max >= elf2Min)
                {
                    totalOverlapNumber++;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error parsing line: " + line);
                Console.WriteLine("Exception message: " + ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Number in line is too large to parse: " + line);
                Console.WriteLine("Exception message: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error occurred: " + ex.Message);
            }
        }

        // Output the result
        Console.WriteLine("Total Overlap Number: " + totalOverlapNumber);
    }
}
