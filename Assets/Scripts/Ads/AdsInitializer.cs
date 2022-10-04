using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
<<<<<<< Updated upstream
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    public string _gameId;
    [SerializeField] bool _testMode = true;
=======
    [SerializeField] string AndroidGameID = "4954199";
    [SerializeField] string IOSGameID = "4954198";
    [SerializeField] bool testMode = true;
    private string gameID;
>>>>>>> Stashed changes

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameID : AndroidGameID;
        Advertisement.Initialize(gameID, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed: {error.ToString()} - {message}");
    }
}
