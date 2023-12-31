using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject defaultWeapon;
    public GameObject Weapon;
    public CamChange cam;

    private const float pointX = 65f;
    private const float pointY = 0f;

    private const int weaponNum0 = 0;
    private const int weaponNum1 = 1;

    // Start is called before the first frame update
    void Start()
    {
        defaultWeapon.SetActive(true);
        Weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OnChangeWeapon();
    }

    void OnChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.instance.WeaponNumChange(false);
            defaultWeapon.SetActive(false);
            Weapon.SetActive(true);

            Vector3 position = SelectWeaponUI.Instance.weaponUI.transform.localPosition;
            position.x = pointX;
            position.y = pointY;
            SelectWeaponUI.Instance.weaponUI.transform.localPosition = position;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            GameManager.instance.WeaponNumChange(true);
            defaultWeapon.SetActive(true);
            Weapon.SetActive(false);

            Vector3 position = SelectWeaponUI.Instance.weaponUI.transform.localPosition;
            position.x = pointY;
            position.y = pointY;
            SelectWeaponUI.Instance.weaponUI.transform.localPosition = position;
        }
    }
}
