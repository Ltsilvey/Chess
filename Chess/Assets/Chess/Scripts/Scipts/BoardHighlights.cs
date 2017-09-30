using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour {

    public static BoardHighlights Instance { get; set; }                                   //make the object public available

    public GameObject highlightPrefab;                                                     //prefab in unity for tile highlights

    private List<GameObject> highlights;                                                   //list of highlighted tiles for possible moves



    private void Start()                                                                    //start method, sets this instance of boardhighlights
    {
        Instance = this;
        highlights = new List<GameObject>();                                                //instantiates highlights list
    }


    private GameObject GetHighlightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);                                //look in highlights list and look where activeSelf is false

        if(go == null)                                                                      //nothing found in highlights where gameobjects activeSelf is false, create a new one                         
        {
            go = Instantiate(highlightPrefab);                                              //Instantiate highlight prefab
            highlights.Add(go);                                                             //add it to list
        }

        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves)                                        //method to highlight the allowed move tiles
    {
        for(int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])                                                            //check to see which tiles are allowed moves in the 2d array
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);                                                     //if move is set to true, set gameobject to active
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);             //set position of highlight tile
                }
            }
        }
    }

    public void HideHighlights()                                                            //hide lights method
    {
        foreach(GameObject go in highlights)                                                //traverse highlights list and deactive active gameobject
        {
            go.SetActive(false);
        }
    }

}
