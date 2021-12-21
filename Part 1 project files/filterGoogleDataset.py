import pandas as pd
from dataclasses import dataclass
import csv
from google_play_scraper import app
import play_scraper
from datetime import datetime
import atexit
import signal
import sys

# Filtro del dataset di Google. Rimosso categorie non necessarie e aggiunto alle app le descrizioni e l'id_genre che prima non erano presenti

try:
    start = datetime.now().strftime("%H:%M:%S")
    googleDataset = pd.read_csv("Google-Playstore.csv")
    googleDataset.drop(columns=['Minimum Installs', 'Maximum Installs', 'Free', 'Price', 'Developer Website', 'Developer Email', 'Released', 'Privacy Policy', 'Ad Supported', 'In App Purchases', 'Editors Choice', 'Scraped Time', 'Rating Count','Currency','Size', 'Minimum Android', 'Developer Id', 'Last Updated' ], inplace=True)
    googleDataset.rename(columns={'App Name': 'Title', 'App Id': 'App ID'}, inplace=True)
    googleDataset = googleDataset[googleDataset.Category.isin(["Puzzle", "Education", "Educational", "Trivia"])]
    googleDataset = googleDataset[googleDataset['Content Rating'].isin(["Everyone", "Teen", "Everyone 10+", "Unrated"])]
    googleDataset['Descriptions'] = ""
    googleDataset['GenreID'] = ""

    googleDataset = googleDataset.reset_index()
    googleDataset.drop(columns=['index'], inplace=True)
    #googleDataset = pd.read_csv("Playstore_parte1.csv")
    #forStating_point = pd.read_csv("Playstore_parte1.csv")
    #starting_point = googleDataset.index[googleDataset['App ID'] == "com.msgames.luxdrawpark"].values.astype(int)[0] #qui bisogna sostituire con l'ultima app aggiunta nella descrizione

    #primo step da 0 a 200000
    for i in range(0,len(googleDataset)):
        try:
            app_id = googleDataset.iloc[i]['App ID']
            print(f'{i / len(googleDataset) * 100:.4f}%: Downloading app {app_id}...')
            result = app(app_id)
            googleDataset.at[i,'Descriptions'] = result["description"]
            googleDataset.at[i, 'GenreID'] = result["genreId"]
            #googleDataset.to_csv('NewGoogleDataset.csv', header=True, index=False, encoding="utf-8") # In questo modo se vuoi stoppare lo script salva comunque l'excel
                                                                                                     # fino a dove sei arrivato
        except Exception:
            print("Expection")
            pass

    print("File saved")

    end = datetime.now().strftime("%H:%M:%S")
    print("Start:", start, "End:", end)
    googleDataset.to_csv('FinalDatasetPlayStore.csv', header=True, index=False, encoding="utf-8")

except KeyboardInterrupt:
    googleDataset.to_csv('FinalDatasetPlayStore.csv', header=True, index=False, encoding="utf-8")
    exit()












