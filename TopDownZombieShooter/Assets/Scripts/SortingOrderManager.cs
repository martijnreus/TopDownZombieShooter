using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] objectsToOrder;

    private void Start()
    {
        objectsToOrder = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        SetSortingOrder();
    }

    private void SetSortingOrder()
    {
        foreach (SpriteRenderer item in objectsToOrder)
        {
            int sortingOrder = -(int)(item.transform.position.y * 100);
            item.sortingOrder = sortingOrder;

            // hard coded fix but dont know how to fix it now
            if (item.gameObject.tag == "Gun")
            {
                item.sortingOrder += 200;
            }
        }
        
    }
}
