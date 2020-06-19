using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowAnimation : MonoBehaviour
{
    public Transform folow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(folow.position.x, folow.transform.position.y, transform.position.z);
    }
}
