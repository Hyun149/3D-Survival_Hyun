using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�޶� �þ� �������� Ray�� ���� ��ȣ�ۿ� ������ ������Ʈ�� �����ϰ�
/// �ش� ������ UIManager�� ���� ȭ�鿡 ǥ���ϴ� ������Ʈ
/// </summary>
public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraContainder;

    /// <summary>
    /// �� �����Ӹ��� ī�޶� �������� Ray�� �߻��� ��ȣ�ۿ� ��� Ž��
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
        }
        else
        {
            UIManager.Instance.HideInspectInfo();
        }
    }
}
