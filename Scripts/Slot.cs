using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;
    public GameObject player;

    public void Start()
    {
    }
    public void UpdateSlotUI()
    {
        
        itemIcon.sprite = item.itemImage;
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
        GameManager.instance.IsInput = false;

        if (isUse)
        {
            Inventory.instance.ReMoveItem(slotnum); // 아이템 갯수를 줄이는거로 변경 
        }
    }
}
