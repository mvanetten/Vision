using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vision.Domain.Entities
{
    public class DocumentLine
    {
        public int documentlineID { get; set; }
        public string TenantID { get; set; }
        public int taxID { get; set; }
        public int documentID { get; set; }

        public string description { get; set; }

        /// <summary>
        /// (INT 0) Quantity
        /// </summary>
        /// 
        public int quantity { get; set; }

        /// <summary>
        /// (PRICE 0.00) Unit is the price per item
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// (PERCENTAGE 100.00) Discount on the total price
        /// </summary>
        public decimal discountpercentage { get; set; }


        /// <summary>
        /// (PRICE 0.00) Total price of a Unit
        /// </summary>
        public decimal subtotal
        {
            get { return quantity * price; }
        }

        public decimal discountprice
        {
            get
            {
                return (subtotal * discountpercentage / 100);
            }
        }

        public decimal total
        {
            get
            {
                return subtotal - discountprice;
            }
        }

        public decimal totaltax
        {
            get
            {
                try
                {
                    return decimal.Round(((subtotal - discountprice) * tax.taxrate / 100), 2, MidpointRounding.AwayFromZero);
                }
                catch (NullReferenceException e)
                {
                    //Mve 20-01-2016 is this catch needed?
                }
                return 0.00M;
            }
        }

        public virtual Tax tax { get; set; }
        public virtual Document document { get; set; }


    }
}
