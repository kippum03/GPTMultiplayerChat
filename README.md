GPT Multiplayer Chat

A social sandbox prototype in Unity + Photon PUN 2 with diegetic world-space chat bubbles—and a roadmap to make chat GPT-powered (assistant NPCs, translation, moderation, and smart commands).

Current state: multiplayer join/spawn + bubble chat over players’ heads.
Near-term: plug in a small backend so GPT features can run safely without exposing keys.


Current Status

Multiplayer flow: connect → join/create room → spawn PlayerPrefab.

Diegetic chat: type in the HUD input → message shows as a world-space chat bubble above the speaker; sent to all clients via RPC.

Clean scene wiring

PlayerPrefab has a child HeadAnchor (bubble spawn reference).

PlayerRefs exposes headAnchor (serialized) for scripts.

ChatBubble prefab (world-space Canvas) lives in Assets/_Project/Resources/.

ChatManager (on the player, MonoBehaviourPun) handles input + RPC.

HUD: single TMP Input Field anchored to the bottom; Enter to send; clears on submit.

Legibility: white TMP text with subtle outline; semi-transparent, sliced background.


Next Step

Planned GPT features are built around in-world, social UX—not a chatbox bolt-on.

Assistant NPCs: a “Guide” in the world you can ask for tips/help (@guide how do I …).

Slash commands:

/gpt <prompt> → assistant responds in a bubble (and optional HUD log).

/translate <lang> → auto translate your last/next message.

/summarize → short recap of recent local chat.

Moderation: toxicity/profanity checks (soft-block, replace, or warn).

Smart delivery: proximity scope (nearby players only) or global; optional “reply suggestions”.

Streaming bubbles: GPT responses type in naturally over the NPC or a virtual “speaker”.

Security: GPT calls will be proxied through a tiny backend (Cloudflare Worker, Firebase Function, or Node/Express). No API keys in the client.

📦 Project structure (high-level)
Assets/
  _Project/
    Prefabs/
      PlayerPrefab.prefab
    Resources/
      ChatBubble.prefab         // required for runtime instantiate
      PlayerPrefab.prefab       // if spawned via Resources
    Scripts/
      Networking/
        ChatManager.cs          // input + RPC to spawn bubbles
      Player/
        PlayerRefs.cs           // exposes HeadAnchor
      UI/
        ChatBubble.cs           // ShowMessage(), timer, TMP refs
      GPT/                      // (reserved) GPT service + command router
    UI/
      HUDCanvas                 // TMP Input Field anchored bottom
      

Roadmap
Near-term (GPT integration)

Backend (minimal): /chat, /moderate, /translate endpoints; rate-limited; logs disabled by default.

Unity GPT Service: IGptClient wrapper + async streaming; retries/exponential backoff.

Command Router: parse /gpt, /translate, /summarize, @guide …; decide GPT vs local broadcast.

Assistant NPC: a world object with GptAgent component (persona/system prompt, cooldown).

Moderation pass: check messages before broadcast; strip/replace or block with feedback.

UX polish:

Up to 3 wrapped lines in a fixed-size bubble; clamp TMP autosize range.

Optional ellipsis vs hard truncate.

Bubble billboards toward camera; offset & on-screen clamping.

Fade in/out; per-speaker stacking with short history.

Later

Proximity chat + global channels; per-player mute/block.

Room/lobby UI (list, join by code); mobile soft-keyboard support.

Basic analytics/diag (ping/region, GPT latency).

Automated layout tests (TMP wrapping/ellipsis invariants) + simple network playtests.
