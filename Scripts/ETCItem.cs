using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETCItem : ItemEffect
{
    public LayerMask isTarget;

    private LivingEntity targetEntity;

    private Collider playerCollirder;
    public int heal = 10;

    // Start is called before the first frame update
    public override bool ExecuteRole()
    {
        if (playerCollirder.gameObject.tag == ("Player"))
        {
            LivingEntity live = playerCollirder.GetComponent<LivingEntity>();
        
            if (live != null && !live.dead)
            {
                targetEntity = live;
        
                //if (live.health >= live.maxHealth)
                //    return false;
        
                live.RestoreHealth(heal);
            }
        }

        return true;
    }
}
