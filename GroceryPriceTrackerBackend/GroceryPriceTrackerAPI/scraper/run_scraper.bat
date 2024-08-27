@echo off
echo Installing Playwright browser binaries...
python -m playwright install

echo Running the scraper...
python scraper.py

pause
