How to run:

1. Setup multiple startup project:
     -TaskAppAPI
     -Blazor


Take note the following configuration on the port endpoints:

~TaskApp\TaskApp\Blazor\Program.cs
line 12: check if this is the port of the API running

~TaskApp\TaskApp\TaskAppAPI\Program.cs
line 32: check if this is the port of the Blazor App running
    
