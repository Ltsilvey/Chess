using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman                                  //inherits from Chessman
{
  
	public override bool[,] PossibleMove()                      //overrides possiblemove from chessman and implements its own specific method, particularly to the knight
    {
        bool[,] r = new bool[8, 8];                             //2d array of knights possible moves
        
        //upLeft
        KnightMove(CurrentX - 1, CurrentY + 2, ref r);          //ref the board because we can move over other chessman
        
        //upRight
        KnightMove(CurrentX + 1, CurrentY + 2, ref r);
        
        //RightUp
        KnightMove(CurrentX + 2, CurrentY + 1, ref r);

        //RightDown
        KnightMove(CurrentX + 2, CurrentY -1, ref r);

        //downLEft
        KnightMove(CurrentX - 1, CurrentY - 2, ref r);

        //downRight
        KnightMove(CurrentX + 1, CurrentY - 2, ref r);

        //LeftUp
        KnightMove(CurrentX - 2, CurrentY + 1, ref r);

        //leftDown
        KnightMove(CurrentX - 2, CurrentY - 1, ref r);

        return r;                                                           //return possible moves    
    }
    public void KnightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;                                                         //Chessman object to see if there is a piece where we are trying to move

        if(x >= 0 && x < 8 && y >= 0 && y < 8)                              //if the move is on the board
        {
            c = BoardManager.Instance.Chessmans[x, y];                      //check current status of Chessman on the board at possible move tiles
            if(c == null)                                                   //if the tile is empty
            {
                r[x, y] = true;                                             //we can move there

            }
            else if(IsWhite != c.IsWhite)                                   //if there is an enemy there, we can move there
            {
                r[x, y] = true;
            }
        }
    }
}
