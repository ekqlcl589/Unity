using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum ItemType
{
    EquipMent, // ��� ��ü�� �������� ���� �ϴ� �̸��� ���� �ص� 
    Use,
    RawFood,
    bullet,
    special,
    Etc
}

[System.Serializable]
public class Item : IItem
{
    public ItemType ItemType { get { return type; } private set { type = value; } }

    private ItemType type;

    public string ItemName { get { return itemName; } private set { itemName = value; } }

    private string itemName;

    public Sprite ItemImage { get { return itemImage; } private set { ItemImage = value; } }

    private Sprite itemImage;

    public StateUI StateUI { get { return stateUI; } private set { stateUI = value; } }

    private StateUI stateUI;

    public string ItemToolTip { get { return itemToolTip; } private set { itemToolTip = value; } }

    private string itemToolTip;

    private const int ammoRemain = 30;

    private const int restore = 30;
    private const int specialRestore = 50;

    public void Swap(Item item)
    {
        type = item.type;
        itemName = item.itemName;
        itemImage = item.ItemImage;
        stateUI = item.stateUI;
        ItemToolTip = item.itemToolTip;
    }
    public void Use(GameObject target)
    {

    }
    public void Auto(GameObject target)
    {

    }

    public bool Used(GameObject target)
    {
        if (type == ItemType.bullet)
        {
            PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

            if (playerShooter != null && playerShooter.GetGunData() != null)
                playerShooter.GetGunData().AmmoRemain = ammoRemain;

        }

        else if (type == ItemType.Use)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.Dead)
            {
                if (playerHealth.Hunger <= playerHealth.MaxHunger)
                    playerHealth.RestoreHunger(restore); // ����ĵ� ä��鼭 ü�µ� ���� ȸ�� 

                playerHealth.RestoreHealth(restore);
            }
        }

        else if (type == ItemType.RawFood)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.Dead)
            {
                if (playerHealth.Hunger <= playerHealth.MaxHunger)
                    playerHealth.RestoreHunger(restore);

                playerHealth.Diminish(restore);
            }
        }
        else if (type == ItemType.special)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.Dead)
            {
                if (playerHealth.Hunger <= playerHealth.MaxHunger)
                    playerHealth.RestoreHunger(restore);

                playerHealth.RestoreHealth(specialRestore);
            }

        }
        else if (type == ItemType.Etc || type == ItemType.EquipMent)
        {
            return false;
        }

        return true;
    }
}
