using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{

    public override bool[,] PossibleMove()                          //implements virtual method from base class
    {
        bool[,] r = new bool[8, 8];                                 //2d array of allowed moves
       

        Chessman c;                                                 //chessman object
        int i, j;                                                   //used for x and y positions of chessman

       

        i = CurrentX;                                               //current x of chessman
        j = CurrentY;                                               //current y of chessman

        while (true)                                                //moves toward the top-left                                         
        {
            i--;                                                    
            j++;

            if (i < 0 || j >= 8)                                    //if the piece gets to the edge of the board, break
                break;
            c = BoardManager.Instance.Chessmans[i, j];              //looks at the instance of the 2d array of chessman and checks to see if there is a piece where we are trying to move

            if(c == null)                                           //if there is not a piece, we can move there
            {
                r[i, j] = true;                                     //possible move is true
            }
            else                                                    //if there is a piece 
            {
                if(IsWhite != c.IsWhite)                            //we check to see if it is an enemy, if it is, we can move there
                {
                    r[i, j] = true;                                 //set possible move to true
                    
                }
                break;                                  
            }
        }

        

        i = CurrentX;                                              //reset to chessman currentX
        j = CurrentY;                                              //reset to chessman currentY

        while (true)                                               //topRight, execute atleast once
        {
            i++;                                                    //move right
            j++;                                                    //move up

            if (i >= 8 || j >= 8)
                break;
            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (IsWhite != c.IsWhite)
                {
                    r[i, j] = true;

                }
                break;
            }
        }
        

        i = CurrentX;                                               //reset to currentX of chessman
        j = CurrentY;                                               //reset to currentY of chessman

        while (true)                                                //downleft move, execute atleast once
        {
            i--;                                                    //move left
            j--;                                                    //move down

            if (i < 0 || j < 0 )
                break;
            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (IsWhite != c.IsWhite)
                {
                    r[i, j] = true;

                }
                break;
            }
        }
        

        i = CurrentX;                                               //reset to currentX of chessman
        j = CurrentY;                                               //reset to currentY of chessman

        while (true)                                                //downright move, execute atleast once
        {
            i++;                                                    //move right
            j--;                                                    //move down

            if (i >= 8|| j <0)
                break;
            c = BoardManager.Instance.Chessmans[i, j];

            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (IsWhite != c.IsWhite)
                {
                    r[i, j] = true;

                }
                break;
            }
        }
        return r;                                                                 //return possible moves
    }
}
