# Equipment Setup
The equipment system is getting a little noodley. This is me taking a step back and looking analyzing the effectiveness of the implementation and how scalable it is.

## Files
The following files make up a single piece of equipment, the rock.  
**Unity Level**
- EquipmentSO: A scriptable object holding the rock's stats
- EquipmentBridge: A monobehaviour that maps player inputs to equipment actions.
- EquipmentFactory: A factory class for building equipment classes and game objects. The responsibilities of this class are very muddied. Originally it was supposed to build instances of `IEquipment` for the inventory system, but then I started using it to make the actual game objects for the equipment. This will be needing a refactor because right now it's a tangled mess.
- RockEquipment: an implementation of `IEquipment` it is supposed to be the physical manifestation of a rock. It mostly does this, but building equipment game objects has proven difficult. That needs to get sorted out. However this class is not a waste. It implements the magazine class, handles firing, reloading, and aiming. It actually does a good job of communicating with its `InventoryItem` backing class.

**Application**
- Magazine: A general purpose magazine class that tracks available bullets and consumes bullets when fired.

**Core**
- IEquipment
- IEquipmentActionRequest
- IEquipmentActionResult
- IEquipmentBridge
- IMagazine

**Primitives**
- EquipmentStats: A struct holding values that describe how a piece of equipment should work. 

Okay looking at this now it doesn't *seem* so bad. I think the real issue is coordinating this with the inventory system.

## Inventory Files
The following files make up the inventory system.  
**Unity Level**
- Inventory: The top level inventory piece. It is the orchestrator for the inventory system. It coordinates requests from the equipment system and events raised by the inventory event bus.
- InventoryPresenter: The heads up display for inventory. It listens for events emitted by pick ups and equipped items to track and display how much of every item you have. Tbh I could probably make it stupider. I'll have to look into it.
- EquipmentAssets: A scriptable object that holds a dictionary relating `ItemType`s to sprites to make sure when you have a rock selected you see a rock in your equipped slot.
- ItemLimitSO: A scriptable object that defines how much of each item you can hold.

**Application**
- BaseInventoryItem: An abstract class that defines how values are tracked when things are deposited or withdrawn. I thought making this class abstract would help in the long run but I don't see that happening. 
- InventorySystem: This class is supposed to track the currently equipped item and provide methods for stocking and consuming items. But thinking about that more, nothing should get consumed via the inventory system. Only equipment can consume things from inventory items... That needs to change.

**Core**
- IInventory <= Why doesn't this have a reference to the inventory presenter? that seems a little silly.. OH also remove any consume calls from this class. It should only send pick up events to the inventory system.
- IInventoryItem <= yes
- IInventoryPresenter <= the red headed step child I can't get to behave..
- IInventorySystem <= remove consume calls