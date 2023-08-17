using BizStream.Migrations.Options;
using Spectre.Console;

namespace BizStream.Migrations.Helpers;
public static class TerminalHelper
{
    public static List<string> PromptMigrationCategorySelection()
       => AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title( "What would you like to migrate?" )
                .PageSize( 10 )
                .MoreChoicesText( "[grey](Move up and down to reveal more options)[/]" )
                .InstructionsText(
                    "[grey](Press [blue]<space>[/] to toggle a migration, " + 
                    "[green]<enter>[/] to accept)[/]")
                .AddChoices( MigrationOptions.All ) );

}
