using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman                                                //inherit from Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];                                         //2d array of possible moves

        Chessman c;                                                         //Chessman object to check for occupied tiles
        int i;
      
        i = CurrentX;                                                       //set i to CurrentX of chessman

        while (true)                                                        //right, execute atleast once
        {
            i++;                                                            //move right

            if(i >= 8)                                                      //keeps the chessman from going off of the right side of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, CurrentY];               //check the tiles of possible moves for chessman

            if(c== null)                                                    //if the tile is empty
            {
                r[i, CurrentY] = true;                                      //we can move there
            }
            else
            {
                if(c.IsWhite != IsWhite)                                    //if the tile has a chessman, and it is an enemy 
                {
                    r[i, CurrentY] = true;                                  //we can move there
                }
                break;
            }
        }

                                                    
        i = CurrentX;                                                       //reset to currentX of chessman

        while (true)                                                        //left, execute atleast once
        {
            i--;                                                            //move left

            if (i < 0)                                                      //keeps the chessman from moving off of the left side of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, CurrentY];               //check to see if there is a chessman at a possiblemove tile

            if (c == null)                                                  //if the tile is empty
            {
                r[i, CurrentY] = true;                                      //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                                   //if the tile of a possible move is occupied, and it is an enemy
                {
                    r[i, CurrentY] = true;                                  //we can move there
                }
                break;
            }
        }


        
        i = CurrentY;                                                       //reset currentY of chessman

        while (true)                                                        //Up, execute atleast once
        {
            i++;                                                            //move up

            if (i >= 8)                                                     //keeps the chessman from moving off of the top of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, i];               //checks the tiles of possible move if the tile is occupied

            if (c == null)                                                  //if the tile is empty
            {
                r[CurrentX, i] = true;                                      //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                                   //if the tile has a chessman, and it is an enemy
                {
                    r[i, CurrentY] = true;                                  //we can move there
                }
                break;
            }
        }

       
        i = CurrentY;                                                       //reset to currentY of chessman  
        
        while (true)                                                         //down
        {
            i--;                                                            //move down

            if (i <0)                                                       //keeps the chessman from moving off of the bottom of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, i];               //checks tiles of possible moves to see if there is a piece there

            if (c == null)                                                  //if the tile is empty
            {
                r[CurrentX, i] = true;                                      //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                                   //if there is a chessman on the tile, and it is an enemy
                {
                    r[i, CurrentY] = true;                                  //we can move there
                }
                break;
            }
        }
        return r;                                                               //return possible moves

    }
}
