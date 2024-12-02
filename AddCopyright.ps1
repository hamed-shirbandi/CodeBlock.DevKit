# Define the copyright header text
$copyrightHeader = "// Copyright (c) CodeBlock.Dev. All rights reserved.`r`n// See LICENSE in the project root for license information.`r`n"

# Get all the .cs files in the project directory (including subdirectories)
$csFiles = Get-ChildItem -Path $PSScriptRoot -Recurse -Filter *.cs

foreach ($file in $csFiles) {
    $fileContent = Get-Content $file.FullName -Raw

    # Trim leading whitespace or newlines before checking if the copyright header is already present
    $trimmedContent = $fileContent.TrimStart()

    # Check if the copyright header already exists at the beginning of the file
    if ($trimmedContent -notlike "$copyrightHeader*") {
        # Add the copyright header to the beginning of the file
        $newContent = $copyrightHeader + "`r`n" + $fileContent
        Set-Content -Path $file.FullName -Value $newContent -Force
    }
}
