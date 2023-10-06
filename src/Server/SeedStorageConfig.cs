using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace jcdcdev.Eco.SeedStorage;

public class SeedStorageConfig
{
    [Description("The multiplier used to determine Wooden Seed Box shelf-life. Default = 2.0")]
    [Category("Performance")]
    [Range(1, int.MaxValue)]
    [DefaultValue(2.0f)]
    public float WoodenSeedBoxShelfLifeMultiplier { get; set; } = 2.0f;

    [Description("The multiplier used to determine Seed Bank shelf-life. Default = 4.0")]
    [Category("Performance")]
    [Range(1, int.MaxValue)]
    [DefaultValue(4.0f)]
    public float SeedBankShelfLifeMultiplier { get; set; } = 4.0f;
}