using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public Item GetItem() { return item; }
    public void SetItemData(Item newitem) { item = newitem; }
    [SerializeField] private Item item;

    public void SetSlotNum(int _slotNum) { slotnum = _slotNum; }
    [SerializeField] private int slotnum;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject player;

    public void Start()
    {
    }
    public void UpdateSlotUI()
    {

        itemIcon.sprite = item.GetItemSpriteImage();
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
            Inventory.Instance.ReMoveItem(slotnum); // 아이템 갯수를 줄이는거로 변경 
        }
    }
}
