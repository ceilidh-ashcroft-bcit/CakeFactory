
﻿using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CakeFactoryProd.Controllers
{
    public class CakeController : Controller
    {
        private readonly CakeFactoryContext _context;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CakeController(ILogger<CakeController> logger, CakeFactoryContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context= context;
            _webHostEnvironment= webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id) 
        {
            CakeRepository cakeRepo = new CakeRepository(_context);

            //Custom cake view 
            if (id == 0)
            {
                CakeOrderVM cakeOrderVM = cakeRepo.CreateCustomCake();

                return View(cakeOrderVM);
            }

            //Pre ordered cake view 
            else
            {
                CakeOrderVM cakeOrderVM = cakeRepo.GetCakeById(id);

                return View(cakeOrderVM);
            }
            
        }
        public IActionResult AdminDetail(int id)
        {
            CakeRepository cakeRepo = new CakeRepository(_context);
            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
            CakeVM cakeVM = cakeRepo.GetCakeDetail(id);
            List<ToppingVM> toppings = toppingsRepo.GetToppingByCakeId(id);
            cakeVM.SelectedToppings = toppings;
            return View(cakeVM);

        }
        public IActionResult Create()
        {

            List<Shape> shapes = _context.Shapes.ToList();
            List<Size> sizes = _context.Sizes.ToList();
            List<Filling> fillings = _context.Fillings.ToList();
            List<Topping> toppings = _context.Toppings.ToList();

            CakeVM cakeVM = new CakeVM
            {
                Shapes = new SelectList(shapes, "Id", "Value"),
                Sizes = new SelectList(sizes, "Id", "Value"),
                Fillings = new SelectList(fillings, "Id", "Flavor"),
                Toppings = toppings
            };

            return View(cakeVM);
        }

        [HttpPost]
  /*      [Bind("Name, IsActive, Sizes, Shapes, Fillings, Toppings, Description, CakeImage, Price")]*/
        public IActionResult Create(CakeVM cakeVM)
        {
            string message = "";
            CakeRepository cakeRepo = new CakeRepository(_context);
            ToppingsRepo toppings = new ToppingsRepo(_context);
            //string fileName = UploadFile(cakeVM);

            if (cakeVM.CakeImage == null || cakeVM.CakeImage.Length == 0)
            {
                ModelState.AddModelError("imageUpload", "Please select an " +
                                                        " image to upload.");
            }
            else if (!IsImageFileTypeValid(cakeVM.CakeImage.ContentType))
            {
                ModelState.AddModelError("imageUpload", "Please upload a PNG, " +
                                                        "JPG, or JPEG file.");
            }
            else
            {
                try
                {
                    byte[] imageData = null;
                    using (var binaryReader = new
                              BinaryReader(cakeVM.CakeImage.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)cakeVM.CakeImage.Length);
                    }
                    Cake cake = new Cake
                    {
                        Name = cakeVM.Name,
                        IsActive = true,
                        Description = cakeVM.Description,
                        ImageName = cakeVM.CakeImage.FileName,
                        ImageCake = imageData,
                        IsPredefined = true,
                        Price = cakeVM.Price,
                        ShapeId = cakeVM.ShapeId,
                        SizeId = cakeVM.SizeId,
                        FillingId = cakeVM.FillingId

                    };
                    cakeRepo.AddCake(cake);
                    var toppingsAccepted = cakeVM.Accepted;
                    toppings.AddCakeHasToppings(toppingsAccepted, cake.Id);

                    message = "Creates a Cake Successfully";
                    return RedirectToAction("Cakes", "Admin", new { message = message });

                }
                catch (Exception)
                {
                    ModelState.AddModelError("imageUpload"
                                            , "An error occured uploading your image. "
                                            + "Please try again.");
                }
            }

            return View(cakeVM);
        }


        private bool IsImageFileTypeValid(string contentType)
        {
            return contentType == "image/png" ||
                   contentType == "image/jpeg" ||
                   contentType == "image/jpg";
        }

        public IActionResult Edit(int id)
        {

            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
            CakeRepository cakeRepo = new CakeRepository(_context);
            CakeVM cake = cakeRepo.GetCakeDetail(id);

            List<ToppingVM> selectedToppings = toppingsRepo.GetToppingByCakeId(id);
            cake.SelectedToppings = selectedToppings;

            List<Size> sizes = _context.Sizes.ToList();

            cake.Sizes = new SelectList(sizes, "Id", "Value");

            return View(cake);
        }

        [HttpPost]
        /*      [Bind("Name, IsActive, Sizes, Shapes, Fillings, Toppings, Description, CakeImage, Price")]*/
        public IActionResult Edit(CakeVM cakeVM)
        {

            string message = "";
            CakeRepository cakeRepo = new CakeRepository(_context);
            try
            {
                if (cakeVM.CakeImage == null || cakeVM.CakeImage.Length == 0)
                {
                    cakeVM.ImageData = null;
                    cakeRepo.UpdateCake(cakeVM);
                    message = "Update a Cake Successfully";
                    return RedirectToAction("Cakes", "Admin", new { message = message });
                }
                else if (!IsImageFileTypeValid(cakeVM.CakeImage.ContentType))
                {
                    ModelState.AddModelError("imageUpload", "Please upload a PNG, " +
                                                            "JPG, or JPEG file.");
                }
                else
                {
                    using (var binaryReader = new BinaryReader(cakeVM.CakeImage.OpenReadStream()))
                    {
                        cakeVM.ImageData = binaryReader.ReadBytes((int)cakeVM.CakeImage.Length);
                    }
                    cakeRepo.UpdateCake(cakeVM);
                    message = "Update a Cake Successfully";
                    return RedirectToAction("Cakes", "Admin", new { message = message });
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("imageUpload"
                                        , "An error occured uploading your image. "
                                        + "Please try again.");
            }
           
            return View(cakeVM);
        }

    }
}
