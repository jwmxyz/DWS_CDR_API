# DWS_CDR_API
Tech test for DWS

## Assumptions
- It is assumed that the incoming .csv is well formed and validated
	+ This is external data and should be from a trusted source. If something is incorrectly formatted or an error detected it is not our decision to modify the data as we do not understand the full context. 
- A proper RDBM would be used for proper Data Annotations
	+ SQL lite is used for simplicity for this tech test

## Getting Started

### Prerequisites

- .Net 7
- Entity framework
	+ `dotnet tool install --global dotnet-ef`

### Setup (Temp)

#### Database Migrations
- Navigate to `..\DWS_CDR_API\src\Crd.DataAccess.Migrations`
- Within powershell run `dotnet ef database update`
