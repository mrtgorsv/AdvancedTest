using System.ComponentModel.DataAnnotations;

namespace AdvancedTest.Data.Enum
{
    /// <summary>
    /// Оценка за задание
    /// </summary>
    public enum Grade
    {
        [Display(Name = "Отлично")]
        A = 5,
        [Display(Name = "Хорошо")]
        B = 4,
        [Display(Name = "Удовлетворительно")]
        C = 3,
        [Display(Name = "Плохо")]
        D = 2,
        [Display(Name = "Неудовлетворительно")]
        E = 1
}
}