# ETL Project

A simple .NET 10 console application that demonstrates an ETL pipeline for customer data.

## Overview

This project reads customer records from a CSV file, transforms the data, and writes the result to a JSON file.

The pipeline includes:
- `CustomerCsvExtractor` for CSV extraction
- `CustomerTransformer` for validation, normalization, and sorting
- `CustomerJsonLoader` for JSON output
- `Logger` for simple console logging
- Extension helper for logging item counts during pipeline execution

## Project structure

- `Program.cs` — application entry point and pipeline orchestration
- `Csv/CustomerCsvExtractor.cs` — extractor implementation
- `Csv/Customertransformer.cs` — transformer implementation
- `Json/CustomJsonLoader.cs` — loader implementation
- `Models/Customer.cs` — customer model
- `Extensions/Extensions.cs` — extension methods used in pipeline
- `Logging/Logging.cs` — logging utility

## Prerequisites

- .NET 10 SDK

## Run the application

From the `ETL` folder, run:

```bash
dotnet run
```

The app will create `input/customers.csv` if it does not exist, then generate `output/customers.json`.

## Input and output

- Input file: `input/customers.csv`
- Output file: `output/customers.json`

The input CSV uses the format:

```csv
Id,Name,Email,Age
```

## Behavior

- If `input/customers.csv` is missing, the app creates a sample CSV file with five customer records.
- The transformer filters out customers younger than 18.
- Names are trimmed and email addresses are normalized to lowercase.
- Customers are ordered by name before being written to JSON.
- The output JSON is indented for readability.

## Extending the pipeline

To extend the ETL pipeline:

1. Add a new extractor implementing `IExtractor<T>`.
2. Add a new transformer implementing `ITransformer<TInput, TOutput>`.
3. Add a new loader implementing `ILoader<T>`.
4. Wire the new components in `Program.cs`.

## Notes

- The project targets `net10.0`.
- Logging is written to the console with timestamped levels.
