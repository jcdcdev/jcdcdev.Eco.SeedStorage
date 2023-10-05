using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;

namespace jcdcdev.Eco.SeedStorage.SeedBank;

[Serialized]
[LocDisplayName("Seed Bank")]
[Ecopedia("Crafted Objects", "Storage", true)]
public class SeedBankItem : WorldObjectItem<SeedBankObject>
{
    public override LocString DisplayDescription =>
        Localizer.DoStr(
            $"The ultimate storage for seeds! The Seed Bank can store all seed types and increases shelf-life by {SeedStoragePlugin.Config.SeedBankShelfLifeMultiplier}x");

    public override DirectionAxisFlags RequiresSurfaceOnSides => 0 | DirectionAxisFlags.Down;
}