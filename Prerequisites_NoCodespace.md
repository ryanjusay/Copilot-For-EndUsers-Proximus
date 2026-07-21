# Prerequisites for Local Development (No Codespace)

To complete the hands-on labs in your local VS Code environment, ensure you have the following:

## Required Software

- **Visual Studio Code** (Latest)  
  https://code.visualstudio.com/

- **Node.js** (v18 or later)  
  https://nodejs.org/  
  _(npm is included with Node.js)_

- **.NET 9.0 SDK** (Required for the WrightBrothersApi backend)  
  https://dotnet.microsoft.com/download/dotnet/9.0

- **Git**  
  https://git-scm.com/downloads

## Required VS Code Extensions

- **GitHub Copilot** (v1.284.0 or later)  
  Extension ID: `github.copilot`  
  https://marketplace.visualstudio.com/items?itemName=GitHub.copilot

- **GitHub Copilot Chat** (v0.25.1 or later)  
  Extension ID: `github.copilot-chat`  
  https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat

- **C# Dev Kit** (for .NET development)  
  Extension ID: `ms-dotnettools.csdevkit`  
  https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit

- **C#**  
  Extension ID: `ms-dotnettools.csharp`  
  https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

- **ESLint** (for JavaScript/TypeScript linting)  
  Extension ID: `dbaeumer.vscode-eslint`  
  https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint

- **REST Client** (by Huachao Mao - for testing HTTP requests)  
  Extension ID: `humao.rest-client`  
  https://marketplace.visualstudio.com/items?itemName=humao.rest-client

## Optional but Recommended Extensions

- **GitHub Pull Requests** (v0.120.0 or later)  
  Extension ID: `github.vscode-pull-request-github`  
  https://marketplace.visualstudio.com/items?itemName=GitHub.vscode-pull-request-github

- **GitHub MCP Server** (for agentic workflows)  
  Extension ID: `github.vscode-github-mcp`  
  https://marketplace.visualstudio.com/items?itemName=github.vscode-github-mcp

- **Playwright** (for UI testing - Used in Lab 4.4)  
  Extension ID: `ms-playwright.playwright`  
  https://marketplace.visualstudio.com/items?itemName=ms-playwright.playwright

- **PowerShell** (for Windows users)  
  Extension ID: `ms-vscode.powershell`  
  https://marketplace.visualstudio.com/items?itemName=ms-vscode.powershell

- **Mermaid Preview** (by Mermaid OSS - for previewing diagrams)  
  Extension ID: `vstirbu.vscode-mermaid-preview`  
  https://marketplace.visualstudio.com/items?itemName=vstirbu.vscode-mermaid-preview

## Initial Setup Steps

1. **Clone the repository:**
   ```powershell
   git clone <repository-url>
   cd Copilot-Bootcamp-ForEndUsers
   ```

2. **Verify installations:**
   ```powershell
   dotnet --version  # Should show 9.0.x
   node --version    # Should show v18.x or higher
   npm --version
   git --version
   ```

3. **Open the repository in VS Code:**
   ```powershell
   code .
   ```

4. **Install recommended extensions** when prompted by VS Code, or manually install the extensions listed above from the VS Code Extensions marketplace.

## Running the Applications

The labs will guide you through installing dependencies and running the applications. Here are the basic commands for reference:

### Backend (WrightBrothersApi)
```powershell
cd WrightBrothersApi/WrightBrothersApi
dotnet run
```
- API runs on: http://localhost:1903
- Swagger UI: http://localhost:1903/swagger

### Frontend (WrightBrothersFrontend)
```powershell
cd WrightBrothersFrontend
npm run frontend
```
- Frontend runs on: http://localhost:5173

### Run Both Simultaneously
```powershell
cd WrightBrothersFrontend
npm run frontend-and-backend
```

## Verification

Before starting the labs, ensure:
- ✅ All required software is installed and accessible from the command line
- ✅ VS Code opens without errors
- ✅ GitHub Copilot and Copilot Chat extensions are installed and activated
- ✅ You can run `dotnet --version` and see version 9.0.x
- ✅ You can run `node --version` and see version 18.x or higher

You're now ready to begin **Lab 1.2** and beyond!

