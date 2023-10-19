namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Networking;
    using System.Collections;

    // Persistent singleton
    public class AppManager : MonoBehaviour
    {
        private const string URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        private const string GAME_SCENE = "Game";

        [SerializeField] JengaBlockData[] data;

        private static AppManager instance;
        private string jsonString;
        private static bool isInitialized = false;

        void Awake()
        {
            if (instance == null)
            instance = this;
        }

        IEnumerator Start()
        {
            yield return StartCoroutine(ProcessRequest(URL));        

            if (string.IsNullOrEmpty(jsonString))
            {
                Debug.LogError("Failed to load data from, " + URL, this);
            }
            else
            {
                Debug.Log("Successfully loaded data from, " + URL, this);
                        
                isInitialized = true;
            }
        }

        public static void GenerateRequest()
        {
            if (!instance)
                return;

            instance.StartCoroutine(instance.ProcessRequest(URL));
        }

        public IEnumerator ProcessRequest(string url)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    jsonString = request.downloadHandler.text;
                    jsonString = JsonHelper.AdjustJson(jsonString);                     
                    data = JsonHelper.FromJson<JengaBlockData>(jsonString);
                }
            }
        }

        public static bool IsInitialized()
        {
            return instance && isInitialized;
        }

        public static JengaBlockData[] GetLoadedData()
        {
            if (instance)
                return instance.data;
            else
                return new JengaBlockData[0];
        }
    }
}