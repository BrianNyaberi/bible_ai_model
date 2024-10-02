import pandas as pd
from transformers import BertTokenizer

def load_data(file_path):
    df = pd.read_csv(file_path)
    return df

def preprocess_data(df, tokenizer):
    inputs = tokenizer(df['question'].tolist(), padding=True, truncation=True, return_tensors="pt")
    labels = tokenizer(df['explanation'].tolist(), padding=True, truncation=True, return_tensors="pt")
    return inputs, labels

# Example Usage
tokenizer = BertTokenizer.from_pretrained('bert-base-uncased')
df = load_data('../data/bible_qa.csv')
inputs, labels = preprocess_data(df, tokenizer)
