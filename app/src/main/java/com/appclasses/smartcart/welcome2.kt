package com.appclasses.smartcart

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat

class welcome2 : AppCompatActivity() {
    private lateinit var btnNext: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_welcome2)

        btnNext=findViewById<Button>(R.id.btnNext);

        btnNext.setOnClickListener {

            val intent= Intent(this,whychoose1::class.java)
            startActivity(intent)
        }


    }
}