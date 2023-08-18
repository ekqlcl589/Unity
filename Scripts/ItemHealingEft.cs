using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public LayerMask isTarget;

    private LivingEntity targetEntity;


    public int heal = 10;
    public override bool ExecuteRole()
    {
        //if (playerCollirder.gameObject.tag == ("Player"))
        //{
        //    LivingEntity live = coll.GetComponent<LivingEntity>();
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
        Debug.Log("Player" + heal);
        return true;
    }
}
