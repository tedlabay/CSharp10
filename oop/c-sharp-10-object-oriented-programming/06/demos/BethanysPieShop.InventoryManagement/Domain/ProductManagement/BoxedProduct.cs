﻿using BethanysPieShop.InventoryManagement.Domain.General;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public sealed class BoxedProduct : Product
    {
        private int amountPerBox;

        public int AmountPerBox
        {
            get { return amountPerBox; }
            set
            {
                amountPerBox = value;
            }
        }

        public BoxedProduct(int id, string name, string? description, Price price, int maxAmountInStock, int amountPerBox) : base(id, name, description, price, UnitType.PerBox, maxAmountInStock)
        {
            AmountPerBox = amountPerBox;
        }

        public override void UseProduct(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = smallestMultiple * AmountPerBox;
                    break;
                }
            }

            base.UseProduct(batchSize);

        }

        public override void IncreaseStock()
        {
            AmountInStock += AmountPerBox;
        }

        //these come boxed, so what we're getting in is the amount of boxes
        public override void IncreaseStock(int amount)
        {

            //it is possible to call the base here too, but we are assuming that this is handled differently

            int newStock = AmountInStock + amount * AmountPerBox;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount * AmountPerBox;
            }
            else
            {
                AmountInStock = maxItemsInStock;//we only store the possible items, overstock isn't stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordere that couldn't be stored.");
            }

            if (AmountInStock > StockTreshold)
            {
                IsBelowStockTreshold = false;
            }
        }

        //public string DisplayBoxedProductDetails()
        //{
        //    //Console.WriteLine(name);
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("Boxed Product\n");

        //    sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

        //    if (IsBelowStockTreshold)
        //    {
        //        sb.Append("\n!!STOCK LOW!!");
        //    }

        //    return sb.ToString();
        //}

        //public void UseBoxedProduct(int items)
        //{

        //    //DecreaseStock(0, "sample");

        //    int smallestMultiple = 0;
        //    int batchSize;

        //    while (true)
        //    {
        //        smallestMultiple++;
        //        if (smallestMultiple * AmountPerBox > items)
        //        {
        //            batchSize = smallestMultiple * AmountPerBox;
        //            break;
        //        }
        //    }

        //    base.UseProduct(batchSize);//use base method explicitly adding the base keyword
        //}
    }
}
