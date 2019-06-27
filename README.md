# Welcome\!

#### This repository contains the source code for:

   * The Multilinks API Service

# Project Overview

Please take a few minutes to review the overview below before diving into the code.

## The Multilinks Platform (The Big Picture)

## The Multilinks API Service

# Dev Environment

## Prerequisite

   * [Git](https://git-scm.com/)
   * [Visual Studio 2019 (as of this writing)](https://visualstudio.microsoft.com/vs/)
      + Required Workloads => .NET Core cross-platform development

## Getting Things Up & Running

   * Clone repository => git clone https://multilinks.visualstudio.com/MultilinksProject/_git/MultilinksApiService
   * Open the Multilinks API Service solution in Visual Studio => [PATH_TO_REPO_FOLDER]/Multilinks.ApiService.sln
   * Using the Secret Manager Tool to store application secrets
      + Right-click on "Multilinks.ApiService" project and select "Manage User Secrets" from the context menu
      + The "secrets.json" will opened
      + Enter Key/Value secrets into "secrets.json" or via the [command line](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2&tabs=windows#set-a-secret)
         - "ConnectionStrings:ApiServiceDb": "[CONNECTION_STRING_TO_LOCAL_API_DB]",
         - "DefaultPagingOptions:limit": 10,
         - "DefaultPagingOptions:offset": 0,
         - "TokenServiceInfo:ApiName": "ApiService",
         - "TokenServiceInfo:AuthorityUrl": "https://localhost:44300",
         - "CorsOrigins:WebIdp": "https://localhost:44300",
         - "CorsOrigins:WebApi": "https://localhost:44301",
         - "CorsOrigins:WebConsole": "https://localhost:44302"
   * Handling self-signed certificate (if required)
      + Open a Powershell session by opening the Package Manager Console => ALT + T, N, O
      + Check that the current directory is the project directory => pwd should show .../[PATH_TO_REPO_FOLDER]/Multilinks.ApiService
      + Change to project directory if not
      + Enable "dev-certs" trust => dotnet dev-certs https --trust
      + A dialog will ask for confirmation
      + Close Package Manager Console => SHFT + ESC
   * API Service should now be ready to launch

