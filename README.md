## Overview
The `BizStream.Migrations` project is an executable sub-project of its own, used for migrating content from a Kentico 12 site to a Kentico 13 site. It was designed in a way that would give extensibility for future migration projects, whether it be from Kentico --> Kentico or from `<Another Source> --> Kentico`.

The main idea behind this project comes from source & destination endpoints (e.g. SQL --> Kentico, in most cases) that dictate the nomenclature of a new Repository class of name `<source>To<Destination>Repository` (e.g. `SqlToTreeNodeRepository`). This repository inherits from 1 source repository (`SqlRepository` in this case) and 1 destination repository (`ITreeNodeRepository`), and thus has the capability to both export & import the necessary data. Repositories should leverage generic typing and require strictly-typed models for data coming from the source (found in the `Models` folder here).


## Setup Steps

### Rebuild CMSApp.csproj
Assuming you have a Kentico 13 web project that you are adding this to, open `WebApp.sln` and rebuild `CMSApp.csproj` to ensure that all pages types are up-to-date in the admin (this is most important for Kentico 13 projects with Continuous Integration enabled).

### Connection Strings
If connection strings are required for both source and destination, the Destination connection string (for the new site) will be placed in the `BizStream.Migrations/connectionStrings.config` file, which should look like this:

```
<connectionStrings>
	<add name="CMSConnectionString" connectionString="<your_connection_string_here>" />
</connectionStrings>
```

The Source connection string (for the old site) is placed in a User Secret in the following format:

```
{
  "ConnectionStrings:ExportCMSConnectionString": "<your_connection_string_here>"
}
```

To add this user secret on your local environment, navigate to the `BizStream.Migrations` project in the Solution Explorer, right click it, and select `Manage User Secrets`.

![image](https://github.com/BizStream/altafiber-web/assets/105802092/a12ba903-8626-42b4-80f9-35b0558632f7)

A file named `secrets.json` should open, and there you will add/update the connection string value. If you're a BizStream employee, you will likely find connection strings in 1Pass.

## Running a Migration
To run the migration (after setup steps are complete), simply open `BizStream.Migrations.sln` and run! A command terminal should show up with a multi-selector of migration options from `Options/MigrationOptions.cs`.

## Adding a Migration
Here are the steps to take for adding a new migration:

1. Make sure the page type, custom table item, etc. code generated from Kentico is in the solution (most likely in the Class Library mentioned below).
2. Add a Model in the `Models` directory with property types and names that match the fields from the source table. The model should implement either the `CustomTableItemModel` or `TreeNodeModel` base type depending on what is being migrated.
3. Add a repository in the `Repositories` directory that implements either the `SqlToTreeNodeRepository` or `SqlToCustomTableItemRepository` interface, depending on what is being migrated.
4. Override the `SqlQuery` string property in the new repository that pulls the desired data from the old (source) Kentico database.
5. Add a mapping profile (or add to an existing one) under the `Mappings` directory that maps the fields from the model's (source) type to the Kentico (destination) type.
6. Add a const string name for the migration into `MigrationOptions.cs` file. This will be used in the CLI list for selecting which migration to run.
7. Add a migrator class that implements the `IMigrator` interface and has the `[Migrator(<name>)]` attribute where `<name>` is a reference to the constant string added in the last step.
8. Add the new repository as a dependency in the constructor in the new migrator class and implement the `Migrate` method. The migrate method should implement code to `Retrieve` the data and `Insert` the data into the new database (follow existing examples).

## Recommendations
For Kentico-specific implementations, leveraging existing C# Class Libraries in your project that contain generated PageType models is recommended. Simply add such class libraries as a project reference to your Migration project and use them at your leisure (likely will be a class library named `<project>.Infrastructure.Kentico`)
