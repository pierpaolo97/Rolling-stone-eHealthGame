import requests
from bs4 import BeautifulSoup
import smtplib
import time
from string import digits
import re
from re import search
from xml.etree import ElementTree as ET
from xml.dom import minidom
import urllib
import pandas as pd
from ast import literal_eval

# Formattazione file
with open('words.txt') as file:
    lines = file.readlines()
    lines = [line.rstrip() for line in lines]
    lines = list(dict.fromkeys(lines))

lines = list(dict.fromkeys(lines)) #tolto i duplicati dal file di testo
parole = pd.DataFrame(columns=["word"], data=lines)
parole['score'] = 0
parole.head(8)
# Formattazione file

class pub:
    url_pub = ""
    abstract_pub = ""
    numberOfWordsDescription = ""
    percentageOfWords = ""
    parole_trovate = 0
    lista_parole = []

    def as_dict(self):
        return {'url_pub': self.url_pub,
                'abstract_pub': self.abstract_pub,
                'parole_trovate': self.parole_trovate,
                'lista_parole': self.lista_parole,
                'numberOfWordsDescription': self.numberOfWordsDescription,
                'percentageOfWords': self.percentageOfWords }

url = "https://pubmed.ncbi.nlm.nih.gov/32284113/"
trova_id = "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=pubmed&term=serious+game&retmode=xml&RetMax=250"
response = requests.get(trova_id)

with open('SeriousGame.xml', 'wb') as file:
    file.write(response.content)

xmldoc = minidom.parse('SeriousGame.xml')
itemlist = xmldoc.getElementsByTagName('Id')
url_xml = "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=pubmed&id=00000000&retmode=xml"

i=0
objs = [pub() for i in range(len(itemlist))]

for item in itemlist:
    total_progress = i / (len(itemlist)) * 100
    print(f'{total_progress:.2f}%: ...')
    inizio_id = url_xml.find("id=")
    url_xml = url_xml[:inizio_id + 3] + item.firstChild.data + url_xml[inizio_id+3+len(item.firstChild.data):] #il 3 è perché ho id= quindi 3 caratteri
    response = urllib.request.urlopen(url_xml)
    data = response.read()
    text = data.decode('utf-8')
    soup = BeautifulSoup(text, 'xml')
    abstract_texts = soup.findAll('AbstractText')
    abstract = ""
    for p in abstract_texts:
        abstract = abstract + p.get_text()

    objs[i].url_pub = url_xml
    objs[i].abstract_pub = abstract
    i=i+1

x = 0
for a in objs:
    tot = 0
    z = {}
    descr = a.abstract_pub
    for i in range(0, len(lines)):
        countWord = 0
        if search(lines[i].lower(), a.abstract_pub.lower()):
            countWord += descr.lower().count(lines[i].lower())

            punteggioParziale = parole[parole["word"] == lines[i]].score.item()
            row = parole[parole["word"] == lines[i]].score.index.values.astype(int)[0]
            parole.at[row, 'score'] = punteggioParziale + countWord

            d = {lines[i]: countWord}
            z.update(d)
            tot += countWord

    wordsInDescription = len(descr.split())
    a.numberOfWordsDescription = wordsInDescription
    if wordsInDescription != 0:
        a.percentageOfWords = round((tot * 100 / wordsInDescription), 2)
    else:
        a.percentageOfWords = 0
    a.lista_parole = z
    a.parole_trovate = tot
    x = x + 1


df = pd.DataFrame([x.as_dict() for x in objs])
df.to_csv("datasetSearchInPubMed.csv")

ordinato = parole.sort_values(by = 'score', ascending = False)
ordinato.to_csv("scoreWordsSenzaStandardizzare.csv")
maxim = ordinato['score'].max()
ordinato['score'] = ordinato['score'].div(maxim)
ordinato.to_csv("scoreWords.csv")


df = pd.read_csv("datasetSearchInPubMed.csv")
df['lista_parole'] = df['lista_parole'].apply(literal_eval)
df = df[df.numberOfWordsDescription != 0]


for l in objs:
    #print(l.url_pub)
    if (l.parole_trovate)>0:
        print('In this publication,', l.url_pub, ',', l.parole_trovate, 'matches has been founded (', l.lista_parole, ')')
