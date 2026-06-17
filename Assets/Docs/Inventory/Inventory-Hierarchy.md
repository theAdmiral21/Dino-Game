# Inventory Hierarchy
I've run into some problems with my inventory. I asked Claude to help and got some insight into what needed to happen. Right now I have the following:
- Inventory: High level container for everything
- InventorySystem: The class that actually holds the inventory items and swaps between active items. It is also responsible for adding more items when they are picked up
- InventoryItem: The representation of the item stock within the InventorySystem. Tracks how much of an item we have, increments and decrements items when they are added or consumed. It also holds a reference to the Equipment class that is the physical representation of the item. 
- Equipment: The physical representation of an item. Holds a magazine class and implements methods for for reloading, aiming, and using the equipment when it is equipped. 
- Magazine: Tracks how many "ready to use items" are available and performs calculations for refilling a magazine.

All of this needs a refactoring because the equipment knows about the item and vice versa and trying to orchestrate firing, reloading, and refilling the magazine quickly fell apart.

## Refactored Structure
The refactor will see some pretty important changes going forward. For starters the inventory class will no longer build a catalog of inventory items on startup. That was kind of silly in hind sight. Instead when items are picked up they will return an InventoryItem that will be added to the inventory system. This will shift organization concerns and events when new items are picked up away from the items themselves and instead to the InventorySystem.

The InventorySystem will be responsible for switching between the currently equipped item as well as wiring reload and fire events. InventorySystem will orchestrate depositing and withdrawing items' stock. 

Items will be pure data. They tell you what the thing is, how much you currently have of it, and how much you are allowed to have. 

Finally Equipment will not know about the inventory system or items. In order to withdraw from the inventory, it will raise an event, the inventory system will handle it, and then return how much was able to be withdrawn. 
