using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.DB;
using WebApplication2.Models;
namespace WebApplication2.Pages;

public class ordermodel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public List<Product> Products { get; set; }

    public ordermodel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        // Charger les produits depuis la base de données
        Products = _context.Products.ToList();
    }

    public IActionResult OnPostFinishOrder([FromBody] Product selectedProduct)
    {
        // Traitez les données du produit sélectionné, par exemple, ajoutez-le à la base de données
        // selectedProduct contiendra les données du produit sélectionné

        // Répondez avec des données supplémentaires si nécessaire (par exemple, un message de confirmation)
        return new JsonResult(new { Message = "Produit ajouté au panier avec succès" });
    }

}
