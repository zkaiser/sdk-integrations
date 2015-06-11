package com.example.myapp;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import io.presage.Presage;
import io.presage.utils.IADHandler;

public class MyActivity extends Activity {
    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Presage.getInstance().setContext(this.getBaseContext());
        Presage.getInstance().start();
    }

    @Override
    protected void onResume() {
        super.onResume();

        Presage.getInstance().adToServe("interstitial", new IADHandler() {

            @Override
            public void onAdNotFound() {
                Log.i("PRESAGE", "ad not found");
            }

            @Override
            public void onAdFound() {
                Log.i("PRESAGE", "ad found");
            }

            @Override
            public void onAdClosed() {
                Log.i("PRESAGE", "ad closed");
            }
        });
    }
}
