using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public GameObject currentWindow;

    [Header("Windows")]
    public GameObject homePage;
    public GameObject plantSuggWindow;
    public GameObject WaterTime;
    public GameObject WeatherPage;
    public GameObject Explore;

    [Header("Initializers")]
    public InitializeSuggestion sugg;
    public InitializeWater wattr;

    [Header("Weather References")]
    public RawImage wIcon;
    public TextMeshProUGUI wMain;
    public TextMeshProUGUI wDesc;
    public TextMeshProUGUI temp;
    public TextMeshProUGUI hum;

    WeatherInfo wInfo;


    // Update is called once per frame
    private void Awake()
    {
        instance = this;
        homePage.SetActive(true);
        plantSuggWindow.SetActive(false);
        WaterTime.SetActive(false);
        WeatherPage.SetActive(false);
        Explore.SetActive(false);
    }

    private void Start()
    {
        wInfo = Info.instance.wInfo;
    }

    public void WaterTimePage() {
        currentWindow.SetActive(false);
        currentWindow = WaterTime;
        currentWindow.SetActive(true);

        wattr.initialize();
    }

    public void ToWeatherPage()
    {
        currentWindow.SetActive(false);
        currentWindow = WeatherPage;
        currentWindow.SetActive(true);
    }

    public void PlantSuggestionPage()
    {
        currentWindow.SetActive(false);
        currentWindow = plantSuggWindow;
        currentWindow.SetActive(true);

        if (!Info.instance.dataChecked) {
            sugg.initialize();
            Info.instance.dataChecked = true;
        }
    }

    public void HomePage()
    {
        currentWindow.SetActive(false);
        currentWindow = homePage;
        currentWindow.SetActive(true);

        SaveData.instance.saveTimes();
    }

    public void ExplorePage()
    {
        currentWindow.SetActive(false);
        currentWindow = Explore;
        currentWindow.SetActive(true);
    }

    public void setInfo()
    {
        string mainW = wInfo.weather[0].main;
        string descW = wInfo.weather[0].description;

        float tmp1 = ((int)(wInfo.main.temp_min - 273) * 100) / 100.0f;
        float tmp2 = ((int)(wInfo.main.temp_max - 273) * 100) / 100.0f;

        string _temp = tmp1 + "°C" + ((tmp1 != tmp2) ? " to " + tmp2 + "°C" : "");
        string _hum = (wInfo.main.humidity) + "%";

        wMain.text = mainW;
        wDesc.text = descW;
        temp.text = (tmp1 != tmp2) ? (tmp1+ "°C" + " to " + tmp2+ "°C") : tmp1 + "°C";
        hum.text = _hum;

        StartCoroutine(getImage("https://openweathermap.org/img/wn/" + wInfo.weather[0].icon + "@2x.png"));
    }

    IEnumerator getImage(string url)
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
        }
        else
        {
            Texture texture = DownloadHandlerTexture.GetContent(req);
            wIcon.texture = texture;
        }
        req.Dispose();
    }

    public void goUrl(string Url) {
        Application.OpenURL(Url);
    }
}
