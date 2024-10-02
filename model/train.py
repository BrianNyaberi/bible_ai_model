import torch
from transformers import BertTokenizer, BertForSequenceClassification, Trainer, TrainingArguments
from preprocess import load_data, preprocess_data

def main():
    # Load dataset
    df = load_data('../data/bible_qa.csv')
    
    # Load pre-trained model and tokenizer
    model = BertForSequenceClassification.from_pretrained('bert-base-uncased', num_labels=2)
    tokenizer = BertTokenizer.from_pretrained('bert-base-uncased')

    # Preprocess the data
    inputs, labels = preprocess_data(df, tokenizer)

    # Training arguments
    training_args = TrainingArguments(
        output_dir='./results',          # output directory
        num_train_epochs=3,              # total number of training epochs
        per_device_train_batch_size=4,   # batch size per device during training
        save_steps=10,                   # number of updates steps before checkpoint is saved
        save_total_limit=2,              # limit total amount of checkpoints
    )

    # Trainer class
    trainer = Trainer(
        model=model,                         # the instantiated 🤗 Transformers model to be trained
        args=training_args,                  # training arguments, defined above
        train_dataset=inputs['input_ids'],   # training dataset
        eval_dataset=labels['input_ids']     # evaluation dataset
    )

    # Train the model
    trainer.train()

if __name__ == "__main__":
    main()
