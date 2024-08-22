package com.appclasses.smartcart

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat

class welcome : AppCompatActivity() {

    private lateinit var btnUnlock: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_welcome)

        btnUnlock=findViewById<Button>(R.id.btnUnlock);

        btnUnlock.setOnClickListener {

            val intent= Intent(this,welcome2::class.java)
            startActivity(intent)
        }

    }
}