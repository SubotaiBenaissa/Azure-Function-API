using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAPI.Models
{
    internal class CarroDeCompraItem
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public DateTime Created { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool Collected { get; set; }
        public string Category { get; set; }
    }

    internal class CreateShoppingCartItem
    {
        public string ItemName { get; set; }
        public string Category { get; set; }
    }

    internal class UpdateShoppingCartItem
    {
        public string ItemName { get; set; }
        public bool Collected { get; set; }
    }
}
