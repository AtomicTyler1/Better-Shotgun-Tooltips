## Links

[![TDPC - Join the discord](https://img.shields.io/badge/TDPC-Join_my_discord!-424db8?style=plastic&logo=discord&logoColor=%23ffffff&labelColor=%235865F2)](https://www.discord.gg/38hGSYrPyj)
[![Static Badge](https://img.shields.io/badge/Lethal_Company_Modding-I_have_a_modding_thread!-830000?style=plastic&logo=discord&logoColor=%23ffffff&labelColor=%23bc1d00)](https://discord.com/channels/1168655651455639582/1297711493151719515)
[![Kofi - Support me](https://img.shields.io/badge/Kofi-Support_me_on_kofi!-cf4922?style=plastic&logo=kofi&logoColor=%23ffffff&labelColor=%23FF6433)](https://ko-fi.com/atomictyler)

[![Lethal Company](https://img.shields.io/badge/AtomicStudio-Lethal_Company_Mods!-2381bf?style=plastic&logo=thunderstore&logoColor=%23ffffff&labelColor=35afff)](https://thunderstore.io/c/lethal-company/p/AtomicStudio/)
[![Content Warning](https://img.shields.io/badge/AtomicStudio-Content_Warning_Mods!-2381bf?style=plastic&logo=thunderstore&logoColor=%23ffffff&labelColor=35afff)](https://thunderstore.io/c/content-warning/p/AtomicStudio/)

[![Nexus](https://img.shields.io/badge/NexusMods-I_have_mods_there_too!-e26a00?style=plastic&logo=nexusmods&logoColor=%23ffffff&labelColor=%23E6832B)](https://next.nexusmods.com/profile/atomictyler1/mods)

## About the mod

This mod is packed with features!
The features include to:
- Customisable Safety
- Ammo Indication
- 8 Configs
- Client Sided
- If using LethalConfig no game restart
- - Compatible with other shotgun mods

This aims to improve you shotgunning experience!
Default custom safety text:
- On: The safety is on: [Q]
- Off: The safety is off: [Q]

Known bugs:
- Custom text tweaks for a second sometimes when changing.
- Automatic detection of On and Off only works when lowercase for colors.

## Configs

The following configs are as follows:
<details>
<summary> Colorize Text </summary>

- Description -> Enable or disable colorized text for shotgun safety.
- Default Value -> False
- Config type: Bool
- Further Explanation -> Makes the on / off text have a color.

</details>

<details>
<summary> Colorize Full-Text </summary>

- Category -> Text Color
- Description -> Enable or disable coloring the entire text instead of just the 'on' or 'off' word.
- Default Value -> False
- Config type -> Bool
- Further Explanation -> No further input needed. 

</details>

<details>
<summary> Colorize Full-Text </summary>

- Category -> Text Color
- Description -> Enable or disable coloring the entire text instead of just the 'on' or 'off' word.
- Default Value -> False
- Config type -> Bool
- Further Explanation -> Colorize text NEEDS to be active to work

</details>

<details>
<summary> On-Text Color </summary>

- Category -> Text Color
- Description -> The color for the 'on' text.
- Default Value -> green
- Config type -> String
- Further Explanation -> If colorize text is on this it'll be this color. HTML Colors only.

</details>

<details>
<summary> Off-Text Color </summary>

- Category -> Text Color
- Description -> The color for the 'off' text.
- Default Value -> red
- Config type -> String
- Further Explanation -> If colorize text is on this it'll be this color. HTML Colors only.

</details>

<details>
<summary> Safety Text Off </summary>

- Category -> Text Settings
- Description -> Custom text for safety off. Must contain the keyword 'off'.
- Default Value -> null / empty
- Config type -> String
- Further Explanation -> If left empty, it will use the default one I have made which is in: About the mod.

</details>

<details>
<summary> Safety Text On </summary>

- Category -> Text Settings
- Description -> Custom text for safety on. Must contain the keyword 'on'.
- Default Value -> null / empty
- Config type -> String
- Further Explanation -> If left empty, it will use the default one I have made which is in: About the mod.

</details>

<details>
<summary> Ammo Indicator </summary>

- Category -> Miscellaneous
- Description -> Shows how much ammo you have in the shotgun.
- Default Value -> False
- Config type -> Bool
- Further Explanation -> This is inside of the Fire tooltip. Example -> Fire (O)(): [LMB]

</details>

<details>
<summary> Number Represents Ammo </summary>

- Category -> Miscellaneous
- Description -> False is more compact, True is more wordy. You can always change this mid game using LethalConfig without a restart
- Default Value -> False
- Config type -> Bool
- Further Explanation -> This is inside of the Fire tooltip. Example -> Fire (1 Loaded): [LMB]

</details>
