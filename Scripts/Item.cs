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
    public ItemType GetItemType() { return type; }

    public void SetItemType(ItemType _type) { type = _type; }

    public string GetItemName() { return itemName; }

    public void SetItemName(string _itemName) { itemName = _itemName; }

    public Sprite GetItemSpriteImage() { return itemImage; }

    public void SetItemSpriteImage(Sprite _itemImage) { itemImage = _itemImage; }

    public StateUI GetStateUI() { return stateUI; }

    public void SetStateUI(StateUI _stateUI) { stateUI = _stateUI; }

    public String GetItemToolTip() { return itemToolTip; }

    public void SetItemToolTip(string _itemToolTip) { itemToolTip = _itemToolTip; }


    private ItemType type;
    private string itemName;
    private Sprite itemImage;
    private StateUI stateUI;
    private string itemToolTip;

    private const int ammoRemain = 30;

    private const int restore = 30;
    private const int specialRestore = 50;

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
                playerShooter.GetGunData().ammoRemain += ammoRemain;

        }

        else if (type == ItemType.Use)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.Dead)
            {
                if (playerHealth.Hunger <= playerHealth.GetMaxHunger())
                    playerHealth.RestoreHunger(restore); // ����ĵ� ä��鼭 ü�µ� ���� ȸ�� 

                playerHealth.RestoreHealth(restore);
            }
        }

        else if (type == ItemType.RawFood)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.Dead)
            {
                if (playerHealth.Hunger <= playerHealth.GetMaxHunger())
                    playerHealth.RestoreHunger(restore);

                playerHealth.Diminish(restore);
            }
        }
        else if (type == ItemType.special)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.Dead)
            {
                if (playerHealth.Hunger <= playerHealth.GetMaxHunger())
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
