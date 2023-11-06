using System;
using System.IO;
using System.Collections.Generic;
// TO DO
// Map the initial state, stop looping after an empty line 
// Simulate the moving and update the grid
// Output requested top box of each stack 
// test 
// stacking is good here, but also vectors would work 
//in case asked to move items from inside the crates instead of the top
// Note: "StreamReader"  can stop when a condition is met and is lighter on the memory.
class Day_5a
{
    static void Main()
    {
        string filePath = "inputs/Day_5.txt";
        List<Stack<char>> stacks = InitializeStacks(filePath);
        ProcessMoves(filePath, stacks);
        PrintFinalStacks(stacks);
    }

    // Initialize the stacks by reading the initial state from the file
    static List<Stack<char>> InitializeStacks(string filePath)
    {
        List<Stack<char>> stacks = new List<Stack<char>>();
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
            {
                ParseInitialState(line, stacks);
            }
        }
        return stacks;
    }

    // Process the move instructions from the file
    static void ProcessMoves(string filePath, List<Stack<char>> stacks)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            bool readingMoves = false;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    readingMoves = true; // Start reading moves after the initial state
                    continue;
                }
                if (readingMoves)
                {
                    SimulateMove(line, stacks);
                }
            }
        }
    }

    // Parse the initial state and populate the stacks
    static void ParseInitialState(string line, List<Stack<char>> stacks)
    {
        string[] crates = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < crates.Length; i++)
        {
            if (crates[i].StartsWith("[") && crates[i].EndsWith("]"))
            {
                char crateLabel = crates[i].Trim('[', ']')[0];
                while (stacks.Count <= i)
                {
                    stacks.Add(new Stack<char>());
                }
                stacks[i].Push(crateLabel);
            }
        }
    }

    // Simulate a move based on the instruction
    static void SimulateMove(string instruction, List<Stack<char>> stacks)
    {
        string[] parts = instruction.Split(' ');
        int numCrates = int.Parse(parts[1]);
        int fromStack = int.Parse(parts[3]) - 1; // Adjust for 0-based index
        int toStack = int.Parse(parts[5]) - 1; // Adjust for 0-based index

        if (fromStack < stacks.Count && toStack < stacks.Count)
        {
            Stack<char> tempStack = new Stack<char>();
            for (int i = 0; i < numCrates && stacks[fromStack].Count > 0; i++)
            {
                tempStack.Push(stacks[fromStack].Pop());
            }
            while (tempStack.Count > 0)
            {
                stacks[toStack].Push(tempStack.Pop());
            }
        }
    }

    // Print the final state of each stack
    static void PrintFinalStacks(List<Stack<char>> stacks)
    {
        for (int i = 0; i < stacks.Count; i++)
        {
            Console.WriteLine($"Stack {i + 1}: {(stacks[i].Count > 0 ? stacks[i].Peek().ToString() : "Empty")}");
        }
    }
}
