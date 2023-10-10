using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public Item GetItem() { return item; }


    public Item item;

    public int slotnum;
    public Image itemIcon;
    public GameObject player;

    public void Start()
    {
    }
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.ItemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Used(player);

        if (isUse)
        {
            Inventory.Instance.ReMoveItem(slotnum); // 아이템 갯수를 줄이는거로 변경 
        }
    }

    public void InitializeSlotNum(int _slotNum) 
    {
        if (item == null)
            return;

        slotnum = _slotNum; 
    }

    public void SetChangeItemData(Item newitem) 
    {
        if (newitem == null)
            return;

        item = newitem; 
    }
}
