using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitializeSuggestion : MonoBehaviour
{
    public static InitializeSuggestion instance;

    public GameObject PlantsScroll;
    public Transform contentObject;
    public GameObject PlantElement;
    public GameObject sorryElement;

    [Header("Description Page")]
    public GameObject DescPage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescText;
    // Update is called once per frame

    private void Awake()
    {
        instance = this;
    }

    public void initialize()
    {
        Data dat = Info.instance.data;

        for (int i = contentObject.childCount-1; i>=0; i--)
        {
            Transform exist = contentObject.GetChild(i);
            GameObject.Destroy(exist.gameObject);
        }
        int sug = 0;
        foreach (Plant plt in dat.plant)
        {
            if (Info.instance.wInfo.main.temp_max > plt.tempRange.x && Info.instance.wInfo.main.temp_min < plt.tempRange.y)
            {
                GameObject newEntry = Instantiate(PlantElement, contentObject);
                newEntry.GetComponent<PlantUIElement>().setElement(plt);
                sug += 1;
            }
        }

        if(sug == 0)
        {
            GameObject newEntry = Instantiate(sorryElement, contentObject);
            Debug.Log("Sorry");
        }
    }



    public void EnableDesc(string Name, string Desc)
    {
        DescPage.SetActive(true);
        PlantsScroll.SetActive(false);

        NameText.text = Name;
        DescText.text = Desc;
    }

    public void disableDesc() {
        DescPage.SetActive(false);
        PlantsScroll.SetActive(true);
    }
}
