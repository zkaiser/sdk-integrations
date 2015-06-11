using UnityEngine;
using System.Collections;
using KiiCorp.Cloud.Storage;
using System;
using KiiCorp.Cloud.Analytics;

public class AuthForm : MonoBehaviour {

	bool OnCallback = false;
	string username = "";
	string password = "";
	KiiUser user = null;

	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID
		// Initializing WPresage
		WPresage.Initialize();
		// Creation of our WHAndlerImpl to Handle Events
		WHandlerImpl handlerImpl = new WHandlerImpl();
		
		// Making AdToServe call to try to shox an Ad if available
		WPresage.AdToServe("interstitial", handlerImpl);
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/**
 * Implementation of the Handler used for AdToServe
 */
	public class WHandlerImpl : WPresage.IADHandler {
		
		public void OnAdNotFound() {
			Debug.Log ("onAdNotFound");
		}
		
		public void OnAdFound() {
			Debug.Log ("onAdFound");
		}
		
		public void OnAdClosed() {
			Debug.Log ("onAdClosed");
		}
	}

	void OnGUI () {
		if (OnCallback)
			GUI.enabled = false;
		else
			GUI.enabled = true;
		
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.BeginVertical ();
		GUI.contentColor = Color.blue;
		
		if (Kii.AppId == null || Kii.AppKey == null || Kii.AppId.Equals ("__KII_APP_ID__") || Kii.AppKey.Equals ("__KII_APP_KEY__")) {
			GUILayout.Space (10);
			GUILayout.Label ("Invalid API keys. See Assets/Readme.txt", GUILayout.ExpandWidth (false));
			GUILayout.Space (20);
			if (GUILayout.Button ("Get API Keys", GUILayout.MinHeight (50), GUILayout.MinWidth (100))) {
				Application.OpenURL("http://developer.kii.com");
			}
		} else {
			GUILayout.Label ("Username");
			username = GUILayout.TextField (username, GUILayout.MinWidth (200));
			GUILayout.Space (10);
			GUILayout.Label ("Password");
			password = GUILayout.PasswordField (password, '*', GUILayout.MinWidth (100));
			GUILayout.Space (30);
			
			if (GUILayout.Button ("Login", GUILayout.MinHeight (50), GUILayout.MinWidth (100))) {
				if( username.Length == 0 || password.Length == 0 )
					Debug.Log ("Username/password can't be empty");
				else {
					Login ();
				}
			}
			
			if (GUILayout.Button ("Register", GUILayout.MinHeight (50), GUILayout.MinWidth (100))) {
				if( username.Length == 0 || password.Length == 0 )
					Debug.Log ("Username/password can't be empty");
				else {
					Register ();
				}
			}
			
			if (user != null) {
				OnCallback = false;
				Application.LoadLevel ("Level");
			}
		}

		GUILayout.EndVertical ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndArea ();
	}

	private void Login () {
		user = null;
		OnCallback = true;
		KiiUser.LogIn(username, password, (KiiUser user2, Exception e) => {
			if (e == null) {
				Debug.Log ("Login completed");
				user = user2;
			} else {
				user = null;
				OnCallback = false;
				Debug.Log ("Login failed : " + e.ToString());
			}
		});
	}
	
	private void Register () {
		user = null;
		OnCallback = true;
		KiiUser built_user = KiiUser.BuilderWithName (username).Build ();
		built_user.Register(password, (KiiUser user2, Exception e) => {
			if (e == null)
			{
				user = user2;
				Debug.Log ("Register completed");
			} else {
				user = null;
				OnCallback = false;
				Debug.Log ("Register failed : " + e.ToString());
			}
			
		});
	}
}
