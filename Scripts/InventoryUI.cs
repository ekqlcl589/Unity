using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    public GameObject inventoryPanel;

    private bool active = false;

    public Slot[] slots;
    public Transform slotHolder;

    private const float destroyCount = 5f;
    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();

    }
    private void Start()
    {
        inventory = Inventory.Instance;
        inventory.onSlotCountChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(active);
        DontDestroyOnLoad(gameObject);
    }

    // slotcnt 갯수 만큼만 활성화 
    private void SlotChange(int value)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].InitializeSlotNum(i);
            if (i < inventory.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;

        }
    }
    private void Update()
    {
        ActivePanel();

        if (GameManager.instance.IsGameover)
            Destroy(gameObject, destroyCount);
    }

    public void AddSlot() // ㄴㄴ 인벤토리 끄는거로 변경 
    {
        inventoryPanel.SetActive(false);
        //inventory.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++) // 이걸 안 하면 사용한 아이템의 정보가 안 사라짐
        {
            slots[i].RemoveSlot(); // 선택된 슬롯의 넘버를 넘겨야 함

        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].SetChangeItemData(inventory.items[i]);
            slots[i].UpdateSlotUI();
        }
    }

    public void ActivePanel()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            inventoryPanel.SetActive(active);
        }

    }
}
