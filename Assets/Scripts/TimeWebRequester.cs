using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeWebRequester : MonoBehaviour
{
    private const string TimeUrl = "https://yandex.com/time/sync.json";

    public event Action<long> OnTimeSynchronization;

    private void Start()
    {
        StartCoroutine(GetTimeJson());
    }

    private IEnumerator GetTimeJson()
    {
        while (true)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(TimeUrl))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + request.error);
                }
            
                else
                {
                    string jsonResponse = request.downloadHandler.text;
                    TimeJson responseObject = JsonUtility.FromJson<TimeJson>(jsonResponse);
                    OnTimeSynchronization?.Invoke(responseObject.time);
                }
            }
            
            yield return new WaitForSeconds(3600);
        }
    }
}