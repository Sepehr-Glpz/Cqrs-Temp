using System.ComponentModel.DataAnnotations;

namespace SGSX.CqrsTemp.Domain.Enums;
public enum CatBreed : byte
{ 
    [Display(Name = "Unknown Breed")]
    Undefined = 0,
    
    [Display(Name = "Main Coon")]
    MainCoon = 1,

    [Display(Name = "Bengal")]
    Bengal = 2,

    [Display(Name = "Persian")]
    Persian = 3,

    [Display(Name = "British Long Hair")]
    BritishLongHair = 4,
}

