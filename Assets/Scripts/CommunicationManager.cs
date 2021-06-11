using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CommunicationManager : MonoBehaviour
{
	public static IEnumerator ConnectServer()
	{
		UnityWebRequest request = UnityWebRequest.Get("https://52.68.212.92/public/registration");
		yield return request.SendWebRequest();

		if(!string.IsNullOrEmpty(request.error))
		{
			Debug.Log(request.error);
		}
		Debug.Log(request.downloadHandler.text);
	}
}
