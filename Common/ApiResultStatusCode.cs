using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "Операция успешно выполнена")]
        Success = 0,

        [Display(Name = "Произошла серверная ошибка")]
        ServerError = 1,

        [Display(Name = "Некорректный запрос")]
        BadRequest = 2,

        [Display(Name = "Ничего не найдено")]
        NotFound = 3,

        [Display(Name = "Список пуст")]
        ListEmpty = 4,

        [Display(Name = "Логическая ошибка")]
        LogicError = 5,

        [Display(Name = "Вы не авторизированы")]
        UnAuthorized = 6
    }
}
