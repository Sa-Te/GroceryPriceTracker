package com.appclasses.smartcart

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat

class whychoose3 : AppCompatActivity() {
    private lateinit var btnNext3: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_whychoose3)

        btnNext3=findViewById<Button>(R.id.btnNext3);

        btnNext3.setOnClickListener {

            val intent= Intent(this,Home::class.java)
            startActivity(intent)
        }

    }
}