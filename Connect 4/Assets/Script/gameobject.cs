using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameobject : MonoBehaviour
{
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(folow.position.x, folow.transform.position.y, transform.position.z);
    }
    public void gamecontroller()
    {
        gm.SetActive(true);
    }
    public void sceneload()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
