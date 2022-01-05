using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkService
{
    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback) {
        // ������ POST � �������������� ������� WWWForm ��� ����� GET ��� ����� �������
        using (UnityWebRequest request = (form == null) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form)) { // ������ UnityWebRequest � ������ GET
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { // ��������� ����� �� ������� ������
                Debug.LogError("Network problem: " + request.error);
            } else if (request.responseCode != (long) System.Net.HttpStatusCode.OK) {
                Debug.LogError("Response error: " + request.responseCode);
            } else {
                callback(request.downloadHandler.text); // ������� ����� ������� ��� ��, ��� � �������� �������
            }
        }
    }

    public IEnumerator DownloadImage(string url, Action<Texture2D> callback) { // ������ ������ ���� �������� ����� ��������� ������� Texture2D
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request)); // �������� ����������� ����������� � ������� ��������� ��������� DownloadHandler
    }
}
