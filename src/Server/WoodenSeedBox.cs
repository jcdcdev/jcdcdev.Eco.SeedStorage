using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
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

[Ecopedia("Crafted Objects", "Storage", subPageName: "Wooden Seed Box")]
public class WoodenSeedBoxRecipe : RecipeFamily
{
    public WoodenSeedBoxRecipe()
    {
        var recipe = new Recipe();
        recipe.Init(
            name: "Wooden Seed Box",
            displayName: Localizer.DoStr("Wooden Seed Box"),
            ingredients: new List<IngredientElement>
            {
                new("HewnLog", 10, typeof(CarpentrySkill),
                    typeof(CarpentryLavishResourcesTalent)),
                new("WoodBoard", 12, typeof(CarpentrySkill),
                    typeof(CarpentryLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<WoodenSeedBoxItem>()
            });
        Recipes = new List<Recipe> { recipe };
        ExperienceOnCraft = 3;
        LaborInCalories = CreateLaborInCaloriesValue(60, typeof(CarpentrySkill));
        CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(WoodenSeedBoxRecipe), start: 2,
            skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent),
            typeof(CarpentryParallelSpeedTalent));
        Initialize(displayText: Localizer.DoStr("Wooden Seed Box"), recipeType: typeof(WoodenSeedBoxRecipe));
        CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
    }
}