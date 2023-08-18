using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public GameObject inventoryPanel;
    bool active = false;

    public Slot[] slots;
    public Transform slotHolder;

    // ���� �� �巡�� �� ��� ��ɿ� �ʿ� 
    private GraphicRaycaster raycaster;
    private PointerEventData eventData;
    private List<RaycastResult> raycastResults;

    private Slot beginDragSlot;
    private Transform beginDragIconTransfrom;

    private Vector3 beginDragIconPoint;   // �巡�� ���� �� ������ ��ġ
    private Vector3 beginDragCursorPoint; // �巡�� ���� �� Ŀ���� ��ġ
    private int beginDragSlotSiblingIndex;
    //


    public Item item;
    private void Start()
    {
        inventory = Inventory.instance;
        slots = GetComponentsInChildren<Slot>();
        inventory.onSlotCountChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(active);
        DontDestroyOnLoad(gameObject);
    }

    // slotcnt ���� ��ŭ�� Ȱ��ȭ 
    private void SlotChange(int value)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            if (i < inventory.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;

        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            inventoryPanel.SetActive(active);
        }

        //eventData.position = Input.mousePosition;
        //if(active == true)
        //{
        //ShowOrHideItemToolTip();
        //
        //    OnPointerDown();
        //    OnPointerDrag();
        //    OnPointerUp();
        //}

        if (GameManager.instance.isGameover)
            Destroy(gameObject, 5f);
    }

    public void AddSlot() // ���� �κ��丮 ���°ŷ� ���� 
    {
        inventoryPanel.SetActive(false);
        //inventory.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++) // �̰� �� �ϸ� ����� �������� ������ �� �����
        {
            slots[i].RemoveSlot(); // ���õ� ������ �ѹ��� �Ѱܾ� ��

        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        raycastResults.Clear();

        raycaster.Raycast(eventData, raycastResults);

        if (raycastResults.Count == 0)
            return null;

        return raycastResults[0].gameObject.GetComponent<T>();
    }

    private void OnPointerDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //beginDragSlot = RaycastAndGetFirstComponent<Slot>();
            beginDragSlot = GetComponent<Slot>();

            if (beginDragSlot != null /* �������� ������ �ִ� ���ȸ�*/)
            {
                beginDragIconTransfrom = beginDragSlot.itemIcon.transform;
                beginDragIconPoint = beginDragIconTransfrom.position;
                beginDragCursorPoint = Input.mousePosition;

                beginDragSlotSiblingIndex = beginDragSlot.transform.GetSiblingIndex();
                beginDragSlot.transform.SetAsLastSibling();

                
            }
            else
            {
                beginDragSlot = null;
            }
        }
    }

    private void OnPointerDrag()
    {

    }

    private void OnPointerUp()
    {

    }

    private void ShowOrHideItemToolTip()
    {
        
    }

    private void UpdateToolTipUI()
    {
        
    }
}
