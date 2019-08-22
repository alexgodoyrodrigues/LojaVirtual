using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        /*
         * ActionResult 
         * IActionResult
         * 
        */
        public ActionResult Visualizar()
        {
            return View();

            //return new ContentResult() { Content = "<h3>Produto -> Visualizar</h3>", ContentType = "text/html"};
        }
    }
}
