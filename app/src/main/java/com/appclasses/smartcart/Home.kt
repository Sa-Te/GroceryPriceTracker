package com.appclasses.smartcart

import android.os.Bundle
import android.util.Log
import android.widget.EditText
import android.widget.ImageView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.Response
import java.io.IOException

class Home : AppCompatActivity() {

    private lateinit var recyclerView: RecyclerView
    private lateinit var productsAdapter: ProductsAdapter
    private lateinit var searchBar: EditText
    private lateinit var searchButton: ImageView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home)

        recyclerView = findViewById(R.id.recycler_view)
        searchBar = findViewById(R.id.search_bar)
        searchButton = findViewById(R.id.search_button)

        recyclerView.layoutManager = LinearLayoutManager(this)

        searchButton.setOnClickListener {
            Log.d("Home", "Search button clicked")
            val query = searchBar.text.toString()
            if (query.isNotEmpty()) {
                fetchProducts(query)
            }
        }
    }

    private fun fetchProducts(query: String) {
        val client = OkHttpClient()
        val request = Request.Builder()
            .url("http://10.0.2.2:5000/api/scraper/search?query=$query") // Replace with your actual backend URL
            .build()

        client.newCall(request).enqueue(object : okhttp3.Callback {
            override fun onFailure(call: okhttp3.Call, e: IOException) {
                Log.e("Home", "Failed to fetch products", e)
            }

            override fun onResponse(call: okhttp3.Call, response: Response) {
                response.body?.string()?.let { json ->
                    val productType = object : TypeToken<List<Product>>() {}.type
                    val products: List<Product> = Gson().fromJson(json, productType)

                    runOnUiThread {
                        productsAdapter = ProductsAdapter(products)
                        recyclerView.adapter = productsAdapter
                        Log.d("Home", "Products fetched and adapter updated")
                    }
                }
            }
        })
    }
}
