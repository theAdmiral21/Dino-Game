# Inventory System
The inventory system will be pretty simple. There are four weapons, three ammo types, and five utility objects.  
- The inventory system needs a way to add and remove inventory items.
- Inventory Items will use enums to distinguish each other

```c#
public enum ItemType
{
    // Weapons
    Taser,
    Shotgun,
    RocketLauncher,
    NerveGas,

    // Ammo
    Shell,
    Rocket,
    Canister,

    // Utility items
    Rocks,
    Flashlight,
    Medkit,
    Flares,
    SmokeGrenade,

}
```
Inventory items can't be moved around. They're either available or they aren't. Maybe I'll make a wheel for throwables and one for weapons and then the flashlight and medkit will be viewable from the main game screen.

Picked up items will need to declare their `ItemType` and quantity. Some items, like weapons, provide a new weapon AND ammo. So the inventory system will need to iterate through all of the ItemTypes found in a single pick up. 

The inventory system will also provide methods for viewing how much of an item you have. That way if you're full on ammo you don't pick it up. Or maybe instead of the inventory system knowing how much of each item you can hold it should instead hold a collection of `InventoryItem` classes that declare a type, current quantity, and max quantity. The `InventoryItem` also handles adding and removing items. Should the `InventoryItem` return an actual item class or just a value? You should know what you're asking for so a number should suffice.

```c#
public interface IInventoryItem
{
    public ItemType Item {get;}
    public int Quantity {get;}
    public int GetCount();
    public void AddItem(IItemProviderRequest provider);
    public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer);
    public bool CanAdd();
    public bool CanConsume();
}
```

```c#
public interface IInventorySystem
{
    public Dictionary<ItemType,InventoryItem> Items {get;}
    public void AddItem(IItemProviderRequest provider);
    public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer);
}
```

```c#
public interface IInventory
{
    public IInventorySystem InventorySystem { get; }
}
```

Next I think I'll need item consumer and item provider classes. Consumers remove things from the inventory, like when you reload the shotgun. Providers add things to the inventory, like when you pick up shells.

However when it comes to consuming items, I want to be able to consume one at a time. For things like shell reload and rocket reload. So I don't think I need a quantity on the consumer. Just sending it returns the item/value. Oh! Oh! These could be expanded to ItemConsumerRequests and ItemProviderRequests! Then I can make structs for consuming and providing all of the items I'll need.

```c#
public interface IItemConsumerRequest
{
    public bool Requested {get;}
    public Type ConsumerType {get;}
    public ItemType Item {get;}
}

public interface IItemProviderRequest
{
    public bool Requested {get;}
    public Type ProviderType {get;}
    public ItemType Item {get;}
    public int Quantity {get;}
}

public struct ShellRequest : IItemConsumerRequest
{
    public Type ConsumerType => typeof(ShellRequest);
    public ItemType Item => ItemType.Shell;
    public bool Requested {get; private set; }

    public ShellRequest(bool requested = true) Requested => requested;
}

public struct ShellProvider : IItemProviderRequest
{
    public Type ProviderType => typeof(ShellProvider);
    public ItemType Item => ItemType.Shell;
    public bool Requested {get; private set; }
    public int Quantity {get; private set;}

    public ShellProvider(int quantity, bool requested = true)
    {
        Quantity = quantity;
        Requested => requested;
    }
}
```

So now the question is, do I need a class for each individual item? I don't *think* so... Because when you consume an item it returns a provider request *for* whatever you consumed. So when you need to move things around, ie reloading, you can send consumer requests to your inventory which then returns provider requests that you store in the gun's inventory. 

Okay something I just realized is that I will need an interaction system to change weapons and equipped items. I'm not sure how I want it to look. Part of my wants to have a weapon slot and a utility slot not including the flashlight or med kits. But switching items could get awkward physically. The weapons could be 1-4 but the utility items would require the player to hold another button and then select which is weird.

I could break things up by weapon, utility, and throwable? Weapons are 1-4, utility could be f for flashlight toggle and z for medkit. Then how do I get the throwables? Maybe T could cycle throwables or throwables are just 1-7. Then T could be a rock short cut since I think you'll be using that a lot, then G could be a throw short cut.

So from there I could add a selector map that uses `ItemType` with an `int` in a dictionary to equip things. The input provider could send a `IEquipRequest` similar to an action request to equip an item from the inventory.

```c#
public interface IEquipRequest
{
    public ItemType Item { get; }
}

public interface IItemEquipper
{
    public ItemType CurrentItem { get; }
    public void EquipItem(IEquipable item);
}
```

Should I also designate a class for items that are equipable? 

```c#
public interface IEquipable
{
    public bool IsEquipable { get; }
}
```
This seems poorly thought out. It could just be a field on some items.

I have split inventory items between ammo and item.  

Items include:
- Weapons
- Throwables
- Utility items

Ammo includes:
- Shells
- Rockets
- Gas Canisters
- Flares
- Smoke Grenades
- Rocks

That is a lot of overlap.. Another thing, only some of the ammo types require you to have a weapon first.  
- Shells
- Rockets

Everything else can either be ammo or a weapon.. Okay so it seems like the Shotgun, Rocket Launcher, Shells, and Rockets are my special cases. They have to be either a weapon or ammo. Except that weapons can be both a weapon AND ammo. Ooof this is getting confusing. I'm going to implement rocks and see what happens.

 Everything else can be a plain old item..

So maybe my splits should be
```c#
public enum WeaponType
{
    Taser,
    Shotgun,
    RocketLauncher,
}

public enum AmmoType
{
    Shells,
    Missiles,
}

public enum ItemType
{
    NerveGas,
    Rocks,
    Flashlight,
    Medkit,
    Flares,
    SmokeGrenade
}
```

Okay I can add rocks to my inventory by picking them up. Now I need to update my hud when that happens. So I'm going to add an inventory presenter. The presenter will display health, flashlight battery, available medkits, currently equipped item, and how much of that item we have. To make this work I think I might need a UI bridge? I don't know yet.

```c#
public interface IInventoryPresenter
{
    public void UpdateEquipped();
    public void UpdateQuantity();
    public void UpdateHealth();
    public void UpdateMedkits();
    public void UpdateFlashlight();
}
```

So the presenter will then have a reference to the actor event bus which raises events for consuming, adding, and changing equipment. 

# 6-14-26
I am feeling some friction between `IInventoryItem` and `IEquipment`. There isn't a nice way to tie a an equipment class to it's inventory item. I considered having the inventory item return an instance of the equipment class but I'm not sure how I want to construct it. Currently inventory items are all the same. They just have different item types. This is nice and reusable. The problem is if I tell the inventory items to construct the equipment they represent it could wind up building multiple different instances of the equipment class OR each inventory item class winds up with a giant constructor just for its equipment.

I think what I need to do is pass the equipment class as a constructor argument. Then the inventory class will check if the `ItemType`s match. To configure the equipment with the inventory I could make a scriptable object holding all of the constructor details for each type of item/equipment. Then I could just feed everything into a factory method inside of `Inventory` that builds the InventoryItem and stores the equipment with it and puts the result in a list for the inventory system to handle. What would that look like? 

```c#
public class InventoryConfig : ScriptableObject
{
    [SerializeField] private List<InventoryConfigEntry> _entries;
    private Dictionary<ItemType,InventoryConfigEntry> _entryDict = new();

    private void OnEnable()
    {
        BuildDictionary();
    }

    public void AddNewEntry(InventoryConfigEntry entry)
    {
        _entries.Add(entry);
    }

    public Dictionary<ItemType,InventoryConfigEntry> GetDict()
    {
        if (_entryDict.Keys.Count == 0)
        {
            BuildDictionary();
        }
        return _entryDict;
    }

    private void BuildDictionary()
    {
        _entryDict = new();
        foreach (var entry in _entries)
        {
            if (!_entryDict.ContainsKey(entry.Key))
            {
                _entryDict[entry.Key] = entry.Clip;
            }
            else
            {
                Debug.LogError($"ItemType {entry.Key} is already paired with InventoryConfigEntry {entry} in inventory config dictionary.");
            }
        }
    }
}

// Entry class
[Serializable]
public class InventoryConfigEntry
{
public ItemType Key
public InventoryConfigSO ConfigData;
}

// Data class
public class InventoryConfigSO : ScriptableObject
{
    public int Capacity;
    public bool IsEquipable;
}
```

...I'm not sure this is working. I think what I need to do is make two config factories. One for equipment and one for inventory then in the inventory class it puts everything together.

TBH none of this *seems* correct. I'm gonna free-ball it with the rocks and see what happens.