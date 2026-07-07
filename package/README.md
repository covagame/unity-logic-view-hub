# CovaGame Logic View Hub

Architecture Logic-View-Hub package for Unity.

## Architecture

Logic View Hub separates Unity presentation from game behavior through a shared HubProperty contract.

- `Logic`: behavior layer. It receives a derived HubProperty interface and does not directly operate Views.
- `View`: `MonoBehaviour` layer. It binds to a HubProperty interface and sends user requests.
- `HubProperty`: communication hub. It defines `ReactiveProperty` values and request/observable event pairs only.

```mermaid
flowchart LR
    View["View (MonoBehaviour)"] -->|"Request API"| Hub["HubProperty"]
    Hub -->|"Observable events"| Logic["Logic"]
    Logic -->|"ReactiveProperty updates"| Hub
    Hub -->|"ReactiveProperty observe"| View
```

`HubProperty` is intentionally not a logic layer. It is the boundary that keeps `Logic` from directly controlling `View`.

## Installation

Add this package through the configured Unity scoped registry.

```json
{
  "dependencies": {
    "jp.covagame.logic-view-hub": "0.1.0"
  }
}
```
