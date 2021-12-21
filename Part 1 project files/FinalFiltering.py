import pandas as pd
from dataclasses import dataclass
import csv
from string import digits
from re import search
import operator
from tinydb import TinyDB
from ast import literal_eval


dataset = pd.read_csv("datasetSearchKeywords")
dataset['wordsList'] = dataset['wordsList'].apply(literal_eval)
dataset = dataset.replace(r'\n',' ', regex=True)
dataset = dataset.replace(r'\r',' ', regex=True)
dataset = dataset.replace(r'\t',' ', regex=True)

dataset = dataset[dataset['Content Rating']!="Mature 17+"]
dataset = dataset[dataset['Content Rating']!="Everyone 10+"]
dataset = dataset[dataset['Content Rating']!="Teen"]

dataset = dataset[dataset['GenreID'].isin(['GAME_PUZZLE', 'EDUCATION', 'GAME_EDUCATIONAL', 'GAME_TRIVIA',
                                           'GAME_BOARD', 'GAME_SIMULATION',
                                           'ENTERTAINMENT', 'GAME_MUSIC', 'GAME_ARCADE',
                                           'GAME_CASUAL','GAME_STRATEGY', 'COMMUNICATION',
                                           'GAME_ROLE_PLAYING', 'GAME_ADVENTURE', 'GAME_WORD','ART_AND_DESIGN',
                                           'HEALTH_AND_FITNESS', 'MEDICAL',
                                           'GAME_ACTION', 'GAME_RACING',
                                           'GAME_CARD', 'GAME_SPORTS', 'SPORTS'])]

dataset = dataset[dataset['percentageOfWords']>13]
dataset = dataset[dataset['numberOfWordsFounded']>30]
dataset = dataset[dataset["wordsList"].map(len)>12]


#dataset = dataset[dataset['wordsList']>10]