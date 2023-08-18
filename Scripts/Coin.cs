using UnityEngine;

// 게임 점수를 증가시키는 아이템
public class Coin : MonoBehaviour, IItem {
    public int ammo = 30; // 충전할 총알 수

    public void Use(GameObject target) {
        //PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();
        //
        //if (playerShooter != null && playerShooter.gun != null)
        //    playerShooter.gun.ammoRemain += ammo;
        //
        //Destroy(gameObject);
    }

    public void Auto(GameObject target)
    {

    }
    public bool Used(GameObject target)
    {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        if (playerShooter != null && playerShooter.gun != null)
            playerShooter.gun.ammoRemain += ammo;
        return true;
    }

}