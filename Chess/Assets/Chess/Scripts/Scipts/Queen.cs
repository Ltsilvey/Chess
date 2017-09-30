using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman                                                       //inherit from chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;

        int i, j;                                                                   //values for chessman position
        
       
        i = CurrentX;
        j = CurrentY;
        
        while (true)                                                                 //right move, execute atleast once
        {
            i++;                                                                    //icrement until off end of the board

            if (i >= 8)
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, CurrentY];                       //check for chessman at each x as we move right

            if (c == null)                                                          //if the tile does not have piece
            {
                r[i, CurrentY] = true;                                              //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                                           //if there is a chessman on the tile, and it is an enemy
                {
                    r[i, CurrentY] = true;                                          //we can move there
                }
                break;
            }
        }

       
        i = CurrentX;                                                               //reset i back to current x of chessman         
     
        while (true)                                                                //left move, execute atleast once
        {
            i--;                                                                    //decrement x 

            if (i < 0)                                                              //doesnt allow the chessman to move off the left side of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[i, CurrentY];                        //checks to see if there is a chessman on tiles to the left

            if (c == null)                                                           //if the tile is empty
            {
                r[i, CurrentY] = true;                                               //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                                            //if there is a chessman at the tile, and it is an enemy
                {
                    r[i, CurrentY] = true;                                           //we can move there
                }
                break;
            }
        }


        
        i = CurrentY;

        while (true)                                                //up move, execute atleast once
        {
            i++;

            if (i >= 8)                                             //stop the move from going off of the top of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, i];       //checks the board to see if their is a chessman on the tile

            if (c == null)                                          //if the tile is empty
            {
                r[CurrentX, i] = true;                              //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                           //if there is a chessman at the tile, and it is an enemy
                {
                    r[i, CurrentY] = true;                          //we can move there
                }
                break;
            }
        }

     
        i = CurrentY;                                               //reset i back to currentY of the chessman

        while (true)                                                //down move, execute atleast once
        {
            i--;

            if (i < 0)                                              //stop chessman from moving off the bottom of the board
            {
                break;
            }

            c = BoardManager.Instance.Chessmans[CurrentX, i];       //checks to see if there is a piece on the tile we can move to

            if (c == null)                                          //if the tile is empty 
            {
                r[CurrentX, i] = true;                              //we can move there
            }
            else
            {
                if (c.IsWhite != IsWhite)                           //if there is a piece, and it is an enemy
                {
                    r[i, CurrentY] = true;                          //we can move there
                }
                break;
            }
        }
    

       

        i = CurrentX;                                                //set i to currentX position of chessman
        j = CurrentY;                                                //set j to currentY position of chessman

        while (true)                                                 //top right move, execute atleast once
        {
            i++;                                                    //move right
            j++;                                                    //move up

            if (i >= 8 || j >= 8)                                   //make sure chessman stays on the board
                break;
            c = BoardManager.Instance.Chessmans[i, j];              //check to see if there is a piece on a tile of a possible move

            if (c == null)                                          //if there is not a piece
            {
                r[i, j] = true;                                     //we can move there
            }
            else
            {
                if (IsWhite != c.IsWhite)                           //if there is a piece, and it is an enemy
                {
                    r[i, j] = true;                                 //we can move there

                }
                break;
            }
        }
       

        i = CurrentX;                                               //reset currentX position of chessman
        j = CurrentY;                                               //reset currentY postion of chessman

        while (true)                                                 //downleft move, execute atleast once
        {
            i--;                                                    //move left
            j--;                                                    //move down

            if (i < 0 || j < 0)                                     //keeps the chessman on the board
                break;
            c = BoardManager.Instance.Chessmans[i, j];              //checks to see if there is a chessman on a tile of a possible move

            if (c == null)                                          //if there is no piece on the tile
            {
                r[i, j] = true;                                     //we can move there
            }
            else
            {
                if (IsWhite != c.IsWhite)                           //if there is a piece, and it is an enemy
                {
                    r[i, j] = true;                                 //we can move there

                }
                break;
            }
        }
        

        i = CurrentX;                                               //reset to currentX of chessman
        j = CurrentY;                                               //reset to currentY of chessman

        while (true)                                                //down right move, execute atleast once
        {
            i++;                                                    //move right 
            j--;                                                    //move down

            if (i >= 8 || j < 0)                                    //keeps the chessman on the board
                break;
            c = BoardManager.Instance.Chessmans[i, j];              //checks to see if there is a piece on the tile of a possible move

            if (c == null)                                          //if there is no piece on the tile
            {
                r[i, j] = true;                                     //we can move there
            }
            else
            {
                if (IsWhite != c.IsWhite)                           //if there is a piece, and it is an enemy
                {
                    r[i, j] = true;                                 //we can move there

                }
                break;
            }
        }

        i = CurrentX;                                               //reset to currentX of chessman
        j = CurrentY;                                               //reset to currentY of chessman

        while (true)                                                //upper left move, execute atleast once
        {
            i--;                                                    //move left
            j++;                                                    //move up

            if (i < 0 || j >= 8)                                    //keeps the chessman on the board
                break;
            c = BoardManager.Instance.Chessmans[i, j];              //check for piece on tile of possible moves

            if (c == null)                                          //if the tile of a possible move is empty
            {
                r[i, j] = true;                                     //we can move there
            }
            else
            {
                if (IsWhite != c.IsWhite)                           //if there is a piece at a tile of a possible move, and it is an enemy
                {
                    r[i, j] = true;                                 //we can move there

                }
                break;
            }
        }
        return r;                                                   //return possible moves

    }
}
