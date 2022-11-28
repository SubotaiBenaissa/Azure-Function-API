using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using APIcurso.Models;
using System.Collections.Generic;
using System.Linq;

namespace APIcurso
{
    public static class FuncionAPI
    {
        static List<CarroDeCompraItem> CarroDeCompraItems = new();
        [FunctionName("GetCarroDeCompraItems")]
        public static async Task<IActionResult> GetCarroDeCompraItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "carrodecompraitem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all items from Shopping Cart.");
            return new OkObjectResult(CarroDeCompraItems);
           
        }

        [FunctionName("GetCarroDeCompraItemByID")]
        public static async Task<IActionResult> GetCarroDeCompraItemByID(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "carrodecompraitem/{id}")] HttpRequest req,
        string id, ILogger log)
        {
            log.LogInformation($"Getting a item from shopping cart with ID: {id}");
            var shoppingCartItem = CarroDeCompraItems.FirstOrDefault(q => q.ID == id);

            if (shoppingCartItem == null) {
                return new NotFoundResult();
            }

            return new OkObjectResult(shoppingCartItem);
        }

        [FunctionName("CreateCarroDeCompraItem")]
        public static async Task<IActionResult> CreateCarroDeCompraItems(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "carrodecompraitem")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("Creating a cart item");
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateShoppingCartItem>(requestData);
            var item = new CarroDeCompraItem
            {
                ItemName = data.ItemName,
                Category = data.Category,
            };

            CarroDeCompraItems.Add(item);

            return new OkObjectResult(item);

        }

        [FunctionName("PutCarroDeCompraItem")]
        public static async Task<IActionResult> PutCarroDeCompraItems(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "carrodecompraitem/{id}")] HttpRequest req,
        ILogger log, string id)
        {
            log.LogInformation($"Updating a cart item with id {id}");
            var itemCarroDeCompra = CarroDeCompraItems.FirstOrDefault(q => q.ID == id);

            if (itemCarroDeCompra == null)
            {
                return new NotFoundResult();
            }

            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateShoppingCartItem>(requestData);
            itemCarroDeCompra.Collected = data.Collected;

            return new OkObjectResult(itemCarroDeCompra);
        }

        [FunctionName("DeleteCarroDeCompraItem")]
        public static async Task<IActionResult> DeleteCarroDeCompraItems(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "carrodecompraitem/{id}")] HttpRequest req,
        ILogger log, string id)
        {
            log.LogInformation("Deleting cart item");
            var shoppingCartItem = CarroDeCompraItems.FirstOrDefault(q => q.ID == id);

            if (shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            CarroDeCompraItems.Remove(shoppingCartItem);
            return new OkResult();
        }
    }
}
