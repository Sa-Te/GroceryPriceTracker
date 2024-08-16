package com.appclasses.smartcart

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat

class whychoose2 : AppCompatActivity() {
    private lateinit var btnNext2: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_whychoose2)

        btnNext2=findViewById<Button>(R.id.btnNext2);

        btnNext2.setOnClickListener {

            val intent= Intent(this,whychoose3::class.java)
            startActivity(intent)
        }

    }
}