using GenericDataAccess.Context.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GenericDataAccess.Context
{
    public class Customer : IPerson, IDbSetBase
    {
        public Customer()
        {

        }        

        [Key]
        public int ID { get; set; }

        public string FName { get; set; }

        public string MI { get; set; }

        public string LName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string StreetAddress { get; set; }

        public string Unit { get; set; }

        public string City { get; set; }

        public string StateAbbr { get; set; }

        public string ZipCode { get; set; }

        public string PrimaryPhone { get; set; }

        public string SecondaryPhone { get; set; }

        public string Email { get; set; }

        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.Now;

        public bool Deleted { get; set; }

        [ForeignKey("CustomerID")]
        public virtual List<Order> Orders { get; set; }
    }

    public class Order :IDbSetBase
    {
        public Order()
        {

        }
        [NotMapped]
        private readonly decimal _salesTax = .0725M;
        
        public int ID { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("OrderID")]
        public virtual List<LineItem> LineItems { get;}

        [NotMapped]
        public decimal Subtotal { get => Math.Round(LineItems.Sum(s => s.LineTotal), 2); }

        [NotMapped]
        public decimal Tax { get => Math.Round(Subtotal * _salesTax); }

        [NotMapped]
        public decimal Total { get => Math.Round(Subtotal + Tax); }

        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.Now;

        public bool Deleted { get; set; }
    }
    
    public class LineItem
    {
        public LineItem()
        { 
        
        }

        [Key]
        public int OrderID { get; set; }

        [Key]
        public int LineNo { get; set; }

        public int ProductID { get; set; }

        public int Qty { get; set; }

        public decimal LineTotal { get => Item.CostPerUnit * Qty; }

        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.Now;

        public bool Deleted { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Item { get; }

    }

    public class Product :IDbSetBase
    {
        public Product() 
        {
        
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal CostPerUnit { get; set; }

        public int OnHand { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.Now;

        public bool Deleted { get; set; }
    }

}
