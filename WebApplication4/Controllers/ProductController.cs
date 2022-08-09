using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers;
public class ProductController : Controller
{
    private readonly IProductRepository _repo;

    public ProductController(IProductRepository productRepository)
    {
        _repo = productRepository;
    }

    public IActionResult Index()
    {
        var products = _repo.GetAllProducts();

        return View(products);
    }

    //[Authorize]
    public IActionResult ViewProduct(int id)
    {
        var product = _repo.GetProduct(id);

        return View(product);
    }

    //[Authorize]
    public IActionResult UpdateProduct(int id)
    {
        Product prod = _repo.GetProduct(id);

        if (prod == null)
        {
            return View("ProductNotFound");
        }

        return View(prod);
    }

    //[Authorize]
    public IActionResult UpdateProductToDatabase(Product product)
    {
        _repo.UpdateProduct(product);

        return RedirectToAction("ViewProduct", new { id = product.ProductID });
    }

    //[Authorize]
    public IActionResult InsertProduct()
    {
        var prod = _repo.AssignCategory();

        return View(prod);
    }

    //[Authorize]
    public IActionResult InsertProductToDatabase(Product productToInsert)
    {
        _repo.InsertProduct(productToInsert);

        return RedirectToAction("Index");
    }

    //[Authorize]
    public IActionResult DeleteProduct(Product product)
    {
        _repo.DeleteProduct(product);

        return RedirectToAction("Index");
    }
}
