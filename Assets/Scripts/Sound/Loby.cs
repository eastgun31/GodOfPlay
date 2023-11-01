using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loby : MonoBehaviour
{

    void Start()
    {
        FindAnyObjectByType<Audio_Manager>().Loby_Music();
    }
}
