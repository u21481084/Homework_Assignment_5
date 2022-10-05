using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class ServiceClass
    {

        public List<Students> FilterStudents(string studentname, string classname, int id )
        {
            List<Students> students= new List<Students>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[students].[studentId] , [dbo].[students].[name] ," +
                    "[dbo].[students].[surname] ," +
                    " [dbo].[students].[class] , [dbo].[students].[point] , CASE WHEN [dbo].[students].[studentId] IN " +
                    " (SELECT [dbo].[borrows].[StudentId] FROM [dbo].[borrows] where  [dbo].[borrows].[broughtDate] IS NULL AND" +
                     "[dbo].[borrows].[bookId]= '" + id + "')" +
                    "THEN 'Return Book' ELSE 'Borrow Book' END AS Status" +
                    " FROM [dbo].[students]" +
                    "WHERE [dbo].[students].[name] LIKE  + '%"+studentname+"%' AND " +
                    "      [dbo].[students].[class] LIKE '%"+classname+"%'"
                    , con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Students student = new Students
                            {
                                StudentID = Convert.ToInt32(read["StudentId"]),
                                StudentName = read["name"].ToString(),
                                StudentSurname = read["surname"].ToString(),
                                cl = read["class"].ToString(),
                                Points = read["point"].ToString(),
                                sStatus = read["Status"].ToString()
                            };
                            students.Add(student);
                        }
                    }
                    con.Close();
                }
                return students;
            }


        }
        public List<BookBorrows> getBookBorrows(int idBook)
        {
            List<BookBorrows> borrows = new List<BookBorrows>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[borrows].[borrowId],  [dbo].[borrows].[takenDate]," +
                    " CASE WHEN [dbo].[borrows].[broughtDate] IS NULL THEN 'OUT' ELSE " +
                    "CONVERT(varchar, [dbo].[borrows].[broughtDate]) END AS BroughtDate ," +
                    "( [dbo].[students].[name]+' '+ [dbo].[students].[surname]) AS Name FROM" +
                    " borrows INNER JOIN  [dbo].[students] on  [dbo].[borrows].[studentId]= [dbo].[students].[studentId]" +
                    " WHERE  [dbo].[borrows].[bookId] ='" + idBook+"'", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            BookBorrows bookborrow = new BookBorrows
                            {
                               bBorrowID= Convert.ToInt32(read["borrowId"]),
                               bTakenDate= (read["takenDate"]).ToString(),
                               bBroughtDate= (read["BroughtDate"]).ToString(),
                               bStudentName=read["Name"].ToString()
                            };
                            borrows.Add(bookborrow);
                        }
                    }
                    con.Close();
                }
                return borrows;
            }
        }

        public string getNameStatus(int id)
        {
            Books book = new Books();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select [dbo].[books].[bookId] , [dbo].[books].[name] ," +
                     "CASE WHEN [dbo].[books].[bookId] IN (SELECT [dbo].[borrows].[bookId] FROM " +
                    " [dbo].[borrows] where  [dbo].[borrows].broughtDate IS NULL)" +
                    " THEN 'Out' ELSE 'Available' END AS Status" +
                    "  FROM [dbo].[books]" +
                    "WHERE [dbo].[books].[bookId]= '"+id+"'", con))
                {

                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {

                            book.BookID = Convert.ToInt32(read["bookId"]);
                                 book.BookName = read["name"].ToString();

                            book.Status = read["Status"].ToString();
                           



                        }
                    }
                    con.Close();
                }
                return (book.BookName+" -"+book.Status);
            }

        }
        public List<Books> Filter(string name, string ty, string auths)
        {
            List<Books> books = new List<Books>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select [dbo].[books].[bookId] , [dbo].[books].[name] ," +
                    " [dbo].[books].[pagecount], [dbo].[books].[point]," +
                    "[dbo].[authors].[name] AS AuthorName , [dbo].[types].[name] AS TypeName," +
                     "CASE WHEN [dbo].[books].[bookId] IN (SELECT [dbo].[borrows].[bookId] FROM " +
                    " [dbo].[borrows] where  [dbo].[borrows].broughtDate IS NULL)" +
                    " THEN 'Out' ELSE 'Available' END AS Status" +
                    "  FROM [dbo].[books] INNER JOIN [dbo].[authors] ON [dbo].[books].[authorId] =[dbo].[authors].[authorId] " +
                    " INNER JOIN [dbo].[types] ON [dbo].[books].[typeId]=[dbo].[types].[typeId] "+
                    " WHERE [dbo].[books].[name] LIKE "+"'%"+name+"%' AND " +
                    "[dbo].[types].[name] LIKE " + "'%" + ty + "%' AND " +
                    "[dbo].[authors].[name] LIKE " + "'%" + auths + "%'"+
                   "Order By  [dbo].[books].[bookId] ", con))
                {
                  
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Books book = new Books
                            {
                                BookID = Convert.ToInt32(read["bookId"]),
                                BookName = read["name"].ToString(),
                                Author = read["AuthorName"].ToString(),
                                Type = read["TypeName"].ToString(),
                                PageCount = Convert.ToInt32(read["pagecount"]),
                                Points = Convert.ToInt32(read["point"]),
                                Status= read["Status"].ToString()
                            };
                            books.Add(book);



                        }
                    }
                    con.Close();
                }
                return books;
            }
        }
        public List<Types> getAllTypes()
        {
            List<Types> types = new List<Types>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[types] ", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Types type = new Types
                            {
                                TypeID = Convert.ToInt32(read["typeId"]),
                                TypeName = read["name"].ToString(),
                               

                            };
                            types.Add(type);



                        }
                    }
                    con.Close();
                }
                return types;
            }
        }
        public List<Authors> getAllAuthors()
        {
            List<Authors> authors = new List<Authors>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select * FROM [dbo].[authors] ", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Authors author = new Authors
                            {
                                AuthorID = Convert.ToInt32(read["authorId"]),
                                AuthorName = read["name"].ToString(),
                                AuthorSurname= read["surname"].ToString(),


                            };
                            authors.Add(author);



                        }
                    }
                    con.Close();
                }
                return authors;
            }
        }

        public List<Class> getClasses()
        {
            List<Class> classes = new List<Class>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT [dbo].[students].[class]" +
                    "FROM [dbo].[students]", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Class cls = new Class
                            {
                                ClassName = read["class"].ToString()
                            };
                            classes.Add(cls);
                        }
                    }
                    con.Close();
                }
                return classes;
            }
        }
        public void BorrowBook(int StudentId, int BookId)
        {
            string varDate = (DateTime.Now).ToString();
            SqlConnection con = new SqlConnection(Global.ConnectionString);
           
            SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT [dbo].[borrows] ON " +
                "INSERT INTO borrows(borrowId,studentId,bookId,takenDate,broughtDate) " +
                "VALUES ((SELECT MAX(borrows.borrowId)+1 FROM borrows),"+StudentId+","+BookId+", CONVERT(datetime,+'"+varDate+"')"+
                ",null)",con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void ReturnBook(int studID, int bookid)
        {
            string varDate = (DateTime.Now).ToString();
            SqlConnection con = new SqlConnection(Global.ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE [dbo].[borrows] " +
                "SET [dbo].[borrows].[broughtDate] = CONVERT(datetime,'" + varDate + "')" +
                " WHERE [dbo].[borrows].[studentId] = '"+studID+ "'"+" AND [dbo].[borrows].[bookId] = '" + bookid + "'" +"AND " +
                " [dbo].[borrows].[broughtDate] IS NULL ", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<Students> getAllStudents(int id)
        {
            List<Students> students = new List<Students>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[students].[studentId] ,[dbo].[students].[name] ," +
                    "[dbo].[students].[surname] ," +
                    " [dbo].[students].[class] , [dbo].[students].[point] , CASE WHEN [dbo].[students].[studentId] IN " +
                    " (SELECT [dbo].[borrows].[StudentId] FROM [dbo].[borrows] where  [dbo].[borrows].[broughtDate] IS NULL AND" +
                    "[dbo].[borrows].[bookId]= '"+id+"')" +
                    " THEN 'Return Book' ELSE 'Borrow Book' END AS Status" +
                    " FROM [dbo].[students]", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Students student = new Students
                            {
                                StudentID = Convert.ToInt32(read["StudentId"]),
                                StudentName = read["name"].ToString(),
                                StudentSurname= read["surname"].ToString(),
                                cl=read["class"].ToString(),
                                Points=read["point"].ToString(),
                                sStatus=read["Status"].ToString()
                            };
                            students.Add(student);
                        }
                    }
                    con.Close();
                }
                return students;
            }
        }

        public List<Books> getAllBooks()
        {
            List<Books> books = new List<Books>();
            using (SqlConnection con = new SqlConnection(Global.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select [dbo].[books].[bookId] , [dbo].[books].[name] ," +
                    " [dbo].[books].[pagecount], [dbo].[books].[point]," +
                    "[dbo].[authors].[name] AS AuthorName , [dbo].[types].[name] AS TypeName, " +
                    "CASE WHEN [dbo].[books].[bookId] IN (SELECT [dbo].[borrows].[bookId] FROM " +
                    " [dbo].[borrows] where  [dbo].[borrows].broughtDate IS NULL)" +
                    " THEN 'Out' ELSE 'Available' END AS Status" +
                    " FROM [dbo].[books] INNER JOIN [dbo].[authors] ON [dbo].[books].[authorId] =[dbo].[authors].[authorId] " +
                    " INNER JOIN [dbo].[types] ON [dbo].[books].[typeId]=[dbo].[types].[typeId]" +
                    "Order By  [dbo].[books].[bookId] ", con))
                {
                   using(SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read()){
                            Books book = new Books
                            {
                                BookID = Convert.ToInt32(read["bookId"]),
                                BookName = read["name"].ToString(),
                                Author = read["AuthorName"].ToString(),
                                Type = read["TypeName"].ToString(),
                                PageCount = Convert.ToInt32(read["pagecount"]),
                                Points = Convert.ToInt32(read["point"]),
                                Status = read["Status"].ToString()
                              
                                
                                

                            };
                            books.Add(book);



                        }
                    }
                    con.Close();
                }
                return books;
            }

        }
    }
}