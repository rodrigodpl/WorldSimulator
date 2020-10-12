# WorldSim

Welcome to WorldSim, your terrain generator and worldbuilding tool!

Below you will find the documentation needed to add or modify the behavior of WorldSim easily and quickly.

## Basic Concepts

### Classes
The main classes used in the software are WS_Tile, WS_Entity, WS_Trait and WS_Event.

- A Tile corresponds to each of the hexagonal units that form the simulation map. Their main purpose is to store most of the simulation's information.

- An Entity corresponds to a group of tiles related between them by a common factor, like a shared religion or culture. Entities store information about themselves as well as apply changes in the behavior of the related tiles.

- A Trait is a given characteristic of an Entity (i.e. Tolerant for Religions), which includes a name, an effect over the Entity's related tiles and a Nature. Entities can't have two Traits of the same Nature (to avoid a Religion having both the Tolerant and Intolerant Traits, for example). Traits are used to provide flavour and distinction between Entities,

- An Event is 
