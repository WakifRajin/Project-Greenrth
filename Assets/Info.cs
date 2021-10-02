using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    public static Info instance;

    [Header("User info")]
    public string userIP = "";
    public LocationInfo loc;

    [Header("Weather info")]
    public WeatherInfo wInfo;

    public Data data;
    public bool dataFound = false;
    public bool dataChecked = false;

    public List<WaterTime> times;


    public List<string> rainConditions;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}

// ------------------------------------------------------- Plant Data -------------------------------------------------------//
[System.Serializable]
public class Data
{
    public string Name;
    public Plant[] plant;
}

[System.Serializable]
public class Plant
{
    public string Name;
    public string SoilType;
    public string Description;
    public Vector2 tempRange;
    public Vector2 HumidRange;
    public string ImageURL;
}
// ------------------------------------------------------- Plant Data -------------------------------------------------------//

// ------------------------------------------------- Water Notification Data ------------------------------------------------//
[System.Serializable]
public class WaterTime
{
    public string TimeOfDay = "Morning";
    public int time = 830;
    public bool enabled = true;

    public int getTimeInt() {
        int h = (time / 100);
        int m = time - h*100;

        if (m == 50) { m = 30; }

        return h * 100 + m;
    }

    public string getTime()
    {
        int tm = getTimeInt();

        int h = (int)(tm / 100);
        int m = tm - h*100;

        string AMPM = (h >= 12)? "PM" : "AM";

        h = (h > 12)? h - 12 : h;

        string hr = (h < 10) ? "0" + h : ""+h;
        string mn = (m == 0) ? "00" : "" + m;

        return hr + " : " + mn + " " + AMPM;
    }
}
// ------------------------------------------------- Water Notification Data ------------------------------------------------//

// ------------------------------------------------------ Location Data -----------------------------------------------------//
[System.Serializable]
public class LocationInfo
{
   public string query = "24.48.0.1";
   public string status = "success";
   public string country = "Canada";
   public string countryCode = "CA";
   public string region = "QC";
   public string regionName = "Quebec";
   public string city = "Montreal";
   public string zip = "H1K";
   public float lat = 45.6085f;
   public float lon = -73.5493f;
   public string timezone = "America/Toronto";
   public string isp = "Le Groupe Videotron Ltee";
   public string org = "Videotron Ltee";
   public string @as = "AS5769 Videotron Telecom Ltee";
}
// ------------------------------------------------------ Location Data -----------------------------------------------------//


// ------------------------------------------------------- Weather Data -----------------------------------------------------//
[System.Serializable]
public class WeatherInfo
{
    public WeatherDesc []weather;
    public MainData main;
}

[System.Serializable]
public class WeatherDesc
{

    public int id;
    public string main;
    public string description;
    public string icon;     
}

[System.Serializable]
public class MainData
{
    public float temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public float pressure;
    public float humidity;
}
// ------------------------------------------------------- Weather Data -----------------------------------------------------//