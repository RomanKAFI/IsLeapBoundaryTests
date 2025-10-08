IsLeapBoundaryTests

This project demonstrates Boundary Value Analysis (BVA) and Equivalence Partitioning (EP) through a set of unit tests for the IsLeap(int year) function — which determines whether a given year is a leap year according to the Gregorian calendar rules.

Using a combination of Boundary Analysis and Equivalence Partitions, create a set of test cases to verify the correctness of the function:

bool IsLeap(int year)

Rules of the Gregorian Calendar:
A year is a leap year if it is divisible by 4, but not divisible by 100, unless it is also divisible by 400.

Task Requirements:
Write a minimal set of test cases that cover all boundary conditions and equivalence partitions.
Name each test case TestMethodX, where X is a sequential counter starting from 1.
A total of 15 test cases are required to cover all partitions and their boundaries, including ±1 values.

Note:
You may implement the function locally for testing, but only submit your test cases to the system.

Project Structure:
IsLeapBoundaryTests
Program.cs – Test runner (executes all test cases)
UnitTests.cs – Contains 15 boundary and partition test cases
IsLeapBoundaryTests.csproj
README.md

How to Run Locally:

Open terminal in the project directory.

Type: dotnet run

Expected console output:
All 15 tests passed.
Summary: 15 passed, 0 failed.
Missing test values count: 0
Extra test values count: 0

Concepts Learned:
Boundary Value Analysis (BVA)
Equivalence Partitioning (EP)
Unit testing with MSTest-style structure
Writing minimal and effective test coverage
Test-driven thinking using input domain partitioning

Author:
Roman Kafitulin
CSD268 – Software Quality Assurance
Lake Washington Institute of Technology
Fall 2025
