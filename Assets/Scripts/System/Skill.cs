using System.Collections;
using System.Collections.Generic;
using System.Reflection;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public enum Skills
{
    Zeus,       //제우스        1 A  o
    Poseidon,   //포세이돈      2 A  o
    Hades,      //하데스        3 A  
    Hera,       //헤라          4 B  o
    Apollo,     //아폴론        5 B  o
    Athena,     //아테나        6 B  v
    Aphrodite,  //아프로디테    7 B  o
    Hermes,     //헤르메스      8 I  o
    Hestia,     //헤스티아      9 I  o
    Dionysus,   //디오니소스    10 I  o
    Demeter,     //데메테르     11 I  o
    Hephaestus, //헤파이토스    12 P  o
    Artemis,    //아르테미스    13 P  o
    Ares       //아레스         14 P  o
}

public class Skill : MonoBehaviour
{
    public static Skill instance;

    float originalDamage; // 능력치 변경 이전의 데미지
    float originalSpeed; // 능력치 변경 이전의 속도

    float buffDuration = 5f; // 스킬 지속 시간
    bool isBuffActive = false; // 스킬 활성화 여부
    public int itemLimit; // 스킬 횟수 제한

    public GameObject skillRangePrefab; // 범위 표시용 프리팹
    private GameObject skillRangeInstance; // 범위 표시용 인스턴스

    private float skillRangeHeight = 0.1f; // 스킬 범위 높이
    private bool isShowSkillRange = false; //범위 표시여부 

    bool isSkillReady_1 = false;
    bool isSkillReady_2 = false;

    public float currentCooldown_1 = 0.0f; //현재 쿨타임
    public float currentCooldown_2 = 0.0f; //현재 쿨타임

    public GameObject ZeusSkill; //제우스 액티브
    public GameObject PoseidonSkill; //포세이돈 액티브
    public GameObject HadesSkill; //하데스 액티브

    public GameObject HeraSkill; //헤라 액티브
    public GameObject ApolloSkill; //아폴론 액티브
    public GameObject AthenaSkill; // 아테나 시야
    public GameObject AphroditeSkill; //아프로디테 이펙트

    Audio_Manager audio_Manager;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        itemLimit = 5;
        audio_Manager = FindAnyObjectByType<Audio_Manager>();
    }

    void Update()
    {
        // 마우스 위치에 스킬 범위를 따라다니도록 업데이트
        if (isShowSkillRange)
        {
            UpdateSkillRangePosition();
        }

        //마우스 클릭
        if (Input.GetMouseButtonDown(0))
        {
            if (isSkillReady_1 && isShowSkillRange)
            {
                if (Skill_Set.instance.Zeus_S)
                {
                    UseZeusSkill();
                    audio_Manager.PlaySFX(audio_Manager.Zeus);
                }                  
                else if (Skill_Set.instance.Poseidon_S)
                {
                    UsePoseidonSkill();
                    audio_Manager.PlaySFX(audio_Manager.Poseidon);
                }               
                else if (Skill_Set.instance.Hades_S)
                {
                    audio_Manager.PlaySFX(audio_Manager.Hades);
                    UseHadesSkill();
                }            
                else { }
            }

            if (isSkillReady_2 && isShowSkillRange)
            {
                if (Skill_Set.instance.Hera_S)
                {
                    UseHeraSkill();
                    audio_Manager.PlaySFX(audio_Manager.Hera);
                }             
                else if (Skill_Set.instance.Apollo_S)
                {
                    UseApolloSkill();
                    audio_Manager.PlaySFX(audio_Manager.Apollo);
                }             
                else if (Skill_Set.instance.Athena_S)
                {
                    UseAthenaSkill();
                    audio_Manager.PlaySFX(audio_Manager.Athena);
                } 
                else if (Skill_Set.instance.Aphrodite_S)
                {
                    UseAphroditeSkill();
                }                 
                else { }
            }
        }

        // 1번 키를 누를 때 스킬 범위를 표시
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentCooldown_1 <= 0f)
        {
            if (isShowSkillRange)
            {
                isSkillReady_1 = false;
                CancelSkill();
            }
            else
            {
                isSkillReady_1 = true;
                isSkillReady_2 = false;
                ShowSkillRange();
            }
        }
        // 2번 키를 누를 때 스킬 범위를 표시
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentCooldown_2 <= 0f)
        {
            if (isShowSkillRange)
            {
                isSkillReady_2 = false;
                CancelSkill();
            }
            else
            {
                isSkillReady_2 = true;
                isSkillReady_1 = false;
                ShowSkillRange();
            }
        }
        // 3번 키를 누르면 3번 스킬 사용
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isBuffActive && itemLimit > 0)
        {
            if (Skill_Set.instance.Hermes_S)
            {               
                UseHermesSkill();
                audio_Manager.PlaySFX(audio_Manager.Hermes);
            }             
            else if (Skill_Set.instance.Hestia_S)
            {               
                UseHestiaSkill();
                audio_Manager.PlaySFX(audio_Manager.Hestia);
            }              
            else if (Skill_Set.instance.Dionysus_S)
            {
                UseDionysusSkill();
                audio_Manager.PlaySFX(audio_Manager.Dionysus);
            }              
            else if (Skill_Set.instance.Demeter_S)
            {
                UseDemeterSkill();
                audio_Manager.PlaySFX(audio_Manager.Demeter);
            }              
            else { }
        }

    }
    //------------------------------------------------------------------

    //스킬 범위 표시
    void ShowSkillRange()
    {
        isShowSkillRange = true;

        // 스킬 범위 표시용 프리팹을 인스턴스화
        //skillRangeInstance = Instantiate(skillRangePrefab);
        skillRangePrefab.SetActive(true);
    }

    //스킬 취소, 스킬을 사용해서 표시되는 범위를 삭제할때도 사용
    void CancelSkill()
    {
        isShowSkillRange = false;
        //Destroy(skillRangeInstance);
        skillRangePrefab.SetActive(false);
    }

    //스킬사용 위치
    void UpdateSkillRangePosition()
    {
        // 마우스 위치를 기반으로 스킬 범위 인스턴스 이동
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 바닥에만 충돌했을 때 스킬 범위 이동
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += skillRangeHeight; // Y 좌표 조절
            //skillRangeInstance.transform.position = newPosition;
            skillRangePrefab.transform.position = newPosition;
        }
    }

    //스킬 쿨다운
    private IEnumerator Num1_Skill_Cooldown(float cooldown)
    {
        currentCooldown_1 = cooldown; //쿨타임 설정

        while (currentCooldown_1 >= 0.0f)
        {
            currentCooldown_1 -= Time.deltaTime;
            yield return null;
        }

        currentCooldown_1 = 0f;
        Debug.Log(cooldown + " 스킬 쿨타임 종료!");
    }

    private IEnumerator Num2_Skill_Cooldown(float cooldown)
    {
        currentCooldown_2 = cooldown; //쿨타임 설정

        while (currentCooldown_2 >= 0.0f)
        {
            currentCooldown_2 -= Time.deltaTime;
            yield return null;
        }

        currentCooldown_2 = 0f;
        Debug.Log(cooldown + " 스킬 쿨타임 종료!");
    }

    IEnumerator DeactiveSkill(GameObject skill, float time)
    {
        yield return new WaitForSeconds(time); // 비활성화까지의 대기 시간(3초)
        skill.SetActive(false);
    }

    IEnumerator Novision(float time)    // 모든 시야 밝히는 지속시간 코루틴 함수
    {
        yield return new WaitForSeconds(time);
        AthenaSkill.SetActive(false);
    }

    //1번 액티브 스킬-------------------------------------------------------------------------------------------
    //제우스 스킬
    void UseZeusSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            ZeusSkill.SetActive(true);
            ZeusSkill.transform.position = spawnPosition;
            //ZeusSkill = Instantiate(skillNum_1, spawnPosition, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("E_Unit"));
            foreach (Collider collider in colliders)
            {
                E_unitMove E_unit = collider.GetComponent<E_unitMove>();
                if (E_unit != null)
                {
                    // 스킬로 인한 데미지 적용
                    E_unit.ZeusDamage(50);
                }
            }
        }
        isSkillReady_1 = false;

        CancelSkill();
        StartCoroutine(Num1_Skill_Cooldown(20f));
        StartCoroutine(DeactiveSkill(ZeusSkill, 3f));
    }
    void UsePoseidonSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            PoseidonSkill.SetActive(true);
            PoseidonSkill.transform.position = spawnPosition;

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("Unit"));
            foreach (Collider collider in colliders)
            {
                UnitController unit = collider.GetComponent<UnitController>();
                if (unit != null)
                {
                    unit.Poseidon.SetActive(true);
                    //보호막 조정
                    unit.PoseidonShield(50);
                }
            }
        }
        isSkillReady_1 = false;

        CancelSkill();
        StartCoroutine(Num1_Skill_Cooldown(25f)); //쿨타임 적용
        StartCoroutine(DeactiveSkill(PoseidonSkill, 4f));
    }
    //하데스 스킬
    void UseHadesSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            HadesSkill.SetActive(true);
            HadesSkill.transform.position = spawnPosition;

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("Unit"));
            foreach (Collider collider in colliders)
            {
                UnitController unit = collider.GetComponent<UnitController>();
                if (unit != null)
                {
                    unit.isHades = true; //부활 온
                    unit.Hades.SetActive(true); //유닛 내부 하데스 이펙트

                    StartCoroutine(unit.HadesDuration(10f));
                }
            }
        }
        isSkillReady_1 = false;

        CancelSkill();
        StartCoroutine(Num1_Skill_Cooldown(30f)); //쿨타임 적용
        StartCoroutine(DeactiveSkill(HadesSkill, 4f));
    }
    //2번 액티브 스킬-------------------------------------------------------------------------------------------
    //헤라 스킬
    void UseHeraSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            HeraSkill.SetActive(true);
            HeraSkill.transform.position = spawnPosition;

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("E_Unit"));
            foreach (Collider collider in colliders)
            {
                E_unitMove E_unit = collider.GetComponent<E_unitMove>();
                if (E_unit != null)
                {
                    //속박 코드
                    StartCoroutine(E_unit.HeraStun(5));
                }
            }
        }
        isSkillReady_2 = false;

        CancelSkill();
        StartCoroutine(Num2_Skill_Cooldown(25f)); //쿨타임 적용
        StartCoroutine(DeactiveSkill(HeraSkill, 1f));
    }
    //아폴론 스킬
    void UseApolloSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            ApolloSkill.SetActive(true);
            ApolloSkill.transform.position = spawnPosition;
            //ApolloSkill = Instantiate(skillNum_5, spawnPosition, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 4.5f, LayerMask.GetMask("Unit"));
            foreach (Collider collider in colliders)
            {
                UnitController unit = collider.GetComponent<UnitController>();
                if (unit != null)
                {
                    //힐량 조정
                    unit.ApolloHeal(30);
                }
            }
        }
        isSkillReady_2 = false;

        CancelSkill();
        StartCoroutine(Num2_Skill_Cooldown(20f)); //쿨타임 적용
        StartCoroutine(DeactiveSkill(ApolloSkill, 3f));
    }
    //아테나 스킬
    void UseAthenaSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 spawnPosition = hit.point;
            spawnPosition.y += skillRangeHeight; // Y 좌표 조정

            AthenaSkill.SetActive(true);
            AthenaSkill.transform.position = spawnPosition;

            StartCoroutine(Novision(3f));
        }

        isSkillReady_2 = false;
        CancelSkill();
        StartCoroutine(Num2_Skill_Cooldown(15f));
    }
    //아프로디테 스킬
    void UseAphroditeSkill()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("E_Unit")))
        {
            Vector3 spawnPosition = hit.point;

            Collider collider = hit.collider;
            E_unitMove e_unit = collider.GetComponent<E_unitMove>();
            e_unit.AphroditeChange(spawnPosition);
            audio_Manager.PlaySFX(audio_Manager.Aphrodite);

            AphroditeSkill.SetActive(true);
            AphroditeSkill.transform.position = spawnPosition;
            StartCoroutine(Num2_Skill_Cooldown(20f));
        }
        isSkillReady_2 = false;

        CancelSkill();
        StartCoroutine(DeactiveSkill(AphroditeSkill, 2f));
    }

    //소모스킬-------------------------------------------------------------------------------------------
    void UseHermesSkill() //이동속도
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                originalSpeed = unit.umoveSpeed;
                unit.umoveSpeed += 3;

                unit.Hermes.SetActive(true);

                StartCoroutine(BuffDelay(unit, originalSpeed, 5.0f, unit.Hermes));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }
    void UseHestiaSkill() //회복
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                unit.uhealth += 15;

                unit.Hestia.SetActive(true);

                StartCoroutine(BuffDelay(unit, 0, 2.0f, unit.Hestia));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }
    void UseDionysusSkill() //공격력
    {
        foreach (UnitController unit in RTSUnitController.instance.UnitList)
        {
            //리스트안에 있는 유닛 전부에게 적용
            if (IsUnitInList(unit))
            {
                originalDamage = unit.uattackPower;
                unit.uattackPower *= 2;

                unit.Dionysus.SetActive(true);

                StartCoroutine(BuffDelay(unit, originalDamage, 5.0f, unit.Dionysus));
            }
        }

        itemLimit--;
        isBuffActive = true;
    }

    void UseDemeterSkill() //재화
    {
        GameManager.instance.gold += 70;

        itemLimit--;
    }

    private IEnumerator BuffDelay(UnitController unit, float originalValue, float delayInSeconds, GameObject effectObject)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // 원래 값으로 되돌립니다.
        if (Skill_Set.instance.Hermes_S)
        {
            unit.umoveSpeed = originalValue;
            isBuffActive = false;
        }

        if (Skill_Set.instance.Ares_S)
        {
            unit.uattackPower = originalValue;
            isBuffActive = false;
        }

        effectObject.SetActive(false);
    }


    //유닛 확인------------------------------------------------------------------------------------------
    //유닛들이 리스트에 있는지 확인
    private bool IsUnitInList(UnitController unit)
    {
        return RTSUnitController.instance.UnitList.Contains(unit);
    }


    // 다른 이벤트 또는 조건에서 스킬을 사용 가능하게 하려면 isSkillReady를 true로 설정하십시오.
    //public void SetSkillReady(bool skillReady)
    //{
    //    isSkillReady = skillReady;
    //}
}
