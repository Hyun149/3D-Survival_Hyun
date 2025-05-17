using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cameraContainder;

    private void Update()
    {
        Vector3 origin = cameraContainder.position;
        Vector3 direction = cameraContainder.forward;

        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance, interactableLayer))
        {
            Debug.Log($"Ray Hit: {hit.collider.name}");

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
