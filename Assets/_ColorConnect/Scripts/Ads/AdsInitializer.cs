using UnityEngine;
using UnityEngine.Advertisements;


public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{
    
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true; 
    private string gameId; 
    private string interstitialAdUnitId = "Interstitial_Android"; 
    private string rewardedAdUnitId = "Rewarded_Android"; 

    
    void Awake()
    {
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
    // Carrega um anúncio intersticial
    public void LoadInterstitialAd()
    {
        Advertisement.Load(interstitialAdUnitId, this);
    }
    
    public void ShowInterstitialAd()
    {
        Advertisement.Show(interstitialAdUnitId, this); 
    }

    
    public void LoadRewardedAd()
    {
        Advertisement.Load(rewardedAdUnitId, this);
    }
    
    public void ShowRewardedAd()
    {
        Advertisement.Show(rewardedAdUnitId, this);
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
            Debug.Log("Rewarded ad completed! Give reward to player.");
            
        }
    }
}