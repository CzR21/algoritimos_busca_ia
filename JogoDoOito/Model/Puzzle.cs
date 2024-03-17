using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDoOito.Model
{
    public class Puzzle
    {
        public int[,] PuzzleState { get; set; }
        public int Depth { get; set; }
        public Puzzle Parent { get; set; }
        public string Action { get; set; }

        public Puzzle(int[,] puzzleState, int depth, Puzzle parent, string action)
        {
            PuzzleState = puzzleState;
            Depth = depth;
            Parent = parent;
            Action = action;
        }

        public bool IsGoalState(int[,] goalState)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (PuzzleState[i, j] != goalState[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
