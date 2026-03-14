# Heyreach CLI

A CLI tool wrapping the [HeyReach API](https://documenter.getpostman.com/view/23808049/2sA2xb5F75), optimized for LLM agent consumption. Built with [Spectre.Console](https://spectreconsole.net/) and .NET 10.

## Setup

```bash
dotnet build src/Heyreach.Console
```

Set your API key:

```bash
export HEYREACH_API_KEY="your-key-here"
```

Or pass it per-command with `--api-key`.

## Output

**YAML by default** — more token-efficient for LLMs (no braces, quotes, or commas). Switch with `--format json` or `--format table`.

## Commands

```
heyreach auth check                                          Validate API key
heyreach campaigns list [--limit] [--offset] [--status]      List campaigns
heyreach campaigns get <id>                                  Campaign details
heyreach campaigns pause <id>                                Pause a campaign
heyreach campaigns resume <id>                               Resume a campaign
heyreach campaigns add-leads <id> [--file]                   Add leads (stdin or file)
heyreach campaigns stop-lead <id> --lead <id>                Stop a lead in a campaign
heyreach lists list [--limit] [--offset]                     List all lead lists
heyreach lists get <id>                                      List details
heyreach lists create --name [--type user|company]           Create empty list
heyreach lists leads <id> [--keyword] [--limit]              Get leads from a list
heyreach lists add-lead <id> --linkedin-url <url>            Add lead to a list
heyreach lists remove-lead <id> --lead <id>                  Remove lead from a list
heyreach lists companies <id> [--limit]                      Get companies from a list
heyreach leads get --linkedin-url <url>                      Lookup lead by LinkedIn URL
heyreach leads lists --linkedin-url <url>                    Which lists contain a lead
heyreach leads update-status --lead <id> --status <status>   Update lead status
heyreach senders network <id> [--limit]                      Sender's connections
heyreach stats get [--from] [--to] [--campaign-id]           Overall stats
```

## Examples

```bash
# List active campaigns
heyreach campaigns list --limit 5

# Look up a prospect
heyreach leads get --linkedin-url "https://linkedin.com/in/jdoe"

# Add leads to a campaign from a file
heyreach campaigns add-leads 12345 --file leads.json

# Get stats for a date range
heyreach stats get --from 2025-01-01 --to 2025-12-31

# JSON output for jq pipelines
heyreach campaigns list --format json | jq '.items[] | select(.status=="IN_PROGRESS") | .name'
```

## Global Options

| Flag | Description |
|---|---|
| `--api-key <key>` | Override `HEYREACH_API_KEY` env var |
| `--format yaml\|json\|table` | Output format (default: yaml) |
| `--verbose` | Print HTTP method, URL, and status code to stderr |
| `--no-color` | Disable colored output |

## Error Handling

Errors are written to stderr as YAML with predictable exit codes:

- `0` — success
- `1` — user/input error (bad arguments, not found)
- `2` — API/network error (rate limit, connection failure)
