using Eco.Core;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace jcdcdev.Eco.SeedStorage.SeedBank;

[Serialized]
[RequireComponent(typeof(PropertyAuthComponent))]
[RequireComponent(typeof(LinkComponent))]
[RequireComponent(typeof(PublicStorageComponent))]
[RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Seed Bank")]
public class SeedBankObject : WorldObject, IRepresentsItem
{
    public override LocString DisplayName => Localizer.DoStr("Seed Bank");
    public override TableTextureMode TableTexture => TableTextureMode.Wood;
    public virtual Type RepresentedItemType => typeof(SeedBankItem);

    protected override void Initialize() => PluginManager.Controller.RunIfOrWhenInited(InitializeStorage);

    private void InitializeStorage()
    {
        var plugin = PluginManager.GetPlugin<SeedStoragePlugin>();
        var storage = GetComponent<PublicStorageComponent>();
        storage.Initialize(56);
        storage.Storage.AddInvRestriction(new StackLimitRestriction(1000));
        storage.Storage.AddInvRestriction(new SeedRestriction());
        storage.ShelfLifeMultiplier = plugin.Config.SeedBankShelfLifeMultiplier;
    }
}