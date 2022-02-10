namespace GestionBibliothequeAPI
{
    public static class Extensions
    {
        public static BookDto AsBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                PublishDate = book.PublishDate,
                PublishingHouse = book.PublishingHouse,
                Quantity = book.Quantity,
                Status = book.Status,
                Title = book.Title
            };
        }
    }
}
