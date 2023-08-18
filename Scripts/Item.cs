using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    EquipMent,
    Use,
    RawFood,
    bullet,
    special,
    Etc
}

[System.Serializable]

public class Item : IItem
{
    public ItemType type;
    public string itemName;
    public Sprite itemImage;
    public StateUI stateUI;
    public string itemToolTip;

    public bool Used(GameObject target)
    {
        if(type == ItemType.bullet)
        {
            PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

            if (playerShooter != null && playerShooter.gun != null)
                playerShooter.gun.ammoRemain += 30;

        }

        else if (type == ItemType.Use)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.dead)
            {
                if (playerHealth.Hunger <= 100)
                    playerHealth.RestoreHunger(30); // 이건 나중에 상수 말고 값 넘겨 받아서 


                playerHealth.RestoreHealth(20);
            }
        }

        else if(type == ItemType.RawFood)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.dead)
            {
                if (playerHealth.Hunger <= 100)
                    playerHealth.RestoreHunger(30);

                playerHealth.Diminish(30);
                //stateUI.UpdateState();
            }
        }
        else if(type == ItemType.special)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.dead)
            {
                if (playerHealth.Hunger <= 100)
                    playerHealth.RestoreHunger(30); // 이건 나중에 상수 말고 값 넘겨 받아서 

                playerHealth.RestoreHealth(50);
            }

        }
        else if (type == ItemType.Etc || type == ItemType.EquipMent)
        {
            return false;
        }

        return true;
    }

    public ItemType Type
    {
        get => type;

    }

    public void Use(GameObject target)
    {
    
    }
    public void Auto(GameObject target)
    {
    
    }
}
