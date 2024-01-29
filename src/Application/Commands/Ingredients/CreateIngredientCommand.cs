using System.Diagnostics.CodeAnalysis;

namespace Application.Commands.Ingredients
{
    [ExcludeFromCodeCoverage]
    public class CreateIngredientCommand : Command
    {
        /// <summary>
        /// Nome do Ingredient.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description do Ingredient.
        /// </summary>
        public string Description { get; set; } = string.Empty;


    }
}
