using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GenericDataAccess.Context.Base
{
    
    public enum Gender
    {
        [Display(Name = "Male")]
        Male = 0,
        [Display(Name = "Female")]
        Female = 1,
        [Display(Name = "Non-Binary")]
        NonBinary = 2,
        [Display(Name = "Prefer Not to Say")]
        None = 3
    }

    public interface IDbSetBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.DateTime)]
        [ConcurrencyCheck]
        public DateTimeOffset ModifiedOn { get; set; }

        public bool Deleted { get; set; }
    }

    public interface IPerson
    {
        [Required]
        [Display(Name ="First Name", AutoGenerateField = true, GroupName = "FullName")]
        [MaxLength(25, ErrorMessage ="First Name can not be longer than 25 characters.")]
        [RegularExpression(@"^([\w]+)", ErrorMessage = "Only Letters are allowed")]
        public string FName { get; set; }

        [Display(Name = "MI", AutoGenerateField = true, GroupName = "FullName")]
        [MaxLength(1, ErrorMessage = "Middle Initial can only be 1 Letter")]
        [RegularExpression(@"^([\w]+)", ErrorMessage = "Only Letters are allowed")]
        public string MI { get; set; }

        [Display(Name = "Last Name", AutoGenerateField = true, GroupName = "FullName")]
        [MaxLength(25, ErrorMessage = "Last Name can not be longer than 25 characters.")]
        [RegularExpression(@"^([\w]+)", ErrorMessage = "Only Letters are allowed")]
        public string LName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Street Address", AutoGenerateField = true, GroupName = "Address")]
        [MaxLength(50, ErrorMessage = "Street Address can not be longer than 50 characters.")]
        public string StreetAddress { get; set; }

        [Display(Name = "Unit/Suite", AutoGenerateField = true, GroupName = "Address")]
        [MaxLength(25, ErrorMessage = "Unit/Suite can not be longer than 25 characters.")]
        public string Unit { get; set; }

        [Required]
        [Display(Name = "City", AutoGenerateField = true, GroupName = "Address")]
        [MaxLength(25, ErrorMessage = "City can not be longer than 25 characters.")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State", AutoGenerateField = true, GroupName = "Address")]
        [MaxLength(2, ErrorMessage = "State must be 2 characters.")]
        [MinLength(2, ErrorMessage = "State must be 2 characters.")]
        public string StateAbbr { get; set; }

        [Required]
        [Display(Name = "Street Address", AutoGenerateField = true, GroupName = "Address")]
        [MaxLength(50, ErrorMessage = "Street Address can not be longer than 50 characters.")]
        [RegularExpression(@"(\d{5})(-\d{4})?", ErrorMessage = "Zipcode must match the format of either 12345 or 12345-6789")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Primary Phone", AutoGenerateField = true, GroupName = "Contact")]
        [MaxLength(25, ErrorMessage = "Last Name can not be longer than 25 characters.")]
        [RegularExpression(@"^([\w]+)", ErrorMessage = "Only Letters are allowed")]
        public string PrimaryPhone { get; set; }

        [Display(Name = "Secondary Phone", AutoGenerateField = true, GroupName = "Contact")]
        [MaxLength(25, ErrorMessage = "Last Name can not be longer than 25 characters.")]
        [RegularExpression(@"^([\w]+)", ErrorMessage = "Only Letters are allowed")]
        public string SecondaryPhone { get; set; }

        [Display(Name = "Email Address", AutoGenerateField = true, GroupName = "Contact")]
        [MaxLength(25, ErrorMessage = "Last Name can not be longer than 25 characters.")]
        [RegularExpression(@"^([\w]+)", ErrorMessage = "Only Letters are allowed")]
        public string Email { get; set; }


    }
}
