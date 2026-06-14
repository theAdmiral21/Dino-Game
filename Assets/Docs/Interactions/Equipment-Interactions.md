# Equipment Interactions
These are kind of weird. They flow through the same input action pipeline as every other action but they are almost completely governed by whatever you have equipped currently. So I need to remove these actions' results from the result sink before they go to the kinematic calculator. 

What I'm thinking about doing is just adding another interface to equipment based interactions.

```c#
public interface IEquipmentActionResult {}
```
...that's pretty much it. From there whatever you're trying to use will evaluate the result. That is where things get tricky because I have a taser, a shotgun, a rocket launcher, and throwables. Thankfully throwables will pretty much the same, but the guns... oh boy.

The problem is that they all behave very differently. The taser has infinite ammo and a one shot magazine. Kind of. The shotgun does not have infinite ammo but it has a 6 round magazine. Finally the rocket launch also doesn't have infinite ammo and a one shot magazine.

Hmm maybe this will work better than I thought. I could make a gun class and a throwable. The gun base class will define how to interact with the inventory and effects while the child classes will define when something can be done. 

Okay I've slept on this now. I think I might need an equipment action dispatcher. This would be the bridge between the input action pipeline and the equipment system. It would have very few actions that it could dispatch: raise, aim, fire, reload. That's not that bad. It would skim the results from the input action pipeline and grab any equipment action requests and evaluate them.

So I'll need:
- an equipment orchestrator
- an equipment dispatcher
- equipment actions?
- and something else I just forgot...

Hmm looking at my movement orchestrator and action dispatcher having a dedicated equipment orchestrator and dispatcher might actually be overkill. What I need is to separate equipment actions results and send that off to an equipment bridge? What would that look like?

Oh I will definitely need an equipment class that can be pulled from my inventory items. Also medkits and the flashlight will not be considered equipment. They will be their own thing.

```c#
public interface IEquipment
{
    // Equipment identification
    public ItemType EquipmentType { get; }

    // Equipment stats
    public EquipmentStats Stats { get; }

    // Equipment state information
    public IInventorySystem Inventory { get; }
    public int RoundCount { get; }

    // Equipment behavior
    public bool AutoReload { get; }
    public event Action OnInventoryEmpty;

    // Effect notification
    public event Action OnFire;
    public event Action OnReload;

    // Equipment orchestrators
    public void Aim();
    public void Reload();
    public void Fire();
}

// This is the seam between the action pipeline and your equipment
public interface IEquipmentBridge
{
    public IEquipment Equipped { get; }

    public void RouteEquipmentResult(IEquipmentActionResult result);
}
```

Do I need a third class for equipment orchestration? Something that connects my inventory and my equipment? What all would it do? 
- It needs to sync with the inventory system to know what is currently equipped.
- It would own the effect driver
- It would own the equipment bridge
- Anything else?  

```c#
public interface IEquipmentOrchestrator
{

}
```
TBH I don't think I need a dedicated orchestrator. Each equipment class is it's own orchestrator. Which might be a smell? 

I need to make sure that equipment state and character state stay synced. ie if the character state is aiming, then the equipment better be aiming. Where should that live? Should I pass a rule view here? Should equipment have it's own state? I'm also wondering if equipment should be at the unity level. I can easily see Aim, Reload, and Fire all being coroutines. It would also allow me to set up scriptable objects with equipment stats.

```c#
public class EquipmentSO : ScriptableObject
{
    public int MagazineSize;
    public float ReloadTime;
    public float FireRate;
    public int Damage;
    public float KnockBack;
    public float HitStun;
    public DamageType HurtType;

    public EquipmentStats BuildRunTime()
    {
        return new EquipmentStats(
                            MagazineSize,
                            ReloadTime,
                            FireRate,
                            Damage,
                            KnockBack,
                            HitStun,
                            HurtType
                        )
    }

}

public struct EquipmentStats
{
    public readonly int MagazineSize;
    public readonly float ReloadTime;
    public readonly float FireRate;
    public readonly int Damage;
    public readonly float KnockBack;
    public readonly float HitStun;
    public readonly DamageType HurtType;

public EquipmentStats(
                        int magSize,
                        float reloadTime,
                        float fireRate,
                        int damage,
                        float knockBack,
                        float hitStun,
                        DamageType damageType)
    {
        MagazineSize = magSize;
        ReloadTime = reloadTime;
        FireRate = fireRate;
        Damage = damage;
        KnockBack = knockBack;
        HitStun = hitStun;
        HurtType = damageType;
    }
}
```

OKay so I will have a bunch of different equipment classes. One for each of my types except for ammo. So how do I efficiently make them? A factory, right? So where do I put that factory? The unity level. I'm making unity classes. Cool. So do I make it a scriptable object? Uhh I don't know. I definitely could make it a scriptable object... I could add entries and have a factory class that spits out a dictionary like a I usually do. But each entry will be kind of heavy, won't it? It also needs a reference to the inventory event system.. So that means I can't make it a Scriptable object. I could put the equipment stats in a scriptable object for convenience, but these classes will have to be built at outside of a scriptable object. oH wait hold up. **It doesn't need the event bus. It needs the inventory system.** Still basically the same constraint. But I could make the factory a mono behaviour, stick it to my inventory game object, and let it rip on start up.

```c#
public class EquipmentFactory : MonoBehaviour
{
    [SerializeField] private EquipmentMap _mapSO;
    private Dictionary<ItemType,EquipmentStats> _statMap = new();

    [SerializeField] private SerializedInterface<IInventory> _inventorySO;
    private IInventorySystem _inventorySystem => _inventorySO.Interface.InventorySystem;

    public IEquipment BuildEquipment(ItemType item)
        {
            switch (item)
            {
                case ItemType.Rock:
                    {
                        var rock = new RockEquipment(item,_statMap[item],_inventorySystem);
                        return rock;
                    }
            }
        }
}
```