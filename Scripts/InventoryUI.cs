using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] private GameObject inventoryPanel;

    private bool active = false;

    [SerializeField] private Slot[] slots;
    [SerializeField] private Transform slotHolder;

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

    // slotcnt ���� ��ŭ�� Ȱ��ȭ 
    private void SlotChange(int value)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlotNum(i);
            if (i < inventory.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            inventoryPanel.SetActive(active);
        }

        if (GameManager.instance.isGameover)
            Destroy(gameObject, destroyCount);
    }

    public void AddSlot() // ���� �κ��丮 ���°ŷ� ���� 
    {
        inventoryPanel.SetActive(false);
        //inventory.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++) // �̰� �� �ϸ� ����� �������� ������ �� �����
        {
            slots[i].RemoveSlot(); // ���õ� ������ �ѹ��� �Ѱܾ� ��

        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].SetItemData(inventory.items[i]);
            slots[i].UpdateSlotUI();
        }
    }
}
