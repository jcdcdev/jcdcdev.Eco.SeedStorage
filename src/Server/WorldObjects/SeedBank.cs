using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Serialization;
using System;
using System.Runtime;
using Eco.Shared.Math;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Mods.TechTree;
using Eco.Gameplay.Skills;
using System.Collections.Generic;
using Eco.Gameplay.Components.Storage;
using Eco.Gameplay.Items.Recipes;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Seed Bank")]
    public partial class SeedBankObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Seed Bank");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        public virtual Type RepresentedItemType => typeof(SeedBankItem);

        protected override void Initialize()
        {
            ModsPreInitialize();
            var storage = GetComponent<PublicStorageComponent>();
            storage.Initialize(56);
            storage.Storage.AddInvRestriction(new StackLimitRestriction(1000));
            storage.Storage.AddInvRestriction(new SeedRestriction());
            storage.ShelfLifeMultiplier = 4.0f;
            ModsPostInitialize();
        }

        partial void ModsPostInitialize();
        partial void ModsPreInitialize();
        
        public override LocString DisplayDescription =>
            Localizer.DoStr(
                $"The ultimate storage for seeds! The Seed Bank can store all seed types and greatly increases shelf-life");
    }

    [Serialized]
    [LocDisplayName("Seed Bank")]
    [Ecopedia("Crafted Objects", "Storage", true)]
    public class SeedBankItem : WorldObjectItem<SeedBankObject> { }

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
}
