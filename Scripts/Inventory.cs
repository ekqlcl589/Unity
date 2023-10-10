using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Inventory : MonoBehaviour
{
    private static Inventory m_instance;

    public static Inventory Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<Inventory>();
            }
            return m_instance;
        }
        private set { }
    }

    public AudioClip pickUpClip;

    private const int foodCnt = 0;
    private AudioSource playerAudioPlayer;

    private const int inventorySlotNum = 16;
    private const int specialFoodCount = 1;

    public delegate void OnSlotCountChange(int value);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public delegate void OnDeleteItem(int value);
    public OnDeleteItem onDeleteItem;

    public List<Item> items = new List<Item>();

    public List<Item> Items
    {
        get => items;
        set
        {
            items = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    private int slotCnt;

    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        m_instance = this;

        playerAudioPlayer = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        slotCnt = inventorySlotNum;
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    public void ReMoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FieldItem"))
        {
            playerAudioPlayer.PlayOneShot(pickUpClip);
            FiledItems filedItems = other.GetComponent<FiledItems>();

            if (filedItems.GetItem().ItemName != "Coin" && filedItems.GetItem().ItemName != "치즈케이크")
                AddFoodCount();

            if (filedItems.GetItem().ItemName == "치즈케이크")
            {
                AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.specialFood,
                    special: specialFoodCount);
            }

            if (AddItem(filedItems.GetItem()))
                filedItems.DestroyItem();
        }
    }

    void AddFoodCount()
    {
        foodCnt++;

        AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.food1,
            food: foodCnt);
    }
}
