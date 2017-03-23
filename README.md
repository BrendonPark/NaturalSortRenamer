# Natural Sort Renamer

Batch file renamer that sorts files using Window's native sorting algorithm and renames them ASCIIbetically. Because sometimes humans don't sort files, computers do.

This program is ridiculously simple, and only accepts a directory and a new name. Numbers are automatically added to the end of the new name, and by default pad to 4 places.

This isn't so much an actual project as just open sourcing one of my internal tools that I use from time to time. So, feel free to fork and go crazy on whatever requirements you have for the new name!

## Technicals

- WinForms
- Targets .NET 4.5
- Invokes StrCmpLogicalW from shlwapi.dll, so results may vary depending on Windows versions