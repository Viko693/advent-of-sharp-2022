// TO DO
// Map the initial state, stop looping after an empty line 
// Reverse that state to circumvent logic lapse during parsing
// Simulate the moving and update the grid
// Output requested top box of each stack 

// Notes
// stacking is good here, but also vectors would work -
// -in case asked to move items from inside the crates instead of the top
// "StreamReader"  can stop when a condition is met and is lighter on the memory.
// Creating a stack preemptively seems to be the way to go, because in C# -
// - you cannot add an item to a non existing stack and create one with that action. \
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day5_a
{
    static void Main()
    {
        string filePath = "inputs/Day_5.txt";

        List<Stack<char>> stacks = InitializeStacks(filePath);

        // Debug log 
        Console.WriteLine("State of the stacks after parsing and before reversing:");
        PrintStacks(stacks);

        // Reverse the stacks to maintain the correct order
        ReverseStacks(stacks);

        // Debug log
        Console.WriteLine("State of the stacks after reversing:");
        PrintStacks(stacks);

        // Process the move instructions
        ProcessMoves(filePath, stacks);

        // Debug log 
        Console.WriteLine("State of the stacks after all simulations:");
        PrintStacks(stacks);

        // Print the final state of the stacks
        PrintFinalStacks(stacks);
    }

    // Initializes the stacks from the file, parsing the initial state
    static List<Stack<char>> InitializeStacks(string filePath)
    {
        List<Stack<char>> stacks = new List<Stack<char>>();
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            // Read each line until a blank line is encountered
            while ((line = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
            {
                // Parse the line to initialize the state of the stacks
                ParseInitialState(line, stacks);
            }
        }
        return stacks;
    }

    // Parses a single line of the initial state and fills the stacks accordingly
    static void ParseInitialState(string line, List<Stack<char>> stacks)
    {
        int stackIndex = 0; // Keeps track of which stack we're on
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '[')
            {
                // If we find a crate, get its label and add it to the current stack
                char crateLabel = line[i + 1];
                stackIndex = i / 4; // Calculate the stack index based on position in line
                // Ensure we have enough stacks to accommodate the current index
                while (stacks.Count <= stackIndex)
                {
                    stacks.Add(new Stack<char>());
                }
                // Add the crate to the appropriate stack
                stacks[stackIndex].Push(crateLabel);
                i += 3; // Skip the next 3 characters as they are part of the crate syntax
            }
            else if (line[i] == ' ' && (i % 4 == 0))
            {
                // If we encounter a space at the right interval, move to the next stack
                stackIndex++;
            }
        }
    }

    // Reverses the stacks to ensure the bottom of the stack is the first element
    static void ReverseStacks(List<Stack<char>> stacks)
    {
        for (int i = 0; i < stacks.Count; i++)
        {
            var stack = stacks[i];
            var reversedStack = new Stack<char>();
            // Pop all elements from the current stack and push them onto a new stack
            while (stack.Count > 0)
            {
                reversedStack.Push(stack.Pop());
            }
            // Replace the original stack with the reversed stack
            stacks[i] = reversedStack;
        }
    }

    // Processes the move instructions from the file
    static void ProcessMoves(string filePath, List<Stack<char>> stacks)
    {
        string[] lines = File.ReadAllLines(filePath);
        bool startProcessingMoves = false; // Flag to start processing moves after initial state
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                // Once we encounter a blank line, we start processing move instructions
                startProcessingMoves = true;
                continue;
            }
            if (startProcessingMoves)
            {
                // Process each move instruction
                SimulateMove(line, stacks);
            }
        }
    }

    // Simulates a move instruction on the stacks
    static void SimulateMove(string instruction, List<Stack<char>> stacks)
    {
        string[] parts = instruction.Split(' ');
        int numCrates = int.Parse(parts[1]); // Number of crates to move
        int fromStack = int.Parse(parts[3]) - 1; // Source stack index (0-based)
        int toStack = int.Parse(parts[5]) - 1; // Destination stack index (0-based)

        // Check if the stack indices are within bounds
        if (fromStack < stacks.Count && toStack < stacks.Count)
        {
            // Move the specified number of crates from the source to the destination stack
            for (int i = 0; i < numCrates && stacks[fromStack].Count > 0; i++)
            {
                char crate = stacks[fromStack].Pop();
                stacks[toStack].Push(crate);
            }
        }
    }

    // Prints the current state of each stack
    static void PrintStacks(List<Stack<char>> stacks)
    {
        for (int i = 0; i < stacks.Count; i++)
        {
            Console.Write($"Stack {i + 1}: ");
            // Print each crate in the stack from bottom to top
            foreach (char crate in stacks[i])
            {
                Console.Write(crate + " ");
            }
            Console.WriteLine(); // Newline for the next stack
        }
    }

    // Prints the top item of each stack
    static void PrintFinalStacks(List<Stack<char>> stacks)
    {
        Console.WriteLine("Top Items On Each Stack:");
        for (int i = 0; i < stacks.Count; i++)
        {
            // Print the top item or 'Empty' if the stack is empty
            Console.WriteLine($"Stack {i + 1}: {(stacks[i].Count > 0 ? stacks[i].Peek().ToString() : "Empty")}");
        }
    }
}