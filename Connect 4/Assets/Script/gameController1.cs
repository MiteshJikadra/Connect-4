using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class gameController1 : MonoBehaviour
{
    public GameObject[] spaceList;
    public GameObject marker;

    public Button leftButton, dropbutton, rightbutton;
    bool lInteract = true, rIntract = true, dIntract = true;
    float markerpos = 0;
    int colValue;
    int[] coreSpaces = new int[6];
    int[] bottomM = new int[7] { 5, 5, 5, 5, 5, 5, 5 };
    int dropCount = 1;
    Color coreColor;
    public GameObject redwins;
    public GameObject bluewins;

    public GameObject balleffect;
    public GameObject gameovereffect;
    public static gameController1 GC1;
    //bool isFirsttime;
   
    private void Awake()
    {
        GC1 = this;
    }
    void Start()
    {
        ColUpdate();
        markerpos = 0;

        leftButton.onClick.AddListener(LeftClick);
        rightbutton.onClick.AddListener(RightClick);
       
    }

   
    public void Update()
    {
        //Camera.main.transform.position = new Vector3(
        // -2.4f, -1.5f, Camera.main.transform.position.z);

        //GameObject g = Instantiate(pieceField, new Vector3(x * -0.8f, y * -0.8f, -1), Quaternion.identity) as GameObject;
        //g.transform.parent = gameObjectField.transform;

        leftButton.interactable = lInteract;
        rightbutton.interactable = rIntract;
        dropbutton.interactable = dIntract;

        if (markerpos <=-225)
        {
            lInteract = false;
        }
        else if (markerpos > -225)
        {
            lInteract = true;
        }

        if (markerpos >= 225)
        {
            rIntract = false;
        }
        else if (markerpos < 225)
        {
            rIntract = true;
        }
       
    }
    public void LeftClick()
    {
        if (markerpos > -225 && lInteract == true)
        {

            lInteract = true;
            marker.transform.position += new Vector3(-150f, 0f, 0f);
            markerpos -= 75f;
        }
        ColUpdate();
    }
    public void RightClick()
    {
        if (markerpos < 225 && rIntract == true)
        {

            rIntract = true;
            marker.transform.position += new Vector3(150f, 0f, 0f);
           
            markerpos += 75f;

        }
        ColUpdate();
    }
    public void DropClick()
    {
        balleffect.SetActive(true);
        
        if (spaceList[coreSpaces[bottomM[colValue]]].GetComponent<Image>().color == Color.white)
            spaceList[coreSpaces[bottomM[colValue]]].GetComponent<Image>().color = coreColor;


        bottomM[colValue]--;
        dropCount++;

        ColUpdate();
    }
   public void ColUpdate()
    {
        winCheck();
        
        for (int i = 0; i < 7; i++)
        {
            if (markerpos == (75 * i) - 225)
            {
                colValue = i;
                break;
            }
        }

        for (int j = 0; j < 6; j++)
            coreSpaces[j] = (7 * j) + colValue;

        if (bottomM[colValue] <= -1)
        {
            dIntract = false;
            bottomM[colValue] = -1;
        }
        else
        {
            dIntract = true;
        }


        if (dropCount % 2 == 0)
        {
            coreColor = Color.blue;
            StartCoroutine(a());
            //DropClick();
            
        }
            
       
        else if (dropCount % 2 != 0)
            coreColor = Color.red;

        marker.GetComponent<Image>().color = coreColor;
        leftButton.interactable = lInteract;
        rightbutton.interactable = rIntract;
        dropbutton.interactable = dIntract;


    }
    void winCheck()
    {
        for (int row = 0; row < 6; row++)  //Horizontal
        {
            for (int col = 0; col < 4; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 1].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 2].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 3].GetComponent<Image>().color == coreColor)
                {
                    winnerColor(coreColor);
                }
                else
                {
                    Debug.Log("aa");
                }
            }
        }

        for (int row = 0; row < 3; row++)  //Vertical
        {
            for (int col = 0; col < 7; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 7].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 14].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 21].GetComponent<Image>().color == coreColor)
                {
                    winnerColor(coreColor);
                }
            }
        }

        for (int row = 0; row < 3; row++)   //Right
        {
            for (int col = 0; col < 4; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 8].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 16].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 24].GetComponent<Image>().color == coreColor)
                {
                    winnerColor(coreColor);
                }
            }
        }

        for (int row = 0; row < 3; row++)   //Left
        {
            for (int col = 0; col < 4; col++)
            {
                if (spaceList[col + (row * 7)].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 6].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 12].GetComponent<Image>().color == coreColor && spaceList[col + (row * 7) + 18].GetComponent<Image>().color == coreColor)
                {
                    winnerColor(coreColor);
                }
            }
        }

    }
    void winnerColor(Color winColor)
    {
        
        if (winColor == Color.blue)
        {
            
            Debug.Log("blue");
            bluewins.SetActive(true);
            gameovereffect.SetActive(true); 
        }
        else if (winColor == Color.red)
        {
            
            Debug.Log("red");
            redwins.SetActive(true);
            gameovereffect.SetActive(true);
        }
        else if (dropCount >=42)
        {
           
            Debug.Log("Tie");
            gameovereffect.SetActive(true);
        }
        leftButton.interactable = false;
        rightbutton.interactable = false;
        dropbutton.interactable = false;
    }
    IEnumerator a()
    {
        yield return new WaitForSeconds(0.5f);
       
        //yield return new WaitForSeconds(1);
        if (markerpos < 225 && rIntract == true)
        {
            lInteract = true;
            RightClick();
            DropClick();
        }
        else if (markerpos > -225 && lInteract == true)
        {
            lInteract = true;
            LeftClick();
            DropClick();
        }
        
        StopAllCoroutines();
    }
    //public void AICode()
    //{
    //    if (isFirsttime)
    //    {

    //    }
    //}
}
