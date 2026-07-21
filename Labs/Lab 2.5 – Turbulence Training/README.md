# Lab 2.5 – Turbulence Training: Mocking and Exceptions

This lab exercise explores how GitHub Copilot can assist in building robust unit tests, including scenarios for mocking dependencies, handling exceptions, and covering edge cases. You’ll practice writing prompts and making hands-on code updates to strengthen your backend’s reliability.

## Prerequisites
- The prerequisites steps must be completed, see [Labs Prerequisites](../Lab%201.1%20-%20Pre-Flight%20Checklist/README.md)

## Estimated time to complete

- 10 minutes.

## Objective
- Learn to leverage Copilot to generate, refine, and expand unit tests with mocking and exception handling, making your codebase more resilient and maintainable.
  - Step 1: Exploring Mocking in Unit Tests
  - Step 2: Adding a Negative Test for an Exception Scenario
  - Step 3: Writing a Unit Test for Exception Handling
  - Step 4: Advanced Mocking (Optional)
  - Step 5: Run and Review All Tests (Optional)

---

### Step 1: Exploring Mocking in Unit Tests

- Open `/WrightBrothersApi.Tests/Controllers/PlanesControllerTests.cs`.

Notice the use of a mock logger via NSubstitute in the test constructor:
```csharp
_logger = Substitute.For<ILogger<PlanesController>>();
_planesController = new PlanesController(_logger);
```

- Open **GitHub Copilot Chat**.

- Click `+` to clear prompt history.

- Paste the following prompt:

```
Explain why we use a mock logger in PlanesControllerTests. Can Copilot show how to mock other dependencies, like repositories, using Moq?
```

- Paste the following prompt:

Generate a unit test method for PlanesController that uses Moq to mock the ILogger<PlanesController> dependency. Add comments explaining how the mock works.

- Accept Copilot’s suggestions or adjust as needed.

- Click `Apply` to insert the unit test into `PlanesControllerTests.cs`.

- Click `Keep` to accept the changes.

---

### Step 2: Adding a Negative Test for an Exception Scenario

- Open `PlanesController.cs` file.

- Highlight the `GetById` or `Post` method.

- Add a line to throw an exception if a specific condition is met (for the exercise).

- Press `Ctrl+I` to open the Inline Editor.

- Paste the following prompt:

```
Add logic in PlanesController.GetById to throw an InvalidOperationException if id == -1.
```

- Accept the suggestion to save your changes.

---

### Step 3: Writing a Unit Test for Exception Handling

- Open **GitHub Copilot Chat**.

- Click `+` to clear prompt history.

- Paste the following prompt:

```
Write a unit test method in PlanesControllerTests.cs that verifies GetById(-1) returns a 500 Internal Server Error if an exception is thrown. 
If the PlanesController constructor requires dependencies, use minimal dummy or fake objects as needed, but do not use any external mocking libraries.
Only include the test method code, with a descriptive method name and a comment explaining what the test does.
```

> [!TIP]
> We’re using simple dummy objects for dependencies—not the full C# mocking libraries—so you can focus on learning Copilot and prompt engineering, not .NET setup details. This keeps the lab quick, clear, and all about Copilot!

- Accept Copilot’s suggestions or adjust as needed.

- Click `Apply` to insert the unit tests into `PlanesControllerTests.cs`.

- Click `Keep` to accept the changes.

## Optional: Bulk Test Generation with Copilot Edits or Agent Mode

Ready to take your unit testing further? Try one of these advanced options:

### Using Copilot Edits (for most users)

- Open **GitHub Copilot Edits**.

- Click `+` to clear prompt history.

* Add `PlanesControllerTests.cs` to the working set.

- Paste the following prompt:

  ```
  Generate all positive and negative test cases for the controllers, including tests for exception scenarios, and organize them with clear comments. Only include the test method code, with a descriptive method name.
  ```

- Accept Copilot’s suggestions or adjust as needed.

- Click `Keep` to apply the units test into `PlanesControllerTests.cs`.

- Close all files.

### Using Agent Mode (for advanced users)

- Open **GitHub Agent Mode**.

- Click `+` to clear prompt history.

- Paste the following prompt:

  ```
  Generate comprehensive unit tests for all controllers, covering edge cases and exception handling. Highlight any files changed.
  ```

- Let Agent Mode automate these additions, then review and run the new tests.

- Click `Keep` to accept the changes.

- Click `Continue` to allow Agent Mode to run the tests.

> **Why Try This?**
> For larger projects, Copilot Edits and Agent Mode can generate or update a wide set of tests at once, helping you spot gaps and avoid repetitive manual work.

---

### Step 4: Advanced Mocking with Moq (Optional)

In this advanced step, you’ll use Copilot to scaffold a repository interface and then mock it in your unit tests with Moq. This is a real-world workflow in modern .NET testing.

**Create the Repository Interface**

  - Open **GitHub Copilot Chat**.

  - Click `+` to clear prompt history.

  - Paste the following prompt:

    ```
    Create an interface called `IPlaneRepository` with methods to Add, Get, and Delete Plane objects.
    ```

  - Click `...` and select `Insert` to add this file.
  
  - Click `Cntl+S` to save the file `IPlaneRepository.cs` in your `Models` folder.

- It should look something like:

  ```csharp
  public interface IPlaneRepository
  {
      void Add(Plane plane);
      Plane Get(int id);
      void Delete(int id);
      // ... Add any additional methods you want
  }
  ```

  - Close the `IPlaneRepository.cs` file.

**Update the PlanesController to Use IPlaneRepository**

  - Open `PlanesController.cs` file.

  - In **GitHub Copilot Chat**.

  - Click `+` to clear prompt history.

  - Paste the following prompt:

    ```
    Update PlanesController to accept an IPlaneRepository in the constructor and use it to manage Plane objects.
    ```

  - Accept Copilot’s suggestions or adjust as needed.

  - Click `Apply` to update the `PlanesController.cs` file.

  - Click `Keep` to accept the changes.


> [!NOTE]
> Review Copilot’s changes to make sure your controller is now loosely coupled and testable.

**Install Moq and Add the Using Statement**

  - In the terminal, navigate to your test project directory.
    - i.e. `cd WrightBrothersApi\WrightBrothersApi.Tests`

  - Paste the following prompt and run it:

    ```sh
    dotnet add package Moq
    ```

 - Add at the top of `PlanesControllerTests.cs`:

    ```csharp
    using Moq;
    ```
  
  - Close the `PlanesController.cs` file.

**Generate the Unit Test with Moq**

  - Open `/WrightBrothersApi.Tests/Controllers/PlanesControllerTests.cs`.

  - In **GitHub Copilot Chat**.

  - Click `+` to clear prompt history.

  - Paste the following prompt:

    ```
    Write a unit test using Moq to mock IPlaneRepository for PlanesController. The test should verify that the Post action calls the repository’s Add method when adding a plane. Only include the test method, with a descriptive name and comments explaining how Moq is used.
    ```

  - Accept Copilot’s suggestions or adjust as needed.

  - Click `Apply` to update the `PlanesControllerTests.cs` file.

  - Click `Keep` to accept the changes.


> [!TIP]
> In real-world .NET projects, using interfaces and mocking libraries like Moq makes your code more testable and your tests more reliable—while letting Copilot do the heavy lifting!

---

## Step 5: Run and Review All Tests

- Run your unit tests again.

  - In the terminal, run your tests:

    ```sh
    dotnet test
    ```

* Did Copilot generate realistic negative and exception test cases?
* How does mocking help in writing isolated and repeatable unit tests?

---

### Congratulations you've made it to the end! &#9992; &#9992; &#9992;

#### And with that, you've now concluded this module. We hope you enjoyed it! &#x1F60A;