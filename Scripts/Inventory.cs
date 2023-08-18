using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private int foodCnt = 0;

    public AudioClip pickUpClip;
    private AudioSource playerAudioPlayer;

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
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        playerAudioPlayer = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        slotCnt = 16; // 그냥 16개 다 활성화 시키고 + 버튼 누르면 꺼지는 거로 하자 뭔 추가냐 ㅋㅋ
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(Item _item)
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
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
        if(other.CompareTag("FieldItem"))
        {
            playerAudioPlayer.PlayOneShot(pickUpClip);
            FiledItems filedItems = other.GetComponent<FiledItems>();

            if(filedItems.GetItem().itemName != "Coin" && filedItems.GetItem().itemName != "치즈케이크")
                AddFoodCount();

            if(filedItems.GetItem().itemName == "치즈케이크")
            {
                AchievementsManager.Instance.OnNotify(AchievementsManager.Achievements.specialFood,
                    special: 1);
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
