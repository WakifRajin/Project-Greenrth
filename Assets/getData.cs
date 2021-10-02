using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class getData : MonoBehaviour
{
    Info info;
    public Button suggestionButton;
    public TextMeshProUGUI buttonText;

    public Button weatherbutton;
    public TextMeshProUGUI wBTxt;
    public Color color;

    public string version = "http://drive.google.com/uc?export=download&id=1TWnq5RjDkAIiUXVYQ_jdx0phyrdjyCtx";
    public string JSON_URL = "http://drive.google.com/uc?export=download&id=1Hdu-0ilf-7iy0wQEY7HZ9EiBzs6ymX_c";
    public string ip_URL = "https://bot.whatismyipaddress.com/";
    public string loc_URL = "http://ip-api.com/json/";

    public string weatherAPI = "api.openweathermap.org/data/2.5/weather?";
    public string ApiKey = "e6146d216fc2b76b8563f915bfbabd91";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getIP(ip_URL));
        info = Info.instance;
    }

    IEnumerator getIP(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error:: " + req.error);
        }
        else
        {   
            string ip = req.downloadHandler.text;

            info.userIP = ip;
            StartCoroutine(getLoc(loc_URL, ip));

        }
        req.Dispose();
    }

    IEnumerator getLoc(string url, string ip) 
    {
        UnityWebRequest req = UnityWebRequest.Get(url+ip);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error:: " + req.error);
        }
        else
        {
            info.loc = JsonUtility.FromJson<LocationInfo>(req.downloadHandler.text);
            StartCoroutine(getJsonData(JSON_URL));
            StartCoroutine(getWeather(weatherAPI, ApiKey));
        }
        req.Dispose();
    }

    IEnumerator getWeather(string url, string key)
    {
        string link = url + "lat=" + info.loc.lat + "&lon=" + info.loc.lon + "&appid=" + key;

        UnityWebRequest req = UnityWebRequest.Get(link);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error:: " + req.error);
        }

        else
        {
            string jsonTxt = req.downloadHandler.text;
            info.wInfo = JsonUtility.FromJson<WeatherInfo>(jsonTxt);
            weatherbutton.interactable = true;
            wBTxt.text = "Check Weather";
            wBTxt.color = color;

            Manager.instance.setInfo();
        }
        req.Dispose();
    }

    IEnumerator getJsonData(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error:: " + req.error);
        }
        else
        {
            info.data = JsonUtility.FromJson<Data>(req.downloadHandler.text);
            info.dataFound = true;
            suggestionButton.interactable = true;
            buttonText.text = "Plant Suggestions";
        }
        req.Dispose();
    }
}


