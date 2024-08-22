package com.tech.productview

import android.graphics.Paint
import android.os.Bundle
import android.widget.ImageView
import android.widget.RatingBar
import android.widget.TextView
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.ContextCompat
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.tech.viewproduct.R

class MainActivity : AppCompatActivity() {
    lateinit var back: ImageView
    lateinit var myProfile: ImageView
    lateinit var discountPrice: TextView
    lateinit var ratingBar: RatingBar
    lateinit var originalPrice: TextView
    lateinit var ratingText: TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_main)

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        // Initializing UI components
        back = findViewById(R.id.iconImageView)
        myProfile = findViewById(R.id.accountIcon)
        discountPrice = findViewById(R.id.discountedPrice)
        ratingBar = findViewById(R.id.ratingBar)
        originalPrice = findViewById(R.id.originalPrice)
        ratingText = findViewById(R.id.ratingText)

        // Setting listeners
        ratingBar.setOnRatingBarChangeListener { _, rating, _ ->
            ratingText.text = "Rating: $rating/5"
        }

        myProfile.setOnClickListener {
            Toast.makeText(applicationContext, "Mobile App Still Is Still Been Improved On.", Toast.LENGTH_SHORT).show()
        }

        back.setOnClickListener {
            finish()
        }

        // Strikethrough for discount price
        discountPrice.text = "£11.50"
        discountPrice.paintFlags = discountPrice.paintFlags or Paint.STRIKE_THRU_TEXT_FLAG

        // Set custom drawable for rating bar
        val darkPinkColor = ContextCompat.getDrawable(this, R.drawable.rating_pink_bar)
        ratingBar.progressDrawable = darkPinkColor

        // Applying paint flags to originalPrice if needed
        originalPrice.paintFlags = originalPrice.paintFlags or Paint.STRIKE_THRU_TEXT_FLAG
    }
}
