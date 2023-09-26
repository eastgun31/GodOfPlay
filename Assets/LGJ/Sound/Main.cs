using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Main : MonoBehaviour
{

    void Start()
    {
        FindAnyObjectByType<Audio_Manager>().Main_Music();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
