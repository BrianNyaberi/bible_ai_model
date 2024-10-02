import torch
from transformers import BertTokenizer, BertForSequenceClassification

def load_model():
    # Load pre-trained model
    model = BertForSequenceClassification.from_pretrained('./results')
    tokenizer = BertTokenizer.from_pretrained('bert-base-uncased')
    return model, tokenizer

def get_answer(question, model, tokenizer):
    inputs = tokenizer(question, return_tensors="pt", padding=True, truncation=True)
    outputs = model(**inputs)
    return outputs.logits.argmax(dim=-1).item()

if __name__ == "__main__":
    # Load the model and tokenizer
    model, tokenizer = load_model()
    
    # Interactive loop for user input
    while True:
        question = input("Ask your spiritual question: ")
        answer = get_answer(question, model, tokenizer)
        print(f"Answer: {answer}")
