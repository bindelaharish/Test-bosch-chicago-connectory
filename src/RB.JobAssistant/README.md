# Welcome to the Bosch Job Assistant Web API

We've made some big updates in this release ...

## This application consists of:

..
..

## How to

..
..

## Overview

.
.

## Setup

.
RB.JobAssistant SoCo project - MySQL and project source warm-up

Basic MySQL DB dependencies of RB.JobAssistant, please note:

MySQL database engine
MySQL Workbench (optional SSMS-like tool)

Basic steps for database setup of RB.JobAssistant:

a) create CREATE DATABASE jobassistant
b) add user jbauser to this database
c) grant privileges to this DB for jbauser

Next, run the following to create the schema:

dotnet run -op=create	=> create schema (to drop, dotnet run -op=drop)

After this, run the following to load-in data:

dotnet run -op=sample	=> load sample data
.

## Build

.
Building:

To build the project from Bosch SoCo, do a git clone on the project.

git clone https://BOSCH-AD-USER-ID@sourcecode.socialcoding.bosch.com/scm/cibdx/rb.jobassistant-netcore.git

dotnet restore
dotnet build
.

## Run & Deploy

.
dotnet net run

From a Web browser, fire off:

http://localhost:8080/swagger/index.html

.

## Next Changes

    <PackageReference Include="RB.Api.Core.Extensions.Startup" Version="1.0.0" />
    <PackageReference Include="RB.Api.Core.Hateoas" Version="1.0.0" />
    <PackageReference Include="RB.Api.Core.JsonMapper" Version="1.0.0" />
    <PackageReference Include="RB.Api.Core.Odata" Version="1.0.0" />

And also adjust this, http://beat-artifactory.de.bosch.com/artifactory/api/nuget/nuget-repo/.
