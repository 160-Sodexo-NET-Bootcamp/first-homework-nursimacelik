using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        public List<Book> books;

        public BookController()
        {
            books = new List<Book>();

            books.Add(new Book { Id = 1, KitapSeriNo = 111, KitapAdi = "Automate the Boring Stuff with Python", Yazar = "Al Sweigart", BaskiTarihi = new DateTime(2015, 04, 14) });
            books.Add(new Book { Id = 2, KitapSeriNo = 112, KitapAdi = "The Hitchhiker's Guide to the Galaxy", Yazar = "Douglas Adams", BaskiTarihi = new DateTime(1979, 10, 12) });
            books.Add(new Book { Id = 3, KitapSeriNo = 113, KitapAdi = "Just For Fun", Yazar = "Linus Torvalds", BaskiTarihi = new DateTime(2001, 01, 01) });
            books.Add(new Book { Id = 4, KitapSeriNo = 114, KitapAdi = "The Universal Turing Machine", Yazar = "Rolf Herken", BaskiTarihi = new DateTime(1992, 01, 02) });
            books.Add(new Book { Id = 5, KitapSeriNo = 115, KitapAdi = "Dirilis", Yazar = "Lev Tolstoy", BaskiTarihi = new DateTime(1899, 01, 01) });
            books.Add(new Book { Id = 6, KitapSeriNo = 116, KitapAdi = "To Kill A MockingBird", Yazar = "Harper Lee", BaskiTarihi = new DateTime(1960, 07, 11) });
        }

        [HttpPost("all")]
        public List<Book> GetAll()
        {
            return books;
        }

        [HttpGet]
        public IActionResult GetFromQuery([FromQuery] int id)
        {
            if (id == 0)
            {
                return Unauthorized();
            }
            var book = books.Where(x => x.Id == id).FirstOrDefault();
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("{id}")]
        public IActionResult GetFromRoute([FromRoute] int id)
        {
            if (id == 0)
            {
                return Unauthorized();
            }
            var book = books.Where(x => x.Id == id).FirstOrDefault();
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if(book == null)
            {
                return BadRequest();
            }
            books.Add(book);
            return Ok(books);

        }

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if(book == null)
            {
                return BadRequest();
            }
            
            var temp = books.Where(x => x.Id == book.Id).FirstOrDefault();
            if(temp == null)
            {
                return NotFound();
            }

            temp.KitapSeriNo = book.KitapSeriNo;
            temp.KitapAdi = book.KitapAdi;
            temp.Yazar = book.Yazar;
            temp.BaskiTarihi = book.BaskiTarihi;

            return Ok(books);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var temp = books.Where((x) => x.Id == id).FirstOrDefault();

            if (temp is null)
            {
                return NotFound();
            }

            books.Remove(temp);
            return Ok(books);
        }
    }
}