namespace MovieRental.Application.Dtos.Review
{
    public class CreateReviewDto
    {
        public string Comment { get; set; } = string.Empty;
        public float Rating { get; set; }
    }
}
