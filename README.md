DemoQA UI Test Automation Framework
This repository contains an advanced, enterprise-grade UI test automation framework designed to test the notoriously challenging and dynamic UI of DemoQA.com.

The project simulates real-world Behavior-Driven Development (BDD) workflows using Reqnroll (the modern successor to SpecFlow) and Selenium WebDriver in C#. It is specifically engineered to handle complex React-based DOM elements, heavy ad-injections, and flaky UI rendering, making it a highly resilient portfolio piece.

Key Features & Architecture
BDD Approach: Clean and readable Gherkin syntax (.feature files) mapped to strongly typed C# Step Definitions.

Page Object Model (POM): Scalable and maintainable class structure separating UI locators from test logic.

Dependency Injection (DI): Utilizes Microsoft.Extensions.DependencyInjection for managing WebDriver instances and Page Objects seamlessly across scenarios.

Advanced Resilience: Custom JavaScript executors and custom Wait mechanisms (e.g., overriding physical clicks with DOM events) to bypass overlapping elements and overlapping ads in Headless mode.

Comprehensive Logging: Integrated Serilog for detailed, timestamped console outputs during test execution.

CI/CD Ready: Fully prepared for pipeline integration. Includes a Jenkinsfile for automated, Headless Chrome execution via Docker, complete with .trx / NUnit XML test reporting.

Tech Stack
Language: C# (.NET)

UI Automation: Selenium WebDriver

BDD Framework: Reqnroll

Test Runner / Assertions: NUnit

Logging: Serilog

CI/CD: Jenkins (Declarative Pipeline)