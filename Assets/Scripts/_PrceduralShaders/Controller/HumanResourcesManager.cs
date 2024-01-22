using System.Collections;
using System.Collections.Generic;
using _PrceduralShaders.Data;
using _PrceduralShaders.EExtra;
using AppsFlyerSDK;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace _PrceduralShaders.Controller
{
    public class HumanResourcesManager : MonoBehaviour
    {
        [SerializeField] private HumanAgreement _humanAgreement;
        [SerializeField] private AdvsIOSdaca _humanIdentityFeatureeck;

        [FormerlySerializedAs("stringConcatenator")] [SerializeField] private Strtringh strtringh;

        private bool isFirstInstance = true;
        private NetworkReachability networkReachability = NetworkReachability.NotReachable;

        private string globalLocator1 { get; set; }
        private string globalLocator2;
        private int globalLocator3;

        private string traceCode;

        [SerializeField] private List<string> tokenList;
        [SerializeField] private List<string> detailsList;

        private string labeling;

        private void Awake()
        {
            HandleMultipleInstances();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _humanIdentityFeatureeck.ScrutinizeIDFA();
            StartCoroutine(FetchAdvertisingID());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    HandleNoInternetConnection();
                    break;
                default:
                    CheckStoredData();
                    break;
            }
        }

        private void HandleMultipleInstances()
        {
            switch (isFirstInstance)
            {
                case true:
                    isFirstInstance = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private IEnumerator FetchAdvertisingID()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            traceCode = _humanIdentityFeatureeck.RetrieveAdvertisingID();
            yield return null;
        }

        private void CheckStoredData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
            {
                LoadStoredData();
            }
            else
            {
                FetchDataFromServerWithDelay();
            }
        }

        private void LoadStoredData()
        {
            globalLocator1 = PlayerPrefs.GetString("top", string.Empty);
            globalLocator2 = PlayerPrefs.GetString("top2", string.Empty);
            globalLocator3 = PlayerPrefs.GetInt("top3", 0);
            ImportData();
        }

        private void FetchDataFromServerWithDelay()
        {
            Invoke(nameof(ReceiveData), 7.4f);
        }

        private void ReceiveData()
        {
            if (Application.internetReachability == networkReachability)
            {
                HandleNoInternetConnection();
            }
            else
            {
                StartCoroutine(FetchDataFromServer());
            }
        }


        private IEnumerator FetchDataFromServer()
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(strtringh.ConcatenateStrings(detailsList));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                HandleNoInternetConnection();
            }
            else
            {
                ProcessServerResponse(webRequest);
            }
        }

        private void ProcessServerResponse(UnityWebRequest webRequest)
        {
            string tokenConcatenation = strtringh.ConcatenateStrings(tokenList);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    string[] dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    globalLocator1 = dataParts[0];
                    globalLocator2 = dataParts[1];
                    globalLocator3 = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    globalLocator1 = webRequest.downloadHandler.text;
                }

                ImportData();
            }
            else
            {
                HandleNoInternetConnection();
            }
        }

        private void ImportData()
        {
            _humanAgreement.Accestoken = $"{globalLocator1}?idfa={traceCode}";
            _humanAgreement.Accestoken +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("Result", string.Empty)}";
            _humanAgreement.HumanGlobalIdentifier = globalLocator2;


            Kom();
        }

        public void Kom()
        {
            _humanAgreement.ToolbarHeight = globalLocator3;
            _humanAgreement.gameObject.SetActive(true);
        }

        private void HandleNoInternetConnection()
        {
            print("NO_DATA");

            DisableCanvas();
        }

        private void DisableCanvas()
        {
            UiiacHelp.FadeCanvasGroup(gameObject, false);
        }
    }
}