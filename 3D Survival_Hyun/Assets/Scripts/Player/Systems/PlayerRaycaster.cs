using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 카메라 시야 방향으로 Ray를 쏴서 상호작용 가능한 오브젝트를 감지하고
/// 해당 정보를 UIManager를 통해 화면에 표시하는 컴포넌트
/// </summary>
public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraContainder;
    [SerializeField] private EquipmentManager equipmentManager;

    /// <summary>
    /// 매 프레임마다 카메라 전방으로 Ray를 발사해 상호작용 대상 탐색
    /// </summary>
    private void Update()
    {
        Vector3 origin = cameraContainder.position;
        Vector3 direction = cameraContainder.forward;
        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, interactableLayer))
        {
            var inspectable = hit.collider.GetComponent<InspectableObject>();
            if (inspectable != null && inspectable.data != null)
            {
                UIManager.Instance.ShowInspectInfo(inspectable.data.objectName, inspectable.data.description);
            }
            else
            {
                UIManager.Instance.HideInspectInfo();
            }

            if (hit.collider.TryGetComponent<EquipmentItem>(out EquipmentItem item))
            {
                UIManager.Instance.ShowEquipPrompt(item.itemName);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    equipmentManager.Equip(item);
                }
            }
            else
            {
                UIManager.Instance.HideEquipPrompt();
            }
        }
        else
        {
            UIManager.Instance.HideInspectInfo();
            UIManager.Instance.HideEquipPrompt();
        }
    }
}
