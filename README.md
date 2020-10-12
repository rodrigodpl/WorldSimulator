# WorldSim

Welcome to WorldSim, your terrain generator and worldbuilding tool!

Below you will find the documentation needed to add or modify the behavior of WorldSim easily and quickly. It is heavily recommended to read the documentation before trying to implement any changes on the simulation.

## Basic Concepts

### Classes
The main classes used in the software are WS_Tile, WS_Entity, WS_Trait and WS_Event.

- A Tile corresponds to each of the hexagonal units that form the simulation map. Their main purpose is to store most of the simulation's information.

- An Entity corresponds to a group of tiles related between them by a common factor, like a shared religion or culture. Entities store information about themselves as well as apply changes in the behavior of the related tiles.

- A Trait is a given characteristic of an Entity (i.e. Tolerant for Religions), which includes a name, an effect over the Entity's related tiles and a Nature. Entities can't have two Traits of the same Nature (to avoid a Religion having both the Tolerant and Intolerant Traits, for example). Traits are used to provide flavour and distinction between Entities.

- An Event is an occurrence that can change the data of a Tile or an Entity, like Population Growth in a tile or a Battle in a war. Events have a chance of firing for each Tile, based on its parameteres. They are the main actors of the simulation and are the base of its behavior.

### Main loop

On Start, the tool will use the Terrain Generation paremeters introduced by the user to create and populate a new world.

Then, and until the tool is closed, the main loop will execute. On the main loop, each Event will travel the whole simulation map and execute its effects if fired. Once an Event has travelled the whole map, the next Event will start executing. Once all Events have travelled the map, a year passes and the process starts again.

IMAGE

## How to add a new Event

1. Start by creating a new Script. There you can implement any Events of the same Module, this is, of related behavior of functionality (i.e. Commerce).
2. Create a new class which inherits from the class WS_BaseEvent. The base class implements four methods that can be overriden in your new Event class.        
  2.1 FireCheck: if the method returns true, the Event will execute its SuccessCheck method. If not overridden, it will always return true.            
  2.2 SuccessCheck: if the method returns true, the Event will execute its Success method. If not, the Event will execute its Fail method. If not overridden, it will always  return true.           
  2.3 Success: main body of the Event, where the changes to the Tile or Entity data happen. It should always be overridden.           
  2.4 Fail: alternative body of the Event, in case there are two possible outcomes. If not overridden, it has no effect.           
3. Add the Event into the list eventPool of WS_World (order matters!).

IMAGE
  
  Note: access the tile data by using the variable _tile_ inside the Event.
  
## How to add a new Entity

1. Start by creating a new Script.
2. Create a new class which inherits from the class WS_Entity. 
3. Create a new item in the enumeration EntityType for your Entity.
4. Add two variables in the new class defining the minimum and maximum Traits this entity can have.
5. Modify WS_Entity Init method to include your new Entity.
6. Add a method for Tiles to reference the your new Entity in the PopulateWorld method of WorldGenerator.

IMAGE

## How to add a new Trait

1. Start by creating a new Script.
2. Create a new class which which inherits from the class WS_Trait.
3. 

