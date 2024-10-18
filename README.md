# Softwarequalitätssicherung

Vorlesung Softwarequalitätssicherung an der Universität Ulm im Wintersemester 2024/2025.

|         |                  |
|---------|------------------|
| Autor:  | **Jonas Merkle** |
| GitHub: | [https://github.com/jonas-merkle/Softwarequalitaetssicherung](https://github.com/jonas-merkle/Softwarequalitaetssicherung) |
| Status: | [![BuildAndDepoly](https://github.com/jonas-merkle/Softwarequalitaetssicherung/actions/workflows/BuildAndDepoly.yml/badge.svg?branch=main)](https://github.com/jonas-merkle/Softwarequalitaetssicherung/actions/workflows/BuildAndDepoly.yml) |

## Inhaltsverzeichnis

- [Softwarequalitätssicherung](#softwarequalitätssicherung)
  - [Inhaltsverzeichnis](#inhaltsverzeichnis)
  - [Disclaimer](#disclaimer)
  - [Übung](#übung)
    - [Übungsblätter](#übungsblätter)
      - [Blatt 00](#blatt-00)
  - [How to build PDF's](#how-to-build-pdfs)

## Disclaimer

**Es wird weder die Vollständigkeit, noch die Richtigkeit der Inhalte garantiert.**
Insbesondere in den Lösungen zu den Übungsblättern kann es sein, dass Aufgaben falsch und / oder unvollständig beantwortet sind oder sogar komplett fehlen!

## Übung

### Übungsblätter

#### [Blatt 00](./Übung/Blatt00/Blatt00.md)

|         |   |
|---------|---|
| GitHub: | [https://github.com/jonas-merkle/Softwarequalitaetssicherung/blob/main/%C3%9Cbung/Blatt00/Blatt00.md](https://github.com/jonas-merkle/Softwarequalitaetssicherung/blob/main/%C3%9Cbung/Blatt00/Blatt00.md) |
| PDF:    | [https://jonas-merkle.github.io/Softwarequalitaetssicherung/UebungsBlatt00/UebungsBlatt00_Jonas-Merkle.pdf](https://jonas-merkle.github.io/Softwarequalitaetssicherung/UebungsBlatt00/UebungsBlatt00_Jonas-Merkle.pdf) |
| ZIP:    | [https://jonas-merkle.github.io/Softwarequalitaetssicherung/UebungsBlatt00/UebungsBlatt00_Jonas-Merkle.zip](https://jonas-merkle.github.io/Softwarequalitaetssicherung/UebungsBlatt00/UebungsBlatt00_Jonas-Merkle.zip) |
| HTML:   | [https://jonas-merkle.github.io/Softwarequalitaetssicherung/UebungsBlatt00/UebungsBlatt00_Jonas-Merkle.html](https://jonas-merkle.github.io/Softwarequalitaetssicherung/UebungsBlatt00/UebungsBlatt00_Jonas-Merkle.html) |

## How to build PDF's

Um aus den im Repository verwendeten Markdown Dateien PDF Dateien zu erstellen folgende Befehle ausführen:

```bash
mkdir build && cd build
cmake ..
make
```

Dafür müssen die folgenden Tools installiert sein:

- cmake & make
- pandoc
- pdf-latex
