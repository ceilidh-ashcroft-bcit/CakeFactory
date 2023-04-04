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

        public List<CakeVM> GetCakesAll()
        {
            var cakes = from c in _context.Cakes
                     join sz in _context.Sizes on c.SizeId equals sz.Id
                     join sp in _context.Shapes on c.ShapeId equals sp.Id
                     select new CakeVM
                     {
                         CakeId = c.Id,
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
            /*            List<Cake> cakes = _context.Cakes.ToList();
                        var c = cakes.Where(x => x.IsActive == true && x.IsPredefined == true);
                        return c.ToList();*/
            return _context.Cakes.Where(x => x.IsActive == true && x.IsPredefined == true).ToList();
        }

        // get cake when you order
        public CakeOrderVM GetCakeById(int id)
        {
            Cake cake = _context.Cakes.Find(id);

            Order order = new Order();
            OrderHasCake orderHasCake = new OrderHasCake();
            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);

            List<Shape> shapes = _context.Shapes.ToList();
            List<Size> sizes = _context.Sizes.ToList(); 
            List<Filling> fillings = _context.Fillings.ToList();
            List<ToppingVM> toppings = toppingsRepo.GetToppingByCakeId(id);

            return new CakeOrderVM
            {
                CakeVM = new CakeVM
                {
                    CakeId = cake.Id,
                    Name = cake.Name,
                    Description = cake.Description,
                    ImageName = cake.ImageName,
                    ImageCake = cake.ImageCake,
                    FillingId = cake.FillingId,
                    SizeId = cake.SizeId,
                    ShapeId = cake.ShapeId,
                    Price = cake.Price,
                    SelectedToppings = toppings
                },
                Shapes = new SelectList(shapes, "Id", "Value"),
                Sizes = new SelectList(sizes, "Id", "Value"),
                Fillings = new SelectList(fillings, "Id", "Flavor"),

                PickupDate = order.PickupDate,
                Quantity = orderHasCake.Quantity,
            };
        }
        
        // get cake when admin edit cake's data
        public CakeVM GetImage(int id)
        {
            Cake cake = _context.Cakes.Find(id);

            CakeVM cakeVM = new CakeVM
            {
                ImageName = cake.ImageName,
                ImageCake = cake.ImageCake,
            };

            return cakeVM;
        }
         // get cake when admin edit cake's data
        public CakeVM GetCakeDetail(int id)
        {
            Cake cake = _context.Cakes.Find(id);

            List<Shape> shapes = _context.Shapes.ToList();
            List<Size> sizes = _context.Sizes.ToList();
            List<Filling> fillings = _context.Fillings.ToList();
            List<Topping> toppings = _context.Toppings.ToList();

            Filling filling = _context.Fillings.Find(cake.FillingId);
            Size size = _context.Sizes.Find(cake.SizeId);
            Shape shape = _context.Shapes.Find(cake.ShapeId);

            CakeVM cakeVM = new CakeVM
            {
                CakeId = cake.Id,
                Name = cake.Name,
                IsActive = cake.IsActive,
                Description = cake.Description,
                ImageName = cake.ImageName,
                ImageCake = cake.ImageCake,
                FillingId = cake.FillingId,
                Filling = filling.Flavor,
                SizeId = cake.SizeId,
                Size = size.Value,
                ShapeId = cake.ShapeId,
                Shape = shape.Value,
                Price = cake.Price,
                Shapes = new SelectList(shapes, "Id", "Value"),
                Sizes = new SelectList(sizes, "Id", "Value"),
                Fillings = new SelectList(fillings, "Id", "Flavor"),
                Toppings = toppings
            };

            return cakeVM;
        }

        public CakeOrderVM GetCustomCake()
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
                    ImageName = cake.ImageName,
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

        public void UpdateCake(CakeVM cakeVM)
        {


            ToppingsRepo toppings = new ToppingsRepo(_context);
            CakeVM image = GetImage(cakeVM.CakeId);


            Cake cake = _context.Cakes.FirstOrDefault(x => x.Id == cakeVM.CakeId);

            cake.Name = cakeVM.Name;
            cake.ImageName = cakeVM.CakeImage == null ? image.ImageName : cakeVM.CakeImage.FileName;
            cake.ImageCake = cakeVM.CakeImage == null ? image.ImageCake : cakeVM.ImageData;
            cake.IsActive = cakeVM.IsActive;
            cake.Description = cakeVM.Description;
            cake.IsPredefined = true;
            cake.Price = cakeVM.Price;
            cake.ShapeId = cakeVM.ShapeId;
            cake.SizeId = cakeVM.SizeId;
            cake.FillingId = cakeVM.FillingId;
            var toppingsAccepted = cakeVM.Accepted;
            toppings.EditCakeHasToppings(toppingsAccepted, cakeVM.CakeId);

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
