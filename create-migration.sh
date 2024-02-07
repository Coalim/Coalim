#!/bin/bash

# Description: Script to migrate a database using Entity Framework Core migrations.
# Usage: ./create-migration.sh <migration_name>
# Parameters:
#   <migration_name>: Name of the migration to be added

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <migration_name>"
    exit 1
fi

# Find the root directory of the git repository
git_root=$(git rev-parse --show-toplevel)

# Change directory to the git root directory
cd "$git_root" || {
    echo "Error: Git root directory not found."
    exit 1
}

cd Coalim.Database.BunkumSupport || {
      echo "Error: Couldn't find the database project folder."
    exit 1
}

# Run EF Core migrations command to add a new migration
dotnet ef migrations add "$1"

# Check if the command was successful
if [ $? -ne 0 ]; then
    echo "Error: Failed to add migration '$1'."
    exit 1
fi

echo "Migration '$1' added successfully."
exit 0