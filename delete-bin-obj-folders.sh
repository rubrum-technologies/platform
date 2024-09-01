#!/bin/bash

# Clear the screen
clear

echo "Deleting all BIN and OBJ folders..."
echo ""

find . \( -name "bin" -o -name "obj" -o -name "Generated" -o -name "ClientProxies" \) -type d -not -path "./node_modules/*" -print0 | while IFS= read -r -d '' dir; do
    echo "Deleting: $dir"
    rm -rf "$dir"
done

echo ""
echo "BIN and OBJ folders have been successfully deleted."
echo "Press Enter to exit..."

read -p "" dummy
