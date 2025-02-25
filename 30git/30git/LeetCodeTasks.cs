using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30git;

public static class LeetCodeTasks
{

    //https://leetcode.com/problems/valid-sudoku/description/
    //Valid Sudoku

    public static bool IsValidSudoku(char[][] board)
    {
        //check rows
        HashSet<char> rows;
        for (int i = 0; i < board.Length; i++)
        {
            rows = new HashSet<char>();
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] == '.')
                    continue;

                if (rows.Contains(board[i][j]))
                    return false;
                else
                    rows.Add(board[i][j]);
            }
        }

        //check columns
        HashSet<char> columns = new HashSet<char>();
        int row_Index = 0;
        int column_Index = 0;
        while (row_Index < board.Length && column_Index < board.Length)
        {
            if (board[row_Index][column_Index] != '.')
            {

                if (columns.Contains(board[row_Index][column_Index]))
                    return false;
                else
                    columns.Add(board[row_Index][column_Index]);
            }

            if (column_Index == 8)
            {
                column_Index = 0;
                row_Index++;
                columns.Clear();
            }
            else
                column_Index++;
        }

        //check 3x3 box
        row_Index = 0;
        column_Index = 0;
        HashSet<char> box = new HashSet<char>();
        int nuberBox = 1;
        while (row_Index < board.Length && column_Index < board.Length)
        {
            if (board[column_Index][row_Index] != '.')
            {
                if (box.Contains(board[column_Index][row_Index]))
                    return false;
                else
                    box.Add(board[column_Index][row_Index]);
            }
            if (row_Index == column_Index && (row_Index + 1) % 3 == 0 && (column_Index + 1) % 3 == 0)
            {
                row_Index++;
                column_Index -= 2;
                box = new HashSet<char>();
                continue;
            }

            if ((row_Index + 1) % 3 == 0)
            {
                row_Index -= 2;
                column_Index++;
            }
            else
                row_Index++;
            

            
        }

        return true;
    }
}

