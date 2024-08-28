package com.appclasses.smartcart

import android.graphics.Paint
import android.os.Bundle
import android.widget.ImageView
import android.widget.RatingBar
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.ContextCompat

data class Product(
    val name: String,
    val price: String,
    val availability: String,
    val imageUrl: String
)
class Product1 : AppCompatActivity() {

    lateinit var back: ImageView
    lateinit var myProfile: ImageView
    lateinit var discountPrice: TextView
    lateinit var ratingBar: RatingBar
    lateinit var originalPrice: TextView
    lateinit var ratingText: TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_product)
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