using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemUseHandler handler = other.GetComponent<ItemUseHandler>();
            if (handler != null)
            {
                handler.UseItem(itemData);
            }

            Destroy(gameObject);
        }
    }
}
