package io.presage;

import org.apache.cordova.CordovaPlugin;
import org.apache.cordova.CordovaInterface;
import org.apache.cordova.CordovaWebView;
import org.apache.cordova.PluginResult;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import io.presage.Presage;
import io.presage.utils.IADHandler;
import android.util.Log;
import org.apache.cordova.CallbackContext;

/**
 * This class is used to do Presage calls
 */
public class CPresage extends CordovaPlugin {

    private static final String AD_TO_SERVE = "adToServe";

    @Override
    public boolean execute(String action, JSONArray args, CallbackContext callbackContext) throws JSONException {
        if (action.equals(AD_TO_SERVE)) {
            adToServe(callbackContext);
            return true;
        }
        return false;
    }

    @Override
    public void initialize(final CordovaInterface cordova, CordovaWebView webView) {
        super.initialize(cordova, webView);
        cordova.getActivity().runOnUiThread(new Runnable() {
            public void run() {
                Presage.getInstance().setContext(cordova.getActivity().getApplicationContext());
            }
        });
        this.start();
    }

    private void start() {
        cordova.getActivity().runOnUiThread(new Runnable() {
            public void run() {
                Presage.getInstance().start();
            }
        });
    }

    private void adToServe(final CallbackContext callbackContext) {
        Presage.getInstance().adToServe("interstitial", new IADHandler() {
            @Override
            public void onAdNotFound() {
              callbackContext.error("AdNotFound");
            }

            @Override
            public void onAdFound() {
                PluginResult adFountResult = new PluginResult(PluginResult.Status.OK, "AdFound");
                adFountResult.setKeepCallback(false);
                callbackContext.sendPluginResult(adFountResult);
            }

            @Override
            public void onAdClosed() {
                PluginResult adClosedResult = new PluginResult(PluginResult.Status.OK, "AdClosed");
                adClosedResult.setKeepCallback(true);
                callbackContext.sendPluginResult(adClosedResult);
            }
        });
    }
}
