using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance
    {
        get
        {
            if(Instance == null)
            {
                Instance = FindObjectOfType<ItemDataBase>();
            }

            return Instance;
        }
    }

    private static ItemDataBase Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject test;
    public Vector3[] pos;
    public void Start()
    {
        //for(int i = 0; i < 3; i++)
        //{
        //    GameObject go = Instantiate(test, pos[i], Quaternion.identity);
        //    //GameObject go = Instantiate(test, DropPos.position, Quaternion.identity);
        //    go.GetComponent<FiledItems>().SetItem(itemDB[Random.Range(0, 3)]);
        //}
    }

    public void Drop()
    {

    }
}
