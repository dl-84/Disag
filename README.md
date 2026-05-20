# Shoootz

## Lizenz

Dieses Projekt steht unter der MIT-Lizenz — siehe [LICENSE](LICENSE) für Details.

## Release

Releases werden automatisch via GitHub Actions gebaut wenn ein Tag gepusht wird:

```bash
git tag v1.0.0
git push origin v1.0.0
```

Der Workflow baut alle Plattformen parallel auf nativen Runnern und erstellt automatisch ein GitHub Release mit folgenden Artefakten:

| Plattform | Datei | Runner |
|---|---|---|
| Windows | `Shoootz.exe` | `windows-latest` |
| macOS (Apple Silicon) | `Shoootz-macos-arm64.zip` | `macos-latest` |
| macOS (Intel) | `Shoootz-macos-x64.zip` | `macos-latest` |
| Linux | `Shoootz.AppImage` | `ubuntu-latest` |

Die Version wird aus dem Git-Tag ausgelesen (`v1.0.0` → `1.0.0`).  
Zum Ändern der Version: `<Version>` in `Shoootz/Shoootz.csproj` anpassen.

## Assets

### Länderflaggen

PNG-Flaggenbilder stammen von **flagcdn.com**:

```
https://flagcdn.com/w40/{code}.png
```

Beispiele: `de` → Deutschland, `gb` → Großbritannien, `at` → Österreich, `fr` → Frankreich  
Verfügbare Ländercodes folgen dem ISO 3166-1 alpha-2 Standard.  
Vollständige Liste: https://flagcdn.com
