using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "Título deve ter no máximo 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição inválida")]
    public string Description { get; set; } = string.Empty;
}
