﻿using BethanysPieShop.InventoryManagement.Domain.General;
using System.Text;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public class FreshProduct: Product
    {
        public DateTime ExpiryDateTime { get; set; }
        public string? StorageInstructions { get; set; }

        public FreshProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : base(id, name, description, price, unitType, maxAmountInStock)
        {

        }

        protected override double GetProductStockValue()
        {
            if (DateTime.Now > ExpiryDateTime)
            {
                return Price.ItemPrice * AmountInStock * 0.8;//since the product stock is expired, the value is lower
            }
            return Price.ItemPrice * AmountInStock;
        }

        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");


            if (IsBelowStockTreshold)
            {
                sb.AppendLine("\n!!STOCK LOW!!");
            }

            sb.AppendLine("Storage instructions: " + StorageInstructions);//since this line needs to go here, we can't call the base here
            sb.AppendLine("Expiry date: " + ExpiryDateTime.ToShortDateString());

            return sb.ToString();
        }

        public override void IncreaseStock()
        {
            AmountInStock++;
        }
    }
}
