using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman                                //inherits from abstract Chessman class
{
    public override bool[,] PossibleMove()                  //overrides possible moves for specific king piece
    {
        bool[,] r = new bool[8, 8];                         //creats 2d boolean array called r

        
        Chessman c;                                         //Chessman object to see if a piece is in on the board

        int i, j;                                           //variables for movement on the board

        i = CurrentX - 1;                                   //left one tile
        j = CurrentY + 1;                                   //up one tile
                                                            //top side movement
        if (CurrentY != 7)                                  //if not at the top of the board    
        {
            for(int k = 0; k < 3; k++)                      //loop 3 times, upper-left, upper-middle, upper-right
            {
                if(i >=0 || i < 8)                          //if within the x boundaries of the board
                {
                    c = BoardManager.Instance.Chessmans[i, j];  //look at the current status of the board inside the 2d array of chessman and see if there is a chessman at one of the upper positions
                    if(c == null)                               //if there is no chessman
                    {
                        r[i, j] = true;                         //we can move there within the 2d array

                    }
                    else if(IsWhite != c.IsWhite)               //if there is a piece, and it is an enemy
                    {
                        r[i, j] = true;                         //we can move there
                    }
                    i++;                                        //check top-left, then top-middle, then top-right
                }
                
            }
        }
        i = CurrentX - 1;                                       //reset to bottom side squares
        j = CurrentY - 1;

       
        if (CurrentY != 0)                                       //bottom side, check to see if we are already at the bottom of the board
        {
            for (int k = 0; k < 3; k++)                         //loop 3 times, to check 3 different squares: bottom-left, bottom-middle, bottom-right
            {
                if (i >= 0 || i < 8)                            //make sure we dont go off of the board
                {
                    c = BoardManager.Instance.Chessmans[i, j];      //check 2d Chessman array to see if there is a piece for a possible move
                    if (c == null)                                  //if there is no chessman at the possible move
                    {
                        r[i, j] = true;                             //we can move there

                    }
                    else if (IsWhite != c.IsWhite)                  //if there is a chessman and it is an enemy piece
                    {
                        r[i, j] = true;                             //we can move there
                    }

                }
                i++;                                                //check bottom-left, then bottom-middle, then bottom-right
            }
        }

       
        if(CurrentX != 0)                                            //middle left, if not a the left most side of the board
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];        //check to see if there is a chessman to our left
            if(c == null)                                                       //if not
            {
                r[CurrentX - 1, CurrentY] = true;                               //we can move there
            }
            else if(IsWhite != c.IsWhite)                                       //if there is a chessman to our left, and it is an enemy 
            {
                r[CurrentX - 1 , CurrentY] = true;                              //we can move there
            }
        }
        
        if(CurrentX != 7)                                                       //middle right, check to see if we are already at the right most side of the board
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];        //check to see if there is a chessman to our right

            if(c == null)                                                       //if no chessman to our right
            {   
                r[CurrentX + 1, CurrentY] = true;                               //we can move there
            }
            else if(IsWhite != c.IsWhite)                                       //if there is a chessman to our right, and it is an enemy
            {
                r[CurrentX + 1, CurrentY] = true;                               //we can move there
            }
        }


        return r;                                                               //return 2d array of possible moves for king
    }
}
