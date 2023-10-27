using System;
using System.IO;

class Day_2b
{
    static void Main_disabled()
    {
        // Read lines from the input file
        var lines = File.ReadAllLines("inputs/Day_2.txt");
        int totalScore = 0;

        // Loop through each line 
        foreach (string line in lines)
        {
            char opponentMove = line[0];
            char desiredOutcome = line[2];

            // Determine the move  
            char yourActualMove = DetermineMove(opponentMove, desiredOutcome);

            // Get the score  
            int moveScore = GetMoveScore(yourActualMove);

            // Get the outcome  
            int outcomeScore = GetOutcomeScore(opponentMove, yourActualMove);

            // Update the total score
            totalScore += moveScore + outcomeScore;
        }

        // Output the total score
        Console.WriteLine("Total Score: " + totalScore);
    }

    // Get the score for your move
    static int GetMoveScore(char yourActualMove)
    {
        switch (yourActualMove)
        {
            case 'R': return 1; //Rock
            case 'P': return 2; //Paper
            case 'S': return 3; //Scissors
            default: return 0;
        }
    }

    // Determine the move based on the test input
    static char DetermineMove(char opponentMove, char desiredOutcome)
    {
        switch ((opponentMove, desiredOutcome))
        {
            case ('A', 'X'): return 'S';
            case ('A', 'Y'): return 'R';
            case ('A', 'Z'): return 'P';
            case ('B', 'X'): return 'R';
            case ('B', 'Y'): return 'P';
            case ('B', 'Z'): return 'S';
            case ('C', 'X'): return 'P';
            case ('C', 'Y'): return 'S';
            case ('C', 'Z'): return 'R';
            default: return 'R';
        }
    }

    // Determine the outcome 
    static int DetermineOutcome(char opponentMove, char yourActualMove)
    {
        switch ((opponentMove, yourActualMove))
        {
            case ('A', 'P'):
            case ('B', 'S'):
            case ('C', 'R'):
                return 6; //case of win
            case ('A', 'R'):
            case ('B', 'P'):
            case ('C', 'S'):
                return 3; // case of draw
            default:
                return 0; // case of loss
        }
    }

    // Get the outcome ( Separated from Determine Outcome in case of needed future extensions or adjustments)
    static int GetOutcomeScore(char opponentMove, char yourActualMove)
    {
        return DetermineOutcome(opponentMove, yourActualMove);
    }
}


