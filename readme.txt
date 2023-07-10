Pri tvorbe zadania som pouzil visual studio 2022 a databazu Microsoft SQL Server Express.
Vo visual studiu je zabudovany Swagger, ktory sa otvori v prehliadaci po spusteni programu.



NAVOD NA SPUSTENIE PROGRAMU:

1. vytvorenie databazy:

- vytvorit databazu pomocou sql skriptu DBScript.sql
- volitelne: naplnit databazu dummy datami v sql skripte DBScriptData

2. spojenie programu s databazou:

- v appsettings.json (riadok 3) treba vlozit connection string databazy:
"DefaultConnection": "TuVlozitConnectionString"







