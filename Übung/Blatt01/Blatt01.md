# Übungsblatt 01

|   |            |
|---|------------|
| Abgabe von: | **Jonas Merkle** [[jonas.merkle@uni-ulm.de](mailto:jonas.merkle@un-ulm.de?subject=%C3%9Cbung%20Angewandte%20Stochastik)] |
| Abgabe bis: | 30.10.2024 08:00 |
| Repository: | [https://github.com/jonas-merkle/Softwarequalitaetssicherung](https://github.com/jonas-merkle/Softwarequalitaetssicherung) |
| Dateien:    | [PDF](https://jonas-merkle.github.io/Softwarequalitaetssicherung/Uebungsblatt01/Uebungsblatt01_Jonas-Merkle.pdf), [ZIP](https://jonas-merkle.github.io/Softwarequalitaetssicherung/Uebungsblatt01/Uebungsblatt01_Jonas-Merkle.zip), [HTML](https://jonas-merkle.github.io/Softwarequalitaetssicherung/Uebungsblatt01/Uebungsblatt01_Jonas-Merkle.html) |

## Inhaltsverzeichnis

- [Übungsblatt 01](#übungsblatt-01)
  - [Inhaltsverzeichnis](#inhaltsverzeichnis)
  - [Aufgabe 1](#aufgabe-1)
  - [Aufgabe 2 + 3 + 4: Implementierung + Property Tests eines  Binary Search Trees](#aufgabe-2--3--4-implementierung--property-tests-eines--binary-search-trees)

## Aufgabe 1

## Aufgabe 2 + 3 + 4: Implementierung + Property Tests eines  Binary Search Trees

Die Implementierung meines Binary Search Trees und der Dazugehörigen property-based (Unit-) Tests ist in `C#` geschrieben. Als Test-Framework wurde [XUnit](https://github.com/xunit/xunit) zusammen mit [FsCheck](https://github.com/fscheck/FsCheck) (QuickCheck `C#` port) verwendet. Das zugehörige Projekt zur befindet sich in folgendem Verzeichnis: [./src/BST/](./src/BST/).

Dabei ist das Projekt wie folgt strukturiert:

```txt
  ./src/BST/
  |-- BinarySearchTree.Lib/
  |   |-- BinarySearchTree.cs           <-- Implementierung des BST
  |   |-- Node.cs                       <-- Helper Class `Node` für den BST
  |   |-- Mutants/
  |   |   |-- M1
  |   |   |   |-- BinarySearchTree.cs   <-- Mutation 1 der BST Implementierung
  |   |   |-- M2
  |   |   |   |-- BinarySearchTree.cs   <-- Mutation 2 der BST Implementierung
  |   |   |-- M*...
  |   |   ...
  |   ...
  |-- BinarySearchTree.Tests/
  |   |-- PropertyTests.cs              <-- Implementierung der property-based (Unit-) Tests
  |   ...
  ...
```
