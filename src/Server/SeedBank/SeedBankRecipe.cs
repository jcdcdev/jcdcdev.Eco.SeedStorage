using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;

namespace jcdcdev.Eco.SeedStorage.SeedBank;

[RequiresSkill(typeof(FarmingSkill), 6)]
[Ecopedia("Crafted Objects", "Storage", subPageName: "Seed Bank")]
public class SeedBankRecipe : RecipeFamily
{
    public SeedBankRecipe()
    {
        var recipe = new Recipe();
        recipe.Init(
            "SeedBank",
            Localizer.DoStr("Seed Bank"),
            new List<IngredientElement>
            {
                new(typeof(IronPipeItem), 6, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new(typeof(IronBarItem), 8, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                new("Lumber", 20, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent))
            },
            new List<CraftingElement>
            {
                new CraftingElement<SeedBankItem>()
            });

        Recipes = new List<Recipe> { recipe };
        ExperienceOnCraft = 3;
        LaborInCalories = CreateLaborInCaloriesValue(600, typeof(FarmingSkill));
        CraftMinutes = CreateCraftTimeValue(typeof(SeedBankRecipe), 10,
            typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent),
            typeof(FarmingFocusedSpeedTalent));

        Initialize(Localizer.DoStr("Seed Bank"), typeof(SeedBankRecipe));
        CraftingComponent.AddRecipe(typeof(ToolBenchObject), this);
    }
}