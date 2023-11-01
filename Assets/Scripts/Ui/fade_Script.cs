using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade_Script : MonoBehaviour
{
    public Image image;
    public Image image2;

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
            _text.text = "여느때와 같이 올림포스의 신들이 연회를 즐기고 있다";
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
            SceneManager.LoadScene("MainScene");
        }
        else
        {

        }

    }

    IEnumerator textboxon()
    {
        yield return new WaitForSeconds(2f);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void next()
    {
        ++i;
      
        if (i == 1)
        {
            im_0.SetActive(false);
            im_1.SetActive(true);
            _text.text = "그들 중 유독 제우스가 지루한 표정으로 술잔을 들이키고 있다";
        }
        else if (i == 2)
        {
            im_2.SetActive(true);
            _text.text = "그 모습을 본 하데스가 인간들이 요새 연극을 즐기는데 우리도 비슷한걸 해보자고 제안한다";
        }
        else if (i == 3)
        {
            im_3.SetActive(true);
            _text.text = "흥미를 느낀 헤라가 그럼 섬안에 인간들을 가둬 서로 싸움을 붙이자 제안한다";
        }
        else if (i == 4)
        {
            im_1.SetActive(false);
            im_2.SetActive(false);
            im_3.SetActive(false);

            im_4.SetActive(true);
            _text.text = "디오니소스도 흥미를 느낀다";
        }
        else if (i == 5)
        {
            im_5.SetActive(true);
            _text.text = "아테나가 그럼 진형을 둘로 나눠 전쟁을 벌이게 하자면서 세세한 요소들을 제안한다";
        }
        else if (i == 6)
        {
            im_6.SetActive(true);
            _text.text = "아프로디테가 각각 진형에 자신들의 축복을 내리고 우리끼리 내기를 하자 말한다";
        }
        else if (i == 7)
        {
            im_4.SetActive(false);
            im_5.SetActive(false);
            im_6.SetActive(false);

            im_7.SetActive(true);
            _text.text = "헤파이토스가 망치를 집어들고 당장 준비를 하러 가겠다고 한다";
        }
        else if (i == 8)
        {
            im_8.SetActive(true);
            _text.text = "아르테미스도 헤타이토스를 돕겠다며 맞장구를 친다";
        }
        else if (i == 9)
        {
            im_9.SetActive(true);
            _text.text = "아폴론이 그럼 바로 일을 진행하자고 제안한다";
        }
        else if (i == 10)
        {
            im_7.SetActive(false);
            im_8.SetActive(false);
            im_9.SetActive(false);

            im_10.SetActive(true);
            _text.text = "포세이돈이 흡족한 표정으로 말한다. 당장 진행시켜!";
        }
        else if (i == 11)
        {
            NEXT = true;
        }
    }
}
