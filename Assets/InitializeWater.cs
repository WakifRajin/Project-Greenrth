using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitializeWater : MonoBehaviour
{
    public static InitializeWater instance;

    public GameObject TimeElement;
    public Transform contentObject;

    private void Awake()
    {
        instance = this;
    }

    public void initialize()
    {
        List<WaterTime> times = Info.instance.times;
        
        for (int i = contentObject.childCount - 1; i >= 0; i--)
        {
            Transform exist = contentObject.GetChild(i);
            GameObject.Destroy(exist.gameObject);
        }
        int index = 0;
        foreach (WaterTime tm in times)
        {
            GameObject newEntry = Instantiate(TimeElement, contentObject);
            newEntry.GetComponent<WtrTime>().setElement(index, tm);
            index++;
        }
    }
}
