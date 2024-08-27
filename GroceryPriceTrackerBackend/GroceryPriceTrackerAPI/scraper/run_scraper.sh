#!/bin/bash

# Install necessary browser binaries for Playwright
python -m playwright install

# Run the scraper
python scraper.py
