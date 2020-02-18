using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool isRed; // selecting if it's tiger Gameobject.
    int i, j, m, l;

    public bool IsFreezedMove (Piece[,] board, int x, int y) // checking Freezed movement for tiger Gameobject only
    {
        if (isRed) // Only if it is tiger
        {
            // for diagonal position movement.
            if (x == y + 3 || y == x + 1 || x == y + 7 || x == y + 11 || y == x + 5)
            {
                // top  side movement
                i = x - 2;
                j = y + 2;
                if (y == 6 || x == 9 || x == 5)
                {
                    for (int K = 0; K < 3; K++)
                    {
                        if (i >= 3 && i < 12)
                        {
                            Piece p = board[i, j];
                            if (p == null)
                                return true;
                        }
                        i += 2;
                    }
                }

                // movement two steps Top
                i = x - 2;
                j = y + 2;
                m = x - 4;
                l = y + 4;
                if (y < 6)
                {
                    for (int K = 0; K < 3; K++)
                    {
                        if (i >= 3 && i < 12 && m >= 3 && m < 12)
                        {
                            Piece p = board[i, j];
                            if (p == null)
                                return true;
                            else if (p != null && p.isRed != isRed)
                            {
                                if (board[m, l] == null)
                                    return true;
                            }
                        }
                        i += 2;
                        m += 4;
                    }
                }

                //Down Side movement
                i = x - 2;
                j = y - 2;
                if (y == 2 || x == 9 || x == 5)
                {
                    for (int K = 0; K < 3; K++)
                    {
                        if (i >= 3 && i < 12)
                        {

                            Piece p = board[i, j];
                            if (p == null)
                                return true;
                        }
                        i += 2;
                    }
                }

                // Down two step movement
                i = x - 2;
                j = y - 2;
                m = x - 4;
                l = y - 4;
                if (y > 2)
                {
                    for (int K = 0; K < 3; K++)
                    {
                        if (i >= 3 && i < 12 && m >= 3 && m < 12)
                        {
                            Piece p = board[i, j];
                            if (p == null)
                                return true;
                            else if (p != null && p.isRed != isRed)
                            {
                                if (board[m, l] == null)
                                    return true;
                            }
                        }
                        i += 2;
                        m += 4;
                    }
                }

                // left step movement
                i = x - 2;
                j = y;
                if (x == 5)
                {
                    if (i >= 3 && i < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                    }
                }

                // Left by two step movement
                i = x - 2;
                j = y;
                m = x - 4;
                if (x > 5)
                {
                    if (i >= 3 && i < 12 && m >= 3 && m < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                        else if (p != null && p.isRed != isRed)
                        {
                            if (board[m, j] == null)
                                return true;
                        }
                    }
                }

                // right step movement
                i = x + 2;
                j = y;
                if (x == 9)
                {
                    if (i >= 3 && i < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                    }
                }

                // Right by two step movement
                i = x + 2;
                j = y;
                m = x + 4;
                if (x < 9)
                {
                    if (i >= 3 && i < 12 && m >= 3 && m < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                        else if (p != null && p.isRed != isRed)
                        {
                            if (board[m, j] == null)
                                return true;
                        }
                    }
                }
            }

            // for non- diagonal point movement
            if (x == y + 1 || x == y + 5 || x == y + 9 || y == x + 3)
            {
                // middle Top movement
                i = x;
                j = y + 2;
                if (y == 6)
                {
                    if (j >= 0 && j < 9)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                    }

                }
                // Middle Top two step movement
                i = x;
                j = y + 2;
                m = y + 4;
                if (y < 6)
                {
                    if (j >= 0 && j < 9 && m >= 0 && m < 9)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                        else if (p != null && p.isRed != isRed)
                        {
                            if (board[i, m] == null)
                                return true;
                        }                       
                    }
                }

                // Middle Down movement
                i = x;
                j = y - 2;
                if (y == 2)
                {
                    if (j >= 0 && j < 9)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                    }
                }

                // Middle Down two step movement
                i = x;
                j = y - 2;
                m = y - 4;
                if (y > 2)
                {
                    if (j >= 0 && j < 9 && m >= 0 && m < 9)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                        else if (p != null && p.isRed != isRed)
                        {
                            if (board[i, m] == null)
                                return true;
                        }
                    }
                }

                // left step movement
                i = x - 2;
                j = y;
                if (x == 5)
                {
                    if (i >= 3 && i < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                    }
                }

                // Left by two step movement
                i = x - 2;
                j = y;
                m = x - 4;
                if (x > 5)
                {
                    if (i >= 3 && i < 12 && m >= 3 && m < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                        else if (p != null && p.isRed != isRed)
                        {
                            if (board[m, j] == null)
                                return true;
                        }
                    }
                }

                // right step movement
                i = x + 2;
                j = y;
                if (x == 9)
                {
                    if (i >= 3 && i < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                    }
                }

                // Right by two step movement
                i = x + 2;
                j = y;
                m = x + 4;
                if (x < 9)
                {
                    if (i >= 3 && i < 12 && m >= 3 && m < 12)
                    {
                        Piece p = board[i, j];
                        if (p == null)
                            return true;
                        else if (p != null && p.isRed != isRed)
                        {
                            if (board[m, j] == null)
                                return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool MoveRule(Piece[,] board, int x1, int y1, int x2, int y2) // setting up move rule for both tiger and goat piece with a position reference from the board.
    {
        // If you are moving on top of another piece or onto piece there exist.
        if (board[x2, y2] != null)
            return false;

        // setting up distance from piece initial position to final position to check if there is a kill move.
        int AbsMoveX = Mathf.Abs(x2 - x1); // in x direction.
        int AbsMoveY = Mathf.Abs(y2 - y1); // in y direction.

        if (isRed)
        {
            if(x1 == y1 + 3 || y1 == x1 + 1 || x1 == y1 + 7 || x1 == y1 + 11 || y1 == x1 + 5) // if tiger is at a place that even has a diagonal movement.
            {
               if ((AbsMoveX == 2 && AbsMoveY == 2) || (AbsMoveX == 0 && AbsMoveY == 2) || (AbsMoveX == 2 && AbsMoveY == 0)) // normal jump then move is true.
               {
                 return true;
               }
               else if ((AbsMoveX == 4 && AbsMoveY == 4) || (AbsMoveX == 0 && AbsMoveY == 4) || (AbsMoveX == 4 && AbsMoveY == 0)) // killer jump with two step jump
               {
                  Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2]; // Piece at a position in between the initial position and the kill jump position(i.e two step jump)
                  if (p != null && p.isRed != isRed) // if position is not empty and is jumping Gameobject is not tiger, kill move is true.
                        return true;
                }
            }

            if (x1 == y1 + 1|| x1 == y1 + 5 || x1 == y1 + 9 || y1 == x1 + 3) // if position does not have a diagonal movement.
            {
                if ((AbsMoveX == 0 && AbsMoveY == 2) || (AbsMoveX == 2 && AbsMoveY == 0)) // normal jump
                {
                    return true;
                }
                else if ((AbsMoveX == 0 && AbsMoveY == 4) || (AbsMoveX == 4 && AbsMoveY == 0)) // killer jump
                {
                    Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2]; 
                    if (p != null && p.isRed != isRed) // if mid position is not empty and jumping GameObject is not tiger, kill move is valid.
                        return true;
                }
            }
        }

        if (!isRed) // if is a goat turn
        {
            var ps = FindObjectsOfType<count>(); // counting number of goat gameObject in the scene since goat count can vary based on score.
            for (int k = ps.Length; k > 0; k--) // since at first all the goats outside of the board are to be placed before they can actually follow the diagonal and non diagonal move rule as per the tiger.
            {
                if ((x1 == 2 || x1 == 12) && (y1 % 2 == 0)) // if moving goat with x value either 2 or 12 with y value to be even 
                {
                    if ((AbsMoveX == 1 || AbsMoveX == 3 || AbsMoveX == 5 || AbsMoveX == 7 || AbsMoveX == 9) && (AbsMoveY == 0 || AbsMoveY == 2 || AbsMoveY == 4 || AbsMoveY == 6)) // normal jump
                    {
                        return true; // true if distance from initial to final move position is as stated.
                    }
                }

                if ((x1 == 2 || x1 == 12) && (y1 % 2 != 0)) // if y position is odd
                {
                    if ((AbsMoveX == 1 || AbsMoveX == 3 || AbsMoveX == 5 || AbsMoveX == 7 || AbsMoveX == 9) && (AbsMoveY == 1 || AbsMoveY == 3 || AbsMoveY == 5)) // normal jump
                    {
                        return true;
                    }
                }

                if ((x1 == 1 || x1 == 13) && (y1 % 2 == 0)) // if moving goat with x value either 1 or 13 with y value to be even
                {
                    if ((AbsMoveX == 2 || AbsMoveX == 4 || AbsMoveX == 6 || AbsMoveX == 8 || AbsMoveX == 10) && (AbsMoveY == 0 || AbsMoveY == 2 || AbsMoveY == 4 || AbsMoveY == 6)) // normal jump
                    {
                        return true; // if above distance calculation is valid move is true
                    }
                }

                if ((x1 == 1 || x1 == 13) && (y1 % 2 != 0)) // if y position is odd
                {
                    if ((AbsMoveX == 2 || AbsMoveX == 4 || AbsMoveX == 6 || AbsMoveX == 8 || AbsMoveX == 10) && (AbsMoveY == 1 || AbsMoveY == 3 || AbsMoveY == 5)) // normal jump
                    {
                        return true;
                    }
                }
            }

            //After all the positions outside of the board are empty, we can move diagonally or non-diagonally based on the Gameobject position
            // checking if all the positions are empty
            if (board[1, 2] == null && board[1, 3] == null && board[1, 4] == null && board[1, 5] == null && board[1, 6] == null && board[2, 2] == null && board[2, 3] == null && board[2, 4] == null && 
                board[2, 5] == null && board[2, 6] == null && board[12, 2] == null && board[12, 3] == null && board[12, 4] == null && board[12, 5] == null && board[12, 6] == null && board[13, 2] == null &&
                board[13, 3] == null && board[13, 4] == null && board[13, 5] == null && board[13, 6] == null)
            {
                if (x1 == y1 + 3 || y1 == x1 + 1 || x1 == y1 + 7 || x1 == y1 + 11 || y1 == x1 + 5) // if has diagonal movement as well.
                {
                    if ((AbsMoveX == 2 && AbsMoveY == 2) || (AbsMoveX == 0 && AbsMoveY == 2) || (AbsMoveX == 2 && AbsMoveY == 0)) // normal jump
                    {
                        return true;
                    }
                }


                if (x1 == y1 + 1 || x1 == y1 + 5 || x1 == y1 + 9 || y1 == x1 + 3) // if has only non-diagonal movement.
                {
                    if ((AbsMoveX == 0 && AbsMoveY == 2) || (AbsMoveX == 2 && AbsMoveY == 0)) // normal jump
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

}
