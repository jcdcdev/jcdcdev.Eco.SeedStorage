using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;

namespace jcdcdev.Eco.SeedStorage.WoodenSeedBox;

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