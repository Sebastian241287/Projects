package com.example.eftim.chatdb;

import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.content.Intent;
import android.widget.EditText;
import android.widget.TextView;

import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONArray;
import org.json.JSONObject;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class Login extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
    }

    public void prijava(View view) {

        RESTTask restTask = new RESTTask();
        restTask.execute();

    }

    public static String md5(String string) {
        byte[] hash;

        try {
            hash = MessageDigest.getInstance("MD5").digest(string.getBytes("UTF-8"));
            StringBuilder hex = new StringBuilder(hash.length * 2);

            for (byte b : hash) {
                int i = (b & 0xFF);
                if (i < 0x10) hex.append('0');
                hex.append(Integer.toHexString(i));
            }

            return hex.toString();
        } catch (Exception e){
            e.printStackTrace();
        }

        return "";
    }

    private class RESTTask extends AsyncTask<Void, Void, String>{

        String user = ((EditText) findViewById(R.id.editText)).getText().toString();
        String pass = ((EditText) findViewById(R.id.editText2)).getText().toString();

        String hashPass = md5(pass);

        String userPass = "http://chatdb.azurewebsites.net/Service1.svc/Login/" + user + "/" + hashPass;
        @Override
        protected String doInBackground(Void... voids) {

            DefaultHttpClient hc = new DefaultHttpClient();
            String result = "";

            try {
                HttpGet request = new HttpGet(userPass);
                result = EntityUtils.toString(hc.execute(request).getEntity());

            } catch (Exception e) {
                e.printStackTrace();
            }

            return result;
        }

        @Override
        protected void onPostExecute(String result) {
            if (result.equals("1")){
                Intent intent = new Intent(Login.this, Chat.class);
                intent.putExtra("username", user);
                intent.putExtra("password", hashPass);

                startActivity(intent);
            }
        }

    }

}
