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
            "WoodenSeedBox",
            Localizer.DoStr("Wooden Seed Box"),
            new List<IngredientElement>
            {
                new("HewnLog", 10, typeof(FarmingSkill),
                    typeof(FarmingLavishResourcesTalent)),
                new("WoodBoard", 12, typeof(FarmingSkill),
                    typeof(FarmingLavishResourcesTalent))
            },
            new List<CraftingElement>
            {
                new CraftingElement<WoodenSeedBoxItem>()
            });
        Recipes = new List<Recipe> { recipe };
        ExperienceOnCraft = 3;
        LaborInCalories = CreateLaborInCaloriesValue(60, typeof(FarmingSkill));
        CraftMinutes = CreateCraftTimeValue(typeof(WoodenSeedBoxRecipe), 2,
            typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent),
            typeof(FarmingParallelSpeedTalent));
        Initialize(Localizer.DoStr("Wooden Seed Box"), typeof(WoodenSeedBoxRecipe));
        CraftingComponent.AddRecipe(typeof(ToolBenchObject), this);
    }
}