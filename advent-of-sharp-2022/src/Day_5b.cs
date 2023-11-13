// TO DO
//Change the logic in the simulate move function
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day5_b
{
    static void Main_disabled()
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

// This time we are going to add a temporary stack that we will put the items in and always reverse the order
// - This way if 1 item is placed nothing happens, but if more are placed they will be reversed accordingly
    static void SimulateMove(string instruction, List<Stack<char>> stacks)
    {
        string[] parts = instruction.Split(' ');
        int numCrates = int.Parse(parts[1]);
        int fromStack = int.Parse(parts[3]) - 1;
        int toStack = int.Parse(parts[5]) - 1;

        if (fromStack < stacks.Count && toStack < stacks.Count)
        {
            List<char> cratesToMove = new List<char>();

            // Extract crates from the source stack
            for (int i = 0; i < numCrates && stacks[fromStack].Count > 0; i++)
            {
                cratesToMove.Add(stacks[fromStack].Pop());
            }

            // Reverse the order of the extracted crates
            cratesToMove.Reverse();

            // Move the crates to the destination stack
            foreach (char crate in cratesToMove)
            {
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