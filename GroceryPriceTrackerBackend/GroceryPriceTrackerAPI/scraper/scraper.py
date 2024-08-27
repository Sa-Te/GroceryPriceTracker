import asyncio
from playwright.async_api import async_playwright

async def search_tesco(query):
    async with async_playwright() as playwright:
        browser = await playwright.chromium.launch(
            headless=False,
            args=["--disable-http2"]  # Disable HTTP/2
        )
        context = await browser.new_context()
        page = await context.new_page()

        try:
            # Navigate to Tesco search page
            search_url = f"https://www.tesco.com/groceries/en-GB/search?query={query}"
            await page.goto(search_url, timeout=60000)  # Increase timeout to 60 seconds

            # Wait for products to load
            await page.wait_for_selector(".styles__StyledTileContentWrapper-dvv1wj-2", timeout=60000)

            # Get all products listed on the page
            products = await page.query_selector_all(".styles__StyledTileContentWrapper-dvv1wj-2")

            scraped_data = []

            for product in products:
                name_element = await product.query_selector(".styles__H3-oa5soe-0.gbIAbl")
                image_element = await product.query_selector(".product-image img")
                price_element = await product.query_selector(".price")
                availability_element = await product.query_selector(".ddsweb-in-line-messaging__inner-container")

                name = await name_element.inner_text() if name_element else "No name"
                image_url = await image_element.get_attribute('src') if image_element else "No image"
                price = await price_element.inner_text() if price_element else "Price not available"
                availability = await availability_element.inner_text() if availability_element else "In Stock"

                product_data = {
                    "name": name,
                    "image_url": image_url,
                    "price": price,
                    "availability": availability
                }
                scraped_data.append(product_data)

            return scraped_data

        except Exception as e:
            print(f"An error occurred: {e}")
            return []

        finally:
            await browser.close()

