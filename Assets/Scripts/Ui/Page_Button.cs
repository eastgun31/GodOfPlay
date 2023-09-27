using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page_Button : MonoBehaviour //건들지 말것
{   [SerializeField]
    GameObject Page_On;
    [SerializeField]
    GameObject Page_Off;

    public GameObject opacity_image;
    public GameObject bar_image;

    void Start()
    {
        //Time 매니저를 하나 만들어두고 NEXT버튼이 활성화 상태에서는 0 비활성화 상태에서는 1로 계속 시간이 흐르게 만든다
    }
    void Update()
    {
        
    }

    public void Page_button()
    {
        Page_On.SetActive(true);
        Page_Off.SetActive(false);
    }

    public void Page00_NEXT_Page01_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Zeus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Poseidon_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Hades_ON.activeSelf == true)
        {
            Page_On.SetActive(true);
            Page_Off.SetActive(false);
        }
        else
        {
            Debug.Log("액티브 스킬이 활성화 되어있지않습니다");
        }
    }
    public void Page01_NEXT_Page02_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Hephaestus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Artemis_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Ares_ON.activeSelf == true)
        {
            Page_On.SetActive(true);
            Page_Off.SetActive(false);
        }
        else
        {
            Debug.Log("패시브 스킬이 활성화 되어있지않습니다");
        }
    }

    public void Page02_NEXT_Page03_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Hera_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Apollo_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Athena_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Aphrodite_ON.activeSelf == true)
        {
            Page_On.SetActive(true);
            Page_Off.SetActive(false);
        }
        else
        {
            Debug.Log("군중제어 및 디버프 스킬이 활성화 되어있지않습니다");
        }
    }

    public void NEXT_button()
    {
        if (FindAnyObjectByType<Skill_Select>().Hermes_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Hestia_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Dionysus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Demeter_ON.activeSelf == true)
        {
            opacity_image.SetActive(false);
            bar_image.SetActive(false);
            Page_On.SetActive(true);
            Page_Off.SetActive(false);

        }
        else
        {
            Debug.Log("소모품 스킬이 활성화 되어있지않습니다");
        }
        
    }
}
