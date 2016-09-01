using System;
using System.Collections;
using UnityEngine;

namespace Oxide.Ext.Hive.Unity
{



	class UnityWWW : MonoBehaviour
	{

		/// <summary>
		/// Downloads the URL.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="url">URL.</param>
		/// <param name="callback">Callback.</param>
		public void DownloadURL(string url, Action<WWW> callback)
		{
			StartCoroutine(requestURL(url, callback));
		}

		private IEnumerator requestURL(string url,Action<WWW> callback)
		{
			WWW www = new WWW(url);
			yield return www;
			callback.Invoke(www);
		}
	}
}