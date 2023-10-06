using UnityEngine;

// 게임 점수를 증가시키는 아이템
public class Coin : MonoBehaviour, IItem
{
    private const int ammo = 30; // 충전할 총알 수

    public void Use(GameObject target)
    {
    }

    public void Auto(GameObject target)
    {

    }
    public bool Used(GameObject target)
    {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();

        if (playerShooter != null && playerShooter.GetGunData() != null)
            playerShooter.GetGunData().AmmoRemain = ammo;

        return true;
    }

}