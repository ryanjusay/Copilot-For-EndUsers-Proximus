# Lab 3.1 – Aviation Incident Analysis ✈ Troubleshooting

In this lab, you'll investigate simulated aviation incidents using GitHub Copilot to identify and fix root causes in your code. Like an aviation investigator, you’ll analyze failures, ask Copilot for help, and apply human oversight to ensure high-quality results.

A reference to the [Aviation Incident Analysis](<https://en.wikipedia.org/wiki/Mayday_(Canadian_TV_series)>) TV show, where the investigators try to find the root cause of an airplane crash.

**Prerequisites:**  
Complete all prior labs and ensure you have the project set up per the [Labs Prerequisites](../Lab%201.1%20-%20Pre-Flight%20Checklist/README.md).

## Estimated time to complete

- 20 minutes.

## Objective
Build skill in using GitHub Copilot to reproduce, diagnose, and fix real failures, then validate your changes through HTTP calls and optional performance work. In this lab you will complete:

- Step 1: Flight Crash Investigation – Fuel Depletion Scenario
- Step 2: Lightning Strikes – Stack Overflow Scenario
- Step 3: False Horizon - Runtime Regression Check
- Step 4: Aerodynamics of an Airplane – Performance Optimization

---

### Step 1 - Fixing a Flight Crash - Fuel Depletion Scenario

- Open `FlightsController.cs` file located in the `Controllers` folder.

- Navigate to the `takeFlight` method.

> [!NOTE]
> The method simulates a flight and throws an exception if the flight runs out of fuel.

```csharp
public class FlightsController : ControllerBase
{
    [HttpPost("{id}/takeFlight/{flightLength}")]
    public ActionResult takeFlight(int id, int flightLength)
    {
        var flight = Flights.Find(f => f.Id == id);

        for (int i = 0; i < flightLength; i++)
        {
            if (flight.FuelRange == 0)
            {
                throw new Exception("Plane crashed, due to lack of fuel");
            }
            else
            {
                var fuelConsumption = 1;

                if (flight.FuelTankLeak)
                {
                    fuelConsumption = 2;
                }

                flight.FuelRange -= fuelConsumption;
            }
        }

        return Ok($"Flight took off and flew {flightLength} kilometers/miles.");
    }
}
```

- Open a terminal and navigate to the `WrightBrothersApi` folder

- Run the application

    ```sh
    dotnet run
    ```

> [!NOTE]
> If you encounter an error message like `Project file does not exist.` or `Couldn't find a project to run.`, it's likely that you're executing the command from an incorrect directory. To resolve this, navigate to the correct directory using the command `cd ./WrightBrothersApi`. If you need to move one level up in the directory structure, use the command `cd ..`. The correct directory is the one that contains the `WrightBrothersApi.csproj` file.

- Open the `Examples/Flights.http` file.

- Click `Send Request` to execute the `takeFlight` request.

```
Send Request
POST http://localhost:1903/flights/1/takeFlight/75 HTTP/1.1
content-type: application/json
```

> [!NOTE]
> You must have the `Rest Client` with identifier `humao.rest-client` extension installed in Visual Studio Code to execute the request. Rest Client is a very useful extension to quickly execute HTTP requests and commit them to Git.

> [!NOTE]
> The flight is taking off and the response is `200 OK`. The flight that is simulated did not run out of fuel.

```json
HTTP/1.1 200 OK
Connection: close
```

- Now execute the request again, but now for flight `3`.

- Change the `1` to `3` in the request and execute it again.

```
POST http://localhost:1903/flights/3/takeFlight/75 HTTP/1.1
content-type: application/json
```

- You will see that the flight is taking off and the response is `500 Internal Server Error`. The flight that is simulated ran out of fuel and crashed.

- The Rest Client response will now include the `FlightLog` property as follows:

    ```json
    HTTP/1.1 500 Internal Server Error
    Connection: close

    System.Exception: Plane crashed, due to lack of fuel
    at FlightsController.takeFlight(Int32 id, Int32 flightLength) in C:\Temp\WrightBrothersApi\WrightBrothersApi\Controllers\FlightsController.cs:line 174
    ```

- Stop the app by pressing `Ctrl + C` or `Cmd + C` in the terminal, or by clicking on the 'Stop' button in the debugger panel.

- Now, let's debug it with GitHub Copilot

- Click on the code tab `FlightsController.cs` to bring it into focus.

- Select all the code for method `takeFlight` in `FlightsController.cs`.

- Open `GitHub Copilot Chat`, select `Ask` click `+` to clear prompt history.

- Paste the following prompt:

    ```prompt
    How should I fix this exception in @terminal for the takeFlight method in FlightsController.cs?
    ```

<img src="../../Images/Screenshot-LackOfFuel-Chat.png" width="800">
  
> [!NOTE]
> When asking Copilot for help, provide the necessary context to help it understand the problem. Simply select the problematic code in the editor or mention the filename if you have multiple files open.

- Copilot will suggest a possible fix on how to handle the exception.

- You can go ahead and replace the `takeFlight` method with the new one and run the application again.

- In the Chat window, click on the `Insert at Cursor` button to replace the `takeFlight` method with the new one.

- Run the application

    ```sh
    dotnet run
    ```

- Now go to `Examples/Flights.http` file, click `Send Request` to execute the `takeFlight` request again.

```
POST http://localhost:1903/flights/3/takeFlight/75 HTTP/1.1
content-type: application/json
```

- You will see that the flight is taking off and the response is `409 Conflict`. Plane crashed, due to lack of fuel.

- Stop the app by pressing `Ctrl + C` or `Cmd + C` in the terminal, or by clicking on the 'Stop' button in the debugger panel.

---

## Step 2 - Lightning Strikes, Unexpected Flight Crash - Stack Overflow Scenario

- Open `FlightsController.cs` file located in the `Controllers` folder.

- Navigate to the `lightningStrike` method.

> [!NOTE]
> The method simulates a lightning strike and causes recursion.

```csharp
public class FlightsController : ControllerBase
{
    // Rest of the FlightsController.cs file

    [HttpPost("{id}/lightningStrike")]
    public ActionResult lightningStrike(int id)
    {
        // Lightning caused recursion on an inflight instrument
        lightningStrike(id);

        return Ok($"Recovers from lightning strike.");
    }
}
```

- Open a terminal and navigate to the `WrightBrothersApi` folder

- Run the application

    ```sh
    dotnet run
    ```

- Go to the `Examples/Flights.http` file, click `Send Request` to execute the `lightningStrike` request.

    ```
    Send Request
    POST http://localhost:1903/flights/1/lightningStrike HTTP/1.1
    content-type: application/json
    ```

- The application will crash.

    > Stack overflow at FlightsController.lightningStrike(Int32).

<img src="../../Images/Screenshot-lightningStrikeError.png" width="800">

- Now, let's debug it with GitHub Copilot

- Click on the code tab `FlightsController.cs` to bring it into focus.

- Select all the code for method `lightningStrike` in `FlightsController.cs`.

- Open `GitHub Copilot Chat`, select `Ask` click `+` to clear prompt history.

- Paste the following prompt:

    ```
    How should I fix this exception in the lightningStrike method in FlightsController.cs?
    ```

- Copilot will suggest a possible fix on how to handle the exception.

- You can go ahead and replace the `lightningStrike` method with the new one and run the application again.

- In the Chat window, click on the `Insert at Cursor` button to replace the `lightningStrike` method with the new one.

- Run the application

    ```sh
    dotnet run
    ```

- Now go to `Examples/Flights.http` file, click `Send Request` to execute the `lightningStrike` request again.

    ```
    Send Request
    POST http://localhost:1903/flights/1/lightningStrike HTTP/1.1
    content-type: application/json
    ```

- The application will now recover from the lightning strike.

- Stop the app by pressing `Ctrl + C` or `Cmd + C` in the terminal, or by clicking on the 'Stop' button in the debugger panel.

---

### Step 3 – False Horizon: Runtime Regression Check

You will introduce a subtle bug that compiles, then use the HTTP file to trigger a runtime error and let Copilot Agent Mode fix it.

- Introduce the regression. Open `Controllers/PlanesController.cs`, in `GetById` replace the safe lookup with a strict one:

- On the first line, replace `FirstOrDefault`, with `First` so it reads:

    ```csharp
    var plane = Planes.First(p => p.Id == id); // throws if not found
    ```

This compiles, but `First` throws when the id does not exist, which should be a 404, not a 500.

- Run the application

    ```sh
    dotnet run
    ```

- Verify the happy path
   Open `/Examples/Planes.http` in VS Code, find the `GET` for a single plane and run it as is:

    ```http
    GET http://localhost:1903/api/planes/1 HTTP/1.1
    ```

You should get `200 OK` with a plane payload.

- Trigger the runtime error
   In the same `/Examples/Planes.http` file, change the id to a non existent value (i.e. 999) and send the request again:

    ```http
    GET http://localhost:1903/api/planes/999 HTTP/1.1
    ```

You should now see `500 Internal Server Error` and errors in the terminal window.

- Press `Ctrl + C` or `Cmd + C` in the terminal to stop the app, but leave the terminal open.

- Switch to the terminal where the API is running, hightlight the full exception, and copy it to clipboard.

- Open `GitHub Copilot Chat`, select `Agent` click `+` to clear prompt history.

- Paste the full exception from the terminal into the chat.

- Next, paste this prompt directly under the exception text:

    ```prompt
    Fix this runtime error in #file:Controllers/PlanesController.cs.
    - When a plane id does not exist, the API must return 404 NotFound, not 500.
    - Explain the root cause in one sentence and show the exact code diff you will apply.
    - Add or update a unit test that asserts 404 for a missing id.
    ```

- Accept the proposed fix.

- Run the application

    ```sh
    dotnet run
    ```

- Retest with `Planes.http`

    ```http
    GET http://localhost:1903/api/planes/999 HTTP/1.1
    ```

You should now get `404 Not Found`. Send the original request to confirm the happy path still works:

- Retest with `Planes.http`

    ```http
    GET http://localhost:1903/api/planes/1 HTTP/1.1
    ```

> [!NOTE]
> Always paste the full terminal error and the exact HTTP request you used into the same Agent Mode chat. Keeping the context together makes Copilot’s fix more accurate and keeps the conversation history useful for follow up prompts.

---

### Step 4 – Aerodynamics of an Airplane - Performance Optimization

You will optimize the `calculateAerodynamics` workflow. Choose one track, or do both if time allows.

> [!TIP]
> If you plan to do both tracks, run **Track A** first, then let **Track B** run on top of it. Agent Mode fixes are cross-cutting and still apply cleanly after the algorithm upgrade, you get the faster hot path plus safety and resiliency improvements. If you are short on time, do Track B only.

#### Track A, Manual algorithm upgrade and timing.

You will switch from a slow prime check to a faster approach, then measure the result using the same HTTP request.

- Open `FlightsController.cs` and review the three methods that drive this flow: `calculateAerodynamics`, `CalculatePrimes`, and `IsPrime`.

- Open a terminal and navigate to the `WrightBrothersApi` folder

- Run the application

    ```sh
    dotnet run
    ```

- Now go to `Examples/Flights.http` file, click `Send Request` to execute the `calculateAerodynamics` request.

    ```
    Send Request
    POST http://localhost:1903/flights/1/calculateAerodynamics HTTP/1.1
    content-type: application/json
    ```

- Response will be:

    > HTTP/1.1 200 OK  
    > Connection: close

- Terminal will show something like this:

    > Found 25997 prime numbers.  
    > Elapsed Time: 4.863 seconds

- The application will calculate the prime numbers in more than 5 seconds.

- Stop the app by pressing `Ctrl + C` or `Cmd + C` in the terminal, or by clicking on the 'Stop' button in the debugger panel.

- Now, let's use GitHub Copilot to optimize the code.

- Open `FlightsController.cs` and select all the code for the 3 following methods:

    - `calculateAerodynamics` method.
    - `CalculatePrimes` method.
    - `IsPrime` method.

    <img src="../../Images/Screenshot-calculateAerodynamicsSelected3.png" width="800">

- Open `GitHub Copilot Chat`, select `Ask` click `+` to clear prompt history.

- Paste the following prompt:

    ```prompt
    What is a more efficient algorithm for finding all prime numbers in a range than checking each number with IsPrime? Please explain how it works.
    ```

- Read Copilot’s answer. Expect a recommendation for the “Sieve of Eratosthenes” or similar, along with an explanation of how it works.

- Paste the following prompt:

    ```prompt
    Can you help me implement the Sieve of Eratosthenes in C#? Please include detailed comments explaining each part. Solve this in a novel way that does not match public code.
    ```

- Apply the proposed changes.

- Click on the `Apply in Editor` to replace the `calculateAerodynamics` methods with the new one.

- Run the application

    ```sh
    dotnet run
    ```

- Now go to `Examples/Flights.http` file, click `Send Request` to execute the `calculateAerodynamics` request again.

    ```
    Send Request
    POST http://localhost:1903/flights/1/calculateAerodynamics HTTP/1.1
    content-type: application/json
    ```

    Example output

    > Found 25997 prime numbers.  
    > Elapsed Time: 0.014 seconds

- The application will now calculate the prime numbers in less than 50 milliseconds.

> [!NOTE]
> **Track A** is about learning new algorithms. GitHub Copilot has knowledge of many algorithmic optimizations and can help you optimize your code performance.

---

#### Track B, Agent Mode hardening and bulk improvements.

You will use **GitHub Copilot Agent Mode** to apply cross-cutting fixes and resiliency patterns to the same controller.

- Open `GitHub Copilot Chat`, select `Agent` click `+` to clear prompt history.

   ```prompt
   Optimize all recursive methods in FlightsController.cs to include a recursion depth limit and proper error handling.
   ```

This targets safety and resiliency concerns across methods.

- For a broader pass, ask Agent Mode to scan the file and propose performance and stability improvements, then apply the changes:

   ```prompt
   Scan FlightsController.cs for all crash-prone or inefficient code and generate fixes and performance improvements.
   ```

- Apply the proposed changes.

- Run the application

    ```sh
    dotnet run
    ```

- Now go to `Examples/Flights.http` file, click `Send Request` to execute the `calculateAerodynamics` request again.

    ```
    POST http://localhost:1903/flights/1/calculateAerodynamics HTTP/1.1
    ```

    Example output

    ```
    Found 25997 prime numbers.
    Elapsed Time: 0.014 seconds
    ```

> [!NOTE]
> **Track B** is about automation at scale. Agent Mode can update multiple methods and patterns in one go, which is ideal when you need safety checks, better errors, and small speedups across the file.&#x20;

---

### Congratulations you've made it to the end! &#9992; &#9992; &#9992;

#### And with that, you've now concluded this module. We hope you enjoyed it! &#x1F60A;
