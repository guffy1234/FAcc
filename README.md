#FAcc PoC

To run PoC you have to install:

1. Visual Studio 2022
1. .NET8
1. MS SQL Server Express LocalDB or PostgreSql (optional - by defualt it works with SQLite database)


ToDo:

- Improve exception throwing and showing. Improve exception use case with expired token
- Implement custom search, filtering and sorting based on already implmented xxxQueryDto classes
- Cover all UI texts with localization that already implemented
- Implement Identity and claims API (also CQRS?).
- Implement Identity and claims replication. [?]
- Possible implement hierarhical dictionaries.
- Implement "batch accounting". [?]
- Implement usecase for storages which can't hold multiple products the same tile (liquid tanks). [?]
- Implement MAUI desctop/mobile application as remote workplace of a branch with own DB and replication to/from main branch Db via replication API.