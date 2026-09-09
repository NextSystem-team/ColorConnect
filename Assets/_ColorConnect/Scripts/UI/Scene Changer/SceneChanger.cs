using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    [SerializeField] private string sceneName;

    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        
        if(sceneName == "MainMenu")
        {
            ShowAds();
        }
    }

    void ShowAds()
    {
        GameObject.Find("AdsInitializer").GetComponent<AdsInitializer>().LoadInterstitialAd();
        GameObject.Find("AdsInitializer").GetComponent<AdsInitializer>().ShowInterstitialAd();

    }
}
