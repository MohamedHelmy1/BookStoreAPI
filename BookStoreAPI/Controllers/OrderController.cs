using BookStoreAPI.DTOs.OrderDTO;
using BookStoreAPI.Models;
using BookStoreAPI.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        UnitOFWork _unit;
        public OrderController(UnitOFWork _unit)
        {
            this._unit = _unit;
        }
        [HttpPost]
        public IActionResult add(AddOrderDTO _order)
        {


            Order baicorderinfo = new Order()
            {
                cust_id = _order.cust_id,
                orderdate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                status = "create"

            };
            decimal totalprice = 0;
            foreach (var item in _order.books)
            {
                Book b = _unit.BooksRepository.selectbyid(item.book_id);
                totalprice = totalprice + (b.price * item.quentity);
                OrderDetails _details = new OrderDetails()
                {
                    order_id = baicorderinfo.id,
                    book_id = item.book_id,
                    quentity = item.quentity,
                    unitprice = b.price,
                };
                if (b.stock > _details.quentity)
                {
                    baicorderinfo.OrderDetails.Add(_details);

                    b.stock -= item.quentity;
                    _unit.BooksRepository.update(b);
                }
                else
                {
                    return BadRequest("invalid quantity");
                }

            }

            baicorderinfo.totalprice = totalprice;

            _unit.savechanges();


            return Ok();
        }
        //[HttpPost]
        //public IActionResult add(AddOrderDTO _order)
        //{


        //    Order baicorderinfo = new Order()
        //    {
        //        cust_id = _order.cust_id,
        //        orderdate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
        //        status = "create"

        //    };

        //   // _unit.OrderRepository.add(baicorderinfo);
        //   // _unit.savechanges();



        //    decimal totalprice = 0;
        //    foreach (var item in _order.books)
        //    {
        //        Book b = _unit.BooksRepository.selectbyid(item.book_id);
        //        totalprice =totalprice+ (b.price * item.quentity);
        //        OrderDetails _details = new OrderDetails()
        //        {
        //            order_id = baicorderinfo.id,
        //            book_id = item.book_id,
        //            quentity = item.quentity,
        //            unitprice = b.price,
        //        };
        //        if (b.stock > _details.quentity)
        //        {
        //            _unit.OrderDetailsRepository.add(_details);

        //            b.stock -= item.quentity;
        //            _unit.BooksRepository.update(b);
        //        }
        //        else
        //        {
        //            _unit.OrderRepository.delete(baicorderinfo.id);
        //            return BadRequest("invalid quantity");
        //        }

        //    }

        //    baicorderinfo.totalprice = totalprice;
        //    _unit.OrderRepository.update(baicorderinfo);

        //    _unit.savechanges();


        //    return Ok();
        //}
    }
}
