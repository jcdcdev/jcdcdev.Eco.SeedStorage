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
[Ecopedia("Crafted Objects", "Storage", subPageName: "Seed Bank")]
public class SeedBankObject :  WorldObject, IRepresentsItem
{
    public virtual Type RepresentedItemType => typeof(SeedBankItem);
    public override LocString DisplayName => Localizer.DoStr("Seed Bank");
    public override TableTextureMode TableTexture => TableTextureMode.Wood;

    protected override void Initialize()
    {
        var storage = GetComponent<PublicStorageComponent>();
        storage.Initialize(56);
        storage.Storage.AddInvRestriction(new StackLimitRestriction(1000));
        storage.Storage.AddInvRestriction(new SeedRestriction());
        storage.ShelfLifeMultiplier = 4.0f;
    }
}

[Serialized]
[LocDisplayName("Seed Bank")]
[Ecopedia("Crafted Objects", "Storage", createAsSubPage: true)]
public class SeedBankItem : WorldObjectItem<SeedBankObject>
{
    public override LocString DisplayDescription => Localizer.DoStr("The ultimate storage for seeds!");
    public override DirectionAxisFlags RequiresSurfaceOnSides => 0 | DirectionAxisFlags.Down;
}

[RequiresSkill(typeof(CarpentrySkill), 3)]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Seed Bank")]
public class SeedBankRecipe : RecipeFamily
{
    public SeedBankRecipe()
    {
        var recipe = new Recipe();
        recipe.Init(
            name: "SeedBank",
            displayName: Localizer.DoStr("Seed Bank"),
            ingredients: new List<IngredientElement>
            {
                new(typeof(IronPipeItem), 6, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                new(typeof(IronBarItem), 8, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                new("Lumber", 20, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
            },
            items: new List<CraftingElement>
            {
                new CraftingElement<SeedBankItem>()
            });

        Recipes = new List<Recipe> { recipe };
        ExperienceOnCraft = 3;
        LaborInCalories = CreateLaborInCaloriesValue(600, typeof(CarpentrySkill));
        CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(SeedBankRecipe), start: 10,
            skillType: typeof(CarpentrySkill), typeof(CarpentryFocusedSpeedTalent),
            typeof(CarpentryParallelSpeedTalent));
            
        Initialize(displayText: Localizer.DoStr("Seed Bank"), recipeType: typeof(SeedBankRecipe));
        CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipe: this);
    }
}