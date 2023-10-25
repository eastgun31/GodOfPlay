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
    Audio_Manager Audio_Manager;

    void Start()
    {
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
    }
    void Update()
    {
        
    }

    public void Page_button()
    {
        Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
        Page_On.SetActive(true);
        Page_Off.SetActive(false);
    }

    public void Page00_NEXT_Page01_button()
    {
        Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
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
        Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
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

    public void Page02_NEXT_Page03_button()
    {
        Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
        if (FindAnyObjectByType<Skill_Select>().Hermes_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Hestia_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Dionysus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Demeter_ON.activeSelf == true)
        {
            //opacity_image.SetActive(false);
            //bar_image.SetActive(false);
            Page_On.SetActive(true);
            Page_Off.SetActive(false);

            //EnemySkillManager.instance.enemySkills.SetActive(true);
        }
        else
        {
            Debug.Log("소모품 스킬이 활성화 되어있지않습니다");
        } 
    }
    public void NEXT_button()
    {
        Audio_Manager.PlaySFX(Audio_Manager.AudioButton);
        if (FindAnyObjectByType<Skill_Select>().Hephaestus_ON.activeSelf || FindAnyObjectByType<Skill_Select>().Artemis_ON.activeSelf ||
            FindAnyObjectByType<Skill_Select>().Ares_ON.activeSelf == true)
        {
            opacity_image.SetActive(false);
            bar_image.SetActive(false);
            Page_On.SetActive(true);
            Page_Off.SetActive(false);

            EnemySkillManager.instance.enemySkills.SetActive(true);
        }
        else
        {
            Debug.Log("패시브 스킬이 활성화 되어있지않습니다");
        }
    }
}
