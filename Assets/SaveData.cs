using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TimeCollecction newCol = new TimeCollecction();
        newCol.times = Info.instance.times;
        string times = JsonUtility.ToJson(newCol);

        if (File.Exists(Application.dataPath + "/timecollection.green"))
        {
            string saved = File.ReadAllText(Application.dataPath + "/timecollection.green");

            TimeCollecction savedCol = JsonUtility.FromJson<TimeCollecction>(saved);
            Info.instance.times = savedCol.times;

            newCol.times = savedCol.times;
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/timecollection.green", times);
        }
    }

    public void saveTimes() {
        TimeCollecction newCol = new TimeCollecction();
        newCol.times = Info.instance.times;
        string times = JsonUtility.ToJson(newCol);

        File.WriteAllText(Application.dataPath + "/timecollection.green", times);

        try
        {
            foreach (WaterTime time in Info.instance.times)
            {
                NotificationScript.instance.sendNot(time.TimeOfDay, time.time, 0);
                NotificationScript.instance.sendNot(time.TimeOfDay, time.time, 1);
                NotificationScript.instance.sendNot(time.TimeOfDay, time.time, 2);
            }
        }
        catch
        {
            Debug.Log("Do Nothing");
        }
    }

    public class TimeCollecction
    {
        public List<WaterTime> times;
    }
}
