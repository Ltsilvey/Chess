using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman                                            //inherit from Chessman
{

	public override bool[,] PossibleMove()                              //override possible move function from Chessman
    {
        bool[,] r = new bool[8, 8];                                     //2d array of possible moves on the board
        int[] e = BoardManager.Instance.EnPassantMove;                  //array to indentify when an Enpassant move can be made("When another pawn moves two spaces on its first move next to an enemy pawn")

        Chessman c, c2;                                                 //create two Chessman object to identify on the board for possible moves

        

        if (IsWhite)                                                    //White team move
        {
           
            if(CurrentX != 0 && CurrentY != 7)                           //upper diagonal left, check to see if we are at the left most side or the top of the board
            {
               
                if (e[0] == CurrentX - 1 && e[1] == CurrentY + 1)       //enpassant identifier
                {
                    r[CurrentX - 1, CurrentY + 1] = true;               //if the move is possible, we can move there
                }

                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];        //gets information from the upper-left tile

                if(c != null && !c.IsWhite)                             //if the tile is not and the piece is not white
                {
                    r[CurrentX - 1, CurrentY + 1] = true;               //we can move there
                }
            }
           

           
            if (CurrentX != 7  && CurrentY !=7)                          //upper diagonal right, if not on the right most tile and top tile of the board
            {
                
                if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;

                }
                c = BoardManager.Instance.Chessmans[CurrentX+1, CurrentY + 1];
                if (c != null && !c.IsWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

           
            if(CurrentY != 7)                                            //upper middle, if not on the top of the board
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];

                if(c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

           
            if(CurrentY == 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];         //upper middle first move
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                if(c == null & c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
        }
        else                   
        {
            
            if (CurrentX != 0 && CurrentY != 0)                                             //downward diagonal left, if not on the left most tile and bottom tile
            {
                if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }


                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.IsWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }


         
         
           
            if (CurrentX != 7 && CurrentY != 0)                                                 //downward diagonal right, if not on right most tile and bottom tile
            {
                if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;

                }

                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.IsWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

           
            if (CurrentY != 0)                                                                       //downward middle, if not on bottom tile
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];

                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

           
            if (CurrentY == 6)                                                                       //downward middle on first move
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                if (c == null & c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }


        }
        return r;                                               //return possible moves
    }


}
