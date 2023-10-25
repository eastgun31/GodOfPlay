using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEvent : MonoBehaviour
{
    public GameObject image_Change;
    public bool im_check = true;
    Audio_Manager audio_Manager;

    void Start()
    {
        audio_Manager = FindAnyObjectByType<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {


        if ((FindAnyObjectByType<HT_OP_Window>().HTP_ON.activeSelf == true) ||
            (FindAnyObjectByType<HT_OP_Window>().OP_ON.activeSelf == true))
        {
            im_check = false;
        }
        else
        {
            im_check = true;
        }
        
    }

    private void OnMouseEnter()
    {
        audio_Manager.PlaySFX(audio_Manager.AudioButton);
        if (im_check = true)
        {
            image_Change.SetActive(true);
        }
        else
        {
        }
    }

    private void OnMouseExit()
    {
        if (im_check = true)
        {
            image_Change.SetActive(false);
        }
        else
        {
        }
    }
}
