import pandas as pd
from google_play_scraper import app
from dataclasses import dataclass
import play_scraper
import csv

from selenium import webdriver
from selenium.webdriver.common.keys import Keys
import urllib
from time import sleep, strftime
import timeit
from selenium.webdriver.common.by import By
from bs4 import BeautifulSoup
from string import digits
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from datetime import datetime
# from concurrent.futures import ThreadPoolExecutor, as_completed


now = datetime.now()
current_time = now.strftime("%H:%M:%S")
print(current_time)

def search_id(key: str):
    global z
    driver = webdriver.Chrome(ChromeDriverManager().install())
    sleep(2)
    link = 'https://play.google.com/store/apps'
    driver.get(link)
    BarraDiRicerca = '//*[@id="gbqfq"]'
    driver.find_element_by_xpath(BarraDiRicerca).send_keys(key + "\n")
    sleep(2)
    driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
    sleep(3)
    driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
    sleep(3)

    for my_elem in driver.find_elements_by_xpath("//*[@class='b8cIId ReQCgd Q9MA7b']/a"):
        href = my_elem.get_attribute("href")
        inizio_id = href.find("id=")
        id_app = href[inizio_id + 3:len(href)]
        print(id_app, href)
        try:
            z.append(id_app)
        except Exception:
            pass


    print("///////////////////////////////////////////////fine key")
    driver.close()
    driver.quit()

keywords = ["Education", "Kids language", "Kids alphabet", 'Kids writing issues', "Kids math",
            "Learning", "Children", "School", 'Kids inclusion','Community', 'Motivation',
            'Experience', 'Kids health', 'Kids learning disorder','ADHD','ADD', 'Dyslexia', 'Dysgraphia',
            'Dyscalculia', 'Processing deficits','Kids memory', 'Hyperactivity', 'Hypoactivity',
            'Hearing', 'Fine motor sKills', 'Handwriting', 'Language processing', 'Non verbal sKills',
            'Visual information', 'Audio processing disorder', 'Aphasia', 'Visual processing disorder',
            'Speech impairment', 'Kids handicap', 'Visual impairment', 'Diagnosis learning disability',
            'Learning difficulties', 'Kids spelling', 'Autism', 'Kids disability', 'Kids development',
            'Asperger', 'Spectrum symptoms', 'Kids neurological problems', 'Disability treatment',
            'Specific learning difficulties', 'Word recognition', 'Kids OCD', 'Attention deficit',
            'Kids motor impairment', 'Kids special needs', 'Learning disability', 'Mental handicap',
            'Attention deficit disorder', 'Problematic children', 'Disadvantaged children', 'Kids special education',
            'Montessori method', 'Montessori games', 'Language-impaired children', 'Handwriting analysis',
            'Learning disorder test', 'Kids pronunciation issues', 'Autistic spectrum disorder', 'Kids behaviour therapy',
            'Attention-Deficit Hyperactive Disorder', 'Auditory memory', 'Dyspraxia', 'Learning disorder early identification',
            'Inclusive education', 'Language-based learning disabilities', 'Kids processing speed', 'Visual-motor skills',
            'Visual representation', 'Visual-spatial skills']


z = []
for key in keywords:
    search_id(key)


@dataclass
class PlayStoreApp:
    title: str
    id_app: str
    genre: str
    genreId: str
    contentRating: str
    contentRatingDescription: str
    installs: int
    descriptions: str
    score: int

count = 0.

def get_playstore_app_from_id(app_id: str) -> PlayStoreApp:
    '''
    Given an app id it returns a PlayStoreApp with the values of the app
    '''
    global count
    try:
        result = app(app_id)
        count += 1
        total_progress = count/(len(z))*100

        print(f'{total_progress:.2f}%: Downloading app {result["title"]}...')

        return PlayStoreApp(
            id_app = app_id,
            title=result["title"],
            genre=result["genre"],
            genreId=result["genreId"],
            contentRating=result["contentRating"],
            contentRatingDescription=result["contentRatingDescription"],
            installs=result["installs"],
            score=result["score"],
            descriptions=result["description"]
            )
    except Exception:
        pass

z = set(z)
objs = [get_playstore_app_from_id(application) for application in z]


with open("ourDataset.csv", "w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)
    writer.writerow(
        [
            "Title",
            "App ID",
            "Genre",
            "GenreID",
            "Content Rating",
            "Content Rating Description",
            "Installs",
            "Score",
            "Descriptions"
        ]
    )
    for obj in objs:
        try:
            print(f'saving files: {obj.title}')
            writer.writerow(
                [
                    obj.title,
                    obj.id_app,
                    obj.genre,
                    obj.genreId,
                    obj.contentRating,
                    obj.contentRatingDescription,
                    obj.installs,
                    obj.score,
                    obj.descriptions
                ]
            )
        except Exception:
            pass

print("File saved")
