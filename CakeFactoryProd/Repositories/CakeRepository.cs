using CakeFactoryProd.Models;
using CakeFactoryProd.Data;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CakeFactoryProd.Repositories
{
    public class CakeRepository
    {
        private readonly CakeFactoryContext _context;

        public CakeRepository(CakeFactoryContext context)
        {
            _context = context;
        }

        public List<Cake> GetAllCakes()
        {
            return _context.Cakes.ToList();
        }

        public CakeOrderVM GetCakeById(int id)
        {
            Cake cake = _context.Cakes.Find(id);

            Order order = new Order();
            OrderHasCake orderHasCake = new OrderHasCake();

            List<Shape> shapes = _context.Shapes.ToList();
            List<Size> sizes = _context.Sizes.ToList();
            List<Filling> fillings = _context.Fillings.ToList();
            List<Topping> toppings = _context.Toppings.ToList();

            return new CakeOrderVM
            {
                Cake = new Cake
                {
                    Id = cake.Id,
                    Name = cake.Name,
                    Description = cake.Description,
                    ImagePath = cake.ImagePath,
                    FillingId = cake.FillingId,
                    SizeId = cake.SizeId,
                    ShapeId = cake.ShapeId,
                    Price = cake.Price,
                },

                Shapes = new SelectList(shapes, "Id", "Value"),
                Sizes = new SelectList(sizes, "Id", "Value"),
                Fillings = new SelectList(fillings, "Id", "Flavor"),
                Toppings = new SelectList(toppings, "Id", "Flavor"),

                Quantity = orderHasCake.Quantity,
            };
        }

        public void AddCake(Cake cake)
        {
            _context.Cakes.Add(cake);
            //_context.SaveChanges();
        }

        public void UpdateCake(Cake cake)
        {
            _context.Cakes.Update(cake);
            //_context.SaveChanges();
        }

        public void DeleteCake(int id)
        {
            Cake cake = _context.Cakes.Find(id);
            _context.Cakes.Remove(cake);
            //_context.SaveChanges();
        }   
    }
}
