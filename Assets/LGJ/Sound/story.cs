using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindAnyObjectByType<Audio_Manager>().Story_Music();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
