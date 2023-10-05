using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;

namespace jcdcdev.Eco.SeedStorage.WoodenSeedBox;

[Serialized]
[LocDisplayName("Wooden Seed Box")]
[Ecopedia("Crafted Objects", "Storage", true)]
public class WoodenSeedBoxItem : WorldObjectItem<WoodenSeedBoxObject>
{
    public override LocString DisplayDescription => Localizer.DoStr(
        $"Basic storage for seeds! The Wooden Seed Box can store all seed types and increases shelf-life by {SeedStoragePlugin.Config.WoodenSeedBoxShelfLifeMultiplier}x");

    public override DirectionAxisFlags RequiresSurfaceOnSides => 0 | DirectionAxisFlags.Down;
}