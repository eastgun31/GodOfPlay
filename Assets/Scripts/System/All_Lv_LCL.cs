using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class All_Lv_LCL : MonoBehaviour
{
    private int Gold;
    private int U;

    public Vector3 warrio_spawnPosition;
    public Vector3 shield_spawnPosition;
    public Vector3 Archer_spawnPosition;
    public Vector3 HorseMan_spawnPosition;


    public Button button_warrio_1Lr; //검사 1레벨 판매 버튼
    public GameObject Warrior_1L_Obj;//검사 1레벨 오브젝트
    public Button button_shield_1Lr; //방패병 1레벨 판매 버튼
    public GameObject Shield_1L_Obj;//방패병 1레벨 오브젝트
    public Button button_archer_1L; //궁수 1레벨 판매 버튼
    public GameObject Archer_1L_Obj; //궁수 1레벨 오브젝트
    public Button button_horseMan_1L; //기마병 1레벨 판매 버튼
    public GameObject HorseMan_1L_Obj; //기마병 1레벨 오브젝트

    public Button button_warrio_2Lr; //검사 2레벨 판매 버튼
    public GameObject Warrior_2L_Obj;//검사 2레벨 오브젝트
    public Button button_shield_2Lr; //방패병 2레벨 판매 버튼
    public GameObject Shield_2L_Obj;//방패병 2레벨 오브젝트
    public Button button_archer_2L; //궁수 2레벨 판매 버튼
    public GameObject Archer_2L_Obj; //궁수 2레벨 오브젝트
    public Button button_horseMan_2L; //기마병 2레벨 판매 버튼
    public GameObject HorseMan_2L_Obj; //기마병 2레벨 오브젝트

    public Button button_warrio_3Lr; //검사 3레벨 판매 버튼
    public GameObject Warrior_3L_Obj;//검사 3레벨 오브젝트
    public Button button_shield_3Lr; //방패병 3레벨 판매 버튼
    public GameObject Shield_3L_Obj;//방패병 3레벨 오브젝트
    public Button button_archer_3L; //궁수 3레벨 판매 버튼
    public GameObject Archer_3L_Obj; //궁수 3레벨 오브젝트
    public Button button_horseMan_3L; //기마병 3레벨 판매 버튼
    public GameObject HorseMan_3L_Obj; //기마병 3레벨 오브젝트

    public Button UpButton2L; //본부 2레벨로 업그레이드 시키는 버튼
    public Button UpButton3L; //본부 3레벨로 업그레이드 시키는 버튼

    public GameObject WarrioPosition;
    public GameObject ShieldPoaition;
    public GameObject ArcherPoaition;
    public GameObject HorseManPoaition;

    void Start()
    {
        button_warrio_1Lr.onClick.AddListener(Down_warrior_1L); //검사 1레벨 판매 버튼 클릭 연결
        button_shield_1Lr.onClick.AddListener(Down_shield_1L); //방패병 1레벨 판매 버튼 클릭 연결
        button_archer_1L.onClick.AddListener(Down_archer_1L); //궁수 1레벨 판매 버튼 클릭 연결
        button_horseMan_1L.onClick.AddListener(Down_horseMan_1L); //기마병 1레벨 판매 버튼 클릭 연결

        button_warrio_2Lr.onClick.AddListener(Down_warrior_2L); //검사 2레벨 판매 버튼 클릭 연결
        button_shield_2Lr.onClick.AddListener(Down_shield_2L); //방패병 2레벨 판매 버튼 클릭 연결
        button_archer_2L.onClick.AddListener(Down_archer_2L); //궁수 2레벨 판매 버튼 클릭 연결
        button_horseMan_2L.onClick.AddListener(Down_horseMan_2L); //기마병 2레벨 판매 버튼 클릭 연결

        button_warrio_3Lr.onClick.AddListener(Down_warrior_3L); //검사 3레벨 판매 버튼 클릭 연결
        button_shield_3Lr.onClick.AddListener(Down_shield_3L); //방패병 3레벨 판매 버튼 클릭 연결
        button_archer_3L.onClick.AddListener(Down_archer_3L); //궁수 3레벨 판매 버튼 클릭 연결
        button_horseMan_3L.onClick.AddListener(Down_horseMan_3L); //기마병 3레벨 판매 버튼 클릭 연결

        UpButton2L.onClick.AddListener(Up_2L); //본부 2레벨로 업그레이드 버튼 클릭 연결                                       
        UpButton3L.onClick.AddListener(Up_3L); //본부 3레벨로 업그레이드 버튼 클릭 연결
    }

    // Update is called once per frame
    void Update()
    {
        Gold = GameManager.instance.gold;
        U = GameManager.instance.All_Obj;
        //warrio_spawnPosition = new Vector3(25, 0, 35);
        //warrio_spawnPosition = new Vector3(30, 0, 36); //위치 지정
        //shield_spawnPosition = new Vector3(32, 0, 34);
        //Archer_spawnPosition = new Vector3(34, 0, 32);
        //HorseMan_spawnPosition = new Vector3(36, 0, 30);

        button_warrio_1Lr.interactable = (Gold >= 5); //검사 1레벨 5코인이 없을 시 버튼 비활성화
        button_shield_1Lr.interactable = (Gold >= 5); //방패병 1레벨 5코인이 없을 시 버튼 비활성화
        button_archer_1L.interactable = (Gold >= 8); //궁수 1레벨 8코인이 없을 시 버튼 비활성화
        button_horseMan_1L.interactable = (Gold >= 15); //기마병 1레벨 15코인이 없을 시 버튼 비활성화

        button_warrio_2Lr.interactable = (Gold >= 5); //검사 2레벨 5코인이 없을 시 버튼 비활성화
        button_shield_2Lr.interactable = (Gold >= 5); //방패병 2레벨 5코인이 없을 시 버튼 비활성화
        button_archer_2L.interactable = (Gold >= 8); //궁수 2레벨 8코인이 없을 시 버튼 비활성화
        button_horseMan_2L.interactable = (Gold >= 15); //기마병 2레벨 15코인이 없을 시 버튼 비활성화

        button_warrio_3Lr.interactable = (Gold >= 5); //검사 2레벨 5코인이 없을 시 버튼 비활성화
        button_shield_3Lr.interactable = (Gold >= 5); //방패병 2레벨 5코인이 없을 시 버튼 비활성화
        button_archer_3L.interactable = (Gold >= 8); //궁수 2레벨 8코인이 없을 시 버튼 비활성화
        button_horseMan_3L.interactable = (Gold >= 15); //기마병 2레벨 15코인이 없을 시 버튼 비활성화

        UpButton2L.interactable = (Gold >= 350); //본부 2레벨로 업그레이드 350코인이 없을 시 버튼 비활성화
        UpButton3L.interactable = (Gold >= 600); //본부 3레벨로 업그레이드 600 없을 시 버튼 비활성화
    }


    private void Down_warrior_1L()
    {
        if (U < 30)//검사 유닛 제한 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_1L_Obj, WarrioPosition.transform.position, Quaternion.identity); //검사 1레벨 유닛 생성
               // newObject.transform.position = WarrioPosition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();


                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
        }
    }

    private void Down_shield_1L()
    {
        if (U < 30)//방패병 유닛 제한 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //버튼을 누르면 검사 1레벨 필요 재화 5씩 없앰
                GameObject newObject = Instantiate(Shield_1L_Obj, ShieldPoaition.transform.position, Quaternion.identity); //방패병 1레벨 유닛 생성
               // newObject.transform.position = ShieldPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);

            }
    }

    private void Down_archer_1L()
    {
        if (U < 30)//궁수 유닛 제한 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //버튼을 누르면 궁수 1레벨 필요 재화 8씩 없앰
                GameObject newObject = Instantiate(Archer_1L_Obj, ArcherPoaition.transform.position, Quaternion.identity); //궁수 1레벨 유닛 생성
                //newObject.transform.position = ArcherPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_horseMan_1L()
    {
        if (U < 30)//기마병 유닛 제한 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //버튼을 누르면 기마병 1레벨 필요 재화 15씩 없앰
                GameObject newObject = Instantiate(HorseMan_1L_Obj, HorseManPoaition.transform.position, Quaternion.identity); //기마병 1레벨 유닛 생성
                //newObject.transform.position = HorseManPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }


    private void Down_warrior_2L()
    {
        if (U < 30)//검사 유닛 제한 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_2L_Obj, WarrioPosition.transform.position, Quaternion.identity); //검사 2레벨 유닛 생성
                //newObject.transform.position = WarrioPosition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
        }
    }

    private void Down_shield_2L()
    {
        if (U < 30)//방패병 유닛 제한 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //버튼을 누르면 검사 1레벨 필요 재화 5씩 없앰
                GameObject newObject = Instantiate(Shield_2L_Obj, ShieldPoaition.transform.position, Quaternion.identity); //방패병 2레벨 유닛 생성
                //newObject.transform.position = ShieldPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_archer_2L()
    {
        if (U < 30)//궁수 유닛 제한 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //버튼을 누르면 궁수 1레벨 필요 재화 8씩 없앰
                GameObject newObject = Instantiate(Archer_2L_Obj, ArcherPoaition.transform.position, Quaternion.identity); //궁수 2레벨 유닛 생성
                //newObject.transform.position = ArcherPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_horseMan_2L()
    {
        if (U < 30)//기마병 유닛 제한 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //버튼을 누르면 기마병 1레벨 필요 재화 15씩 없앰
                GameObject newObject = Instantiate(HorseMan_2L_Obj, HorseManPoaition.transform.position, Quaternion.identity); //기마병 2레벨 유닛 생성
                //newObject.transform.position = HorseManPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }


    private void Down_warrior_3L()
    {
        if (U < 30)//검사 유닛 제한 30
        {
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5;
                GameObject newObject = Instantiate(Warrior_3L_Obj, WarrioPosition.transform.position, Quaternion.identity); //검사 2레벨 유닛 생성
                //newObject.transform.position = WarrioPosition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
        }
    }

    private void Down_shield_3L()
    {
        if (U < 30)//방패병 유닛 제한 30
            if (Gold >= 5)
            {
                GameManager.instance.gold -= 5; //버튼을 누르면 검사 1레벨 필요 재화 5씩 없앰
                GameObject newObject = Instantiate(Shield_3L_Obj, ShieldPoaition.transform.position, Quaternion.identity); //방패병 2레벨 유닛 생성
                //newObject.transform.position = ShieldPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_archer_3L()
    {
        if (U < 30)//궁수 유닛 제한 30
            if (Gold >= 8)
            {
                GameManager.instance.gold -= 8; //버튼을 누르면 궁수 1레벨 필요 재화 8씩 없앰
                GameObject newObject = Instantiate(Archer_3L_Obj, ArcherPoaition.transform.position, Quaternion.identity); //궁수 2레벨 유닛 생성
                //newObject.transform.position = ArcherPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }

    private void Down_horseMan_3L()
    {
        if (U < 30)//기마병 유닛 제한 30
            if (Gold >= 15)
            {
                GameManager.instance.gold -= 15; //버튼을 누르면 기마병 1레벨 필요 재화 15씩 없앰
                GameObject newObject = Instantiate(HorseMan_3L_Obj, HorseManPoaition.transform.position, Quaternion.identity); //기마병 2레벨 유닛 생성
                //newObject.transform.position = HorseManPoaition.transform.position;
                GameManager.instance.All_Obj++;
                GameManager.instance.Aobj();

                UnitController unit = newObject.GetComponent<UnitController>();
                RTSUnitController.instance.UnitList.Add(unit);
            }
    }


    private void Up_3L()
    {
        if (Gold >= 600)
        {
            GameManager.instance.gold -= 600; //버튼을 누르면 본부 3레벨로 업그레이드 하기 위한 필요 재화 600 없앰

        }
    }

    private void Up_2L()
    {
        if (Gold >= 350)
        {
            GameManager.instance.gold -= 350; //버튼을 누르면 본부 2레벨로 업그레이드 하기 위한 필요 재화 350 없앰

        }
    }

}
