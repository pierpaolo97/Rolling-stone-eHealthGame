import pandas as pd
from dataclasses import dataclass
import csv
from string import digits
from re import search
import operator
from tinydb import TinyDB

# Ricerca delle Keywords del file di testo nel dataset completo (COMPLETEdataset.csv). Il file COMPLETEdataset inizialmente ha vuote le colonne relative alla ricerca
# che vengono riempite con questo script. Il salvataggio del dataset è nel file datasetSearchKeywords.csv


with open('words.txt') as file:
    lines = file.readlines()
    lines = [line.rstrip() for line in lines]
    lines = list(dict.fromkeys(lines))  # tolto i duplicati dal file di testo nel caso ci fossero

def search_words(row):
    tot = 0
    z = {}

    descr = dataset.iloc[row]['Descriptions']

    for j in range(0, len(lines)):
        countWord = 0
        if search(lines[j].lower(), descr.lower()):
            countWord += descr.lower().count(lines[j].lower())
            d = {lines[j]: countWord}
            z.update(d)
            #z.append(lines[j])
            tot += countWord
    wordsInDescription = len(descr.split())
    dataset.at[row, 'numberOfWordsFounded'] = tot
    dataset.at[row, 'numberOfWordsDescription'] = wordsInDescription
    dataset.at[row, 'percentageOfWords'] = round((tot * 100 / wordsInDescription), 2)
    dataset.at[row, 'wordsList'] = z
    '''item = {'Title': dataset.iloc[row]['Title'],
            'AppID': dataset.iloc[row]['App ID'],
            'Category': dataset.iloc[row]['Category'],
            'Rating': dataset.iloc[row]['Rating'],
            'Installs': dataset.iloc[row]['Installs'],
            'Content Rating': dataset.iloc[row]['Content Rating'],
            'Descriptions': dataset.iloc[row]['Descriptions'],
            'GenreID': dataset.iloc[row]['GenreID'],
            'Number': dataset.iloc[row]['Descriptions'],
            'numberOfWordsFounded': dataset.iloc[row]['numberOfWordsFounded'],
            'numberOfWordsDescription': dataset.iloc[row]['numberOfWordsDescription'],
            'percentageOfWords': dataset.iloc[row]['percentageOfWords'],
            'wordsList': dataset.iloc[row]['wordsList']}
    tiny_db.insert(item)'''


dataset = pd.read_csv("COMPLETEdataset.csv")
print(dataset.dtypes)
dataset = dataset.astype({"wordsList": object})
print(dataset.dtypes)
#tiny_db = TinyDB('Dataset.json')

for i in range(0, len(dataset)):
    print(f'{i / len(dataset) * 100:.4f}%: Searching Keywords for the app {dataset.iloc[i]["App ID"]}...')
    search_words(i)

#dataset.to_csv('datasetSearchKeywords.csv', header=True, index=False, encoding="utf-8")








