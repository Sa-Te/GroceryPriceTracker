package com.appclasses.smartcart

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat

class whychoose1 : AppCompatActivity() {
    private lateinit var btnNext1: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_whychoose1)

        btnNext1    =findViewById<Button>(R.id.btnNext1);

        btnNext1.setOnClickListener {

            val intent= Intent(this,whychoose2::class.java)
            startActivity(intent)
        }


    }
}