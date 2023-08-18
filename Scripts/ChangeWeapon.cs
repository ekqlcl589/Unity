using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject defaultWeapon;
    public GameObject Weapon;
    public CamChange cam;

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
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameManager.instance.weaponNum = 1;
            defaultWeapon.SetActive(false);
            Weapon.SetActive(true);

            Vector3 position = SelectWeaponUI.Instance.weaponUI.transform.localPosition;
            position.x = 65f;
            position.y = 0f;
            SelectWeaponUI.Instance.weaponUI.transform.localPosition = position;
            //CamChange.Instance.firstPersonCamera.enabled = false;
            //CamChange.Instance.overheadCamera.enabled = true;

            //cam.onChangePriority();

        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            GameManager.instance.weaponNum = 0;
            defaultWeapon.SetActive(true);
            Weapon.SetActive(false);

            Vector3 position = SelectWeaponUI.Instance.weaponUI.transform.localPosition;
            position.x = 0f;
            position.y = 0f;
            SelectWeaponUI.Instance.weaponUI.transform.localPosition = position;
            //CamChange.Instance.ShowFirstPersonView();
            //cam.onChangePriority();

        }
    }
}
