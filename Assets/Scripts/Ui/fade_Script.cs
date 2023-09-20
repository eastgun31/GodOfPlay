using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade_Script : MonoBehaviour
{
    public Image image;
    public GameObject text_box;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIN());
        StartCoroutine(textboxon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIN()
    {
        float fadeCount = 1f;
        while (fadeCount <= 1.0f)
        {
            fadeCount -= 0.002f;
            yield return new WaitForSeconds(0.001f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    
    }

    IEnumerator textboxon()
    {
        yield return new WaitForSeconds(2f);
        text_box.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Loby_Scene");
    }
}
