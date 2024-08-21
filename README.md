# Zeitvertreib Common

A plugin that implements silly ideas from the community

requested by Dark_fox, DungPie, max.bambus76 & LOLFOX 

> [!IMPORTANT]
> This plugin requires Exiled.CustomItems which is sometimes missing in the install check in RemoteAdmin via `plym show`

# Additions:

## Changed SCP-914 recipe: 
- MTF Private now gives Containment Engineer on 1:1 setting (suggested by Dark_fox)

## Added Commands for RemoteAdmin:

### playersize
Changes size of a player

Permission: `zeitvertreib.team`

Usage: `playersize <RemoteAdmin-ID> <ScaleX> <ScaleY> <ScaleZ>`

### spawnitems
Spawns an amount of items with special attributes

Permission: `zeitvertreib.team`

Usage: `spawnitems <RemoteAdmin-ID> <Item> <Scale> <Amount>`

Spawns `<Amount>` of `<Item>` at location of `<RemoteAdmin-ID>` with scale `<Scale>`

Note: `<Amount>` is clamped to a number between and including:  `1-250`

## Added CustomItems (only availabe using RemoteAdmin):
### Granatenteilchenbeschleuniger:
Launches 300 HE-Grenades per second (suggested by DungPie / max.bambus76)

To give yourself the weapon use: `ci g 1`

Due to the imense power of this weapon when dropped a player based broadcast is sent and when picked up a serverwide broadcast is sent. 
Also when picked up / dropped an automatic message is sent to Staff Chat with the username of the player


### Blitzer:
Launches 300 SCP-018 per second (suggested by max.bambus76)

To give yourself the weapon use: `ci g 2`

Due to the imense power of this weapon when dropped a player based broadcast is sent and when picked up a serverwide broadcast is sent. 
Also when picked up / dropped an automatic message is sent to Staff Chat with the username of the player

### Fake500:
Fake SCP-500 that kills you instead of healing you (suggested by max.bambus76)

This item does not display any Exiled.CustomItem messages to make it indistinguishable from a real SCP-500 

To give yourself the weapon use: `ci g 3`

This item chooses the method of death randomly (suggested by LOLFOX): 
- Player gets kicked with "Timed out" message (suggested by Dark_fox)
- Player explodes (suggested by radston)
- Player gets CardiacArrest and loses Hands (suggested by LOLFOX)
- Player gets damaged 65535 HP with death reason "You were bitten very hard by Belu`s pet!" (suggested by max.bambus76)
- Player gets teleported 50 meters up to to die to falldamage (suggested by Dark_fox)
- Player loses hands (suggested by LOLFOX)
- Player gets frozen and teleported inbetween a tesla gate (suggested by radston)
- Player gets frozen loses all vision and dies to cardiac arrest (suggested by Dark_fox)


(Note: All of the above mentioned death methodes have a chance of 12.5 %)