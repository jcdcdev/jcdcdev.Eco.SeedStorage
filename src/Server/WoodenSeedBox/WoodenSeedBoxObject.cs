using Eco.Core;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Serialization;

namespace jcdcdev.Eco.SeedStorage.WoodenSeedBox;

[Serialized]
[RequireComponent(typeof(PropertyAuthComponent))]
[RequireComponent(typeof(LinkComponent))]
[RequireComponent(typeof(PublicStorageComponent))]
[RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Wooden Seed Box")]
public class WoodenSeedBoxObject : WorldObject, IRepresentsItem
{
    public override TableTextureMode TableTexture => TableTextureMode.Wood;

    public Type RepresentedItemType => typeof(WoodenSeedBoxItem);

    protected override void Initialize() => PluginManager.Controller.RunIfOrWhenInited(InitializeStorage);

    private void InitializeStorage()
    {
        var plugin = PluginManager.GetPlugin<SeedStoragePlugin>();
        var storage = GetComponent<PublicStorageComponent>();
        storage.Initialize(16);
        storage.Storage.AddInvRestriction(new StackLimitRestriction(100));
        storage.Storage.AddInvRestriction(new SeedRestriction());
        storage.ShelfLifeMultiplier = plugin.Config.WoodenSeedBoxShelfLifeMultiplier;
    }
}