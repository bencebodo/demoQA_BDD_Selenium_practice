# DemoQA - Advanced UI Automation & CI/CD Framework

![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue)
![Selenium](https://img.shields.io/badge/Selenium-WebDriver_4-green)
![Reqnroll](https://img.shields.io/badge/BDD-Reqnroll-yellow)
![Jenkins](https://img.shields.io/badge/CI%2FCD-Jenkins_Docker-orange)

## Overview
This repository showcases an enterprise-grade UI Automation Framework designed to tackle the complex, dynamic, and often "fragile" elements of [DemoQA.com](https://demoqa.com/). 

Testing DemoQA is a significant challenge due to its heavy use of React, aggressive ad injections, and unconventional UI behaviors (frozen pop-ups, overlapping elements). This project serves as a proof-of-concept for building a **highly resilient** automation suite that remains stable even in high-concurrency, **headless Docker environments**.

## Key Technical Challenges Solved
Unlike standard "tutorial" projects, this framework implements "Senior-level" solutions for real-world automation hurdles:
* **Headless Resilience:** Configured with specialized Chrome arguments (`--headless=new`, `--no-sandbox`, `--disable-dev-shm-usage`) to ensure 100% stability in Linux Docker containers.
* **Event Dispatcher Strategy:** Bypassed flaky physical mouse interactions on React components by implementing a custom **JavaScript Event Dispatcher** for complex actions like `double-click` and `context-menu`.
* **Smart State Management:** Implemented a custom `WaitForPageToLoad` mechanism that monitors `document.readyState` (handling both `complete` and `interactive` states) to eliminate race conditions.
* **Anti-Ad Logic:** Integrated a DOM-cleaner utility that programmatically removes intrusive advertisements and iframes via JavaScript before test execution to prevent `ElementClickIntercepted` exceptions.

## Architecture
* **BDD with Reqnroll:** Human-readable Gherkin scenarios mapped to strongly typed C# step definitions.
* **Page Object Model (POM):** A clean, scalable structure separating locators from business logic.
* **Dependency Injection (DI):** Uses `Microsoft.Extensions.DependencyInjection` to manage the lifecycle of the `IWebDriver` and Page Objects.
* **Fluent Assertions:** High-readability validation logic using NUnit.
* **Centralized Logging:** Integrated **Serilog** for detailed, timestamped console and file logs.

## Technology Stack
* **Language:** C# (.NET 8)
* **UI Automation:** Selenium WebDriver 4
* **BDD Framework:** Reqnroll (Successor to SpecFlow)
* **Test Runner:** NUnit
* **CI/CD:** Jenkins (Declarative Pipeline)
* **Infrastructure:** Docker

## 🔄 CI/CD Pipeline
The framework includes a fully functional `Jenkinsfile` designed for a Dockerized execution flow:
1.  **Environment Setup:** Automatically installs Google Chrome and required Linux dependencies inside the .NET SDK container.
2.  **Build:** Restores NuGet packages and compiles the solution.
3.  **Headless Execution:** Runs tests in a virtualized environment with optimized memory management.
4.  **Artifact Management:** Archives NUnit XML test results and console logs for every build.

## 🚀 Installation & Execution

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Version 8.0 or higher)
* Google Chrome (latest version)

### Local Execution (Headed)
1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/yourusername/DemoQA-Automation.git](https://github.com/yourusername/DemoQA-Automation.git)
    cd DemoQA-Automation
    ```
2.  **Restore packages:**
    ```bash
    dotnet restore
    ```
3.  **Run tests:**
    ```bash
    dotnet test
    ```

    Author 

    Bence Bodo - Automation QA Engineer
