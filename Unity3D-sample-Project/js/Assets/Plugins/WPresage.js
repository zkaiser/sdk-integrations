/**
 * This version of Presage's Wrapper was tested with 1.6.0 Presage lib
 */
public class WPresage
{
	private static final var WPRESAGE_ID = "io.presage.Presage";
	private static final var WHANDLER_ID = "io.presage.utils.IADHandler";

	// Method used to Initialize Presage SDK
	static function Initialize() {
		var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var activity = unityPlayer.GetStatic.<AndroidJavaObject>("currentActivity");
		
		activity.Call("runOnUiThread", new AndroidJavaRunnable(function()
       	{
			var presageClass = new AndroidJavaClass(WPRESAGE_ID);
			var presage = presageClass.CallStatic.<AndroidJavaObject> ("getInstance");
			presage.Call("setContext", activity);
			presage.Call("start");
		}));
	}

	/**
	 * Method used to Call AdToServ to show interstitial
	*/
	static function AdToServe(name, handler) {
		var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var activity = unityPlayer.GetStatic.<AndroidJavaObject>("currentActivity");
		
		activity.Call("runOnUiThread", new AndroidJavaRunnable(function()
       	{
			var presageClass = new AndroidJavaClass(WPRESAGE_ID);
			var presage = presageClass.CallStatic.<AndroidJavaObject> ("getInstance");
			var proxy = new WHandlerProxy (handler);
			presage.Call("adToServe", name, proxy);
		}));
	}

	/**
	 * Proxy to interface with the WHandler of the jar
	*/
	public class WHandlerProxy extends AndroidJavaProxy {

		var myHandler: IADHandler;

		function WHandlerProxy(handler) {
			super(WHANDLER_ID);
			myHandler = handler;
		}

		function onAdNotFound() {
			// Add was'nt found
			myHandler.OnAdNotFound();
		}

		function onAdFound() {
			// Add was found
			myHandler.OnAdFound();
		}

		function onAdClosed() {
			// Add was closed
			myHandler.OnAdClosed();
		}
	}

	/**
	 * Interface to be implemented by the app
	 */
	public interface IADHandler {
		function OnAdNotFound();
		function OnAdFound();
		function OnAdClosed();
	}
}