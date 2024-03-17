using JogoDoOito.Model;
using System;
using System.Collections.Generic;

class Program
{
    public static int[,] Entrada = {
        {0, 1, 2},
        {3, 4, 5},
        {6, 7, 8}
    };
     
    public static void Main(string[] args)
    {
        int[,] initialState = {
            {7, 2, 4},
            {5, 0, 6},
            {8, 3, 1}
        };

        List<Puzzle> solutionSteps = Solve(initialState);

        if (solutionSteps != null)
        {
            solutionSteps.ForEach(x => Console.WriteLine(x.Action));
        }
        else
        {
            Console.WriteLine("Solução não encontrada");
        }
    }

    public static List<Puzzle> Solve(int[,] initialState)
    {
        Queue<Puzzle> frontier = new Queue<Puzzle>();
        HashSet<string> explored = new HashSet<string>();

        Puzzle initialNode = new Puzzle(initialState, 0, null, "");
        frontier.Enqueue(initialNode);

        while (frontier.Count > 0)
        {
            Puzzle currentNode = frontier.Dequeue();
            explored.Add(PuzzleToString(currentNode.PuzzleState));

            if (currentNode.IsGoalState(Entrada))
            {
                return ExtractSolution(currentNode);
            }

            List<Puzzle> successors = GetSuccessors(currentNode);
            foreach (var successor in successors)
            {
                string successorString = PuzzleToString(successor.PuzzleState);
                if (!explored.Contains(successorString))
                {
                    frontier.Enqueue(successor);
                }
            }
        }

        return null;
    }

    public static List<Puzzle> GetSuccessors(Puzzle node)
    {
        List<Puzzle> successors = new List<Puzzle>();

        int zeroRow = -1;
        int zeroCol = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (node.PuzzleState[i, j] == 0)
                {
                    zeroRow = i;
                    zeroCol = j;
                    break;
                }
            }
        }

        int[] dRow = { -1, 1, 0, 0 };
        int[] dCol = { 0, 0, -1, 1 };

        string[] acoes = { "Cima", "Baixo", "Direita", "Esquerda" };

        for (int i = 0; i < 4; i++)
        {
            int newRow = zeroRow + dRow[i];
            int newCol = zeroCol + dCol[i];

            if (newRow >= 0 && newRow < 3 && newCol >= 0 && newCol < 3)
            {
                int[,] newState = (int[,])node.PuzzleState.Clone();
                int movedNumber = newState[newRow, newCol];
                newState[zeroRow, zeroCol] = movedNumber;
                newState[newRow, newCol] = 0;

                successors.Add(new Puzzle(newState, node.Depth + 1, node, $"{movedNumber} - {acoes[i]}"));
            }
        }

        return successors;
    }

    public static List<Puzzle> ExtractSolution(Puzzle goalNode)
    {
        List<Puzzle> solution = new List<Puzzle>();
        Puzzle currentNode = goalNode;

        while (currentNode.Parent != null)
        {
            solution.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        solution.Reverse();
        return solution;
    }

    public static string PuzzleToString(int[,] puzzleState)
    {
        string puzzleString = "";

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                puzzleString += puzzleState[i, j];
            }
        }

        return puzzleString;
    }

}
