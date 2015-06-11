#pragma strict

//we create a blank texture that actually has no texture attached so we can hide unity's generic button graphics. they are added to the buttons below to do this.
private var blankGfx:Texture;

function Start () {
	#if UNITY_ANDROID
     // Creation of our WHAndlerImpl to Handle Events
    var handlerImpl = new WHandlerImpl();

    // Making AdToServe call to try to shox an Ad if available
    WPresage.AdToServe("interstitial", handlerImpl);
#endif

}
function OnGUI () {

//Play 1 Axis
if(GUI.Button(Rect(Screen.width/3,Screen.height/2.07,Screen.width/3,Screen.height/6), blankGfx, "")){
Application.LoadLevel("game");
}
//Play 2 Axis
if(GUI.Button(Rect(Screen.width/3,Screen.height/1.47,Screen.width/3,Screen.height/6), blankGfx, "")){
Application.LoadLevel("game2");
}
}

/**
 * Implementation of the Handler used for AdToServe
 */
class WHandlerImpl extends MonoBehaviour implements WPresage.IADHandler {
    function OnAdNotFound() {
        // Not Found
    }

    function OnAdFound() {
        // Found
    }

    function OnAdClosed() {
        // Closed
    }
}