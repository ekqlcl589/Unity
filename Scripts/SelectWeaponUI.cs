using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectWeaponUI : MonoBehaviour
{
    Image _weaponUI;

    public Image weaponUI
    {
        get
        {
            if(_weaponUI == null)
            {
                GameObject ui = GameObject.Find("HUD Canvas/WeaponUI/OutLine/Image");
                if (ui != null)
                    _weaponUI = ui.GetComponent<Image>();
            }
            return _weaponUI;
        }
    }

    static SelectWeaponUI _instance;

    public static SelectWeaponUI Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("SelectWeaponUI");
                _instance = obj.AddComponent<SelectWeaponUI>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    //Ȥ�� �𸣴� ����� �� ���� ��� �� �̸� Weapon_R
}
