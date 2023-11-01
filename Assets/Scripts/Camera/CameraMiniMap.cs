using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMiniMap : MonoBehaviour, IPointerClickHandler
{
    public Camera mainCamera; // 주 게임 카메라
    public RectTransform miniMapRect; // 미니맵 UI 요소의 RectTransform

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 위치를 미니맵 상의 로컬 좌표로 가져옵니다.
        Vector2 localClick;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (miniMapRect, eventData.position, eventData.pressEventCamera, out localClick))
        {
            Debug.Log("로컬 좌표" + localClick);
            Vector3 worldPosition = new Vector3
                (localClick.x+250, mainCamera.transform.position.y, localClick.y+250);

            // 카메라를 해당 위치로 이동합니다.
            mainCamera.transform.position = worldPosition;
            Debug.Log("월드 좌표" + worldPosition);
            // 카메라를 해당 위치로 이동합니다.
            mainCamera.transform.position = worldPosition;
        }
    }
}