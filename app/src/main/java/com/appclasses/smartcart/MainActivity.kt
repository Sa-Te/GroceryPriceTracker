package com.appclasses.smartcart

import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash)

        // Delay for a few seconds and then start the welcome activity
        Handler(Looper.getMainLooper()).postDelayed({
            val intent = Intent(this, welcome::class.java)
            startActivity(intent)
            finish() // Close this activity
        }, 2000) // 2 seconds delay
    }
}
