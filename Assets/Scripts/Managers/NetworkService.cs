using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkService
{
    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback) {
        // запрос POST с использованием объекта WWWForm или запос GET без этого объекта
        using (UnityWebRequest request = (form == null) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form)) { // создаём UnityWebRequest в режиме GET
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { // проверяем ответ на наличие ошибок
                Debug.LogError("Network problem: " + request.error);
            } else if (request.responseCode != (long) System.Net.HttpStatusCode.OK) {
                Debug.LogError("Response error: " + request.responseCode);
            } else {
                callback(request.downloadHandler.text); // делегат можно вызвать так же, как и исходную функцию
            }
        }
    }

    public IEnumerator DownloadImage(string url, Action<Texture2D> callback) { // вместо строки этот обратный вызов принимает объекты Texture2D
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request)); // получаем загруженное изображение с помощью служебной программы DownloadHandler
    }
}
