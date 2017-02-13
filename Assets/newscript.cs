
using UnityEngine;
using System.Collections;
using Vuforia;
using System;
using UnityEngine.UI;
public class newscript : MonoBehaviour, ITrackableEventHandler {

	float native_width= 1920f;
	float native_height= 1080f;
	public Texture btntexture;
	public Texture LogoTexture;
	public Texture MobiliyaTexture;
	public Texture highlight;
	public Texture scoreboard;
	public Texture chatroom;

	private TrackableBehaviour mTrackableBehaviour;

	private bool mShowGUIButton = false;
	private bool mShowGUIScoreBoard = false; 
	private bool mShowGUIHighlights = false;
	private bool mShowGUIChatRoom = false;

	public Rect logoRect = new Rect (1800-200, 500, 300, 300);
	public Rect scoreboardRect = new Rect (10, 40, 1000, 300);
	public Rect highlightRect = new Rect (20, 20, 500, 500);
	public Rect chatroomRect = new Rect (20, 20, 500, 500);

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			mShowGUIButton = true;
		}
		else
		{
			mShowGUIButton = false;
		}
	}

	void OnGUI() {
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1));

		Rect mButtonRect = new Rect(1800-215,550,100,100);
		GUIStyle myTextStyle = new GUIStyle(GUI.skin.textField);
		myTextStyle.fontSize = 150;
		myTextStyle.richText=true;

		GUI.DrawTexture(new Rect(5,1080- 115,110,110),LogoTexture); 
		GUI.DrawTexture (new Rect (1530, 970, 350, 110), MobiliyaTexture);

		if (!btntexture) // This is the button that triggers AR and UI camera On/Off
		{
			Debug.LogError("Please assign a texture on the inspector");
			return;
		}

		if (mShowGUIButton) {

			if (GUI.Button(new Rect(1700-215,800,400,50), "Score Board")) {
				mShowGUIButton = false;
				mShowGUIScoreBoard = true;

			}
			if (GUI.Button(new Rect(1700-215,900,400,50), "Highlights")) {
				mShowGUIButton = false;
				mShowGUIHighlights = true;
			}
			if (GUI.Button(new Rect(1700-215,1000,400,50), "Live Chat Room")) {
				mShowGUIButton = false;
				mShowGUIChatRoom = true;
			}
		}
		if (mShowGUIScoreBoard) { 
			GUI.DrawTexture (scoreboardRect, scoreboard);
			//scoreboardRect = GUI.Window(0, scoreboardRect, DoMyWindow, scoreboard);

			if(GUI.Button(new Rect(1700-215, 1000, 400, 50), "Back")) { 
				mShowGUIScoreBoard = false; 
				mShowGUIButton = true; 
			}
		}
		if (mShowGUIHighlights) { 
			highlightRect.height = 400;
			highlightRect.width = 400;
			GUI.DrawTexture (highlightRect, highlight);
			//highlightRect = GUI.Window(1, highlightRect, DoMyWindow, highlight);

			if(GUI.Button(new Rect(1700-215, 1000, 400, 50), "Back")) { 
				mShowGUIHighlights = false; 
				mShowGUIButton = true; 
			}
		}
		if (mShowGUIChatRoom) { 
			GUI.DrawTexture (chatroomRect, chatroom);
			//chatroomRect = GUI.Window(2, chatroomRect, DoMyWindow, chatroom);

			if(GUI.Button(new Rect(1700-215, 1000, 400, 50), "Back")) { 
				mShowGUIChatRoom = false; 
				mShowGUIButton = true; 
			}
		}
	}

	public void OpenVideoActivity()
	{
		var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
		// Accessing the class to call a static method on it
		var jc = new AndroidJavaClass("com.mobiliya.gepoc.StartVideoActivity");
		// Calling a Call method to which the current activity is passed
		jc.CallStatic("Call", jo);
	}

	void DoMyWindow(int windowID) {
		
	}

}  