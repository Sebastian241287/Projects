package com.example.eftim.chatdb;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;


public class Chat extends AppCompatActivity {

    private String user;
    private String pass;
    private String lastID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chat);

        Intent myIntent = getIntent();
        user = myIntent.getStringExtra("username");
        pass = myIntent.getStringExtra("password");

        RESTTaskID restID = new RESTTaskID();
        restID.execute();

        RESTTask restTask = new RESTTask();
        restTask.execute();

    }

    private class RESTTask extends AsyncTask<Void ,Void, String> {

        private static final String Messages = "http://chatdb.azurewebsites.net/Service1.svc/Messages";

        @Override
        protected String doInBackground(Void... voids) {

            DefaultHttpClient hc = new DefaultHttpClient();
            String result = "";

            try{
                HttpGet request = new HttpGet(Messages);
                request.setHeader("Accept", "application/json");
                request.setHeader("Authorization", user + ":" + pass);
                result = EntityUtils.toString(hc.execute(request).getEntity());


            }catch(Exception e){
                e.printStackTrace();
            }

            return result;
        }

        @Override
        protected void onPostExecute(String result) {
            result = result.substring(1);
            TextView tv = (TextView)findViewById(R.id.messagesView);
            String[] mess = result.split("spaceespaciopresledek");

            for (int i = 0; i < mess.length - 1; i++) {
                tv.setText(tv.getText() + mess[i] + "\n");
            }
        }
    }

    public void poslji(View view) {

        RESTTaskSend restTask = new RESTTaskSend();
        restTask.execute();

        osvezi(view);
    }

    private class RESTTaskSend extends AsyncTask<Void ,Void, String>{

        String text = ((EditText) findViewById(R.id.editText5)).getText().toString().replaceAll(" ", "%20");
        String Message = String.format("http://chatdb.azurewebsites.net/Service1.svc/Send/%s/%s", user, text);
        @Override
        protected String doInBackground(Void... voids) {

            DefaultHttpClient hc = new DefaultHttpClient();
            HttpPost hp = new HttpPost(Message);
            try{
                hp.setHeader("Accept", "application/json");
                hp.setHeader("Authorization", user + ":" + pass);
                hc.execute(hp);
            }catch(Exception e){
                e.printStackTrace();
            }

            return null;
        }
    }

    public void osvezi(View view) {

        RESTTaskRef restRef = new RESTTaskRef();
        restRef.execute();

        RESTTaskID restID = new RESTTaskID();
        restID.execute();

    }

    private class RESTTaskRef extends AsyncTask<Void ,Void, String>{

        String newMessages = "http://chatdb.azurewebsites.net/Service1.svc/Messages/" + lastID;
        @Override
        protected String doInBackground(Void... voids) {

            DefaultHttpClient hc = new DefaultHttpClient();
            String result = "";

            try{
                HttpGet request = new HttpGet(newMessages);
                request.setHeader("Accept", "application/json");
                request.setHeader("Authorization", user + ":" + pass);
                result = EntityUtils.toString(hc.execute(request).getEntity());


            }catch(Exception e){
                e.printStackTrace();
            }

            return result;
        }

        @Override
        protected void onPostExecute(String result) {
            result = result.substring(1);
            TextView tv = (TextView)findViewById(R.id.messagesView);
            String[] mess = result.split("spaceespaciopresledek");

            for (int i = 0; i < mess.length - 1; i++) {
                tv.setText(tv.getText() + mess[i] + "\n");
            }
        }
    }

    private class RESTTaskID extends AsyncTask<Void ,Void, String>{

        private static final String Ids = "http://chatdb.azurewebsites.net/Service1.svc/Id";

        @Override
        protected String doInBackground(Void... voids) {

            DefaultHttpClient hc = new DefaultHttpClient();
            String res = "";

            try{

                HttpGet req = new HttpGet(Ids);
                res = EntityUtils.toString(hc.execute(req).getEntity());

            }catch (Exception e){
                e.printStackTrace();
            }

            return res;
        }

        @Override
        protected void onPostExecute(String res) {
            String[] allIds = res.split(",");
            lastID = allIds[0].substring(1);
        }
    }

}
