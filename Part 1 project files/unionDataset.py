import pandas as pd
from dataclasses import dataclass
import csv
from string import digits
from re import search
import operator

# Unire il dataset di Google filtrato (FinalDatasetPlayStore.csv) con quello generato dalle nostre keywords (provaScraping.csv) nel dataset COMPLETEdataset.csv

ourDataset = pd.read_csv("provaScraping.csv")
ourDataset.rename(columns={'Genre': 'Category', 'Score': 'Rating'}, inplace=True)
ourDataset.drop(columns=['Content Rating Description'], inplace=True)

googleDataset = pd.read_csv("FinalDatasetPlayStore.csv") #googleDataset = googleDataset.drop_duplicates(subset='Descriptions', keep="last") Alcune app hanno la stessa descrizione, le teniamo?

dataset = pd.concat([googleDataset, ourDataset], ignore_index=True)
dataset = dataset.drop_duplicates(subset='App ID', keep="last")
dataset['numberOfWordsFounded'] = ""
dataset['numberOfWordsDescription'] = ""
dataset['percentageOfWords'] = ""
dataset['wordsList'] = ""
dataset.to_csv('COMPLETEdataset.csv', header=True, index=False, encoding="utf-8")