using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Ports.Out;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            // Validar ISBN único
            var existingBook = await _bookRepository.GetByISBNAsync(createBookDto.ISBN);
            if (existingBook != null)
                throw new InvalidOperationException($"El ISBN '{createBookDto.ISBN}' ya está registrado.");

            var book = _mapper.Map<Book>(createBookDto);
            var createdBook = await _bookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(createdBook);
        }

        public async Task<BookDto?> UpdateBookAsync(int id, CreateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            // Validar ISBN único (excluyendo el libro actual)
            if (!await _bookRepository.IsISBNUniqueAsync(updateBookDto.ISBN, id))
                throw new InvalidOperationException($"El ISBN '{updateBookDto.ISBN}' ya está registrado en otro libro.");

            book.Title = updateBookDto.Title;
            book.Author = updateBookDto.Author;
            book.ISBN = updateBookDto.ISBN;
            book.Stock = updateBookDto.Stock;

            await _bookRepository.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            await _bookRepository.DeleteAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}