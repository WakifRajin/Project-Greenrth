using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class PlantUIElement : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage img;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Soil;
    public TextMeshProUGUI Desc;


    Plant plt;
    public void setElement(Plant plnt) {
        //img.texture = plnt.ImageURL;
        plt = plnt;

        Name.text = plnt.Name;
        Soil.text = plnt.SoilType;
        Desc.text = plnt.Description;

        StartCoroutine(getImage(plnt.ImageURL));
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
            img.texture = texture;
        }
        req.Dispose();
    }

    public void EnableDesc() {
        InitializeSuggestion.instance.EnableDesc(plt.Name, plt.Description);
    }
}
