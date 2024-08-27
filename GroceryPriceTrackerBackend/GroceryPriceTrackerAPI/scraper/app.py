from fastapi import FastAPI
import uvicorn

app = FastAPI()

@app.get("/scrape")
async def scrape(query: str):
    from scraper import search_tesco
    data = await search_tesco(query)
    return data

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
