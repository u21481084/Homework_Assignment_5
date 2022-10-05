using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student(int id)
        {
            ServiceClass svClass = new ServiceClass();
            StudentClassVM scV = new StudentClassVM();
            scV.stds = svClass.getAllStudents(id);
            scV.bID = id;
           
            scV.cClass = svClass.getClasses();
            return View(scV);
        }
        [HttpPost]
        public ActionResult Student(string studentname, string classname, int id)
        {
            ServiceClass svClass = new ServiceClass();
            StudentClassVM scV = new StudentClassVM();
            scV.stds = svClass.FilterStudents(studentname,classname, id);
            scV.bID = id;
            scV.cClass = svClass.getClasses();
            return View(scV);
        }
        public ActionResult Borrow(int sId, int bId)
        {
            ServiceClass svClass = new ServiceClass();
            svClass.BorrowBook(sId, bId);
            return RedirectToAction("Books");
        }
        public ActionResult Return(int sId, int bId)
        {
            ServiceClass svClass = new ServiceClass();
            svClass.ReturnBook(sId, bId);
            return RedirectToAction("Books");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Books()
        {

            ServiceClass svClass = new ServiceClass();
            BookTypeAuthor bta = new BookTypeAuthor();
            bta.books = svClass.getAllBooks();
            bta.types = svClass.getAllTypes();
            bta.auth = svClass.getAllAuthors();

            return View(bta);
        }
        [HttpPost]
        public ActionResult Books(string bookname, string typename, string authorname)
        {
            string x = bookname;
            string y = typename;
            string z = authorname;
            ServiceClass svClass = new ServiceClass();
            BookTypeAuthor bta = new BookTypeAuthor();
            bta.books = svClass.Filter(x,y,z);
            bta.types = svClass.getAllTypes();
            bta.auth = svClass.getAllAuthors();
            return View(bta);
        }

        public ActionResult BookUsers(int id)
        {
            ServiceClass svClass = new ServiceClass();
            List<BookBorrows> bBorrows = new List<BookBorrows>();
            bBorrows = svClass.getBookBorrows(id);
            BookBorrowsVM z = new BookBorrowsVM();
            z.bb = bBorrows;
            z.bbNameStatus = svClass.getNameStatus(id);
            z.ID = id;
            
            return View(z);
            
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}