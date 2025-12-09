using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();

            // Loan mappings
            CreateMap<Loan, LoanDto>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<CreateLoanDto, Loan>();
        }
    }
}
