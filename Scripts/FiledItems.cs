using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiledItems : MonoBehaviour
{
    [SerializeField] private Item item;

    public void SetItem(Item _item)
    {
        item.Swap(_item);
        //item.ItemName = _item.ItemName;
        //item.ItemImage = _item.ItemImage;
        //item.ItemType = _item.ItemType;
        //item.ItemToolTip = _item.ItemToolTip;
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
