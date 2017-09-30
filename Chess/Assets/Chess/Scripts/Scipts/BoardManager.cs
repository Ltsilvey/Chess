using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour                       //BoardManager Class
{

    public static BoardManager Instance { get; set; }           //accessible property instance of current boardmanager status
    private bool[,] allowedMoves { get; set; }                  //2d array property, checks to see if a move is valid or not

    public Chessman[,] Chessmans                                //2d array property of chessman on the board
    {
        get;set;
    }                           
    public int[] EnPassantMove { get; set; }                    //array to check for enpassant move
    private Chessman SelectedChessman;                          //current selected chessman
    private const float TILE_SIZE = 1.0f;                       //tile size
    private const float TILE_OFFSET = .5f;                      //offset to put the chessman at the center of the tile
    private const float spawnRotation = 90.0f;                  //rotates the chessman to face forward at spawn
    private Material previousMat;                               //original material of chessman
    public Material selectedMat;                                //selected mat for identifying a selected chessman

    private int selectionX = -1;                                //set mouse selection input x to -1
    private int selectionY = -1;                                //set mouse selection input y to -1

    public List<GameObject> chessmanPrefabs;                    //list of gameobjects, prefabs from unity

    private List<GameObject> activeChessman = new List<GameObject>();       //active chessman on the board

    public bool isWhiteTurn = true;                             //sets the turn to white team

    private void Start()                                        //start method, begins match by spawning all chessman and setting instance propety to this object
    {
        SpawnAllChessman();
        Instance = this;                                        //current instance of the board property
    }

    public void Update()                                        //update method, called each frame
    {
        DrawChessBoard();                                       //draw chessboard
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))                  //get input from right-click on the mouse            
        {
            if(selectionX >= 0 && selectionY >= 0)              //if click is on the board
            {
                if(SelectedChessman == null)                    //if a chessman is not already selected
                {
                    
                    SelectChessMan(selectionX, selectionY);     //use SelectChessMan function
                }
                else
                {
                    
                    MoveChessMan(selectionX, selectionY);       //use MoveChessMan function
                }
            }
        }
    }
    

    private void SelectChessMan(int x, int y)                               //called when getting input from mouse to see if there is a chessman at x,y
    {
        if(Chessmans[x,y] == null)                      //uses 2d array Chessmans to see if there is a chessman at x,y, if the position is null, return out of method
        {
            return;
        }
        if(Chessmans[x,y].IsWhite != isWhiteTurn)       //if Chessman is at the position of x and y, it must be the appropriate teams piece, if it is not, return out of method
        {
            return;
        }

        bool hasAtleastOneMove = false;                 
        allowedMoves = Chessmans[x, y].PossibleMove();          //return a true or a false if the selected chessman has a possiblemove
        for (int i = 0; i < 8; i++)                             //iterate through allowed moves 2d array and look for any possiblemoves
        {
            for(int j = 0; j < 8; j++)
            {
                if(allowedMoves[i, j])
                   hasAtleastOneMove = true;                    
            }
        }
        if (!hasAtleastOneMove)                                 //if there are no possible moves return out of selectedChessMan
            return;
        
        SelectedChessman = Chessmans[x, y];                     //if a move is found, move the piece in the Chessmans 2d array
        previousMat = SelectedChessman.GetComponent<MeshRenderer>().material;       //used for highlighting selected piece, sets original material to previous material
        selectedMat.mainTexture = previousMat.mainTexture;                          //uses previous texture in selected texture
        SelectedChessman.GetComponent<MeshRenderer>().material = selectedMat;       //selected material for highlighting is used on selected piece
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);               //uses BoardHighLights method HighAllowedMoves to highlight board tiles

    }
    private void MoveChessMan(int x, int y)                                 //method to move chessman to a x and y position on the board
    {
        if (allowedMoves[x,y])                                              //if there is an allowed move at selected x and y tile
        {
            Chessman c = Chessmans[x, y];                                   //set c to a chessman object and look in the Chessmans 2d array to see if there is a piece at the position on the board

            if(c != null && c.IsWhite != isWhiteTurn)                       //checks to see if there is a piece at Chessmans[x,y] and if it is an enemy piece, if true, we take the piece and move to the position 
            {
                                                                    
                if(c.GetType() == typeof(King))                             //if king, we end the game
                {
                    EndGame();
                    return;
                }
                activeChessman.Remove(c.gameObject);                        //if not king, we remove the chessman from the active chessman list
                Destroy(c.gameObject);                                      //destroy gameobject from the board

            }
            if(x == EnPassantMove[0] && y == EnPassantMove[1])
            {
                if(isWhiteTurn)                                             //white team               
                     c = Chessmans[x, y -1];                
                else                                                        //black team                                                        
                     c = Chessmans[x, y + 1];

                activeChessman.Remove(c.gameObject);                        //remove from active chessman on the board
                Destroy(c.gameObject);                                      //destroy the gameobject
            }
            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;

            if(SelectedChessman.GetType() == typeof(Pawn))              //if a pawn is selected
            {

                if(y == 7)                                              //promotion
                {
                    activeChessman.Remove(SelectedChessman.gameObject);
                    Destroy(SelectedChessman);
                    SpawnQueen(1, x, y);
                    SelectedChessman = Chessmans[x, y];

                }
                else if(y == 0)                                         //promotion
                {
                    activeChessman.Remove(SelectedChessman.gameObject);
                    Destroy(SelectedChessman);
                    SpawnQueen(7, x, y);
                    SelectedChessman = Chessmans[x, y];

                }
                if(SelectedChessman.CurrentY == 1 && y == 3)            //pawn double move, used for enpassant
                {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y - 1;
                   
                }
                else if (SelectedChessman.CurrentY == 6 && y == 4)      //pawn double move, used for enpassant
                {                                        
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = y + 1;
                    
                }
            }

            Chessmans[SelectedChessman.CurrentX, SelectedChessman.CurrentY] = null;             //reset Chessmans selection

            if (SelectedChessman.GetType() == typeof(King))                                      //the follow lines are used to better position the chessman on the board when selected and moved
            {
                SelectedChessman.transform.position = GetTileCenterForKing(x, y);              
            }
            else if (SelectedChessman.GetType() == typeof(Queen))
            {
                SelectedChessman.transform.position = GetTileCenterForKing(x, y);
            }
            else if (SelectedChessman.GetType() == typeof(Rook))
            {
                SelectedChessman.transform.position = GetTileCenterForKnightAndRook(x, y);
            }
            else if (SelectedChessman.GetType() == typeof(Knight))
            {
                SelectedChessman.transform.position = GetTileCenterForKnightAndRook(x, y);
            }
            else if (SelectedChessman.GetType() == typeof(Pawn))
            {
                SelectedChessman.transform.position = GetTileCenterForPawn(x, y);
            }
            else
                SelectedChessman.transform.position = GetTileCenterForBishop(x, y);


            SelectedChessman.SetPosition(x, y);                                                 //set position of selected chessman
            Chessmans[x, y] = SelectedChessman;                                                 //position of selected chessman is set to 2d array of chessman
            isWhiteTurn = !isWhiteTurn;                                                         //change turns
        }
        SelectedChessman.GetComponent<MeshRenderer>().material = previousMat;                   //set chessmans material back to the previous one

        BoardHighlights.Instance.HideHighlights();                                              //hide highlights

        SelectedChessman = null;                                                                //unselect chessman
    }

   

    
    private Vector3 GetTileCenterForPawn(int x, int y)              //method to center pawn on a tile
    {
        Vector3 origin = Vector3.zero;                              //set origin vector3 to zero

        origin.x += (TILE_SIZE * x) + TILE_OFFSET;                  //set piece to designated tile and center it
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y = .24f;                                            //place piece on top of the board
        return origin;
    }     
    private Vector3 GetTileCenterForBishop(int x, int y)            //method to center bishop on a tile
    {
        Vector3 origin = Vector3.zero;

        origin.x += (TILE_SIZE * x) + TILE_OFFSET;                  //set piece to designated tile and center it
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y = .4f;
        return origin;
    }     
    private Vector3 GetTileCenterForKing(int x, int y)              //method to center king on a tile
    {
        Vector3 origin = Vector3.zero;

        origin.x += (TILE_SIZE* x) + TILE_OFFSET;                   //set piece to designated tile and center it
        origin.z += (TILE_SIZE* y) + TILE_OFFSET;
        origin.y = .45f;
        return origin;
    }       
    private Vector3 GetTileCenterForKnightAndRook(int x, int y)     //method to center the knight and rook on a tile
    {
        Vector3 origin = Vector3.zero;

        origin.x += (TILE_SIZE * x) + TILE_OFFSET;                  //set piece to designated tile and center it
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y = .35f;
        return origin;
    }      
    private Vector3 GetTileCenterForQueen(int x, int y)             //method to center queen on a tile
    {
        Vector3 origin = Vector3.zero;

        origin.x += (TILE_SIZE * x) + TILE_OFFSET;                  //set piece to designated tile and center it
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y = .45f;
        return origin;
    }                       


    private Quaternion orientation = Quaternion.Euler(90, 180,0);           //orientate piece spawn, rotate 90 degrees toward enemies, and set them up-right
    

    private void EndGame()                                          //called when king is taken
    {
        if (isWhiteTurn)                                            //if it is white teams turn when king is taken, white team is printed
        {
            Debug.Log("White team wins");
        }
        else
            Debug.Log("Black team wins.");                          //if it is black teams turn when the king it taken, black team is printed


        foreach(GameObject go in activeChessman)                    //destroy the gameobjects still left on the board
        {
            Destroy(go);
        }

       

        BoardHighlights.Instance.HideHighlights();                  //remove board highlights

        SpawnAllChessman();                                         //spawn all chessman to starting positions
        isWhiteTurn = true;                                         //white teams turn
    }

    private void SpawnAllChessman()                                 //spawn all chessman
    {
        activeChessman = new List<GameObject>();                    //list of chessman still in play             

        EnPassantMove = new int[2] {-1,-1 };                        //array to identify when an enpassant move can be used
        Chessmans = new Chessman[8, 8];                             //2d array of chessman

                                                        //spawn white team
      
        SpawnKing(0, 3,0);                              //king 
       
        SpawnQueen(1,4,0);                              //queen
       
        SpawnKnightAndRook(2,0,0);                      //rook
       
        SpawnKnightAndRook(2,7,0);                      //rook
        
        SpawnBishop(3,2,0);                             //bishop
        
        SpawnBishop(3,5,0);                             //bishop
        
        SpawnKnightAndRook(4,1,0);                      //knight

        SpawnKnightAndRook(4,6,0);                      //knight
        
        for (int x = 0; x<8; x++)                       //pawns
        {
            SpawnPawn(5,x,1);
        }

         
        SpawnKing(6,4,7);                               //king
       
        SpawnQueen(7,3,7);                              //queen
        
        SpawnKnightAndRook(8,0,7);                      //rook
     
        SpawnKnightAndRook(8,7,7);                      //rook
       
        SpawnBishop(9,2,7);                             //bishop
       
        SpawnBishop(9,5,7);                             //bishop
        
        SpawnKnightAndRook(10,1,7);                     //knight

        SpawnKnightAndRook(10,6,7);                     //knight
        
        for (int x = 0; x < 8; x++)                     //pawns
        {
            SpawnPawn(11,x,6);
        }

    }

    private void UpdateSelection() //called each frame to see if a new chessman has been selected
    {
        if (!Camera.main)
            return;

        RaycastHit hit;     //variable to store a layer hit

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))        //if raycast return a layer of "Chessplane" x and y are stored in selectionX and selectionY
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else          //if raycast does not hit "Chessplane" set selectionX and selectionY to -1, I.E. nothing    
        {
            selectionX = -1;            
            selectionY = -1;

        }
      


    }

    private void DrawChessBoard()           //draws the outline of the chessboard
    {
        Vector3 widthLine = Vector3.right * 8;          //widthline, 8 tiles
        Vector3 heightLine = Vector3.forward * 8;       //height line, 8 tiles

        for(int i = 0; i <= 8; i++)                     //loop to draw 8 tiles forward
        {
            Vector3 start = Vector3.forward * i;        //identify where to start drawing
            Debug.DrawLine(start, start + widthLine);   //draw the horizontal lines

            for(int j = 0; j <= 8; j++)                 //loop to draw 8 width tiles
            {
                start = Vector3.right * j;              //identify where to start drawing
                Debug.DrawLine(start, start + heightLine);  //draw vertical lines
            }
        }

        //draw selection

       if(selectionX >= 0 && selectionY >= 0)                       //what does this do??
        {

            Debug.DrawLine(
                 Vector3.forward * selectionY + Vector3.right * selectionX, 
                 Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));

        }
      
      
    }

   
    private void SpawnPawn(int index, int x, int y)                 //spawn pawn at x and y 
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenterForPawn(x, y), orientation) as GameObject;     //instantiate chessman object on the board
        Chessmans[x, y] = go.GetComponent<Chessman>();                                                                  //chessman is added in the 2d array of chessmans
        Chessmans[x, y].SetPosition(x, y);                                                                              //set the current position of the chessman in the 2d array
        go.transform.SetParent(transform);                                                                              //actually move the object to the right spot on the board
        activeChessman.Add(go);                                                                                         //add the chessman to the chessman in play
    }
    private void SpawnKing(int index, int x, int y)                                                                     //spawn king
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenterForKing(x, y), orientation) as GameObject;
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }
    private void SpawnKnightAndRook(int index, int x, int y)                                                           //spawn knight and rook
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenterForKnightAndRook(x, y), orientation) as GameObject;
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }
    private void SpawnBishop(int index, int x, int y)                                                                   //spawn bishop
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenterForBishop(x, y), orientation) as GameObject;
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }
    private void SpawnQueen(int index, int x, int y)                                                                    //spawn queen
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenterForBishop(x, y), orientation) as GameObject;
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }
}
