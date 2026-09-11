using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    public static AdsInitializer Instance {get; private set;}

    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;
    private string gameId;
    private string interstitialAdUnitId = "Interstitial_Android";
    private string rewardedAdUnitId = "Rewarded_Android";

    private Text moneyText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAds();
    }

    public void InitializeAds()
    {

#if UNITY_IOS
 gameId = iOSGameId;
#else
        gameId = androidGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }
    }
  
    public void LoadInterstitialAd()
    {
        Advertisement.Load(interstitialAdUnitId, this);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(interstitialAdUnitId, this);
    }

    public void GetInterstitialAd()
    {
        ShowInterstitialAd();
        LoadInterstitialAd();
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(rewardedAdUnitId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(rewardedAdUnitId, this);
    }

    public void GetRewardedAd()
    {
        ShowRewardedAd();
    }

    public void GetRewardedAd(Text textToEdit)
    {
        ShowRewardedAd();
        moneyText = textToEdit;
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadInterstitialAd();
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
    }


    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(rewardedAdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            GameManager.Instance.money += 100;
            moneyText.text = GameManager.Instance.money.ToString();
            moneyText = null;

            LoadRewardedAd();
        }
    }
}