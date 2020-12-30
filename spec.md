# Design Specification

## Considerations
* Improperly encoded data could still be decoded without the parser failing.

## Design

* The library is broken up into two steps. The first step produces a `ProductInfo` object
  that the translator consumes. The parsing and translator have no relationship between
  each other and can be tested completely independently.
  
   * Parsing
   * Translating

## Trade Offs

* The design is focused on ease of adding more fields and not solely on performance.


## Clarifications

* The input data table assumes the file has a leading space but the sample
  input does not. I've matched the code to reflect the sample text file.

* The rule `The first flag in the left-to-right array is #1` wasn't understood
  so `PerWeightItem` is the 3rd element in the flag array which is consistent with
  some of the examples in the input-samples.txt
  
* The relationship between display price, unit of measure and product size is unclear. 
  If the item is by weight why is there only the options of each vs split? Wouldn't it be each vs split vs weight?
  

## Input Data Format
The file is in an ASCII-encoded flat file (fixed width) format. For this first phase of the project, you only need to ingest the first 10 fields of the record. There are actually several hundred fields that you'll add to the data model once you've circled back with the team on this first phase and there's consensus on the pattern you introduce. Here's the schema of the first 10 fields:

| Start | End [Inclusive] | Name                       | Type     |
|-------|-----------------|----------------------------|----------|
| 1     | 8               | Product ID                 | Number   |
| 10    | 68              | Product Description        | String   |
| 70    | 77              | Regular Each Price         | Currency |
| 79    | 86              | Sale Each Price            | Currency |
| 88    | 95              | Regular Split Price        | Currency |
| 97    | 104             | Sale Split Price           | Currency |
| 106   | 113             | Regular Split Quantity     | Number   |
| 115   | 122             | Sale Split Quantity        | Number   |
| 124   | 132             | Flags                      | Flags    |
| 134   | 142             | Product Size               | String   |
...

### Field Data Types
* Number - an integer value 8-digits long, zero left-padded
* String - ASCII encoded string, space right-or-left-padded
* Currency - US dollar value, where last two digits represent cents.  The leading zero will be replaced with a dash if the value is negative
* Flag - Y/N

### Pricing Information
* Prices can either be an each price (e.g. $1.00 each) or a split price (e.g. 2 for $0.99). Only one price (each or split) per price level (regular or sale) will exist. The price data for an undefined price will be all 0's.
* If a price is split pricing, the Calculator Price is Split Price / Split Quantity
* You can be guaranteed that the input file will follow these rules – consider it a contract that the producer will always abide by.  No error checking is required for this first stage.

### Flags
The first flag in the left-to-right array is #1
* If Flag #3 is set, this is a per-weight item
* If Flag #5 is set, the item is taxable.  Tax rate is always 7.775%

## ProductRecord object should contain at a minimum:
* Product ID
* Product Description
* Regular Display Price (English-readable string of your choosing, e.g. "$1.00" or "3 for $1.00")
* Regular Calculator Price (price the calculator should use, rounded to 4 decimal places, half-down)
* Sale Display Price, if it exists (same format as regular display price string)
* Sale Calculator Price, if it exists
* Unit of Measure ("Each" or "Pound").  Weighted items are per pound
* Product size
* Tax rate