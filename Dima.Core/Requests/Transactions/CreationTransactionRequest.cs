using System.ComponentModel.DataAnnotations;
using Dima.Core.Enums;

namespace Dima.Core.Requests.Categories
{
    public class CreationTransactionRequest : Request
    {   
        [Required(ErrorMessage = "Título inválido.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo inválido.")]
        public ETransactionType Type { get; set; }

        [Required(ErrorMessage = "Valor inválido.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria inválida.")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválida.")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}