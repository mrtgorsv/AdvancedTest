using System.ComponentModel.DataAnnotations;

namespace AdvancedTest.Data.Enum
{

    /// <summary>
    /// Перечисление типов заданий
    /// </summary>
    public enum TheoryType
    {
        [Display(Name = "Тема")]
        Theme,
        [Display(Name = "Лабораторная работа")]
        Laboratory,
        [Display(Name = "Раздел")]
        Section,
        [Display(Name = "")]
        None
    }
}