using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiledItems : MonoBehaviour
{
    public Item item;

    public void SetItemDataSwap(Item _item)
    {
        item.ItemDataSwap(_item);
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
