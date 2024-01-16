# Define the base directory where the projects are located

# Get all the project directories
$projects  = Get-ChildItem  -Recurse | Where-Object { $_.Extension -match 'csproj' }

foreach ($csproj in $projects) {
	Write-Host $csproj
    # Extract the numeric part of the folder path
    if ($csproj -match '(\d+)') {
        $numericPart = $matches[1]
        # Add the project to the solution file with the -s flag
        dotnet sln add $csproj -s $numericPart
    }
}

# Output a message when done
Write-Host "All projects added to the solution file."
