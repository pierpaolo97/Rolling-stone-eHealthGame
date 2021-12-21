#!/usr/bin/env python
# coding: utf-8

# In[31]:


import pandas as pd
import csv
import numpy as np
from ast import literal_eval
from sklearn.preprocessing import MinMaxScaler
from sklearn.model_selection import train_test_split


# In[32]:


datasetCompleto = pd.read_csv("DatasetConPunteggioSenzaGame.csv")
datasetCompleto.drop(columns=["Unnamed: 0"], inplace=True)
datasetCompleto['wordsList'] = datasetCompleto['wordsList'].apply(literal_eval)
datasetCompleto.head(1)


# In[33]:


trans = MinMaxScaler()
datasetCompleto["AppScore"] = trans.fit_transform(datasetCompleto[["AppScore"]])
datasetCompleto = datasetCompleto.sort_values(by = 'AppScore', ascending = False)


# In[34]:


dataset = pd.read_csv("GoldenStandard.csv")
dataset.drop(columns=["Unnamed: 0"], inplace=True)
dataset.rename(columns={'Serio (0: no, 1:si)': "Target"}, inplace=True)
print(dataset.shape)


# In[35]:


dataset['Target'].astype(str).astype(float)

uno_iniziali = dataset[dataset['Target']==1].shape[0]
zero_iniziali = dataset[dataset['Target']==0].shape[0]

print("Numeri di 1 trovati: ", dataset[dataset['Target']==1].shape)
print("Numeri di 0 trovati: ", dataset[dataset['Target']==0].shape)


# In[36]:


GoldenStandard = pd.merge(dataset, datasetCompleto[['App ID', 'numberOfWordsFounded','numberOfWordsDescription', 'percentageOfWords', 'wordsList', 'AppScore']], on=['App ID'], how="left")
print(GoldenStandard.shape)
GoldenStandard.head(1)


# # Decision Tree Classifier

# In[37]:


GoldenStandard.dtypes


# In[38]:


from sklearn.tree import DecisionTreeClassifier
dataset = GoldenStandard

for i in range(0,len(dataset)):
    dataset.at[i, 'wordsList'] = len(dataset.iloc[i].wordsList)

y = dataset['Target']

#X = dataset.drop(columns=["Target", "Descriptions", "Content Rating", "App ID", "Installs", "Title", "Category", "GenreID", "Rating"])
X = dataset.drop(columns=["Target", "Descriptions", "App ID", "Installs", "Title", "Rating"])

dummies = pd.get_dummies(X[['Content Rating', 'Category', 'GenreID']],drop_first=True) 
dummies.tail(2) # Convert categorical variables into dummy variables

X = pd.concat([dummies,X], axis = 1)
X = X.drop(columns=['Content Rating', 'Category', 'GenreID'])
X.tail(3)


# In[39]:


y.shape


# In[40]:


X_train, X_test, y_train, y_test = train_test_split(X, y, 
                                                    test_size =0.30, #by default is 75%-25%
                                                    #shuffle is set True by default,
                                                    stratify=y, #preserve target propotions 
                                                    random_state= 123) #fix random seed for replicability


# In[41]:


print(X_train.shape)
print(y_train.shape)
print(X_test.shape)
print(y_test.shape)


# In[42]:


from sklearn.tree import DecisionTreeClassifier
import numpy as np
from sklearn.model_selection import GridSearchCV

classifier = DecisionTreeClassifier()
parameters = {'criterion': ['entropy','gini'], 
              'max_depth': [2,3,4,5,6,8],
              'min_samples_split': [2,3,5,7,10,12,11],
              'min_samples_leaf': [2,4,5,6]}

gs = GridSearchCV(classifier, parameters, cv=6, scoring = 'accuracy', verbose=10, n_jobs=-1, refit=True)
gs = gs.fit(X_train, y_train)


# In[43]:


#summarize the results of your GRIDSEARCH
print('***GRIDSEARCH RESULTS***')

print("Best score: %f using %s" % (gs.best_score_, gs.best_params_))
means = gs.cv_results_['mean_test_score']
stds = gs.cv_results_['std_test_score']
params = gs.cv_results_['params']

for mean, stdev, param in zip(means, stds, params):
    print("%f (%f) with: %r" % (mean, stdev, param))


# In[44]:


best_model = gs.best_estimator_
y_pred = best_model.predict(X_test)
y_pred_train = best_model.predict(X_train)


# In[45]:


#PRINT SOME FURTHER METRICS
from sklearn import metrics
from sklearn.metrics import classification_report
from sklearn.metrics import confusion_matrix

print(classification_report(y_train, y_pred_train))
print(classification_report(y_test, y_pred))


# In[46]:


import seaborn as sns
import matplotlib.pyplot as plt     

ax= plt.subplot()
sns.heatmap(confusion_matrix(y_test, y_pred), annot=True, fmt='d', cmap="Reds", cbar=True)

ax.set_xlabel('Predicted labels');ax.set_ylabel('True labels')
ax.set_title('Confusion Matrix');


# In[47]:


from sklearn import tree
r = tree.export_text(best_model,feature_names=X_test.columns.tolist())
print(r)
tree.plot_tree(best_model, fontsize=8)


# In[48]:


from sklearn.tree import export_graphviz
export_graphviz(best_model, out_file='decision_tree.dot', feature_names = X_test.columns.tolist())
get_ipython().system('dot -Tpng decision_tree.dot -o decision_tree.png -Gdpi=600')
from IPython.display import Image
Image(filename = 'decision_tree.png')


# In[ ]:





# # Apply to the Dataset

# In[49]:


dataset = pd.read_csv("DatasetConPunteggioSenzaGame.csv")
dataset.drop(columns=["Unnamed: 0"], inplace=True)
dataset['wordsList'] = dataset['wordsList'].apply(literal_eval)
dataset.head(2)


# In[50]:


dataset = dataset.replace(r'\n',' ', regex=True)
dataset = dataset.replace(r'\r',' ', regex=True)
dataset = dataset.replace(r'\t',' ', regex=True)
dataset = dataset.replace(r'<b>',' ', regex=True)

trans = MinMaxScaler()
dataset["AppScore"] = trans.fit_transform(dataset[["AppScore"]])
#dataset = dataset.sort_values(by = 'AppScore', ascending = False)


# In[51]:


for i in range(0,len(dataset)):
    dataset.at[i, 'wordsList'] = len(dataset.iloc[i].wordsList)


# In[52]:


nuovo_Dataset = dataset.drop(columns=["Descriptions", "App ID", "Installs", "Title", "Rating"])
dummies = pd.get_dummies(nuovo_Dataset[['Content Rating', 'Category', 'GenreID']],drop_first=True) 
dummies.tail(2) # Convert categorical variables into dummy variables
nuovo_Dataset = pd.concat([dummies,nuovo_Dataset], axis = 1)
nuovo_Dataset = nuovo_Dataset.drop(columns=['Content Rating', 'Category', 'GenreID'])
nuovo_Dataset.tail(3)


# In[53]:


col_da_rimuovere = []
for col in nuovo_Dataset.columns:
    if col not in X.columns:
        col_da_rimuovere.append(col)


# In[54]:


for col in col_da_rimuovere:
    nuovo_Dataset.drop(columns=col, inplace=True)


# In[55]:


predizioni = best_model.predict(nuovo_Dataset)


# In[56]:


dataset['Target'] = pd.Series(predizioni, index=dataset.index)


# In[57]:


finalDataset = dataset[dataset['Target']==1]


# In[58]:


finalDataset


# In[59]:


finalDataset.sort_values(by = 'AppScore', ascending = False)


# # riattaccare la wordsList

# In[60]:


#finalDataset.drop(columns=['wordsList', 'Target'], inplace=True)
finale = pd.merge(finalDataset, datasetCompleto[['App ID','wordsList']], on=['App ID'], how="left")
print(finale.shape)
finale.head(2)


# In[61]:


finale.drop(columns=["wordsList_x", "Target" ], inplace=True)


# In[62]:


finale.head(2)


# In[63]:


finale.rename(columns={'numberOfWordsFounded': "numberOfWordsFound", 'wordsList_y': "wordsList"}, inplace=True)


# In[64]:


finale


# In[69]:


import random
n = random.randint(0,len(finale))

finale.iloc[n]


# In[ ]:





# In[ ]:





# In[ ]:





# In[ ]:





# In[70]:


finale.to_csv("FinalDataset.csv")


# In[ ]:





# In[ ]:





# In[ ]:




