using BookStoreAPI.Models;
using BookStoreAPI.Repository;

namespace BookStoreAPI.UnitOfWork
{
    public class UnitOFWork
    {
        BookStoreContext db;
      GenericRepository<Book> booksRepository;
      GenericRepository<Order> orderRepository;
        GenericRepository<OrderDetails> orderDEtailsRepository;
     //   GenericRepository<Department> departmentRepository;

        public UnitOFWork(BookStoreContext db)
        {
            this.db = db;
     
        }

        public GenericRepository<Book> BooksRepository
        {
            get
            {
                if (booksRepository == null)
                {
                    booksRepository = new GenericRepository<Book>(db);

                }
                return booksRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new GenericRepository<Order>(db);

                }
                return orderRepository;
            }
        }
        public GenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {
                if (orderDEtailsRepository == null)
                {
                    orderDEtailsRepository = new GenericRepository<OrderDetails>(db);

                }
                return orderDEtailsRepository;
            }
        }

        //public GenericRepository<Department> DepartmentRepository
        //{
        //    get
        //    {
        //        if (departmentRepository == null)
        //        {
        //            departmentRepository = new GenericRepository<Department>(db);
        //        }
        //        return departmentRepository;
        //    }
        //}
        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
