using Microsoft.AspNetCore.Mvc;
using Library.Application.Dtos;
using Library.Application.Interfaces;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetLoans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

        // GET: api/loans/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetActiveLoans()
        {
            var loans = await _loanService.GetActiveLoansAsync();
            return Ok(loans);
        }

        // GET: api/loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetLoan(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);

            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        // POST: api/loans
        [HttpPost]
        public async Task<ActionResult<LoanDto>> PostLoan(CreateLoanDto createLoanDto)
        {
            try
            {
                var loan = await _loanService.CreateLoanAsync(createLoanDto);
                return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/loans/5/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnLoan(int id)
        {
            var loan = await _loanService.ReturnLoanAsync(id);

            if (loan == null)
                return NotFound();

            return NoContent();
        }
    }
}