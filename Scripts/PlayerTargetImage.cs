using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTargetImage : MonoBehaviour
{
    public Transform target;
    Vector3 lookDir;   // public GameObject targetNav; // this
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = true;

        //targetNav.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(target)
        //{
            // 방향은 노멀라이즈
            lookDir = (target.position - image.transform.position).normalized;

            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.LookRotation(lookDir);

            image.transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime);
        //}
    }
}
