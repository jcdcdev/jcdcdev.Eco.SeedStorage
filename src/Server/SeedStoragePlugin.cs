using Eco.Shared.Localization;
using jcdcdev.Eco.Core;

namespace jcdcdev.Eco.SeedStorage;

public class SeedStoragePlugin : PluginBase<SeedStorageConfig>
{
    protected override void BuildStatusText(LocStringBuilder sb)
    {
        sb.AppendLine(new LocString($"Seed Bank Shelf Life Multiplier: {Config.SeedBankShelfLifeMultiplier}"));
        sb.AppendLine(new LocString($"Wooden Seed Box Shelf Life Multiplier: {Config.WoodenSeedBoxShelfLifeMultiplier}"));
    }
}