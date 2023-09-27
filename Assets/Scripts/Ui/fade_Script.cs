using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade_Script : MonoBehaviour
{
    public Image image;
    public Image image2;

    public GameObject text_box;

    public GameObject im_0;
    public GameObject im_1;
    public GameObject im_2;
    public GameObject im_3;
    public GameObject im_4;
    public GameObject im_5;
    public GameObject im_6;
    public GameObject im_7;
    public GameObject im_8;
    public GameObject im_9;
    public GameObject im_10;

    public Text Name;
    public Text _text;

    public int i = 0;
    public bool NEXT = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIN());
        StartCoroutine(textboxon());
        if (i == 0)
        {
            im_0.SetActive(true);
            Name.text = "이름";
            _text.text = "대화내용";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NEXT == true)
        {
            StartCoroutine(FadeOff());
        }
        
    }

    IEnumerator FadeIN()
    {
        float fadeCount = 1f;
        while (fadeCount <= 1.0f)
        {
            fadeCount -= 0.025f;
            yield return new WaitForSeconds(0.001f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    
    }

    IEnumerator FadeOff()
    {
        float fadeCount = 0f;
        while (fadeCount <= 1f)
        {
            fadeCount += 0.025f;
            yield return new WaitForSeconds(0.001f);
            image2.color = new Color(0, 0, 0, fadeCount);
        }

        if (fadeCount > 0.8f)
        {
            SceneManager.LoadScene("Loby_Scene");
        }
        else
        {

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

    public void next()
    {
        ++i;
      
        if (i == 1)
        {
            im_0.SetActive(false);
            im_1.SetActive(true);
            Name.text = "1";
            _text.text = "1";
        }
        else if (i == 2)
        {
            im_2.SetActive(true);
            Name.text = "2";
            _text.text = "2";
        }
        else if (i == 3)
        {
            im_3.SetActive(true);
            Name.text = "3";
            _text.text = "3";
        }
        else if (i == 4)
        {
            im_1.SetActive(false);
            im_2.SetActive(false);
            im_3.SetActive(false);

            im_4.SetActive(true);
            Name.text = "4";
            _text.text = "4";
        }
        else if (i == 5)
        {
            im_5.SetActive(true);
            Name.text = "5";
            _text.text = "5";
        }
        else if (i == 6)
        {
            im_6.SetActive(true);
            Name.text = "6";
            _text.text = "6";
        }
        else if (i == 7)
        {
            im_4.SetActive(false);
            im_5.SetActive(false);
            im_6.SetActive(false);

            im_7.SetActive(true);
            Name.text = "7";
            _text.text = "7";
        }
        else if (i == 8)
        {
            im_8.SetActive(true);
            Name.text = "8";
            _text.text = "8";
        }
        else if (i == 9)
        {
            im_9.SetActive(true);
            Name.text = "9";
            _text.text = "9";
        }
        else if (i == 10)
        {
            im_7.SetActive(false);
            im_8.SetActive(false);
            im_9.SetActive(false);

            im_10.SetActive(true);
            Name.text = "10";
            _text.text = "10";
        }
        else if (i == 11)
        {
            NEXT = true;
        }
    }
}
