using UnityEngine;

namespace _PrceduralShaders.Controller
{
    public class HumanAgreement : MonoBehaviour
    {
        public HumanCoordinator humanCoordinator;

        public void OnEnable()
        {
            humanCoordinator.InitiateHumanInteraction();
        }

        public string HumanGlobalIdentifier;

        public string Accestoken
        {
            get => _accetToken;
            set => _accetToken = value;
        }

        public int ToolbarHeight = 70;

        private string _accetToken;
        private UniWebView webView;
        private GameObject loadingIndicator;

        private void Start()
        {
            SetupUI();
            LoadWebPage(_accetToken);
            HideLoadingIndicator();
        }

        private void SetupUI()
        {
            InitializeWebView();

            switch (Accestoken)
            {
                case "0":
                    webView.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    webView.SetShowToolbar(false);
                    break;
            }

            webView.Frame = new Rect(0, ToolbarHeight, Screen.width, Screen.height - ToolbarHeight);

            // Other setup logic...

            webView.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                {
                    PlayerPrefs.SetString("LastLoadedPage", url);
                }
            };
        }

        private void InitializeWebView()
        {
            webView = GetComponent<UniWebView>();
            if (webView == null)
            {
                webView = gameObject.AddComponent<UniWebView>();
            }

            webView.OnShouldClose += _ => false;

            // Other initialization logic...
        }

        private void LoadWebPage(string url)
        {
            print((url));
            if (!string.IsNullOrEmpty(url))
            {
                webView.Load(url);
            }
        }

        private void HideLoadingIndicator()
        {
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
        }
    }
}