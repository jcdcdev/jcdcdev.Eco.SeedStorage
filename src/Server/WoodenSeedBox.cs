using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;

namespace jcdcdev.Eco.SeedStorage;

[Serialized]
[RequireComponent(typeof(PropertyAuthComponent))]
[RequireComponent(typeof(LinkComponent))]
[RequireComponent(typeof(PublicStorageComponent))]
[RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Wooden Seed Box")]
public class WoodenSeedBoxObject : WorldObject, IRepresentsItem
{
    protected override void Initialize()
    {
        var storage = GetComponent<PublicStorageComponent>();
        storage.Initialize(16);
        storage.Storage.AddInvRestriction(new StackLimitRestriction(100));
        storage.Storage.AddInvRestriction(new SeedRestriction());
        storage.ShelfLifeMultiplier = 2.0f;
    }

    public override TableTextureMode TableTexture => TableTextureMode.Wood;

    public Type RepresentedItemType => typeof(WoodenSeedBoxItem);
}

[Serialized]
[LocDisplayName("Wooden Seed Box")]
[Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
public class WoodenSeedBoxItem : WorldObjectItem<WoodenSeedBoxObject>
{
    public override LocString DisplayDescription => Localizer.DoStr("A storage box for seeds!");

    public override DirectionAxisFlags RequiresSurfaceOnSides => 0 | DirectionAxisFlags.Down;
}

[RequiresSkill(typeof(FarmingSkill), 3)]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Wooden Seed Box")]
public class WoodenSeedBoxRecipe : RecipeFamily
{
    public WoodenSeedBoxRecipe()
    {
        var recipe = new Recipe();
        recipe.Init(
            name: "WoodenSeedBox",
            displayName: Localizer.DoStr("Wooden Seed Box"),
            ingredients: new List<IngredientElement>
            {
                new("HewnLog", 10, typeof(FarmingSkill),
                    typeof(FarmingLavishResourcesTalent)),
                new("WoodBoard", 12, typeof(FarmingSkill),
                    typeof(FarmingLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<WoodenSeedBoxItem>()
            });
        Recipes = new List<Recipe> { recipe };
        ExperienceOnCraft = 3;
        LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));
        CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(WoodenSeedBoxRecipe), start: 2,
            skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent),
            typeof(FarmingParallelSpeedTalent));
        Initialize(displayText: Localizer.DoStr("Wooden Seed Box"), recipeType: typeof(WoodenSeedBoxRecipe));
        CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
    }
}