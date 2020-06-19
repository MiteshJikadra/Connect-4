using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour
{
    public Vector3 statPos;
    private Vector3 endPos;
    private float firstClickTime, timeBetweenClicks;
    private bool coroutineAllowed;
    private int clickCounter;
    //[SerializeField]private float screenWidth;

    private void Start()
    {
        firstClickTime = 0f;
        timeBetweenClicks = 0.2f;
        clickCounter = 0;
        coroutineAllowed = true;

    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickCounter += 1;
        }
        if (clickCounter == 1 && coroutineAllowed)
        {
            firstClickTime = Time.time;
            StartCoroutine(DoubleClickDetection());
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                statPos = Input.GetTouch(0).deltaPosition;
            }
            if (Input.GetTouch(0).phase==TouchPhase.Ended)
            {
                endPos = Input.GetTouch(0).deltaPosition;
                if (endPos.x - statPos.x < 0)
                {
                    gameController1.GC1.LeftClick();
                }
                if (endPos.x - statPos.x > 0)
                {
                    gameController1.GC1.RightClick();
                }

            }
            //if (Input.GetTouch(1).phase==TouchPhase.Ended)
            //{
            //    endPos = Input.GetTouch(1).deltaPosition;
            //    if (endPos.y-statPos.y < 0)
            //    {
            //        gameController1.GC1.DropClick();
            //    }
            //}
        }
        //float xPos=((Input.mousePosition.x/Screen.width)*screenWidth-(screenWidth/2f));
        //transform.position = new Vector3(xPos,transform.position.y,z:0);
    }
    private IEnumerator DoubleClickDetection()
    {
        coroutineAllowed = false;
        while(Time.time < firstClickTime + timeBetweenClicks)
        {
            if (clickCounter == 2)
            {
                Debug.Log("click");
                gameController1.GC1.DropClick();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        clickCounter = 0;
        firstClickTime = 0f;
        coroutineAllowed = true;
    }
        

}
    //    private Vector3 dragOrigin; //Where are we moving?
    //    private Vector3 clickOrigin = Vector3.zero; //Where are we starting?
    //    private Vector3 basePos = Vector3.zero; //Where should the camera be initially?

    //    void Update()
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            if (clickOrigin == Vector3.zero)
    //            {
    //                clickOrigin = Input.mousePosition;
    //                basePos = transform.position;
    //            }
    //            dragOrigin = Input.mousePosition;
    //        }

    //        if (!Input.GetMouseButton(0))
    //        {
    //            clickOrigin = Vector3.zero;
    //            return;
    //        }

    //        transform.position = new Vector3(basePos.x + ((clickOrigin.x - dragOrigin.x) * .01f), basePos.y + ((clickOrigin.y - dragOrigin.y) * .01f), -10);
    //    }
    //}
    //    public float speed = 1.0f;
    //    public GameObject marker;

    //    void Update()
    //    {
    //        foreach (Touch touch in Input.touches)
    //        {
    //            Vector3 newPosition = transform.position;
    //            newPosition.x += touch.deltaPosition.x * speed * Time.deltaTime;
    //            marker.transform.position = newPosition;
    //            //Vector3.Lerp(transform.position, newPosition, 01f);
    //            //gameController1.GC1.ColUpdate();
    //        }

    //    }
    //}

    //Or you can use a simple logic like subway surfers,

    //using UnityEngine;
    //using System.Collections;

  
    //    public Transform player; //= new Transform[2]; // Drag your players here
    //    public float speed = 1.0f;
    //    void Update()
    //    {
    //        for (int i = 0; i < Input.touchCount; i++)
    //        {
    //            if (Input.GetTouch(i).position.y < Screen.width / 1)
    //            {
    //                Vector3 newPosition = player.position;
    //                newPosition.x += Input.GetTouch(i).deltaPosition.x * speed * Time.deltaTime;
    //                player.position = newPosition;
    //            }
    //            if (Input.GetTouch(i).position.y > Screen.width / 1)
    //            {
    //                Vector3 newPosition = player.position;
    //                newPosition.x += Input.GetTouch(i).deltaPosition.x * speed * Time.deltaTime;
    //                player.position = newPosition;
    //            }
    //        }
    //    }
    //}

    
    //// Update is called once per frame
    //void Update()
    //{
    //    if (swipe.SwipeLeft)
    //    {
    //        desiredPosition += Vector3.left;
    //    }
    //    if (swipe.SwipeRight)
    //    {
    //        desiredPosition += Vector3.right;
    //    }
    //    player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, 3f * Time.deltaTime);



