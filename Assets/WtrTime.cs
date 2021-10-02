using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WtrTime : MonoBehaviour
{
    public int index;
    public bool Enabled;
    string tOd;

    public TextMeshProUGUI TOD;
    public TextMeshProUGUI Time;

    [Header("Toggling")]
    public GameObject _enabled;
    public GameObject _disabled;

    public GameObject options;
    // Start is called before the first frame update
    
    public void setElement(int ind, WaterTime time)
    {
        TOD.text = time.TimeOfDay + ((time.enabled)? " -Enabled" : " -Disabled");
        tOd = time.TimeOfDay;
        Time.text = time.getTime();
        Enabled = time.enabled;

        if (time.enabled) { _enabled.SetActive(true); _disabled.SetActive(false); }
        else { _enabled.SetActive(false); _disabled.SetActive(true); }

        index = ind;
    }

    public void toggleOptions()
    {
        if (options.activeInHierarchy == true)
        {
            options.SetActive(false);
            triggerOthers();            
        }
        else{
            options.SetActive(true);
            triggerOthers();
        }
    }

    void triggerOthers()
    {
        WtrTime[] elements = FindObjectsOfType<WtrTime>();

        foreach (WtrTime elem in elements)
        {
            if (elem != this){ elem.options.SetActive(false); }
            LayoutRebuilder.ForceRebuildLayoutImmediate(elem.GetComponent<RectTransform>());
        }
    }

    public void changeTime(int increament)
    {
        Info.instance.times[index].time += increament;
        Time.text = Info.instance.times[index].getTime();
    }

    public void toggleActive()
    {
        if (Enabled)
        {
            Enabled = false;
            _enabled.SetActive(false);
            _disabled.SetActive(true);

            TOD.text = tOd +" -Disabled";

            Info.instance.times[index].enabled = false;
        }
        else
        {
            Enabled = true;
            _enabled.SetActive(true);
            _disabled.SetActive(false);

            TOD.text = tOd +" -Enabled";

            Info.instance.times[index].enabled = true;
        }
    }
}
