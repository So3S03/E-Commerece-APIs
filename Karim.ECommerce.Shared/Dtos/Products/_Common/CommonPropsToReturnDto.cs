using System.Text.Json.Serialization;

namespace Karim.ECommerce.Application.Abstraction.Dtos.Products._Common
{
    public class CommonPropsToReturnDto
    {
        public int Id { get; set; }
        public required string NormalizedName { get; set; }
        public string? MainImage { get; set; }
    }
}
