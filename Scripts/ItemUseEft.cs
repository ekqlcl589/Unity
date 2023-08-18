using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Use")]

public class ItemUseEft : ItemEffect, IItem
{
    // Start is called before the first frame update
    public LayerMask isTarget;

    private LivingEntity targetEntity;

    public int heal = 10;

    [SerializeField] GameObject _target;

    public override bool ExecuteRole()
    {
        //if (collider.gameObject)
        //{
        //    LivingEntity live = collider.GetComponent<LivingEntity>();
        //
        //    if (live != null && !live.dead)
        //    {
        //        targetEntity = live;
        //
        //        if (live.health >= live.maxHealth)
        //            return false;
        //
        //        live.RestoreHealth(heal);
        //    }
        //}
        Use(_target);
        return true;
    }

    public void Use(GameObject target)
    {
            PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        if (playerShooter != null && playerShooter.gun != null)
            playerShooter.gun.ammoRemain += heal;
    }

    public void Auto(GameObject target)
    {

    }
    public bool Used(GameObject target)
    {
        return true;
    }

}
