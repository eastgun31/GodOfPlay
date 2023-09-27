using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerUnit;
    [SerializeField]
    private LayerMask layerGround;

    public GameObject ClickPointer;
    private GameObject ClickPointer_instance;

    private Camera mainCamera;
    private RTSUnitController rtsUnitController;

    private void Awake()
    {
        mainCamera = Camera.main;
        rtsUnitController = GetComponent<RTSUnitController>();
    }

    private void Update()
    {
        // 마우스 왼쪽 클릭으로 유닛 선택 or 해제
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 광선에 부딪히는 오브젝트가 있을 때 (=유닛을 클릭했을 때)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit))
            {
                if (hit.transform.GetComponent<UnitController>() == null) return;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
                }
                else
                {
                    rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
                }
            }
            // 광선에 부딪히는 오브젝트가 없을 때
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    rtsUnitController.DeselectAll();
                }
            }
        }

        // 마우스 오른쪽 클릭으로 유닛 이동
        //if (Input.GetMouseButtonDown(1))
        //{
        //    RaycastHit hit;
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //    // 유닛 오브젝트(layerUnit)를 클릭했을 때
        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
        //    {
        //        rtsUnitController.MoveSelectedUnits(hit.point);
        //    }
        //}

        // 마우스 오른쪽 클릭으로 유닛 이동
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // 유닛 오브젝트(layerUnit)를 클릭했을 때
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            {
                // 원형 정렬을 위한 변수 설정
                Vector3 center = hit.point; // 원의 중심점
                float radius = 3.0f; // 원의 반지름
                int numUnits = rtsUnitController.GetSelectedUnits().Count; // 선택된 유닛 수

                //클릭 이펙트
                ClickPointer_instance = Instantiate(ClickPointer);
                ClickPointer_instance.transform.position = hit.point;
                Destroy(ClickPointer_instance, 0.7f);

                // 각 유닛을 원형으로 정렬
                List<UnitController> selectedUnits = rtsUnitController.GetSelectedUnits();
                for (int i = 0; i < numUnits; i++)
                {
                    // 각도 계산
                    float angle = i * (360f / numUnits);

                    // 원형 정렬 위치 계산
                    Vector3 unitPosition = center + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)) * radius;
                    selectedUnits[i].MoveTo(unitPosition);
                }
            }
        }
    }

}

