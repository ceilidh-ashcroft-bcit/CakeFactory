
﻿using CakeFactoryProd.Models;
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


        public List<CakeVM> GetCakesAll()
        {

            var cakes = from c in _context.Cakes
                     join sz in _context.Sizes on c.SizeId equals sz.Id
                     join sp in _context.Shapes on c.ShapeId equals sp.Id
                     select new CakeVM
                     {
                         Name = c.Name,
                         Price = c.Price,
                         IsActive = c.IsActive,
                         Size = sz.Value,
                         Shape = sp.Value
                     };
            /*IQueryable<CakeVM> cakes = _context.Cakes.Select(c => new CakeVM { Name = c.Name, Price = c.Price, IsActive = c.IsActive, Shape = c.ShapeId, Size = c.SizeId });
*/
            return cakes.ToList();
        }

        public List<Cake> GetAllActivePredefinedCakes()

        {
            return _context.Cakes.Where(x => x.IsActive == true && x.IsPredefined == true).ToList();
        }

        public CakeOrderVM GetCakeById(int id)
        {
            Cake cake = _context.Cakes.Find(id);

            Order order = new Order();
            OrderHasCake orderHasCake = new OrderHasCake();


            List<Shape> shapes = _context.Shapes.ToList();
            List<Size> sizes = _context.Sizes.ToList();

            return new CakeOrderVM
            {
                CakeVM = new CakeVM
                {
                    CakeId = cake.Id,
                    Name = cake.Name,
                    Description = cake.Description,
                    CakeImage = cake.ImagePath,
                    FillingId = cake.FillingId,
                    SizeId = cake.SizeId,
                    ShapeId = cake.ShapeId,
                    Price = cake.Price,
                },
                Shapes = new SelectList(shapes, "Id", "Value"),
                Sizes = new SelectList(sizes, "Id", "Value"),

                PickupDate = order.PickupDate,
                Quantity = orderHasCake.Quantity,
            };
        }

        public CakeOrderVM CreateCustomCake()
        {
            Cake cake = new Cake();
            Order order = new Order();
            OrderHasCake orderHasCake = new OrderHasCake();

            List<Shape> shapes = _context.Shapes.ToList();
            List<Size> sizes = _context.Sizes.ToList();
            List<Filling> fillings = _context.Fillings.ToList();
            List<Topping> toppings = _context.Toppings.ToList();

            return new CakeOrderVM
            {
                CakeVM = new CakeVM
                {
                    CakeId = cake.Id,
                    Name = cake.Name,
                    Description = cake.Description,
                    CakeImage = cake.ImagePath,
                    FillingId = cake.FillingId,
                    SizeId = cake.SizeId,
                    ShapeId = cake.ShapeId,
                    Price = cake.Price,
                },

                PickupDate = order.PickupDate,

                Shapes = new SelectList(shapes, "Id", "Value"),
                Sizes = new SelectList(sizes, "Id", "Value"),
                Fillings = new SelectList(fillings, "Id", "Flavor"),

                Toppings = toppings,
                Quantity = orderHasCake.Quantity,
            };
        }
        
        public void AddCake(Cake cake)
        {
            _context.Cakes.Add(cake);
            _context.SaveChanges();
        }

        public void UpdateCake(Cake cake)
        {
            _context.Cakes.Update(cake);
            _context.SaveChanges();
        }

        public void DeleteCake(int id)
        {
            Cake cake = _context.Cakes.Find(id);
            _context.Cakes.Remove(cake);
            _context.SaveChanges();
        }   

    }
}
