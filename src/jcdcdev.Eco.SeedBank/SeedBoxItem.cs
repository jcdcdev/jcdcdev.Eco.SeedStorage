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

namespace jcdcdev.Eco.SeedBank;

[Serialized]
[RequireComponent(typeof(PropertyAuthComponent))]
[RequireComponent(typeof(LinkComponent))]
[RequireComponent(typeof(PublicStorageComponent))]
[RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Seedbox Item")]
public class SeedboxObject : WorldObject, IRepresentsItem
{
    protected override void Initialize()
    {
        var storage = GetComponent<PublicStorageComponent>();
        storage.Initialize(16);
        storage.Storage.AddInvRestriction(new StackLimitRestriction(200));
        storage.Storage.AddInvRestriction(new SeedRestriction());
        storage.ShelfLifeMultiplier = 2.0f;
    }

    public override TableTextureMode TableTexture => TableTextureMode.Wood;

    public Type RepresentedItemType => typeof(SeedboxItem);
}

[Serialized]
[LocDisplayName("Seedbox")]
[Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
public class SeedboxItem : WorldObjectItem<SeedboxObject>
{
    public override LocString DisplayDescription => Localizer.DoStr("A storage box for seeds!");

    public override DirectionAxisFlags RequiresSurfaceOnSides => 0 | DirectionAxisFlags.Down;
}

[Ecopedia("Crafted Objects", "Storage", subPageName: "Seedbox Item")]
public class SeedboxRecipe : RecipeFamily
{
    public SeedboxRecipe()
    {
        var recipe = new Recipe();
        recipe.Init(
            name: "Seedbox",
            displayName: Localizer.DoStr("Seedbox"),
            ingredients: new List<IngredientElement>
            {
                new("HewnLog", 10, typeof(CarpentrySkill),
                    typeof(CarpentryLavishResourcesTalent)),
                new("WoodBoard", 12, typeof(CarpentrySkill),
                    typeof(CarpentryLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<SeedboxItem>()
            });
        Recipes = new List<Recipe> { recipe };
        ExperienceOnCraft = 3;
        LaborInCalories = CreateLaborInCaloriesValue(60, typeof(CarpentrySkill));
        CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(SeedboxRecipe), start: 2,
            skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent),
            typeof(CarpentryParallelSpeedTalent));
        Initialize(displayText: Localizer.DoStr("Seedbox"), recipeType: typeof(SeedboxRecipe));
        CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
    }
}