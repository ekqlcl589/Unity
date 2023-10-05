using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiledItems : MonoBehaviour
{
    [SerializeField] private Item item;

    public void SetItem(Item _item)
    {
        item.SetItemName(_item.GetItemName());
        item.SetItemSpriteImage(_item.GetItemSpriteImage());
        item.SetItemType(_item.GetItemType());
        item.SetItemToolTip(_item.GetItemToolTip());
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
