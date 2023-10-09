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
namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    [Ecopedia("Crafted Objects", "Storage", subPageName: "Wooden Seed Box")]
    public partial class WoodenSeedBoxObject : WorldObject, IRepresentsItem
    {
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        public Type RepresentedItemType => typeof(WoodenSeedBoxItem);

        protected override void Initialize()
        {
            ModsPreInitialize();
            var storage = GetComponent<PublicStorageComponent>();
            storage.Initialize(16);
            storage.Storage.AddInvRestriction(new StackLimitRestriction(100));
            storage.Storage.AddInvRestriction(new SeedRestriction());
            storage.ShelfLifeMultiplier = 1.5f;
            ModsPostInitialize();
        }

        partial void ModsPostInitialize();
        partial void ModsPreInitialize();
    }

    [Serialized]
    [LocDisplayName("Wooden Seed Box")]
    [Ecopedia("Crafted Objects", "Storage", true)]
    public class WoodenSeedBoxItem : WorldObjectItem<WoodenSeedBoxObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr(
            $"Basic storage for seeds! The Wooden Seed Box can store all seed types and slightly increases shelf-life");

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
}