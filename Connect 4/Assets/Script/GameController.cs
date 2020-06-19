using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class GameController : MonoBehaviour
{
    [Range(3, 8)]
    public int numRows = 4;
    [Range(3, 8)]
    public int numColumns = 4;

    public GameObject pieceRed;
    public GameObject pieceBlue;
    public GameObject pieceField;

    GameObject gameObjectField;
    //  Field field;

    // Start is called before the first frame update
    void Start()
    {
        CreateField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateField()
    {
        gameObjectField = GameObject.Find("Field");
        if (gameObjectField != null)
        {
            DestroyImmediate(gameObjectField);
        }
        gameObjectField = new GameObject("Field");
        //field = new Field(numRows, numColumns, numPiecesToWin, allowDiagonally);

        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                GameObject g = Instantiate(pieceField, new Vector3(x *-0.8f, y * -0.8f, -1), Quaternion.identity) as GameObject;
                g.transform.parent = gameObjectField.transform;
            }
        }
        Camera.main.transform.position = new Vector3(
        -2.4f,-1.5f, Camera.main.transform.position.z);

        //winningText.transform.position = new Vector3(
        //  (numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f) + 1, winningText.transform.position.z);

        //btnPlayAgain.transform.position = new Vector3(
        //  (numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f) - 1, btnPlayAgain.transform.position.z);
    }
    //GameObject SpawnPiece()
    //{
    //    Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //}
}
