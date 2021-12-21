import pandas as pd
from dataclasses import dataclass
import csv
from string import digits
from re import search
import operator
from tinydb import TinyDB
import numpy as np
from ast import literal_eval

dataset = pd.read_csv("datasetSearchKeywords.csv")
dataset['wordsList'] = dataset['wordsList'].apply(literal_eval)
dataset['AppScore'] = 0.0

scoreDataset = pd.read_csv("scoreWords.csv")
scoreDataset.drop(columns=["Unnamed: 0"], inplace=True)
scoreDataset['score'] = scoreDataset['score'].round(4)

for i in range(0,len(dataset)):
    total_progress = i / (len(dataset)) * 100
    print(f'{total_progress:.2f}%: ...')
    lista = dataset.iloc[i]['wordsList']
    totaleParola = 0
    for key in lista:
        score=scoreDataset[scoreDataset["word"]==key].score.item()
        totaleParola += lista[key]*score
    dataset.at[i, 'AppScore'] = round(totaleParola,4)

dataset.to_csv("DatasetConPunteggio.csv")

dataset = dataset[dataset.numberOfWordsFounded != 0]
vMax = dataset["AppScore"].max()
dataset["AppScore"] = round(dataset["AppScore"].divide(vMax),2)
dataset = dataset.sort_values(by = 'AppScore', ascending = False)

dataset.to_csv("datasetConPunteggioStandardizzato.csv")


